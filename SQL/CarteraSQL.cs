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

        public async Task<List<Contrato>> CarteraGrupos(string userID)
        {
            //return new List<Contrato>() { new Contrato { contratoId = 1} };
            List<Contrato> contratos = new List<Contrato>();

            using (SqlConnection connection = new SqlConnection(_appSettings.cadenaConexionSQLServer))
            {
                SqlParameter[] Parameters =
                {
                    new SqlParameter("@userID", userID) { SqlDbType = SqlDbType.VarChar, Size = 150, Direction = ParameterDirection.Input },
                    new SqlParameter("@status", 1) { SqlDbType = SqlDbType.Bit, Direction = ParameterDirection.Input},
                    new SqlParameter("@dateFrom", DBNull.Value) { SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input},
                    new SqlParameter("@dateTo", DBNull.Value) { SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input}
                };

                DataTable exec = Helpers.SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, _appSettings.procedureContratos, Parameters, 1);
                
                foreach(DataRow dataRow in exec.Rows)
                {
                    Contrato contrato = new Contrato
                    {
                        fechaTermina = dataRow[1].ToString(),
                        nombreGeneral = dataRow[2].ToString(),
                        contratoId = int.Parse(dataRow[3].ToString())
                    };
                    contratos.Add(contrato);
                }
            }

            return contratos;
        }

        public async Task<ContratoDetalle> CarteraContratoDetalle(long contrato)
        {
            ContratoDetalle contratoDetalle = new ContratoDetalle();

            using (SqlConnection connection = new SqlConnection(_appSettings.cadenaConexionSQLServer))
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@contrato", contrato){SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input}
                };

                DataTable exec = Helpers.SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, _appSettings.procedureContratoDetalle, parameters, 2);
            
                foreach(DataRow dataRow in exec.Rows)
                {
                    if(contratoDetalle.integrantesCant == 0) 
                    {
                        contratoDetalle.fechaTermina = dataRow[0].ToString();
                        contratoDetalle.fechaInicio = dataRow[1].ToString();
                        contratoDetalle.importe = double.Parse(dataRow[2].ToString());
                        contratoDetalle.saldoActual = double.Parse(dataRow[3].ToString());
                        contratoDetalle.saldoAtrazado = double.Parse(dataRow[4].ToString());
                        contratoDetalle.diasAtrazo = int.Parse(dataRow[5].ToString());
                        contratoDetalle.pagoXPlazo = double.Parse(dataRow[6].ToString());
                        contratoDetalle.ultimoPlazoPag = int.Parse(dataRow[7].ToString());
                        contratoDetalle.plazos = int.Parse(dataRow[8].ToString());
                        contratoDetalle.capital = double.Parse(dataRow[9].ToString());
                        contratoDetalle.interes = double.Parse(dataRow[10].ToString());
                        contratoDetalle.contacto = dataRow[11].ToString();
                        contratoDetalle.status = dataRow[12].ToString();
                        contratoDetalle.integrantesCant = exec.Rows.Count;
                        contratoDetalle.integrantes = new List<Integrante>();
                    }

                    Integrante integrante = new Integrante
                    {
                        cveCli = dataRow[13].ToString(),
                        nombreCom = dataRow[14].ToString(),
                        telefonoCel = dataRow[15].ToString(),
                        importeT = double.Parse(dataRow[16].ToString())
                    };
                    contratoDetalle.integrantes.Add(integrante);
                }
            }
            
            return contratoDetalle;
        }
    
        public async Task<IntegranteDetalle> CarteraCreditoDetalle(long contrato, string cveCliente)
        {
            IntegranteDetalle integranteDetalle = new IntegranteDetalle();
            using(SqlConnection connection = new SqlConnection(_appSettings.cadenaConexionSQLServer))
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@contratoGrupo", contrato){SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input},
                    new SqlParameter("@cveCliente", cveCliente){SqlDbType = SqlDbType.VarChar, Size = 7, Direction = ParameterDirection.Input}
                };

                DataTable exec = Helpers.SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, _appSettings.procedureCreditoDetalle, parameters, 3);

                if (exec.Rows.Count > 0)
                {
                    integranteDetalle = new IntegranteDetalle
                    {
                        fechaTermina = exec.Rows[0][0].ToString(),
                        fechaUltimoPago = exec.Rows[0][1].ToString(),
                        noCda = long.Parse(exec.Rows[0][2].ToString()),
                        importeT = double.Parse(exec.Rows[0][3].ToString()),
                        saldoActual = double.Parse(exec.Rows[0][4].ToString()),
                        salAtr = double.Parse(exec.Rows[0][5].ToString()),
                        diaAtr = int.Parse(exec.Rows[0][6].ToString()),
                        pagos = int.Parse(exec.Rows[0][7].ToString()),
                        folio = int.Parse(exec.Rows[0][8].ToString()),
                        capital = double.Parse(exec.Rows[0][9].ToString()),
                        interes = double.Parse(exec.Rows[0][10].ToString())
                    };
                }
            }
            return integranteDetalle;
        }
    }
}