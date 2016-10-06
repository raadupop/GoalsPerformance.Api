using AutoMapper;

namespace Mpdp.Api.Infrastructure.Mappings
{
  public class AutomapperConfiguration 
  {
    public static void Configure()
    {
      Mapper.Initialize(x =>
      {
        x.AddProfile<DomainToViewModelMappingProfile>();
        x.AddProfile<ViewModelToDomainMappingProfile>();
      });
    }

  }
}