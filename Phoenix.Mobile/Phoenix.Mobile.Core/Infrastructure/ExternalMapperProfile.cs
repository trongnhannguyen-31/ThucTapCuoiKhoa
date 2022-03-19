using AutoMapper;
using Phoenix.Mobile.Core.Models.Common;
using Phoenix.Mobile.Core.Models.Setting;
using Phoenix.Mobile.Core.Models.Vendor;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Vendor;

namespace Phoenix.Mobile.Core.Infrastructure
{
    public class ExternalMapperProfile : Profile
    {
        public ExternalMapperProfile()
        {

            //mapping dto to model
            CreateMap<CrudResult, CrudResultModel>();
            

            //setting
            CreateMap<SettingDto, SettingModel>();
            CreateMap<VendorDto, VendorModel>();

        }
    }
}
