using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blitz.Models;

namespace Blitz.DataAccess
{
    public interface IProjectDAO
    {
        List<Project> GetProjects();
        Project GetProjectById(int Id);
        int AddProject(Project p);
        int UpDateProject(Project p);
        int DeleteProject(int Id);

    }
}
