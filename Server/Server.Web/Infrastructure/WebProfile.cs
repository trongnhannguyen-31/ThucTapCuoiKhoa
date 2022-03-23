using AutoMapper;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Web.Areas.Admin.Models.Customer;
using Phoenix.Server.Web.Areas.Admin.Models.ProductType;
using Phoenix.Server.Web.Areas.Admin.Models.Warehouse;
using Phoenix.Shared.Warehouse;

namespace Phoenix.Server.Web.Infrastructure
{
    public class AutoMapperExtendWebProfile : Profile
    {
        public AutoMapperExtendWebProfile()
        {
            CreateMap<ProductType, ProductTypeModel>();

            CreateMap<Customer, CustomerModel>();

            CreateMap<Warehouse, WarehouseModel>();
        }
    }
}