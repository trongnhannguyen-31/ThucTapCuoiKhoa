using AutoMapper;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Web.Areas.Admin.Models.ProductType;
using Phoenix.Shared.ProductType;

namespace Phoenix.Server.Web.Infrastructure
{
    public class AutoMapperExtendWebProfile : Profile
    {
        public AutoMapperExtendWebProfile()
        {
            CreateMap<ProductType, ProductTypeModel>();
        }
    }
}