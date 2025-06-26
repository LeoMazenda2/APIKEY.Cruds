using APIKEY.Crudes.Data;
using APIKEY.Crudes.Models;
using APIKEY.Crudes.Repositories.Implementations._Base;
using APIKEY.Crudes.Repositories.Interfaces;
using System;

namespace APIKEY.Crudes.Repositories.Implementations;

public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
{
    public ClienteRepository(AppDbContext context) : base(context) { }
}
