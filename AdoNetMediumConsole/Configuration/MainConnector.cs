using AdoNetMediumConsole.Configuration;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetMediumLib.Configuration
{
    public class MainConnector
    {
        readonly SqlConnection connection = new SqlConnection(ConnectionString.MsSqlConnection);
        public async Task<bool> ConnectAsync()
        {

            bool result;
            try
            {

                await connection.OpenAsync();
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }
        public async void DisconnectAsync()
        {
            if (connection.State == ConnectionState.Open)
            {
                await connection.CloseAsync();
            }
        }
        public SqlConnection GetConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                return connection;
            }
            else
            {
                throw new Exception("Подключение уже закрыто!");
            }
        }
    }
}
