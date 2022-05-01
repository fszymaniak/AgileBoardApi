using AgileBoard.Api.Contracts.Responses;
using AgileBoard.Api.Domain;
using AutoMapper;

namespace AgileBoard.Api.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<UserStory, UserStoryResponse>();
        }
    }
}
