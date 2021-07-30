using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.PVT.Query
{
    public class ParamEntryDTO
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public string Symbol { get; set; }
    }
}
