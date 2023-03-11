using AgileBoard.Core.Contracts.Responses;
using AgileBoard.Core.Domain;
using AutoMapper;

namespace AgileBoard.Infrastructure.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<UserStory, UserStoryResponse>();
        }
    }
}
