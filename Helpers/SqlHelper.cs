using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AsesoresAPI.Helpers
{
    public class SqlHelper
    {
        public static DataTable ExecuteDataTable(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParams, int config_dt)
        {
            DataTable dt = new DataTable();
            switch(config_dt)
            {
                case 1:
                    dt.Columns.Add("status");
                    dt.Columns.Add("Termina");
                    dt.Columns.Add("referencia");
                    dt.Columns.Add("id");
                    break;
                case 2:
                    dt.Columns.Add("Termina");
                    dt.Columns.Add("Inicio");
                    dt.Columns.Add("Importe");
                    dt.Columns.Add("SaldoAct");
                    dt.Columns.Add("SaldoAtr");
                    dt.Columns.Add("DiasAtrazo");
                    dt.Columns.Add("pago_x_plazo");
                    dt.Columns.Add("UltmoPlazoPag");
                    dt.Columns.Add("Plazos");
                    dt.Columns.Add("Capital");
                    dt.Columns.Add("Interes");
                    dt.Columns.Add("Contacto");
                    dt.Columns.Add("Status");
                    dt.Columns.Add("CveCli");
                    dt.Columns.Add("NombreCom");
                    dt.Columns.Add("TelCel");
                    dt.Columns.Add("ImporteT");
                    dt.Columns.Add("CapitalI");
                    dt.Columns.Add("dia_atr");
                    dt.Columns.Add("NoCda");
                    dt.Columns.Add("Renovado");
                    dt.Columns.Add("Tesorero");
                    dt.Columns.Add("Presidente");
                    break;
                case 3:
                    dt.Columns.Add("Termina");
                    dt.Columns.Add("fec_ultimo_pago");
                    dt.Columns.Add("NoCda");
                    dt.Columns.Add("ImporteT");
                    dt.Columns.Add("SaldoActual");
                    dt.Columns.Add("sal_atr");
                    dt.Columns.Add("dia_atr");
                    dt.Columns.Add("Pagos");
                    dt.Columns.Add("Folio");
                    dt.Columns.Add("Capital");
                    dt.Columns.Add("interes");
                    break;
                default:
                    break;
            }

            SqlDataReader dr = ExecuteReader(conn, cmdType, cmdText, cmdParams);

            switch (config_dt) 
            {
                case 1:
                    while (dr.Read())
                    {
                        dt.Rows.Add(dr[0], dr[1], dr[2], dr[3]);
                    }
                    break;
                case 2:
                    while (dr.Read())
                    {
                        dt.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12], dr[13], dr[14], dr[15], dr[16], dr[17], dr[18], dr[19], dr[20], dr[21], dr[22]);
                    }
                    break;
                case 3:
                    while (dr.Read())
                    {
                        dt.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10]);
                    }
                    break;
                default:
                    break;
            }

            return dt;
        }

        public static SqlDataReader ExecuteReader(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParams)
        {
            SqlCommand cmd = conn.CreateCommand();
            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParams);
            var rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return rdr;
        }

        private static void PrepareCommand(SqlCommand cmd ,SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParams)
        {
            if(conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if(trans != null) 
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = cmdType;
            if(cmdParams != null)
            {
                AttachParameters(cmd, cmdParams);
            }
        }

        private static void AttachParameters(SqlCommand cmd, SqlParameter[] cmdParams)
        {
            foreach(SqlParameter p in cmdParams)
            {
                if((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                {
                    p.Value = DBNull.Value;
                }
                cmd.Parameters.Add(p);
            }
        }
    }
}
