using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using REGIS_DATOS.Conections;
using REGIS_DATOS.Models;
using REGIS_NEG.UtilidadServicio;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace REGIS_NEG.Login_Neg
{
    public class NLogin
    {
        #region Variables & Objetos
        ConectionDB objConexion = new ConectionDB();
        Users objUser = new Users();
        Util objUtilidad = new Util();
        Session objSession = new Session();
        Role objRol = new Role();
        #endregion

        public Users ValidateLogin(string correo, string clave)
        {
            //Abre la conexion a BD
            SqlConnection sqlConnection = objConexion.GetConnected();

            //Define obj SQL a ejecutar
            SqlCommand command = new SqlCommand("sp_ValidateLogin", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;

            // Parámetros
            command.Parameters.AddWithValue("@correo", correo);
            command.Parameters.AddWithValue("@clave", clave);

            using (SqlDataReader dr = command.ExecuteReader())
            {
                if (dr.Read())
                {
                    objUser = new Users()
                    {
                        IdUsers = dr["IdUsers"]?.ToString(),
                        Email = dr["Email"]?.ToString(),
                        IdRole = dr["IdRole"]?.ToString(),
                        RegistrationDate = (DateTime)dr["RegistrationDate"],
                        LastAccessed = (DateTime)dr["LastAccessed"],
                        IdRegional = dr["IdRegional"]?.ToString(),
                        Confirmed = (bool)dr["Confirmed"],
                        Reinstate = (bool)dr["Reinstate"],
                        IdPerson = dr["IdPerson"]?.ToString(),
                        isActive = (bool)dr["isActive"],
                        Token = objUtilidad.GenerarToken()
                    };
                }
            }

            objConexion.CloseConnected();

            objConexion.GetConnected();
            //Define obj SQL a ejecutar
            SqlCommand cmdUp = new SqlCommand("sp_LogLastAccess", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;

            // Parámetros
            command.Parameters.AddWithValue("@ID", objUser.IdUsers);
            try
            {
                int filasAfectadas = cmdUp.ExecuteNonQuery();//Ejecuta el Qeury
            }
            catch (Exception)
            {
                objConexion.CloseConnected();
            }
            
            objConexion.CloseConnected();

            //Registra el Login en la tabla
            LogIn(objUser);
            return objUser;
        }

        /// <summary>
        /// Registra el Login del usuario
        /// </summary>
        /// <param name="usuario"></param>
        public void LogIn(Users usuario)
        {
            //Abre la conexion a BD
            SqlConnection sqlConnection = objConexion.GetConnected();

            //Define obj SQL a ejecutar
            SqlCommand command = new SqlCommand("sp_LogIn", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;

            // Parámetros
            command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsers);
            command.Parameters.AddWithValue("@Token", usuario.Token);

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

        /// <summary>
        /// Registra Cierre de Session
        /// </summary>
        /// <param name="usuario"></param>
        public void LogOff(Users usuario)
        {
            //Abre la conexion a BD
            SqlConnection sqlConnection = objConexion.GetConnected();

            //Define obj SQL a ejecutar
            SqlCommand command = new SqlCommand("sp_LogOff", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;

            // Parámetros
            command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsers);

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

        /// <summary>
        /// Consulta Session por usuario
        /// </summary>
        /// <param name="IdUser"></param>
        /// <returns></returns>
        public Session GetSessionUser(string IdUser)
        {
            //Abre la conexion a BD
            SqlConnection sqlConnection = objConexion.GetConnected();

            //Define obj SQL a ejecutar
            SqlCommand command = new SqlCommand("sp_GetSessionUser", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;

            // Parámetros
            command.Parameters.AddWithValue("@IdUser", IdUser);

            try
            {
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        objSession = new Session()
                        {
                            IdSession = dr["IdSession"]?.ToString(),
                            IdUsers = dr["IdUsers"]?.ToString(),
                            Token = dr["Token"]?.ToString(),
                            StartDate = DateTime.Parse(dr["StartDate"].ToString()),
                            EndDate = DateTime.Parse(dr["EndDate"].ToString())
                        };
                    }
                }
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
            return objSession;
        }

        /// <summary>
        /// Registra una nueva session
        /// </summary>
        /// <param name="sesion"></param>
        /// <returns></returns>
        public void InsertNewSession(Session sesion)
        {
            using (SqlConnection sqlConnection = objConexion.GetConnected())
            {
                SqlCommand command = new SqlCommand("sp_InsertNewSession", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@IdUsers", sesion.IdUsers);
                command.Parameters.AddWithValue("@Token", sesion.Token);
                command.Parameters.AddWithValue("@StartDate", sesion.StartDate);
                command.Parameters.AddWithValue("@EndDate", null);

                sqlConnection.Open();
                command.ExecuteNonQuery();
            }
            objConexion.CloseConnected(); 
        }

        /// <summary>
        /// Actualiza Session
        /// </summary>
        /// <param name="sesion"></param>
        public void UpdateSession(Session sesion)
        {
            using (SqlConnection sqlConnection = objConexion.GetConnected())
            {
                SqlCommand command = new SqlCommand("sp_UpdateSession", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@IdUsers", sesion.IdUsers);
                command.Parameters.AddWithValue("@Token", sesion.Token);
                command.Parameters.AddWithValue("@StartDate", sesion.StartDate);
                command.Parameters.AddWithValue("@EndDate", sesion.EndDate);

                sqlConnection.Open();
                command.ExecuteNonQuery();
            }
            objConexion.CloseConnected();
        }

        /// <summary>
        /// Desactiva usuario creada
        /// </summary>
        /// <param name="user"></param>
        public void DesactivateUserById(string IdUser)
        {
            using (SqlConnection sqlConnection = objConexion.GetConnected())
            {
                SqlCommand command = new SqlCommand("sp_DesactivateUser", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@IdUser", IdUser);

                sqlConnection.Open();
                command.ExecuteNonQuery();
            }
            objConexion.CloseConnected();
        }
    }
}