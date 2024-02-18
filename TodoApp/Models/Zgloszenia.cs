using System.ComponentModel.DataAnnotations;

public class Zgloszenia
{
    public int Id { get; set; }

    [Display(Name = "Tytul")]
    public string Title { get; set; }

    [Display(Name = "Opis")]
    public string Description { get; set; }
}
