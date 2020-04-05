using AsesoresAPI.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace AsesoresAPI.SQL
{
    public class RegistroSQL
    {
        private readonly AppSettings _appSettings;

        public RegistroSQL(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<ExpandoObject> RegistroClienteBuro(Cliente cliente)
        {
            //JsonResult result = new JsonResult { error = 1, result = "NO DATA" };
            dynamic result = new ExpandoObject();
            result.error = 1;
            result.result = "NO DATA";
            string proc;

            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(_appSettings.cadenaConexionSQLServerPrueba))
            {
                System.Data.SqlClient.SqlParameter[] parameters =
                {
                    new System.Data.SqlClient.SqlParameter("@error", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output },
                    new System.Data.SqlClient.SqlParameter("@resultado", System.Data.SqlDbType.VarChar, 400) { Direction = System.Data.ParameterDirection.Output },
                    new System.Data.SqlClient.SqlParameter("@Usu", cliente.usuarioRegistra ) {SqlDbType =System.Data.SqlDbType.Int, Direction = System.Data.ParameterDirection.Input },
                    new System.Data.SqlClient.SqlParameter("@cveEncar", "") { SqlDbType = System.Data.SqlDbType.VarChar, Size = 6, Direction = System.Data.ParameterDirection.Input},
                    new System.Data.SqlClient.SqlParameter("@CveCliNva", "" ) {SqlDbType =System.Data.SqlDbType.VarChar, Size = 12, Direction = System.Data.ParameterDirection.Output },
                    new System.Data.SqlClient.SqlParameter("@XRBID", System.Data.SqlDbType.Int ) {Direction = System.Data.ParameterDirection.Output },
                    new System.Data.SqlClient.SqlParameter("@userID", cliente.userID ) {SqlDbType =System.Data.SqlDbType.VarChar, Size = 150, Direction = System.Data.ParameterDirection.Input },
                    new System.Data.SqlClient.SqlParameter("@solicitudId", cliente.solicitudID ) {SqlDbType =System.Data.SqlDbType.VarChar, Size = 150, Direction = System.Data.ParameterDirection.Input },
                    new System.Data.SqlClient.SqlParameter("@Nombre", cliente.nombre ) {SqlDbType =System.Data.SqlDbType.VarChar, Size = 64, Direction = System.Data.ParameterDirection.Input },
                    new System.Data.SqlClient.SqlParameter("@ApellidoPat", cliente.apellidoPat ) {SqlDbType =System.Data.SqlDbType.VarChar, Size = 64, Direction = System.Data.ParameterDirection.Input },
                    new System.Data.SqlClient.SqlParameter("@ApellidoMat", cliente.apellidoMat ) {SqlDbType =System.Data.SqlDbType.VarChar, Size = 64, Direction = System.Data.ParameterDirection.Input },
                    new System.Data.SqlClient.SqlParameter("@TelCel", cliente.TelCel ) {SqlDbType =System.Data.SqlDbType.VarChar, Size = 24, Direction = System.Data.ParameterDirection.Input },
                    new System.Data.SqlClient.SqlParameter("@Status", "A" ) {SqlDbType =System.Data.SqlDbType.VarChar, Size = 1, Direction = System.Data.ParameterDirection.Input },
                    new System.Data.SqlClient.SqlParameter("@RFC", cliente.rfc ) {SqlDbType =System.Data.SqlDbType.VarChar, Size = 13, Direction = System.Data.ParameterDirection.Input },
                    new System.Data.SqlClient.SqlParameter("@fec_nac", cliente.fechaNac ) {SqlDbType =System.Data.SqlDbType.DateTime, Direction = System.Data.ParameterDirection.Input },
                    new System.Data.SqlClient.SqlParameter("@Nombre_Vialidad", cliente.nombreVialidad ) {SqlDbType =System.Data.SqlDbType.VarChar, Size = 50, Direction = System.Data.ParameterDirection.Input },
                    new System.Data.SqlClient.SqlParameter("@curp", cliente.curp ) {SqlDbType =System.Data.SqlDbType.VarChar, Size = 18, Direction = System.Data.ParameterDirection.Input },
                    new System.Data.SqlClient.SqlParameter("@COD_POS", cliente.cp ) {SqlDbType =System.Data.SqlDbType.VarChar, Size = 5, Direction = System.Data.ParameterDirection.Input },
                    new System.Data.SqlClient.SqlParameter("@NombreAdicional", cliente.nombreAdicional ) {SqlDbType =System.Data.SqlDbType.VarChar, Size = 75, Direction = System.Data.ParameterDirection.Input },
                    new System.Data.SqlClient.SqlParameter("@Estado", cliente.estado ) {SqlDbType =System.Data.SqlDbType.VarChar, Size = 30, Direction = System.Data.ParameterDirection.Input },
                    new System.Data.SqlClient.SqlParameter("@Municipio", cliente.municipio ) {SqlDbType =System.Data.SqlDbType.VarChar, Size = 50, Direction = System.Data.ParameterDirection.Input },
                    new System.Data.SqlClient.SqlParameter("@Colonia", cliente.colonia ) {SqlDbType =System.Data.SqlDbType.VarChar, Size = 50, Direction = System.Data.ParameterDirection.Input },
                    new System.Data.SqlClient.SqlParameter("@Ciudad", cliente.ciudad ) {SqlDbType =System.Data.SqlDbType.VarChar, Size = 50, Direction = System.Data.ParameterDirection.Input }
                };

                switch (cliente.sistema)
                {
                    case 1:
                        proc = _appSettings.procedureRegistroClienteBuroVR;
                        break;
                    case 2:
                        proc = _appSettings.procedureRegistroClienteBuroOPOR;
                        break;
                    case 3:
                        proc = _appSettings.procedureRegistroClienteBuroCR;
                        break;
                    case 4:
                        proc = _appSettings.procedureRegistroClienteBuroGYT;
                        break;
                    default:
                        proc = "";
                        break;
                }

                await connection.OpenAsync();
                using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(proc, connection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    command.Parameters.AddRange(parameters);
                    await command.ExecuteNonQueryAsync();
                    result.error = int.Parse(parameters[0].Value.ToString());
                    result.result = parameters[1].Value.ToString();
                    result.CveCli = parameters[4].Value.ToString();
                    result.idXRB = int.Parse(parameters[5].Value.ToString());
                }
            }


            return result;
        }
    }
}
