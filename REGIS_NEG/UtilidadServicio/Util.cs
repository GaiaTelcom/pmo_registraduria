using REGIS_DATOS.Conections;
using REGIS_DATOS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace REGIS_NEG.UtilidadServicio
{
    public class Util
    {
        #region Variables & Objetos
        ConectionDB objConexion = new ConectionDB();
        #endregion

        /// <summary>
        /// Encripta el texto ingresado
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public string ConvertirHA256(string texto)
        {
            string hash = string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                //Obtener el hash del texto recibido
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));

                //Convertir el array nyte en cadena de texto
                foreach (byte b in hashValue)
                    hash += $"{b:X2}";
            }
            return hash;
        }

        /// <summary>
        /// Genera el token de ingreso a la aplicación
        /// </summary>
        /// <returns></returns>
        public string  GenerarToken()
        {
            string token = Guid.NewGuid().ToString("N");
            return token;
        }

        /// <summary>
        /// Registro de actividad por usuario
        /// </summary>
        /// <param name="log"></param>
        public void RegistryLog(Log log)
        {
            using (SqlConnection connection = new SqlConnection(""))
            {
                //Define obj SQL a ejecutar
                SqlCommand command = new SqlCommand("sp_RegistrarLOG", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Parámetros
                command.Parameters.AddWithValue("@IdUsers", log.IdUsers);
                command.Parameters.AddWithValue("@Sitio", log.Url);
                command.Parameters.AddWithValue("@Accion", log.Accion);

                try
                {
                    objConexion.GetConnected();//abre conexión
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
            }
        }

        /// <summary>
        /// Devuelve los tipos de personas disponibles
        /// </summary>
        /// <returns></returns>
        public List<PersonType> GetTypePerson()
        {
            PersonType objTypePerson = new PersonType();
            List<PersonType> listTypePerson = new List<PersonType>();
            using (SqlConnection sqlConnection = objConexion.GetConnected())
            {
                SqlCommand command = new SqlCommand("sp_GetListTypePerson", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        objTypePerson = new PersonType()
                        {
                            IdPersonType = dr["IdPersonType"]?.ToString(),
                            Title = dr["Title"]?.ToString(),
                            Description = dr["Description"]?.ToString(),
                            isActive = (bool)dr["isActive"]
                        };
                        if (objTypePerson.isActive == true)
                        {
                            listTypePerson.Add(objTypePerson);
                        }
                    }
                }
            }
            return listTypePerson;
        }
    }
}