namespace APIKEY.Crudes.Models
{
    public class Carro
    {
        public int Id { get; set; }
        public string Modelo { get; set; } = string.Empty;
        public int ClienteId { get; set; }

        public Cliente? Cliente { get; set; }
    }
}
