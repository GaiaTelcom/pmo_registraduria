using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REGIS_DATOS.Conections;
using REGIS_DATOS.Models;
using REGIS_NEG.UtilidadServicio;

namespace REGIS_NEG
{
    public class NProject
    {
        #region Variables & Objetos
        ConectionDB objConexion = new ConectionDB();
        Project objProject = new Project();
        Util objUtilidad = new Util();
        #endregion

        /// <summary>
        /// Consulta info Proyecto X Id
        /// </summary>
        /// <param name="IdCompany"></param>
        /// <returns></returns>
        public Project GetProjectyById(string IdProject)
        {
            //Abre la conexion a BD
            SqlConnection sqlConnection = objConexion.GetConnected();

            //Define obj SQL a ejecutar
            SqlCommand command = new SqlCommand("sp_GetProjectById", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;

            // Parámetros
            command.Parameters.AddWithValue("@ID", IdProject);

            using (SqlDataReader dr = command.ExecuteReader())
            {
                if (dr.Read())
                {
                    objProject = new Project()
                    {
                        IdProject = dr["IdProject"]?.ToString(),
                        Name = dr["Name"]?.ToString(),
                        Description = dr["Description"]?.ToString(),
                        StarDate = DateTime.Parse(dr["StarDate"]?.ToString()),
                        EndDate = DateTime.Parse(dr["EndDate"]?.ToString()),
                        IdStatusProject = dr["IdStatusProject"]?.ToString(),
                        Comments = dr["Comments"]?.ToString(),
                        IdRegional = dr["IdRegional"]?.ToString(),
                        IdCompany = dr["IdCountry"]?.ToString(),
                        isActive = (bool)dr["isActive"]
                    };
                }
            }
            objConexion.CloseConnected();
            return objProject;
        }

        /// <summary>
        /// Consulta info proyecto x NumeroIdentificacion
        /// </summary>
        /// <param name="NameProject"></param>
        /// <returns></returns>
        public Project GetProjectByName(string NameProject)
        {
            //Abre la conexion a BD
            SqlConnection sqlConnection = objConexion.GetConnected();

            //Define obj SQL a ejecutar
            SqlCommand command = new SqlCommand("sp_GetProjectByName", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;

            // Parámetros
            command.Parameters.AddWithValue("@Name", NameProject);

            using (SqlDataReader dr = command.ExecuteReader())
            {
                if (dr.Read())
                {
                    objProject = new Project()
                    {
                        IdProject = dr["IdProject"]?.ToString(),
                        Name = dr["Name"]?.ToString(),
                        Description = dr["Description"]?.ToString(),
                        StarDate = DateTime.Parse(dr["StarDate"]?.ToString()),
                        EndDate = DateTime.Parse(dr["EndDate"]?.ToString()),
                        IdStatusProject = dr["IdStatusProject"]?.ToString(),
                        Comments = dr["Comments"]?.ToString(),
                        IdRegional = dr["IdRegional"]?.ToString(),
                        IdCompany = dr["IdCountry"]?.ToString(),
                        isActive = (bool)dr["isActive"]
                    };
                }
            }
            objConexion.CloseConnected();
            return objProject;
        }

        /// <summary>
        /// Devuelve todas las compañias registradas en una lista
        /// </summary>
        /// <returns></returns>
        public List<Project> GetListProject()
        {
            List<Project> projectList = new List<Project>();

            using (SqlConnection sqlConnection = objConexion.GetConnected())
            {
                SqlCommand command = new SqlCommand("sp_GetListCompany", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Project proj = new Project()
                        {
                            IdProject = dr["IdProject"]?.ToString(),
                            Name = dr["Name"]?.ToString(),
                            Description = dr["Description"]?.ToString(),
                            StarDate = DateTime.Parse(dr["StarDate"]?.ToString()),
                            EndDate = DateTime.Parse(dr["EndDate"]?.ToString()),
                            IdStatusProject = dr["IdStatusProject"]?.ToString(),
                            Comments = dr["Comments"]?.ToString(),
                            IdRegional = dr["IdRegional"]?.ToString(),
                            IdCompany = dr["IdCountry"]?.ToString(),
                            isActive = (bool)dr["isActive"]
                        };
                        if (proj.isActive == true)
                        {
                            projectList.Add(proj);
                        }
                    }
                }
            }
            return projectList;
        }

        /// <summary>
        /// Registrar nuevo proyecto
        /// </summary>
        /// <param name="project"></param>
        public void RegistryNewProject(Project project)
        {
            using (SqlConnection sqlConnection = objConexion.GetConnected())
            {
                SqlCommand command = new SqlCommand("sp_RegistryNewProject", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                //command.Parameters.AddWithValue("@IdProject", project.IdProject);
                command.Parameters.AddWithValue("@Name", project.Name);
                command.Parameters.AddWithValue("@Description", project.Description);
                command.Parameters.AddWithValue("@StarDate", project.StarDate);
                command.Parameters.AddWithValue("@EndDate", project.EndDate);
                command.Parameters.AddWithValue("@IdStatusProject", project.IdStatusProject);
                command.Parameters.AddWithValue("@Comments", project.Comments);
                command.Parameters.AddWithValue("@IdRegional", project.IdRegional);
                command.Parameters.AddWithValue("@IdCompany", project.IdCompany);
                command.Parameters.AddWithValue("@isActive", project.isActive);

                sqlConnection.Open();
                command.ExecuteNonQuery();
            }
            objConexion.CloseConnected();
        }

        /// <summary>
        /// Desactiva proyecto
        /// </summary>
        /// <param name="proyecto"></param>
        public void DesactivateCompanyById(Project project)
        {
            using (SqlConnection sqlConnection = objConexion.GetConnected())
            {
                SqlCommand command = new SqlCommand("sp_DesactivateProject", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@IdProject", project.IdProject);

                sqlConnection.Open();
                command.ExecuteNonQuery();
            }
            objConexion.CloseConnected();
        }

        /// <summary>
        /// Actualiza datos del proyecto
        /// </summary>
        /// <param name="project"></param>
        public void UpdateCompanyById(Project project)
        {
            //Abre la conexion a BD
            SqlConnection sqlConnection = objConexion.GetConnected();

            //Define obj SQL a ejecutar
            SqlCommand command = new SqlCommand("sp_UpdateProjectById", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;

            // Parámetros
            command.Parameters.AddWithValue("@IdProject", project.IdProject);
            command.Parameters.AddWithValue("@Name", project.Name);
            command.Parameters.AddWithValue("@Description", project.Description);
            command.Parameters.AddWithValue("@StarDate", project.StarDate);
            command.Parameters.AddWithValue("@EndDate", project.EndDate);
            command.Parameters.AddWithValue("@IdStatusProject", project.IdStatusProject);
            command.Parameters.AddWithValue("@Comments", project.Comments);
            command.Parameters.AddWithValue("@IdRegional", project.IdRegional);
            command.Parameters.AddWithValue("@IdCompany", project.IdCompany);

            try
            {
                command.ExecuteNonQuery();//ejecuta query
                objConexion.CloseConnected();//cierra conexión
            }
            catch (SqlException ex)
            {
                objConexion.CloseConnected();
            }
            finally
            {
                objConexion.CloseConnected();//cierra conexión 
            }
            objConexion.CloseConnected();
        }
    }
}
