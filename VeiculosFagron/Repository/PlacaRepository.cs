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
    public class PlacaRepository : IPlacaRepository
    {
        #region Injeção de Dependências

        private readonly IConfiguration _config;

        public PlacaRepository(IConfiguration config)
        {
            _config = config;
        }
        
        #endregion

        public async Task<List<Placa>> GetPlacas()
        {

            var response = new List<Placa>();

            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var query = @"SELECT * FROM Placa(nolock)";
            response = (List<Placa>)await connection.QueryAsync<Placa>(query);

            return response;

        }
        public async Task<Placa> GetPlaca(int id_placa)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));


            var param = new DynamicParameters();
            param.Add("id_placa", id_placa, direction: ParameterDirection.Input);

            var query = @"SELECT * FROM Placa(nolock) WHERE id_placa = @id_placa";

            var response = await connection.QueryFirstAsync<Placa>(query, param);

            return response;

        }
        public async Task<bool> CreatePlaca(Placa model)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var param = new DynamicParameters();

            param.Add("id_placa", model.id_placa, direction: ParameterDirection.Input);
            param.Add("numero_placa", model.numero_placa, direction: ParameterDirection.Input);
            param.Add("modelo_placa", model.modelo_placa, direction: ParameterDirection.Input);

            var Id = "(SELECT isnull(max(id_placa),0)+1 AS id_placa FROM Placa)";

            var query = $@"INSERT INTO Placa (id_placa, modelo_placa, modelo_placa)
                        VALUES
                        ({Id}, @modelo_placa, @modelo_placa)";

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
        public async Task<bool> UpdatePlaca(Placa model)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var param = new DynamicParameters();

            param.Add("id_placa", model.id_placa, direction: ParameterDirection.Input);
            param.Add("numero_placa", model.numero_placa, direction: ParameterDirection.Input);
            param.Add("modelo_placa", model.modelo_placa, direction: ParameterDirection.Input);

            var query = @"UPDATE Placa SET
                        id_placa = @id_placa,
                        numero_placa = @numero_placa
                        modelo_placa = @modelo_placa
                        WHERE id_placa = @id_placa";

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
        public async Task<bool> DeletePlaca(Placa model)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var param = new DynamicParameters();

            param.Add("id_placa", model.id_placa, direction: ParameterDirection.Input);
            param.Add("numero_placa", model.numero_placa, direction: ParameterDirection.Input);
            param.Add("modelo_placa", model.modelo_placa, direction: ParameterDirection.Input);

            var query = @"DELETE FROM Placa 
                        WHERE id_placa = @id_placa";

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
