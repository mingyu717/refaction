using System;
using System.Collections.Generic;
using ProductsService.Contract.Models;

namespace ProductsService.Contract
{
    public interface IProductOptionsRepository
    {
        Models.ProductOption Get(Guid id);
        IEnumerable<ProductOption> GetAll();
        IEnumerable<ProductOption> GetByProductId(Guid productId);
        void Save(Models.ProductOption productOption);
        void Delete(Guid id);
        void Update(ProductOption productOption);
        void Create(ProductOption productOption);
    }
}
