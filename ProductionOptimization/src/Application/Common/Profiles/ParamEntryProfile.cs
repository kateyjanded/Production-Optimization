using Application.PVT.Command;
using Application.PVT.Query;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Profiles
{
    public class ParamEntryProfile: Profile
    {
        public ParamEntryProfile()
        {
            CreateMap<ParamEntry, ParamEntryDTO>().ReverseMap();
        }
    }
}
