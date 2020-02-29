using AsesoresAPI.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace AsesoresAPI.SQL
{
    public class CarteraSQL
    {
        private readonly AppSettings _appSettings;

        public CarteraSQL(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<List<Contrato>> CarteraGrupos(string usuarioId, string uniqueDevice)
        {
            return new List<Contrato>() { new Contrato { contratoId = 1} };
            //using (SqlConnection connection = new SqlConnection(_appSettings.cadenaConexionSQLServer))
            //{
            //    SqlParameter[] parameters =
            //    {
            //        /*00*/new SqlParameter("@usuario", SqlDbType.VarChar, 10) { Value = usuarioId },
            //        /*01*/new SqlParameter("@device", SqlDbType.VarChar, 10) { Value = uniqueDevice }
            //    };

            //    await connection.OpenAsync();
            //    using (SqlCommand command = new SqlCommand(_appSettings.procedureCarteraGrupos, connection) { CommandType = System.Data.CommandType.StoredProcedure })
            //    {
            //        command.Parameters.AddRange(parameters);

            //        using (SqlDataReader r = await command.ExecuteReaderAsync())
            //        {
            //            if (r.HasRows)
            //            {
            //                return new List<Contrato>();
            //            }
            //            else
            //            {
            //                throw new Exception();
            //            }
            //        }
            //    }
            //    //}
            //}
        }
    }
}