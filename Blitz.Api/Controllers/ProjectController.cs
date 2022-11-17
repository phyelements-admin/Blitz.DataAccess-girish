﻿using Blitz.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Blitz.DataAccess;
using Newtonsoft.Json;
using System.Data.Common;

namespace Blitz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
       // const string Cn = @"Server=DESKTOP-DBCGGSG;Database=Projectdatabase;User ID=sa;Password=sa_123;";
        
        IProjectDAO projectDAO = new ProjectDAO();
        
        [Route("getall")]
        [HttpGet]
        public List<Project> GetAll()
        {
            return projectDAO.GetProjects();

        }

        [Route("get/{id}")]
        [HttpGet]
        public Project Get(int id)
        {

            return projectDAO.GetProjectById(id);
        }

        [Route("addproject")]
        [HttpPost]
        public void AddProject1(Project p) 
        {
            projectDAO.AddProject(p);
            //ProjectDAO.AddProject(p);
        }

        [Route("deleteprojects")]
        [HttpDelete]
        public void DeleteProjects(int id) 
        {
           projectDAO.DeleteProject(id);

        }

        [Route("updateprojects")]
        [HttpPut]
        public void PutProject(Project p)
        {

            projectDAO.UpDateProject(p);
        }

       
    }

}
