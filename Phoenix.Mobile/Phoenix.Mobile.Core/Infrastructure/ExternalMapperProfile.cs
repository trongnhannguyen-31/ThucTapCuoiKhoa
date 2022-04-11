using AutoMapper;
using Phoenix.Mobile.Core.Models.Common;
using Phoenix.Mobile.Core.Models.Setting;
using Phoenix.Mobile.Core.Models.Vendor;
using Phoenix.Mobile.Core.Models.ProductType;
using Phoenix.Mobile.Core.Models.Product;
using Phoenix.Mobile.Core.Models.ProductSKU;
using Phoenix.Mobile.Core.Models.Order;
using Phoenix.Mobile.Core.Models.OrderDetail;
using Phoenix.Mobile.Core.Models.Customer;
using Phoenix.Mobile.Core.Models.Warehouse;
using Phoenix.Mobile.Core.Models.Rating;
using Phoenix.Mobile.Core.Models.ImageRecord;
using Phoenix.Mobile.Core.Models.Cart;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Vendor;
using Phoenix.Shared.ProductType;
using Phoenix.Shared.Product;
using Phoenix.Shared.ProductSKU;
using Phoenix.Shared.Order;
using Phoenix.Shared.OrderDetail;
using Phoenix.Shared.Customer;
using Phoenix.Shared.Warehouse;
using Phoenix.Shared.Rating;
using Phoenix.Shared.ImageRecord;
using Phoenix.Shared.CartItem;
using Phoenix.Mobile.Core.Models.CartItem;

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
            CreateMap<VendorModel, VendorDto>();
            CreateMap<ProductTypeDto, ProductTypeModel>();
            CreateMap<ProductDto, ProductModel>();
            CreateMap<ProductSKUDto, ProductSKUModel>();
            CreateMap<OrderAppDto, OrderModel>();
            CreateMap<OrderDetailAppDto, OrderDetailModel>();
            CreateMap<CustomerDto, CustomerModel>();
            CreateMap<WarehouseDto, WarehouseModel>();
            CreateMap<RatingAppDto, RatingModel>();
            CreateMap<ImageRecordDto, ImageRecordModel>();

            CreateMap<ProductMenuDto, ProductMenuModel>();
            CreateMap<CartItemDto, CartItemModel>();
            CreateMap<CartListDto, CartListModel>();
            CreateMap<OrderDetailHistoryDto, OrderDetailHistoryModel>();

        }
    }
}
