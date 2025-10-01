using System.ComponentModel.DataAnnotations;

namespace CrazyMusicians.Api.Models
{
    public class Musician
    {
        public int Id { get; set; }

        [Required, StringLength(60)]    // Ad - zorunlu
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(60)]    // Meslek - zorunlu
        public string Profession { get; set; } = string.Empty;

        [Required, StringLength(200)]   // Eğlenceli özellik - zorunlu
        public string FunTrait { get; set; } = string.Empty;
    }
}
