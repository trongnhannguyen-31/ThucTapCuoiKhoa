using AutoMapper;
using Phoenix.Server.Data.Entity;
using Phoenix.Shared.Vendor;
using Phoenix.Shared.ProductType;
using Phoenix.Shared.Product;
using Phoenix.Shared.ProductSKU;
using Phoenix.Shared.Warehouse;
using Phoenix.Shared.Order;

namespace Phoenix.Server.Services.Infrastructure
{
    public class AutoMapperApiProfile : Profile
    {
        public AutoMapperApiProfile()
        {
            CreateMap<Vendor, VendorDto>();
                /*.ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.ImageRecord.AbsolutePath))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name));*/

            CreateMap<ProductType, ProductTypeDto>();

            CreateMap<Product, ProductDto>();

            CreateMap<ProductSKU, ProductSKUDto>();

            CreateMap<Warehouse, WarehouseDto>();

            CreateMap<Order, OrderDto>();
        }
    }
}

