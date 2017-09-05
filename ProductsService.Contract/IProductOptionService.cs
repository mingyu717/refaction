using System;
using System.Collections.Generic;
using ProductsService.Contract.Models;

namespace ProductsService.Contract
{
    public interface IProductOptionService
    {
        ProductOption Get(Guid id);
        IEnumerable<ProductOption> GetByProductId(Guid productId);
        void Save(Guid productId, ProductOption productOption);
        void Create(Guid productId, ProductOption option);
        void Update(Guid id, ProductOption option);
        void Delete(Guid id);
    }
}
