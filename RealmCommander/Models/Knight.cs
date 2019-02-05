using System.ComponentModel.DataAnnotations;

namespace RealmCommander.Models
{
  public class Knight
  {
    public int Id { get; set; }
    public string UserId { get; set; }

    [Required]
    [MinLength(1)]
    public string Name { get; set; }
  }
}