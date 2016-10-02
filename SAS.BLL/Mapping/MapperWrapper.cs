using System.Collections.Generic;
using AutoMapper;

namespace SAS.BLL.Mapping
{
    public class MapperWrapper<TDAL, TBLL>
    {
        IMapper mapper;

        public MapperWrapper()
        {
            mapper = new Mapper(new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<TDAL, TBLL>();
                cfg.CreateMap<TBLL, TDAL>();
            }));
        }

        public TDAL Map(TBLL dto)
        {
            return mapper.Map<TDAL>(dto);
        }

        public TBLL Map(TDAL entity)
        {
            return mapper.Map<TBLL>(entity);
        }

        public IEnumerable<TBLL> Map(IEnumerable<TDAL> entities)
        {
            return mapper.Map<IEnumerable<TDAL>, IEnumerable<TBLL>>(entities);
        }
    }
}
