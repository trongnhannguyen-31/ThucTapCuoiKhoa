using AutoMapper;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Web.Areas.Admin.Models.Customer;
using Phoenix.Server.Web.Areas.Admin.Models.Product;
using Phoenix.Server.Web.Areas.Admin.Models.ProductSKU;
using Phoenix.Server.Web.Areas.Admin.Models.ProductType;
using Phoenix.Server.Web.Areas.Admin.Models.Rating;
using Phoenix.Server.Web.Areas.Admin.Models.Warehouse;
using Phoenix.Server.Web.Areas.Admin.Models.WarehouseMenu;
using Phoenix.Shared.Warehouse;

namespace Phoenix.Server.Web.Infrastructure
{
    public class AutoMapperExtendWebProfile : Profile
    {
        public AutoMapperExtendWebProfile()
        {
            CreateMap<ProductType, ProductTypeModel>();

            CreateMap<Customer, CustomerModel>();

            CreateMap<Warehouse, WarehouseModel>()
                .ForMember(d => d.Product_Name, o => o.MapFrom(s => s.ProductSKU.Product.Name)); 

            CreateMap<Rating, RatingModel>();

            CreateMap<WarehouseMenu, WarehouseMenuModel>();

            CreateMap<ProductSKU, ProductSKUModel>();

            CreateMap<Product, ProductModel>();
        }
    }
}