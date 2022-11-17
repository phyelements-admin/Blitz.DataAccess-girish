using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Blitz.Common
{
    
    public  class DBCommon
    {
        const string Cn = @"Server=DESKTOP-DBCGGSG;Database=Projectdatabase;User ID=sa;Password=sa_123;";
        public  static SqlConnection GetConnection()
        {
            return  new SqlConnection(Cn);
        }

        public static int ExecuteNonQuerySql(string cmdText,
            List<SqlParameter> parameters = null,
            CommandType commandType = CommandType.Text)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(Cn))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(cmdText, connection);
                command.CommandType = commandType;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters.ToArray());
                }

                result = command.ExecuteNonQuery();

                if (command.Parameters.Contains("@successStatus"))
                {
                    var status = command.Parameters["@successStatus"].Value.ToString();
                    return Convert.ToInt32(status.ToLower() == "true" ? 1 : 0);
                }
            }
            return result;
        }

        public static DataTable GetResultDataTableBySql(string cmdText,
                                                List<SqlParameter> parameters = null,
                                                CommandType commandType = CommandType.Text)
        {
            DataTable result = new DataTable();
            using (SqlConnection connection = new SqlConnection(Cn))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(cmdText, connection);
                command.CommandType = commandType;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters.ToArray());
                }

                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                result.Load(reader);
                foreach (DataColumn column in result.Columns) column.ReadOnly = false;
            }
            return result;
        }


    }
}
