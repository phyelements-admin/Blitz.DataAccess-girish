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
        List<Project> IProjectDAO.GetProjects()
             {
            
            DataTable dataTable= DBCommon.GetResultDataTableBySql("select * from Project");
            List<Project> project = new List<Project>();
            int i = 0;
            
                while (i < dataTable.Rows.Count)
                {
                project.Add(new Project
                {
                    Id = Convert.ToInt32(dataTable.Rows[i]["ID"]),
                    Name = Convert.ToString(dataTable.Rows[i]["Name"]),
                    StartDate = Convert.ToDateTime(dataTable.Rows[i]["StartDate"])
                    

                }) ;
                    i++;
                }
            Console.WriteLine("hi");
            if (project.Count == 0)
            {
                project.Add(new Project());
            }

            return project;
            }

        //Add Projects
        public int AddProject(Project p)
        {
            return DBCommon.ExecuteNonQuerySql("insert into project (id,name,startdate) values (10,'Harish','2022-10-25 00:00:00.000')");
             
        }

        //Delete Projects by id
        int IProjectDAO.DeleteProject(int Id)
        {
          
           return DBCommon.ExecuteNonQuerySql("DELETE FROM Project WHERE Id='3'");
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
