using System.Globalization;

namespace WebTemplate.Models
{
    public class Projekcija
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(15)]
        public required string Naziv { get; set; }
        
        public DateTime Vreme { get; set; }
        [MaxLength(10)]
        public required string Sala { get; set; }

        public uint Sifra { get; set; }
        public uint BrojReda { get; set; }
        public uint BrojSedistaURedu { get; set; }

        public List<Karta>? Karte { get; set; }
    }
}
