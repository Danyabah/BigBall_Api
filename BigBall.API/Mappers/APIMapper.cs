using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using BigBall.API.Enums;
using BigBall.API.Models.CreateRequest;
using BigBall.API.Models.Request;
using BigBall.API.Models.Response;
using BigBall.Services.Contracts.Enums;
using BigBall.Services.Contracts.Models;
using BigBall.Services.Contracts.ModelsRequest;

namespace BigBall.API.Mappers
{
    /// <summary>
    /// Маппер
    /// </summary>
    public class APIMapper : Profile
    {
        public APIMapper()
        {
            CreateMap<TrackModelType, TrackTypeResponse>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();

            CreateMap<CreateInstitutionRequest, InstitutionModel>(MemberList.Destination)
            .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<CreatePaymentRequest, PaymentModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<CreatePersonRequest, PersonModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<CreatePromotionRequest, PromotionModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<CreateTrackRequest, TrackModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<TrackRequest, TrackModel>(MemberList.Destination).ReverseMap();
            CreateMap<InstitutionRequest, InstitutionModel>(MemberList.Destination).ReverseMap();
            CreateMap<PaymentRequest, PaymentModel>(MemberList.Destination).ReverseMap();
            CreateMap<PersonRequest, PersonModel>(MemberList.Destination).ReverseMap();
            CreateMap<PromotionRequest, PromotionModel>(MemberList.Destination).ReverseMap();
            CreateMap<ReservationRequest, ReservationModel>(MemberList.Destination)
                .ForMember(x => x.Track, opt => opt.Ignore())
                .ForMember(x => x.Promotion, opt => opt.Ignore())
                .ForMember(x => x.Person, opt => opt.Ignore())
                .ForMember(x => x.Institution, opt => opt.Ignore())
                .ForMember(x => x.Payment, opt => opt.Ignore()).ReverseMap();

            CreateMap<ReservationRequest, ReservationModelRequest>(MemberList.Destination);
            CreateMap<CreateReservationRequest, ReservationModelRequest>(MemberList.Destination)
            .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<TrackModel, TrackResponse>(MemberList.Destination);
            CreateMap<InstitutionModel, InstitutionResponse>(MemberList.Destination);
            CreateMap<PaymentModel, PaymentResponse>(MemberList.Destination);
            CreateMap<PromotionModel, PromotionResponse>(MemberList.Destination);
            CreateMap<ReservationModel, ReservationResponse>(MemberList.Destination);
            CreateMap<PersonModel, PersonResponse>(MemberList.Destination)
                .ForMember(x => x.Name, opt => opt.MapFrom(src => $"{src.LastName} {src.FirstName} {src.Patronymic}"));
        }
    }
}
