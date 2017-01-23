namespace Mpdp.Entities
{
  public class UserProfile : IEntityBase
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Location { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; }
  }
}
