using System.ComponentModel.DataAnnotations;

namespace CrazyMusicians.Api.Dtos
{
    public class MusicianCreateDto
    {
        [Required, StringLength(60)] public string Name { get; set; } = string.Empty;
        [Required, StringLength(60)] public string Profession { get; set; } = string.Empty;
        [Required, StringLength(200)] public string FunTrait { get; set; } = string.Empty;
    }

    public class MusicianUpdateDto
    {
        [Required, StringLength(60)] public string Name { get; set; } = string.Empty;
        [Required, StringLength(60)] public string Profession { get; set; } = string.Empty;
        [Required, StringLength(200)] public string FunTrait { get; set; } = string.Empty;
    }

    // PATCH için kısmi alanlar
    public class MusicianPatchDto
    {
        [StringLength(60)] public string? Name { get; set; }
        [StringLength(60)] public string? Profession { get; set; }
        [StringLength(200)] public string? FunTrait { get; set; }
    }
}
