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
        public async Task<Modelo> GetModelo(int id_modelo)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));


            var param = new DynamicParameters();
            param.Add("id_modelo", id_modelo, direction: ParameterDirection.Input);

            var query = @"SELECT * FROM Modelo(nolock) WHERE id_modelo = @id_modelo";

            var response = await connection.QueryFirstAsync<Modelo>(query, param);

            return response;

        }
        public async Task<bool> CreateModelo(Modelo model)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var param = new DynamicParameters();

            param.Add("id_modelo", model.id_modelo, direction: ParameterDirection.Input);
            param.Add("descricao_modelo", model.descricao_modelo, direction: ParameterDirection.Input);

            var Id = "(SELECT isnull(max(id_modelo),0)+1 AS id_modelo FROM Modelo)";

            var query = $@"INSERT INTO Modelo (id_modelo, descricao_modelo)
                        VALUES
                        ({Id}, @descricao_modelo)";

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
        public async Task<bool> UpdateModelo(Modelo model)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var param = new DynamicParameters();

            param.Add("id_modelo", model.id_modelo, direction: ParameterDirection.Input);
            param.Add("descricao_modelo", model.descricao_modelo, direction: ParameterDirection.Input);

            var query = @"UPDATE Veiculo SET
                        id_modelo = @id_modelo,
                        descricao_modelo = @descricao_modelo
                        WHERE id_modelo = @id_modelo AND
                        descricao_modelo = @descricao_modelo";

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
        public async Task<bool> DeleteModelo(Modelo model)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var param = new DynamicParameters();

            param.Add("id_modelo", model.id_modelo, direction: ParameterDirection.Input);
            param.Add("descricao_modelo", model.descricao_modelo, direction: ParameterDirection.Input);

            var query = @"DELETE FROM Modelo 
                        WHERE id_modelo = @id_modelo";

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
