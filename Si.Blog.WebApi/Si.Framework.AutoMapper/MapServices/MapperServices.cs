using AutoMapper;

namespace Si.Framework.AutoMapper.MapServices
{
    public class MapperServices : IMapperService
    {
        private readonly IMapper _mapper;

        public MapperServices(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TDestination>(object source)
        {
            try
            {
                return _mapper.Map<TDestination>(source);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Mapping failed.", ex);
            }
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            try
            {
                return _mapper.Map<TSource, TDestination>(source);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Mapping failed.", ex);
            }
        }

        public bool TryMap<TDestination>(object source, out TDestination destination)
        {
            try
            {
                destination = _mapper.Map<TDestination>(source);
                return true;
            }
            catch (Exception ex)
            {
                destination = default;
                return false;
            }
        }

        public bool TryMap<TSource, TDestination>(TSource source, out TDestination destination)
        {
            try
            {
                destination = _mapper.Map<TSource, TDestination>(source);
                return true;
            }
            catch (Exception ex)
            {
                destination = default;
                return false;
            }
        }
    }
}
