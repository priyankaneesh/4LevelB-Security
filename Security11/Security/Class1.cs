using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Security
{
    class Class1
    {
        public SqlConnection con;
        public SqlCommand cmd;

        public SqlConnection connect()
        {
            con = new SqlConnection("Data Source=priyanka;Initial Catalog=FourLevelBSecurity;Integrated Security=True;TrustServerCertificate=True;");
            con.Open();
            return con;
        }

    }
}
