using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using ProductsService.Contract;
using ProductsService.Contract.Models;

namespace ProductsService.Implementation
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly SqlConnection _sqlConnection;

        public ProductsRepository(SqlConnection sqlConnection)
        {
            if (sqlConnection == null) throw new ArgumentNullException(nameof(sqlConnection));
            _sqlConnection = sqlConnection;
        }

        public IEnumerable<Product> GetAll()
        {
            using (var cmd = new SqlCommand("select * from product", _sqlConnection))
            {
                _sqlConnection.Open();

                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    yield return ConstructProduct(false, rdr["Id"]?.ToString(), rdr["Name"]?.ToString(),
                    rdr["Description"]?.ToString(), rdr["Price"]?.ToString(), rdr["DeliveryPrice"]?.ToString());
                }
            }
        }

        public Product Get(Guid id)
        {
            using (var cmd = new SqlCommand("select * from product where id = @Id", _sqlConnection))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = id;

                cmd.Parameters.Add(param);
                var rdr = cmd.ExecuteReader();
                if (!rdr.Read())
                    return null;

                return ConstructProduct(false, rdr["Id"]?.ToString(), rdr["Name"]?.ToString(),
                    rdr["Description"]?.ToString(), rdr["Price"]?.ToString(), rdr["DeliveryPrice"]?.ToString());

            }
        }

        public IEnumerable<Product> Get(string name)
        {
            var products = new List<Product>();

            using (var cmd = new SqlCommand("select * from product where name = @name", _sqlConnection))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = name;

                cmd.Parameters.Add(param);
                _sqlConnection.Open();
                var rdr = cmd.ExecuteReader();
                if (!rdr.Read())
                    return products;
                
                while (rdr.Read())
                {
                    products.Add(ConstructProduct(false, rdr["Id"]?.ToString(), rdr["Name"]?.ToString(),
                    rdr["Description"]?.ToString(), rdr["Price"]?.ToString(), rdr["DeliveryPrice"]?.ToString()));
                }

                return products;
            }
        }

        private Product ConstructProduct(bool isNew, string id, string name, string description, string price,
            string deliveryprice)
        {
            return new Product
            {
                IsNew = false,
                Id = Guid.Parse(id),
                Name = name, 
                Description = description,
                Price = decimal.Parse(price),
                DeliveryPrice = decimal.Parse(deliveryprice)
            };
        }

        public void Save(Product product) {
            var existingRecord = Get(product.Id);
            if (existingRecord != null)
            {
                Update(product);
            }
            else {
                Create(product);
            }
        }

        public void Create(Product product) {
            using (var cmd = new SqlCommand("insert into product (id, name, description, price, deliveryprice) values ('@Id', '@Name', '@Description', '@Price', '@DeliveryPrice')", _sqlConnection)) {
                cmd.Parameters.AddWithValue("@Id", product.Id);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@DeliveryPrice", product.DeliveryPrice);

                _sqlConnection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Product product) {
            using (var cmd = new SqlCommand("update product set name = '@Name', description = '@Description', price = '@Price', deliveryprice = '@DeliveryPrice' where id = '@Id'", _sqlConnection))
            {
                cmd.Parameters.AddWithValue("@Id", product.Id);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@DeliveryPrice", product.DeliveryPrice);


                _sqlConnection.Open();
                cmd.ExecuteNonQuery();
            }

        }
        public void Delete(Guid id) {
           
            using (var cmd = new SqlCommand("delete from product where id = '@Id'", _sqlConnection)) {
                cmd.Parameters.AddWithValue("@Id", id);

                _sqlConnection.Open();
                cmd.ExecuteNonQuery();
            }
        }
        
    }
}
