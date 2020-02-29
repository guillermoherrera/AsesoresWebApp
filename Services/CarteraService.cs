using AsesoresAPI.Entities;
using AsesoresAPI.SQL;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace AsesoresAPI.Services
{
    public interface ICarteraService
    {
        Task<List<Contrato>> GetCartera(string usuarioId, string uniqueDevice);
        Task<List<object>> GetCarteraContratoDetalle(string usuarioId, int productoId, long contratoId, string uniqueDevice);
        Task<List<object>> GetCarteraCreditoDetalle(string usuarioId, int productoId, long contratoId, long creditoId, string uniqueDevice);
    }

    public class CarteraService : ICarteraService
    {
        private readonly AppSettings _appSettings;

        public CarteraService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<List<Contrato>> GetCartera(string usuarioId, string uniqueDevice)
        {
            CarteraSQL sql = new CarteraSQL(_appSettings);
            List<Contrato> datos = await sql.CarteraGrupos(usuarioId, uniqueDevice);
            return datos;
        }

        public Task<List<object>> GetCarteraContratoDetalle(string usuarioId, int productoId, long contratoId, string uniqueDevice)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<object>> GetCarteraCreditoDetalle(string usuarioId, int productoId, long contratoId, long creditoId, string uniqueDevice)
        {
            throw new System.NotImplementedException();
        }

        //public Task<List<object>> GetGrupos(string usuarioId)
        //{
        //    CarteraSQL sql = new CarteraSQL(_appSettings);
        //    //var cartera = sql.CarteraGrupos(usuarioId);

        //    //if (cartera == null)
        //        return null;

        //    //return cartera;
        //}
    }
}
