using System;
using System.Collections.Generic;
using ProductsService.Contract.Models;

namespace ProductsService.Contract
{
    public interface IProductsRepository
    {
        void Save(Models.Product product);
        void Delete(Guid id);
        IEnumerable<Product> GetAll();
        IEnumerable<Product> Get(string name);
        Models.Product Get(Guid id);
        void Create(Product product);
        void Update(Product product);
    }
}
