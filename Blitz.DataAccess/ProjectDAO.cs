using Blitz.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Blitz.Common;
using System.Collections;
using System.Data.Common;
using System.Data.SqlTypes;

namespace Blitz.DataAccess
{
    public class ProjectDAO : IProjectDAO
    {  
        //Get Project By Id
        Project IProjectDAO.GetProjectById(int Id)
        {
            SqlDataReader reader = null;
            string query = "Select * from Project where Id = " + Id + " ";
            using (SqlConnection ConnectionString = DBCommon.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = ConnectionString;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select * from Project where Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", Id);
                    ConnectionString.Open();
                    reader = cmd.ExecuteReader();
                    Project emp = null;

                    while (reader.Read())
                    {
                        emp = new Project();
                        emp.Id = Convert.ToInt32(reader.GetValue(0));
                        emp.Name = reader.GetValue(1).ToString();
                        emp.StartDate = Convert.ToDateTime(reader.GetValue(2));
                    }
                    return emp;
                }
            }
        }
        
        //Get All Projects
        IEnumerable<Project> IProjectDAO.GetProjects()
             {
              List<Project> p1 = new List<Project>();
              string query = "Select * from Project";
              using (SqlConnection con = DBCommon.GetConnection())
              {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            p1.Add(new Project
                            {
                                Id = Convert.ToInt32(sdr["Id"]),
                                Name = Convert.ToString(sdr["Name"]),
                                StartDate = Convert.ToDateTime(sdr["StartDate"])
                            });
                        }
                    }
                    con.Close();
                }
              }

                if (p1.Count == 0)
                {
                    p1.Add(new Project());
                }
                return p1;
            }

        //Add Projects
        public int AddProject(Project p)
        {
            using (SqlConnection myconnection = DBCommon.GetConnection())
            {
                SqlCommand sqlcmd = new SqlCommand("insert into project (id,name,startdate) values (@id,@name,@startdate)", myconnection);
                sqlcmd.CommandType = CommandType.Text;
                sqlcmd.Connection = myconnection;
                sqlcmd.Parameters.AddWithValue("@id", p.Id);
                sqlcmd.Parameters.AddWithValue("@name", p.Name);
                sqlcmd.Parameters.AddWithValue("@startdate", p.StartDate);
                myconnection.Open();
                int rowinserted = sqlcmd.ExecuteNonQuery();
                return rowinserted;
            }

        }

        //Delete Projects by id
        int IProjectDAO.DeleteProject(int Id)
        {
            using (SqlConnection myConnection = DBCommon.GetConnection())
            {

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "delete from Project where Id=@Id";
                sqlCmd.Parameters.AddWithValue("@Id", Id);
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                int rowDeleted = sqlCmd.ExecuteNonQuery();
                return rowDeleted;
                //myConnection.Close();
            }
            
        }

        //Update Project
        int IProjectDAO.UpDateProject(Project p)
        {
            using (SqlConnection myConnection = DBCommon.GetConnection())
            {
                string sqlText = "update Project set Name=@Name where Id=@Id ";
                SqlCommand sqlCmd = new SqlCommand(sqlText, myConnection);
                sqlCmd.Parameters.AddWithValue("@Id", p.Id);
                sqlCmd.Parameters.AddWithValue("@Name", p.Name);
                //sqlCmd.Parameters.AddWithValue("@StartDat", p.StartDate);
                myConnection.Open();
                int i = sqlCmd.ExecuteNonQuery();
                //myConnection.Close();
                return i;
            }
        }
       
    }
}
