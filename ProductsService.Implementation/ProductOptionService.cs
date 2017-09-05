using System;
using System.Collections.Generic;
using ProductsService.Contract;
using ProductsService.Contract.Models;

namespace ProductsService.Implementation
{
    public class ProductOptionService : IProductOptionService
    {
        private readonly IProductOptionsRepository _productOptionsRepository;

        public ProductOptionService(IProductOptionsRepository productOptionsRepository)
        {
            if (productOptionsRepository == null) throw new ArgumentNullException(nameof(productOptionsRepository));
            _productOptionsRepository = productOptionsRepository;
        }

        public IEnumerable<ProductOption> GetByProductId(Guid productId)
        {
            return _productOptionsRepository.GetByProductId(productId);
        }

        public void Save(Guid productId, ProductOption productOption)
        {
            if (productOption == null) throw new ArgumentNullException(nameof(productOption));
            productOption.ProductId = productId;
            _productOptionsRepository.Save(productOption);
        }

        public ProductOption Get(Guid id)
        {
           return _productOptionsRepository.Get(id);
        }

        public void Create(Guid productId, ProductOption option)
        {
            if (option == null) throw new ArgumentNullException(nameof(option));
            option.ProductId = productId;
            _productOptionsRepository.Create(option);
        }

        public void Update(Guid id, ProductOption option)
        {
            if (option == null) throw new ArgumentNullException(nameof(option));
            var orig = _productOptionsRepository.Get(id);

            if (orig != null)
            {
                option.Id = id;
                _productOptionsRepository.Update(option);
            }
        }

        public void Delete(Guid id)
        {
           _productOptionsRepository.Delete(id);
        }
    }
}
