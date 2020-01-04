using AutoMapper;
using Ecommerce.Catalog.Application.ViewModel;
using Ecommerce.Catalog.Domain.DomainService;
using Ecommerce.Catalog.Domain.Entities;
using Ecommerce.Catalog.Domain.Repository;
using Ecommerce.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Catalog.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;

        public ProductAppService(IProductRepository productRepository, IStockService stockService, IMapper mapper)
        {
            _productRepository = productRepository;
            _stockService = stockService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetAll());
        }

        public async Task<IEnumerable<ProductViewModel>> GetByCategory(int code)
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetByCategory(code));
        }

        public async Task<ProductViewModel> GetById(Guid id)
        {
            return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            return _mapper.Map<IEnumerable<CategoryViewModel>>(await _productRepository.GetCategories());
        }

        public async Task AddProduct(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);
            _productRepository.Add(product);

            await _productRepository.UnitOfWork.Commit();
        }

        public async Task<ProductViewModel> DebitStock(Guid id, int quantity)
        {
            if (!_stockService.DebitStock(id, quantity).Result)
                throw new DomainException("Fail to debit stock");

            return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
        }

        public async Task<ProductViewModel> ReplenishStock(Guid id, int quantity)
        {
            if (!_stockService.ReplenishStock(id, quantity).Result)
                throw new DomainException("Fail to replenish stock");

            return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
        }

        public async Task UpdateProduct(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);
            _productRepository.Update(product);

            await _productRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
            _stockService?.Dispose();
        }
    }
}
