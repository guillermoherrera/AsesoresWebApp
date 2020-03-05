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
        Task<List<Contrato>> GetCartera(string userID);
        Task<ContratoDetalle> GetCarteraContratoDetalle(long contrato);
        Task<IntegranteDetalle> GetCarteraCreditoDetalle(int contrato, string cveCliente);
    }

    public class CarteraService : ICarteraService
    {
        private readonly AppSettings _appSettings;

        public CarteraService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<List<Contrato>> GetCartera(string userID)
        {
            CarteraSQL sql = new CarteraSQL(_appSettings);
            List<Contrato> datos = await sql.CarteraGrupos(userID);
            return datos;
        }

        public async Task<ContratoDetalle> GetCarteraContratoDetalle(long contrato)
        {
            CarteraSQL sql = new CarteraSQL(_appSettings);
            ContratoDetalle contratoDetalle = await sql.CarteraContratoDetalle(contrato);
            return contratoDetalle;
            //throw new System.NotImplementedException();
        }

        public async Task<IntegranteDetalle> GetCarteraCreditoDetalle(int contrato, string cveCliente)
        {
            CarteraSQL sql = new CarteraSQL(_appSettings);
            IntegranteDetalle integranteDetalle = await sql.CarteraCreditoDetalle(contrato, cveCliente); 
            return integranteDetalle;
            //throw new System.NotImplementedException();
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
