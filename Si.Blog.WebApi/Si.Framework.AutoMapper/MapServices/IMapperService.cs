namespace Si.Framework.AutoMapper.MapServices
{
    public interface IMapperService
    {
        TDestination Map<TDestination>(object source);

        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);

        bool TryMap<TDestination>(object source, out TDestination destination);

        bool TryMap<TSource, TDestination>(TSource source, out TDestination destination);

    }
}
