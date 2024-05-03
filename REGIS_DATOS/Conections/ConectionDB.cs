using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REGIS_DATOS.Models;
using System.Data;

namespace REGIS_DATOS.Conections
{
    public class ConectionDB()
    {
        //CadenaConexión
        private string cadenaConexion = "Server=DESKTOP-FUU5T0Q; Database=REGISTY; Trusted_Connection=True;";

        /// <summary>
        /// Abre Conexión BD
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetConnected()
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                //Abre conexión
                conexion.Open();
            }
            catch (SqlException ex)
            {
                conexion = null;
            }
            return conexion;
        }

        /// <summary>
        /// Cierra Conexión BD
        /// </summary>
        /// <returns></returns>
        public SqlConnection CloseConnected()
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                //Limpia la conexión
                conexion.Dispose();

                //Cierra la conexión
                conexion.Close();
            }
            catch (SqlException ex)
            {
                conexion = null;
            }
            return conexion;
        }

        /// <summary>
        /// Abre Conexión Async
        /// </summary>
        /// <returns></returns>
        public async Task<SqlConnection> GetConnectedAsync()
        {
            SqlConnection connection = new SqlConnection(cadenaConexion);

            try
            {
                //Abre conexión
                await connection.OpenAsync();
            }
            catch (SqlException ex)
            {
                connection = null;
            }

            return connection;
        }

    }
}
