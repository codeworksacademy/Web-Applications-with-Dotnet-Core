using System.ComponentModel.DataAnnotations;

namespace RealmCommander.Models
{
  public class Quest
  {
    public int Id { get; set; }
    public string UserId { get; set; }

    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }
  }
}