using AutoMapper;
using Totter.Tweets;
using Totter.Users;

namespace Totter.Mappings {
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<User, GetUserDTO>();
            CreateMap<Tweet, GetTweetDTO>();
        }
    }
}
