using System;
using System.Collections.Generic;
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
        
    }
}
