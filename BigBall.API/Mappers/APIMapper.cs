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
            .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<CreatePaymentRequest, PaymentModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<CreatePersonRequest, PersonModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<CreatePromotionRequest, PromotionModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<CreateTrackRequest, TrackModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<TrackRequest, TrackModel>(MemberList.Destination);
            CreateMap<InstitutionRequest, InstitutionModel>(MemberList.Destination);
            CreateMap<PaymentRequest, PaymentModel>(MemberList.Destination);
            CreateMap<PersonRequest, PersonModel>(MemberList.Destination);
            CreateMap<PromotionRequest, PromotionModel>(MemberList.Destination);
            CreateMap<ReservationRequest, ReservationModel>(MemberList.Destination)
                .ForMember(x => x.Track, opt => opt.Ignore())
                .ForMember(x => x.Promotion, opt => opt.Ignore())
                .ForMember(x => x.Person, opt => opt.Ignore())
                .ForMember(x => x.Institution, opt => opt.Ignore())
                .ForMember(x => x.Payment, opt => opt.Ignore());

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
