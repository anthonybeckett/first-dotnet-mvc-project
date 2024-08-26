using System.ComponentModel.DataAnnotations;

namespace FirstProject.Local.Data;

public class Category
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}
