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
using Phoenix.Shared.Rating;
using Phoenix.Shared.User;
using Falcon.Web.Core.Auth;

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
            CreateMap<ProductSKUAppDto, ProductSKUDto>();
            CreateMap<Warehouse, WarehouseDto>();
            CreateMap<Order, OrderDto>();            
            CreateMap<WarehouseMenu, WarehouseMenuDto>();
            CreateMap<ImageRecord, ImageRecordDto>();
            //CreateMap<ProductMenu, ProductMenuDto>();
            CreateMap<CartItem, CartItemDto>();
            CreateMap<OrderDetail, OrderDetailAppDto>();
            CreateMap<Order, OrderAppDto>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<Rating, RatingAppDto>();
            CreateMap<Warehouse, WarehouseOrderDto>();
            CreateMap<User, UserDto>();
        }
    }
}

