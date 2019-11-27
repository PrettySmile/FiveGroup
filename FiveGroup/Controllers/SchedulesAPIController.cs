using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using FiveGroup.Models;
using System.Configuration;

namespace FiveGroup.Controllers
{
    public class SchedulesAPIController : ApiController
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
            var sql = @"SELECT A.*,
                   B.doc_name,C.dep_name,D.hos_name
            FROM schedule AS A
            LEFT JOIN doctor AS B ON B.doc_id=A.doc_id
            LEFT JOIN department AS C ON C.dep_id=A.dep_id
            LEFT JOIN hospital AS D ON D.hos_id=A.hos_id";
            DataTable sc = QuerySql(sql);

            return sc;
        }

        
        public DataTable Get(string doc_name, string dep_name, string hos_name, string date)
        {
            var sql = @"SELECT A.*,
                   B.doc_name,C.dep_name,D.hos_name
            FROM schedule AS A
            LEFT JOIN doctor AS B ON B.doc_id=A.doc_id
            LEFT JOIN department AS C ON C.dep_id=A.dep_id
            LEFT JOIN hospital AS D ON D.hos_id=A.hos_id";
            if (doc_name != null || dep_name != null || hos_name != null || date != null)
            {
                sql += " WHERE ";
                if (doc_name != null)
                {
                    sql += " B.doc_name like '%" + doc_name + "%'";
                    if (dep_name != null || hos_name != null || date != null)
                        sql += " and ";
                }
                if (dep_name != null)
                {
                    sql += " C.dep_name like '%" + dep_name + "%'";
                    if (hos_name != null || date != null)
                        sql += " and ";
                }
                if (hos_name != null)
                {
                    sql += " D.hos_name like '%" + hos_name + "%'";
                    if (date != null)
                        sql += " and ";
                }
                if (date != null)
                {
                    sql += " A.starttime <= CONVERT(datetime,'" + date + "')" +
                         " and A.endtime >= CONVERT(datetime,'" + date + "')";
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