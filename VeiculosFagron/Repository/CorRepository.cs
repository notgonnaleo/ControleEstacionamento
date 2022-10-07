using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace VeiculosFagron.Repository
{
    public class CorRepository : ICorRepository
    {
        #region Injeção de Dependências

        private readonly IConfiguration _config;

        public CorRepository(IConfiguration config)
        {
            _config = config;
        }
        
        #endregion

        public async Task<List<Cor>> GetCor()
        {

            var response = new List<Cor>();

            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var query = @"SELECT * FROM Cor(nolock)";
            response = (List<Cor>)await connection.QueryAsync<Cor>(query);

            return response;

        }

        public async Task<Cor> GetCor(int id_cor)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));


            var param = new DynamicParameters();
            param.Add("id_cor", id_cor, direction: ParameterDirection.Input);

            var query = @"SELECT * FROM Cor(nolock) WHERE id_cor = @id_cor";

            var response = await connection.QueryFirstAsync<Cor>(query, param);

            return response;

        }

    }
}
