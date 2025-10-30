namespace WebTemplate.Models
{
    public class Karta
    {
        [Key]
        public int Id { get; set; }
        public uint Red { get; set; }
        public uint Sediste { get; set; }

        public Projekcija? Projekcija { get; set; }
    }
}
