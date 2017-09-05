using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using ProductsService.Contract;
using ProductsService.Contract.Models;
using refactor_me.Models;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private readonly IProductService _productService;
        private readonly IProductOptionService _productOptionService;

        public ProductsController(IProductService productService, IProductOptionService productOptionService)
        {
            if (productService == null) throw new ArgumentNullException(nameof(productService));
            if (productOptionService == null) throw new ArgumentNullException(nameof(productOptionService));
            _productService = productService;
            _productOptionService = productOptionService;
        }

        [Route]
        [HttpGet]
        public Products GetAll()
        {
            var products = new Products {Items = _productService.GetAll().ToList()};

            return products;
        }

        [Route]
        [HttpGet]
        public Products SearchByName(string name)
        {
            var products = new Products { Items = _productService.GetByName(name).ToList() };

            return products;
        }

        [Route("{id}")]
        [HttpGet]
        public Product GetProduct(Guid id)
        {
            var product = _productService.GetById(id);
            if (product == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return product;
        }

        [Route]
        [HttpPost]
        public void Create(Product product)
        {
            _productService.Save(product);
        }

        [Route("{id}")]
        [HttpPut]
        public void Update(Guid id, Product product)
        {
            _productService.Update(id, product);
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {
            _productService.Delete(id);
        }

        [Route("{productId}/options")]
        [HttpGet]
        public ProductOptions GetOptions(Guid productId)
        {
            var productOptions = new ProductOptions {Items = _productOptionService.GetByProductId(productId).ToList()};

            return productOptions;
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ProductOption GetOption(Guid productId, Guid id)
        {
            var option = _productOptionService.Get(id);
            if (option == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return option;
        }

        [Route("{productId}/options")]
        [HttpPost]
        public void CreateOption(Guid productId, ProductOption option)
        {
            _productOptionService.Save(productId, option);
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateOption(Guid id, ProductOption option)
        {
            _productOptionService.Update(id, option);
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid id)
        {
            _productOptionService.Delete(id);
        }
    }
}
