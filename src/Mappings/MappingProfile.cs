namespace Totter.Mappings;

using AutoMapper;
using Totter.Tweets;
using Totter.Users;

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<User, GetUserDTO>();
        CreateMap<Tweet, GetTweetDTO>();
    }
}
