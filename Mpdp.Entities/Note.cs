namespace Mpdp.Entities
{
  public class Note : IEntityBase
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public int UserProfileId { get; set; }

	  public virtual UserProfile UserProfile { get; set; }

	  public Rank Priority { get; set; }
  }
}
