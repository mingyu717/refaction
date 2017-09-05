using System;
using System.Collections;
using System.Collections.Generic;
using ProductsService.Contract.Models;

namespace ProductsService.Contract
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetByName(string name);
        Product GetById(Guid id);
        void Save(Product product);
        void Create(Product product);
        void Update(Guid id, Product product);
        void Delete(Guid id);
    }
}
