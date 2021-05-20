using AutoMapper;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DTOs.Request;
using RentingSystemAPI.DTOs.Response;

namespace RentingSystemAPI.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<RegisterUserRequest, User>()
                .ForMember(u => u.UserName, m => m.MapFrom(u => u.Email))
                .ReverseMap();
            CreateMap<Item, ItemListResponse>();
            CreateMap<Item, AddItemRequest>().ReverseMap();
            CreateMap<Cart, AddItemRequest>().ReverseMap();
            CreateMap<Item, Cart>().ReverseMap();
            CreateMap<Cart, ItemListResponse>();
            CreateMap<Item, RentedItem>();
            CreateMap<Rent, RentedItemsResponse>();
            CreateMap<RentedItem, RentedItemsResponse>();
            CreateMap<User, AdminPanelResponse>()
                .ForMember(u => u.UserId, m => m.MapFrom(e => e.Id));
            CreateMap<ItemRequest, Item>()
                .ForMember(x => x.DocumentationUrl, y => y.MapFrom(u => u.Url));
            CreateMap<CategoryResponse, Category>().ReverseMap();
        }
    }
}