using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace VeiculosFagron.Repository
{
    public class ModeloRepository : IModeloRepository
    {
        #region Injeção de Dependências

        private readonly IConfiguration _config;


        public ModeloRepository(IConfiguration config)
        {
            _config = config;
        }
        
        #endregion

        public async Task<List<Modelo>> GetModelos()
        {

            var response = new List<Modelo>();

            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var query = @"SELECT * FROM Modelo(nolock)";
            response = (List<Modelo>)await connection.QueryAsync<Modelo>(query);

            return response;

        }

    }
}
