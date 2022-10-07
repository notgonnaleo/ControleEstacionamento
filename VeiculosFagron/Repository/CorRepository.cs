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

        public async Task<List<Cor>> GetCores()
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

        public async Task<bool> CreateCor(Cor model)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var param = new DynamicParameters();

            param.Add("id_cor", model.id_cor, direction: ParameterDirection.Input);
            param.Add("nome_cor", model.nome_cor, direction: ParameterDirection.Input);


            var Id = "(SELECT isnull(max(id_cor),0)+1 AS id_cor FROM Cor)";

            var query = $@"INSERT INTO Cor (id_cor, nome_cor)
                        VALUES
                        ({Id}, @nome_cor)";

            var response = await connection.ExecuteAsync(query, param);

            if (response > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
