using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ProductsService.Contract;
using ProductsService.Contract.Models;

namespace ProductsService.Implementation
{
    public class ProductOptionsRepository : IProductOptionsRepository
    {
        private readonly SqlConnection _sqlConnection;

        public ProductOptionsRepository(SqlConnection sqlConnection)
        {
            if (sqlConnection == null) throw new ArgumentNullException(nameof(sqlConnection));
            _sqlConnection = sqlConnection;
        }

        public ProductOption Get(Guid id)
        {
            using (var cmd = new SqlCommand("select * from productoption where id = @Id", _sqlConnection))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = id;

                cmd.Parameters.Add(param);
                var rdr = cmd.ExecuteReader();
                if (!rdr.Read())
                    return null;

                return ConstructProductOption
                    (false, rdr["Id"]?.ToString(), rdr["Name"]?.ToString(),
                        rdr["Description"]?.ToString(), rdr["ProductId"].ToString()
                    );
            }
        }

        public IEnumerable<ProductOption> GetAll()
        {
            using (var cmd = new SqlCommand("select * from productoption", _sqlConnection))
            {
                _sqlConnection.Open();

                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    yield return ConstructProductOption
                        (false, rdr["Id"]?.ToString(), rdr["Name"]?.ToString(),
                            rdr["Description"]?.ToString(), rdr["ProductId"].ToString()
                        );
                }
            }
        }

        public IEnumerable<ProductOption> GetByProductId(Guid productId)
        {
            var productOptions = new List<ProductOption>();

            using (var cmd = new SqlCommand("select * from productoption where productId = @productId", _sqlConnection))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@productId";
                param.Value = productId;

                cmd.Parameters.Add(param);
                var rdr = cmd.ExecuteReader();
                if (!rdr.Read())
                    return productOptions;

                productOptions.Add(ConstructProductOption
                    (false, rdr["Id"]?.ToString(), rdr["Name"]?.ToString(),
                        rdr["Description"]?.ToString(), rdr["ProductId"].ToString()
                    ));

                return productOptions;
            }
        }

        private ProductOption ConstructProductOption(bool isNew, string id, string name, string description, string productId)
        {
            return new ProductOption
            {
                IsNew = isNew,
                Id = Guid.Parse(id),
                Name = name,
                Description = description,
                ProductId = Guid.Parse(productId)
            };
        }


        public void Save(ProductOption productOption)
        {
            var existingRecord = Get(productOption.Id);
            if (existingRecord != null)
            {
                Update(productOption);
            }
            else
            {
                Create(productOption);
            }
        }

        public void Create(ProductOption productOption)
        {
            using (var cmd = new SqlCommand("insert into productoption(id, productid, name, description) values('@Id', '@ProductId', '@Name', '@Description')", _sqlConnection))
            {
                cmd.Parameters.AddWithValue("@Id", productOption.Id);
                cmd.Parameters.AddWithValue("@Name", productOption.Name);
                cmd.Parameters.AddWithValue("@Description", productOption.Description);
                cmd.Parameters.AddWithValue("@ProductId", productOption.ProductId);

                _sqlConnection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(ProductOption productOption)
        {
            using (var cmd = new SqlCommand("update productoption set name = '@Name', description = '@Description' where id = '@Id'", _sqlConnection))
            {
                cmd.Parameters.AddWithValue("@Id", productOption.Id);
                cmd.Parameters.AddWithValue("@Name", productOption.Name);
                cmd.Parameters.AddWithValue("@Description", productOption.Description);

                _sqlConnection.Open();
                cmd.ExecuteNonQuery();
            }

        }

        public void Delete(Guid id)
        {
            using (var cmd = new SqlCommand("delete from productoption where id = '@Id'", _sqlConnection))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                _sqlConnection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
