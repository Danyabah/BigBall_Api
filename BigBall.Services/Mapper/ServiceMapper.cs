using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using BigBall.Context.Contracts.Enums;
using BigBall.Context.Contracts.Models;
using BigBall.Services.Contracts.Enums;
using BigBall.Services.Contracts.Models;
using BigBall.Services.Contracts.ModelsRequest;

namespace BigBall.Services.Mapper
{
    public class ServiceMapper : Profile
    {
        public ServiceMapper()
        {
            CreateMap<TrackType, TrackModelType>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();

            CreateMap<Institution, InstitutionModel>(MemberList.Destination).ReverseMap();
            CreateMap<Payment, PaymentModel>(MemberList.Destination).ReverseMap();
            CreateMap<Person, PersonModel>(MemberList.Destination).ReverseMap();
            CreateMap<Promotion, PromotionModel>(MemberList.Destination).ReverseMap();
            CreateMap<Track, TrackModel>(MemberList.Destination).ReverseMap();
            CreateMap<Reservation, ReservationModel>(MemberList.Destination)
                .ForMember(x => x.Institution, opt => opt.Ignore())
                .ForMember(x => x.Payment, opt => opt.Ignore())
                .ForMember(x => x.Promotion, opt => opt.Ignore())
                .ForMember(x => x.Person, opt => opt.Ignore())
                .ForMember(x => x.Track, opt => opt.Ignore()).ReverseMap();

            CreateMap<ReservationModelRequest, Reservation>(MemberList.Destination)
                .ForMember(x => x.Institution, opt => opt.Ignore())
                .ForMember(x => x.Payment, opt => opt.Ignore())
                .ForMember(x => x.Promotion, opt => opt.Ignore())
                .ForMember(x => x.Person, opt => opt.Ignore())
                .ForMember(x => x.Track, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.DeletedAt, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
                .ForMember(x => x.UpdatedBy, opt => opt.Ignore()).ReverseMap();
        }
    }
}
