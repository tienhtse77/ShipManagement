namespace Application.Common.Models.Mapping
{
    public interface IMapFrom<T>
    {
        void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap(typeof(T), GetType());
        }
    }
}