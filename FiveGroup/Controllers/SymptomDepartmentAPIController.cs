using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using FiveGroup.Models;
using System.Configuration;

namespace FiveGroup.Controllers
{
    public class SymptomDepartmentAPIController : ApiController
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
        public DataTable GET()
        {
            var sql = @"SELECT 
                   D.part_name,C.sym_name,B.dep_name
            FROM dep_sym_ref AS A
            LEFT JOIN department AS B ON B.dep_id=A.dep_id
            LEFT JOIN symptom AS C ON C.sym_id=A.sym_id
            LEFT JOIN bodypart AS D ON D.part_id=A.part_id";
            DataTable sc = QuerySql(sql);

            return sc;
        }

        
        public DataTable Get(string part_name, string sym_name)
        {
            var sql = @"SELECT 
                   D.part_name,C.sym_name,B.dep_name
            FROM dep_sym_ref AS A
            LEFT JOIN department AS B ON B.dep_id=A.dep_id
            LEFT JOIN symptom AS C ON C.sym_id=A.sym_id
            LEFT JOIN bodypart AS D ON D.part_id=A.part_id";
            if (part_name != null || sym_name != null)
            {
                sql += " WHERE ";
                if (part_name != null)
                {
                    sql += " D.part_name like '%" + part_name + "%'";
                    if (sym_name != null)
                        sql += " and ";
                }
                if (sym_name != null)
                {
                    sql += " C.sym_name like '%" + sym_name + "%'";
                }
            }

            DataTable sc = QuerySql(sql);
            if (sc == null)
            {
                return null;
            }
            else
            {
                return sc;
            }
        }
    }
}