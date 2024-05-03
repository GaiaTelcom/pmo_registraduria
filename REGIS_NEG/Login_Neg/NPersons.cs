using REGIS_DATOS.Conections;
using REGIS_NEG.UtilidadServicio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REGIS_DATOS.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace REGIS_NEG.Login_Neg
{
    public class NPersons
    {
        #region Variables & Objetos
        ConectionDB objConexion = new ConectionDB();
        Persons objPersona = new Persons();
        Util objUtilidad = new Util();
        #endregion

        /// <summary>
        /// Consulta info persona X Id
        /// </summary>
        /// <param name="IdCompany"></param>
        /// <returns></returns>
        public Persons GetPersonById(string IdPersona)
        {
            //Abre la conexion a BD
            SqlConnection sqlConnection = objConexion.GetConnected();

            //Define obj SQL a ejecutar
            SqlCommand command = new SqlCommand("sp_GetPersonById", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;

            // Parámetros
            command.Parameters.AddWithValue("@ID", IdPersona);

            using (SqlDataReader dr = command.ExecuteReader())
            {
                if (dr.Read())
                {
                    objPersona = new Persons()
                    {
                        IdPersons = dr["IdPersons"]?.ToString(),
                        IdPersonType = dr["IdPersonType"]?.ToString(),
                        IdIdentificationType = dr["IdIdentificationType"]?.ToString(),
                        IdentificationNumber = dr["IdentificationNumber"]?.ToString(),
                        Name = dr["Name"]?.ToString(),
                        LastName = dr["LastName"]?.ToString(),
                        Phone = dr["Phone"]?.ToString(),
                        Email = dr["Email"]?.ToString(),
                        Gender = dr["Gender"]?.ToString(),
                        IdCountry = dr["IdCountry"]?.ToString(),
                        IdDepartment = dr["IdDepartment"]?.ToString(),
                        IdCity = dr["IdCity"]?.ToString(),
                        IdRegional = dr["IdRegional"]?.ToString(),
                        HabeasData = (bool)dr["HabeasData"],
                        IdStatus = dr["IdStatus"]?.ToString(),
                        isActive = (bool)dr["isActive"]
                    };
                }
            }

            objConexion.CloseConnected();
            return objPersona;
        }

        /// <summary>
        /// Consulta info persona x Numero Identificacion
        /// </summary>
        /// <param name="IdentificationNumer"></param>
        /// <returns></returns>
        public Persons GetPersonByDocument(int IdentificationNumer)
        {
            //Abre la conexion a BD
            SqlConnection sqlConnection = objConexion.GetConnected();

            //Define obj SQL a ejecutar
            SqlCommand command = new SqlCommand("sp_GetPersonByDocument", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;

            // Parámetros
            command.Parameters.AddWithValue("@IdentificationNumber", IdentificationNumer);

            using (SqlDataReader dr = command.ExecuteReader())
            {
                if (dr.Read())
                {
                    objPersona = new Persons()
                    {
                        IdPersons = dr["IdPersons"]?.ToString(),
                        IdPersonType = dr["IdPersonType"]?.ToString(),
                        IdIdentificationType = dr["IdIdentificationType"]?.ToString(),
                        IdentificationNumber = dr["IdentificationNumber"]?.ToString(),
                        Name = dr["Name"]?.ToString(),
                        LastName = dr["LastName"]?.ToString(),
                        Phone = dr["Phone"]?.ToString(),
                        Email = dr["Email"]?.ToString(),
                        Gender = dr["Gender"]?.ToString(),
                        IdCountry = dr["IdCountry"]?.ToString(),
                        IdDepartment = dr["IdDepartment"]?.ToString(),
                        IdCity = dr["IdCity"]?.ToString(),
                        IdRegional = dr["IdRegional"]?.ToString(),
                        HabeasData = (bool)dr["HabeasData"],
                        IdStatus = dr["IdStatus"]?.ToString(),
                        isActive = (bool)dr["isActive"]
                    };
                }
            }

            objConexion.CloseConnected();
            return objPersona;
        }

        /// <summary>
        /// Devuelve todas las personas registradas en una lista
        /// </summary>
        /// <returns></returns>
        public List<Persons> GetListPersons()
        {
            List<Persons> personsList = new List<Persons>();

            using (SqlConnection sqlConnection = objConexion.GetConnected())
            {
                SqlCommand command = new SqlCommand("sp_GetListPerson", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Persons objPersona = new Persons()
                        {
                            IdPersons = dr["IdPersons"]?.ToString(),
                            IdPersonType = dr["IdPersonType"]?.ToString(),
                            IdIdentificationType = dr["IdIdentificationType"]?.ToString(),
                            IdentificationNumber = dr["IdentificationNumber"]?.ToString(),
                            Name = dr["Name"]?.ToString(),
                            LastName = dr["LastName"]?.ToString(),
                            Phone = dr["Phone"]?.ToString(),
                            Email = dr["Email"]?.ToString(),
                            Gender = dr["Gender"]?.ToString(),
                            IdCountry = dr["IdCountry"]?.ToString(),
                            IdDepartment = dr["IdDepartment"]?.ToString(),
                            IdCity = dr["IdCity"]?.ToString(),
                            IdRegional = dr["IdRegional"]?.ToString(),
                            HabeasData = (bool)dr["HabeasData"],
                            IdStatus = dr["IdStatus"]?.ToString(),
                            isActive = (bool)dr["isActive"]
                        };
                        if (objPersona.isActive == true)
                        {
                            personsList.Add(objPersona);
                        }
                    }
                }
            }
            return personsList;
        }

        /// <summary>
        /// Registrar nueva persona
        /// </summary>
        /// <param name="persona"></param>
        public void RegistryNewPerson(Persons persona)
        {
            using (SqlConnection sqlConnection = objConexion.GetConnected())
            {
                SqlCommand command = new SqlCommand("sp_RegistryNewPerson", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@IdPersonType", persona.IdPersonType);
                command.Parameters.AddWithValue("@IdIdentificationType", persona.IdIdentificationType);
                command.Parameters.AddWithValue("@IdentificationNumber", persona.IdentificationNumber);
                command.Parameters.AddWithValue("@Name", persona.Name);
                command.Parameters.AddWithValue("@LastName", persona.LastName);
                command.Parameters.AddWithValue("@Phone", persona.Phone);
                command.Parameters.AddWithValue("@Email", persona.Email);
                command.Parameters.AddWithValue("@Gender", persona.Gender);
                command.Parameters.AddWithValue("@MaritalStatus", persona.MaritalStatus);
                command.Parameters.AddWithValue("@IdCountry", persona.IdCountry);
                command.Parameters.AddWithValue("@IdDepartment", persona.IdDepartment);
                command.Parameters.AddWithValue("@IdCity", persona.IdCity);
                command.Parameters.AddWithValue("@IdRegional", persona.IdRegional);
                command.Parameters.AddWithValue("@HabeasData", persona.HabeasData);
                command.Parameters.AddWithValue("@IdStatus", persona.IdStatus);
                command.Parameters.AddWithValue("@isActive", 1);

                sqlConnection.Open();
                command.ExecuteNonQuery();
            }
            objConexion.CloseConnected();
        }

        /// <summary>
        /// Desactiva persona creada
        /// </summary>
        /// <param name="persona"></param>
        public void DesactivatePersonById(Persons persona)
        {
            using (SqlConnection sqlConnection = objConexion.GetConnected())
            {
                SqlCommand command = new SqlCommand("sp_DeactivatePerson", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@IdPerson", persona.IdPersons);

                sqlConnection.Open();
                command.ExecuteNonQuery();
            }
            objConexion.CloseConnected();
        }

        /// <summary>
        /// Actualiza datos de persona
        /// </summary>
        /// <param name="persona"></param>
        public void UpdatePersonById(Persons persona)
        {
            //Abre la conexion a BD
            SqlConnection sqlConnection = objConexion.GetConnected();

            //Define obj SQL a ejecutar
            SqlCommand command = new SqlCommand("sp_UpdatePersonById", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;

            // Parámetros
            command.Parameters.AddWithValue("@IdPersons", persona.IdPersons);
            command.Parameters.AddWithValue("@IdPersonType", persona.IdPersonType);
            command.Parameters.AddWithValue("@IdIdentificationType", persona.IdIdentificationType);
            command.Parameters.AddWithValue("@IdentificationNumber", persona.IdentificationNumber);
            command.Parameters.AddWithValue("@Name", persona.Name);
            command.Parameters.AddWithValue("@LastName", persona.LastName);
            command.Parameters.AddWithValue("@Phone", persona.Phone);
            command.Parameters.AddWithValue("@Email", persona.Email);
            command.Parameters.AddWithValue("@Gender", persona.Gender);
            command.Parameters.AddWithValue("@MaritalStatus", persona.MaritalStatus);
            command.Parameters.AddWithValue("@IdCountry", persona.IdCountry);
            command.Parameters.AddWithValue("@IdDepartment", persona.IdDepartment);
            command.Parameters.AddWithValue("@IdCity", persona.IdCity);
            command.Parameters.AddWithValue("@IdRegional", persona.IdRegional);
            command.Parameters.AddWithValue("@HabeasData", persona.HabeasData);
            command.Parameters.AddWithValue("@IdStatus", persona.IdStatus);
            command.Parameters.AddWithValue("@isActive", persona.isActive);

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
