using APIKEY.Crudes.Data;
using APIKEY.Crudes.Models;
using APIKEY.Crudes.Repositories.Implementations._Base;
using APIKEY.Crudes.Repositories.Interfaces;

namespace APIKEY.Crudes.Repositories.Implementations;

public class CarroRepository : GenericRepository<Carro>, ICarroRepository
{
    public CarroRepository(AppDbContext context) : base(context) { }
}
