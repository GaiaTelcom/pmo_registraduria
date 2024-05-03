using REGIS_DATOS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REGIS_NEG.UtilidadServicio;
using REGIS_DATOS.Conections;
using REGIS_NEG.Login_Neg;

namespace REGIS_NEG
{
    public class NCompany
    {
        #region Variables & Objetos
        ConectionDB objConexion = new ConectionDB();
        Company objcompany = new Company();
        Util objUtilidad = new Util();
        #endregion

        /// <summary>
        /// Consulta info compañia X Id
        /// </summary>
        /// <param name="IdCompany"></param>
        /// <returns></returns>
        public Company GetCompanyById(string IdCompany)
        {
            //Abre la conexion a BD
            SqlConnection sqlConnection = objConexion.GetConnected();

            //Define obj SQL a ejecutar
            SqlCommand command = new SqlCommand("sp_GetCompanyById", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;

            // Parámetros
            command.Parameters.AddWithValue("@ID", IdCompany);

            using (SqlDataReader dr = command.ExecuteReader())
            {
                if (dr.Read())
                {
                    objcompany = new Company()
                    {
                        IdCompany = dr["IdCompany"]?.ToString(),
                        IdIdentificationType = dr["IdIdentificationType"]?.ToString(),
                        IdentificationNumber = dr["IdentificationNumber"]?.ToString(),
                        CompanyName = dr["CompanyName"]?.ToString(),
                        AddressStreet = dr["AddressStreet"]?.ToString(),
                        PhoneNumber = int.Parse(dr["PhoneNumber"]?.ToString()),
                        Url = dr["Url"]?.ToString(),
                        DateRegistry = DateTime.Parse(dr["DateRegistry"]?.ToString()),
                        IdCountry = dr["IdCountry"]?.ToString(),
                        IdDepartament = dr["IdDepartament"]?.ToString(),
                        IdCity = dr["IdCity"]?.ToString(),
                        IdRegional = dr["IdRegional"]?.ToString(),
                        isActive = (bool)dr["isActive"]
                    };
                }
            }

            objConexion.CloseConnected();
            return objcompany;
        }

        /// <summary>
        /// Consulta info compañia x Numero Identificacion
        /// </summary>
        /// <param name="IdentificationNumer"></param>
        /// <returns></returns>
        public Company GetCommpanyByDocument(string IdentificationNumer)
        {
            //Abre la conexion a BD
            SqlConnection sqlConnection = objConexion.GetConnected();

            //Define obj SQL a ejecutar
            SqlCommand command = new SqlCommand("sp_GetCompanyByDocument", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;

            // Parámetros
            command.Parameters.AddWithValue("@IdentificationNumber", IdentificationNumer);

            using (SqlDataReader dr = command.ExecuteReader())
            {
                if (dr.Read())
                {
                    objcompany = new Company()
                    {
                        IdCompany = dr["IdCompany"]?.ToString(),
                        IdIdentificationType = dr["IdIdentificationType"]?.ToString(),
                        IdentificationNumber = dr["IdentificationNumber"]?.ToString(),
                        CompanyName = dr["CompanyName"]?.ToString(),
                        AddressStreet = dr["AddressStreet"]?.ToString(),
                        PhoneNumber = int.Parse(dr["PhoneNumber"]?.ToString()),
                        Url = dr["Url"]?.ToString(),
                        DateRegistry = DateTime.Parse(dr["DateRegistry"]?.ToString()),
                        IdCountry = dr["IdCountry"]?.ToString(),
                        IdDepartament = dr["IdDepartament"]?.ToString(),
                        IdCity = dr["IdCity"]?.ToString(),
                        IdRegional = dr["IdRegional"]?.ToString(),
                        isActive = (bool)dr["isActive"]
                    };
                }
            }

            objConexion.CloseConnected();
            return objcompany;
        }

        /// <summary>
        /// Devuelve todas las compañias registradas en una lista
        /// </summary>
        /// <returns></returns>
        public List<Company> GetListCompany()
        {
            List<Company> companyList = new List<Company>();

            using (SqlConnection sqlConnection = objConexion.GetConnected())
            {
                SqlCommand command = new SqlCommand("sp_GetListCompany", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Company company = new Company()
                        {
                            IdCompany = dr["IdCompany"]?.ToString(),
                            IdIdentificationType = dr["IdIdentificationType"]?.ToString(),
                            IdentificationNumber = dr["IdentificationNumber"]?.ToString(),
                            CompanyName = dr["CompanyName"]?.ToString(),
                            AddressStreet = dr["AddressStreet"]?.ToString(),
                            PhoneNumber = int.Parse(dr["PhoneNumber"]?.ToString()),
                            Url = dr["Url"]?.ToString(),
                            DateRegistry = DateTime.Parse(dr["DateRegistry"]?.ToString()),
                            IdCountry = dr["IdCountry"]?.ToString(),
                            IdDepartament = dr["IdDepartament"]?.ToString(),
                            IdCity = dr["IdCity"]?.ToString(),
                            IdRegional = dr["IdRegional"]?.ToString(),
                            isActive = (bool)dr["isActive"]
                        };
                        if (company.isActive == true)
                        {
                            companyList.Add(company);
                        }
                    }
                }
            }
            return companyList;
        }

        /// <summary>
        /// Registrar nueva compañia
        /// </summary>
        /// <param name="company"></param>
        public void RegistryNewCompany(Company company)
        {
            using (SqlConnection sqlConnection = objConexion.GetConnected())
            {
                SqlCommand command = new SqlCommand("sp_RegistryNewCompany", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@IdIdentificationType", company.IdIdentificationType);
                command.Parameters.AddWithValue("@IdentificationNumber", company.IdentificationNumber);
                command.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                command.Parameters.AddWithValue("@AddressStreet", company.AddressStreet);
                command.Parameters.AddWithValue("@PhoneNumber", company.PhoneNumber);
                command.Parameters.AddWithValue("@Url", company.Url);
                command.Parameters.AddWithValue("@DateRegistry", company.DateRegistry);
                command.Parameters.AddWithValue("@IdCountry", company.IdCountry);
                command.Parameters.AddWithValue("@IdDepartament", company.IdDepartament);
                command.Parameters.AddWithValue("@IdCity", company.IdCity);
                command.Parameters.AddWithValue("@IdRegional", company.IdRegional);

                sqlConnection.Open();
                command.ExecuteNonQuery();
            }
            objConexion.CloseConnected();
        }

        /// <summary>
        /// Desactiva compañia
        /// </summary>
        /// <param name="persona"></param>
        public void DesactivateCompanyById(Company company)
        {
            using (SqlConnection sqlConnection = objConexion.GetConnected())
            {
                SqlCommand command = new SqlCommand("sp_DesactivateCompany", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@IdCompany", company.IdCompany);

                sqlConnection.Open();
                command.ExecuteNonQuery();
            }
            objConexion.CloseConnected();
        }

        /// <summary>
        /// Actualiza datos de compañia
        /// </summary>
        /// <param name="persona"></param>
        public void UpdateCompanyById(Company company)
        {
            //Abre la conexion a BD
            SqlConnection sqlConnection = objConexion.GetConnected();

            //Define obj SQL a ejecutar
            SqlCommand command = new SqlCommand("sp_UpdateCompanyById", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;

            // Parámetros
            command.Parameters.AddWithValue("@IdCompany", company.IdCompany);
            command.Parameters.AddWithValue("@IdIdentificationType", company.IdIdentificationType);
            command.Parameters.AddWithValue("@IdentificationNumber", company.IdentificationNumber);
            command.Parameters.AddWithValue("@CompanyName", company.CompanyName);
            command.Parameters.AddWithValue("@AddressStreet", company.AddressStreet);
            command.Parameters.AddWithValue("@PhoneNumber", company.PhoneNumber);
            command.Parameters.AddWithValue("@Url", company.Url);
            command.Parameters.AddWithValue("@DateRegistry", company.DateRegistry);
            command.Parameters.AddWithValue("@IdCountry", company.IdCountry);
            command.Parameters.AddWithValue("@IdDepartament", company.IdDepartament);
            command.Parameters.AddWithValue("@IdCity", company.IdCity);
            command.Parameters.AddWithValue("@IdRegional", company.IdRegional);

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
