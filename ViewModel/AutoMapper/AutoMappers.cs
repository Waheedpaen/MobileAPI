

using ViewModel.ViewModels.UserViewModel;

namespace ViewModel.AutoMapper;
    public class AutoMappers : Profile
    {
    public AutoMappers()
    {
        CreateMap<Carousel, CarouselDto>().ReverseMap();
        CreateMap<Mobile, MobileDtoForList>().ReverseMap();
        CreateMap<Mobile, MobileDtoForSave>().ReverseMap();
        CreateMap<Brand, BrandDto>().ReverseMap();
        CreateMap<Country, CountryDto>().ReverseMap();
        CreateMap<OperatingSystems, OperatingSystemDto>().ReverseMap();
        CreateMap<City, CityDtoForList>().ReverseMap();
        CreateMap<City, CityDtoForList>().ReverseMap();
        CreateMap<City, CityDtoForSave>().ReverseMap();
        CreateMap<OperatingSystems, OperatingSystemDto>().ReverseMap();
        CreateMap<OSVersion, OSVersionDtoForList>().ReverseMap();
        CreateMap<OSVersion, OSVersionDtoForSave>().ReverseMap();
        CreateMap<PaymentCard, PaymentCardDto>().ReverseMap();
        CreateMap<MobileImage, MobileImageDtoForList>().ReverseMap();
        CreateMap<MobileImage, MobileImageDtoForSave>().ReverseMap();
        CreateMap<Color, ColorDto>().ReverseMap();
        CreateMap<UserDtoLogin, LoginUserDto>().ReverseMap();
        CreateMap<UserTypes, UserTypesDto>().ReverseMap();
        CreateMap<User, UserListDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
        CreateMap<User, UserListDto>().ReverseMap();
        //CreateMap<User, UserListDto>()
        //       .ForMember(dest =>
        //       dest.Name,
        //       opt => opt.MapFrom(src => src.)).ReverseMap();

    }



}