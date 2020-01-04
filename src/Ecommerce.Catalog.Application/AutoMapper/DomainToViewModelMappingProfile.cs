using AutoMapper;
using Ecommerce.Catalog.Application.ViewModel;
using Ecommerce.Catalog.Domain.Entities;

namespace Ecommerce.Catalog.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(d => d.Height, o => o.MapFrom(s => s.Dimensions.Height))
                .ForMember(d => d.Width, o => o.MapFrom(s => s.Dimensions.Width))
                .ForMember(d => d.Depth, o => o.MapFrom(s => s.Dimensions.Depth));
            CreateMap<Category, CategoryViewModel>();
        }
    }
}
