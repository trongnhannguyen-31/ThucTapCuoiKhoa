using AutoMapper;
using Phoenix.Server.Data.Entity;
using Phoenix.Shared.Vendor;
using Phoenix.Shared.ProductType;
using Phoenix.Shared.Product;
using Phoenix.Shared.ProductSKU;
using Phoenix.Shared.Warehouse;
using Phoenix.Shared.Order;
using Phoenix.Shared.WarehouseMenu;
using Phoenix.Shared.CartItem;
using Phoenix.Shared.OrderDetail;
using Phoenix.Server.Services.MainServices.Common.Models;
using Phoenix.Shared.z_User;
using Phoenix.Shared.Customer;

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
            CreateMap<WarehouseMenu, WarehouseMenuDto>();
            CreateMap<ImageRecord, ImageRecordDto>();
            //CreateMap<ProductMenu, ProductMenuDto>();
            CreateMap<CartItem, CartItemDto>();
            CreateMap<OrderDetail, OrderDetailDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<z_User, z_UserDto>();
            CreateMap<Customer, CustomerDto>();

            CreateMap<Warehouse, WarehouseOrderDto>();
        }
    }
}

