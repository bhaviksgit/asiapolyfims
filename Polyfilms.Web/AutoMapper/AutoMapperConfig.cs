using AutoMapper;
using PolyFilms.Data.Entity;
using PolyFilms.Data.Models;

namespace PolyFilms.Web
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<TblUser, UserModel>().ReverseMap();
            CreateMap<TblRoles, RolesModel>().ReverseMap();
            CreateMap<TblCustomer, CustomerModel>().ReverseMap();
            CreateMap<TblConsignee, ConsigneeModel>().ReverseMap();
            CreateMap<TblProduct, ProductModel>().ReverseMap();
            CreateMap<TblJumbo, JumboModel>().ReverseMap();
            CreateMap<TblOrder, OrderModel>()
                //.ForMember(x=>x.PhoneNumber , m=>m.MapFrom(y=>y.Consignee.PhoneNumber))
                //.ForMember(x => x.DeliveryAddress, m => m.MapFrom(y => y.Consignee.DeliveryAddress))
                .ReverseMap();
            
            CreateMap<TblOrderDetail, OrderDetailModel>().ReverseMap();

            //CreateMap<TblSlitting, SlittingModel>().ForMember(x=>x.JumboJointId , m=>m.MapFrom(z=> PolyFilms.Common.CommonHelper.ConvertStringToIntList(z.JumboJointId)))
            //                .ReverseMap().ForMember(x => x.JumboJointId, m => m.MapFrom(a => string.Join(',', a.JumboJointId)));

            CreateMap<TblSlitting, SlittingModel>().ReverseMap();
            CreateMap<TblDispatch, DispatchModel>().ReverseMap();
            CreateMap<TblDispatchDetail, DispatchDetailModel>().ReverseMap();
            CreateMap<TblAudit, AuditModel>().ReverseMap();
            CreateMap<TblAudit, AuditDetailsModel>().ReverseMap();
            CreateMap<TblInvoice,InvoiceModel>().ReverseMap();
            CreateMap<TblInvoiceDetail, InvoiceDetailModel>().ReverseMap();
            CreateMap<TblCore, CoreModel>().ReverseMap();
            CreateMap<TblSlitting, AdditionalSlittingDetailModel>().ReverseMap();

            //CreateMap<TblSlitting, AdditionalSlittingDetailModel>().ForMember(x => x.JumboJointId, m => m.MapFrom(z => PolyFilms.Common.CommonHelper.ConvertStringToIntList(z.JumboJointId)))
            //    .ReverseMap().ForMember(x => x.JumboJointId, m => m.MapFrom(a => string.Join(',', a.JumboJointId)));
        }
    }
}
