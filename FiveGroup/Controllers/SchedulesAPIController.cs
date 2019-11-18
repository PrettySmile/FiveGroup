using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FiveGroup.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
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

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}