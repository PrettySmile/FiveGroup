using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using FiveGroup.Models;
using System.Configuration;

namespace FiveGroup.Controllers
{
    public class IngrediantsAPIController : ApiController
    {
        Project2Entities db = new Project2Entities();
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        SqlCommand Cmd = new SqlCommand();
        SqlDataAdapter adp = new SqlDataAdapter();
        private void ExecuteSql(string sql)
        {
            Cmd.CommandText = sql;
            Cmd.Connection = Conn;

            Conn.Open();
            Cmd.ExecuteNonQuery();
            Conn.Close();
        }
        private DataTable QuerySql(string sql)
        {

            Cmd.CommandText = sql;
            Cmd.Connection = Conn;
            adp.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            adp.Fill(ds);

            return ds.Tables[0];
        }
        public IEnumerable<ingrediant> GET()
        {
            return db.ingrediant.ToList();
        }
        public DataTable GET(string ing_name)
        {
            var sql = @"SELECT A.* 
                       FROM ingrediant as A";
            if (ing_name != null)
            {
                sql += " WHERE ";
                if (ing_name != null)
                {
                    sql += "A.ing_name  ='" + ing_name + "'";
                }
            }
            DataTable sc = QuerySql(sql);

            return sc;
        }
    }
}