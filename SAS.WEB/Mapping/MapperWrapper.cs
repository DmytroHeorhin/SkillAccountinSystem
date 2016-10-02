using System.Collections.Generic;
using AutoMapper;

namespace SAS.WEB.Mapping
{
    public class MapperWrapper<TBLL, TWEB>
    {
        IMapper mapper;

        public MapperWrapper()
        {
            mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TBLL, TWEB>();
                cfg.CreateMap<TWEB, TBLL>();
            }));
        }

        public TBLL Map(TWEB dto)
        {
            return mapper.Map<TBLL>(dto);
        }

        public TWEB Map(TBLL entity)
        {
            return mapper.Map<TWEB>(entity);
        }

        public IEnumerable<TWEB> Map(IEnumerable<TBLL> DTOs)
        {
            return mapper.Map<IEnumerable<TBLL>, IEnumerable<TWEB>>(DTOs);
        }

        public IEnumerable<TBLL> Map(IEnumerable<TWEB> Models)
        {
            return mapper.Map<IEnumerable<TWEB>, IEnumerable<TBLL>>(Models);
        }
    }
}
