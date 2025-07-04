using AutoMapper;
using WebAppActions.Models.ViewModels;

namespace WebAppActions.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Book, BookFormViewModel>().ReverseMap();

            CreateMap<Book, BookDetailsViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Owners, opt => opt.MapFrom(src => src.UserBooks))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews));

            CreateMap<Review, ReviewViewModel>();
            CreateMap<UserBook, BookOwnerViewModel>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName));
        }
    }
} 