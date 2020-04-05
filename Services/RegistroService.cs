using AsesoresAPI.Entities;
using AsesoresAPI.SQL;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace AsesoresAPI.Services
{
    public interface IRegistroService
    {
        Task<ExpandoObject> RegistraCliente(Cliente cliente);
    }
    public class RegistroService : IRegistroService
    {
        public readonly AppSettings _appSettings;

        public RegistroService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<ExpandoObject> RegistraCliente(Cliente cliente)
        {
            RegistroSQL sql = new RegistroSQL(_appSettings);
            ExpandoObject jsonResult = await sql.RegistroClienteBuro(cliente);
            return jsonResult;
        }
    }
}
