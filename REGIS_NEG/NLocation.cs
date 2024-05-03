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
    public class NLocation
    {
        #region Variables & Objetos
        ConectionDB objConexion = new ConectionDB();
        Country objPais = new Country();
        Departament objDepartamento = new Departament();
        City objCiudad = new City();
        Util objUtilidad = new Util();
        #endregion

        /// <summary>
        /// Consulta todos los paises
        /// </summary>
        /// <returns></returns>
        public List<Country> GetListCountry()
        {
            List<Country> listCountry = new List<Country>();
            using (SqlConnection sqlConnection = objConexion.GetConnected())
            {
                SqlCommand command = new SqlCommand("sp_GetListCountry", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        objPais = new Country()
                        {
                            IdCountry = dr["IdCountry"]?.ToString(),
                            Name = dr["Name"]?.ToString(),
                            Code = dr["Code"]?.ToString()
                        };
                        listCountry.Add(objPais);
                    }
                }
            }
            return listCountry;
        }

        /// <summary>
        /// Consulta todos los departamentos x pais
        /// </summary>
        /// <param name="IdCountry"></param>
        /// <returns></returns>
        public List<Departament> GetDepartamentsByIdCountry(string IdCountry)
        {
            List<Departament> listDepartem = new List<Departament>();
            using (SqlConnection sqlConnection = objConexion.GetConnected())
            {
                SqlCommand command = new SqlCommand("sp_GetListDepartXIdCountry", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                // Parámetros
                command.Parameters.AddWithValue("@IdCountry", IdCountry);

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        objDepartamento = new Departament()
                        {
                            IdDepartament = dr["IdDepartament"]?.ToString(),
                            IdCountry = dr["IdCountry"]?.ToString(),
                            Name = dr["Name"]?.ToString(),
                            Code = dr["Code"]?.ToString()
                        };
                        listDepartem.Add(objDepartamento);
                    }
                }
            }
            return listDepartem;
        }

        /// <summary>
        /// Consulta todas las ciudades x IDdepartamentos y x IdPais
        /// </summary>
        /// <param name="IdCountry"></param>
        /// <param name="IdDepartament"></param>
        /// <returns></returns>
        public List<City> GetCitiesByIdCountryIdDepartament(string IdCountry, string IdDepartament)
        {
            List<City> listCity = new List<City>();
            using (SqlConnection sqlConnection = objConexion.GetConnected())
            {
                SqlCommand command = new SqlCommand("sp_GetListCityByIdDepartXIdCountry", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                // Parámetros
                command.Parameters.AddWithValue("@IdCountry", IdCountry);
                command.Parameters.AddWithValue("@IdDepartament", IdDepartament);

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        objCiudad = new City()
                        {
                            IdCity = dr["IdCity"]?.ToString(),
                            IdDepartament = dr["IdDepartament"]?.ToString(),
                            IdCountry = dr["IdCountry"]?.ToString(),
                            Name = dr["Name"]?.ToString(),
                            Code = dr["Code"]?.ToString()
                        };
                        listCity.Add(objCiudad);
                    }
                }
            }
            return listCity;
        }
    }
}
