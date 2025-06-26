namespace APIKEY.Crudes.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public ICollection<Carro> Carros { get; set; } = new List<Carro>();
    }
}
