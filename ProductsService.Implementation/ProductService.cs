using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ProductsService.Contract;
using ProductsService.Contract.Models;

namespace ProductsService.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductService(IProductsRepository productsRepository)
        {
            if (productsRepository == null) throw new ArgumentNullException(nameof(productsRepository));
            _productsRepository = productsRepository;
        }

        public IEnumerable<Product> GetAll()
        {
            return _productsRepository.GetAll();
        }

        public IEnumerable<Product> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            return _productsRepository.Get(name);
        }

        public Product GetById(Guid id)
        {
            return _productsRepository.Get(id);
        }

        public void Save(Product product)
        {
            _productsRepository.Save(product);
        }

        public void Create(Product product)
        {
           _productsRepository.Create(product);
        }

        public void Update(Guid id, Product product)
        {
            var orig = _productsRepository.Get(id);

            if (orig != null)
            {
                product.Id = id;
                _productsRepository.Update(product);
            }
        }

        public void Delete(Guid id)
        {
            _productsRepository.Delete(id);
        }
    }
}
