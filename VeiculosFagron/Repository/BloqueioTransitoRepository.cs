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
    public class BloqueioTransitoRepository : IBloqueioTransitoRepository
    {
        #region Injeção de Dependências

        private readonly IConfiguration _config;

        public BloqueioTransitoRepository(IConfiguration config)
        {
            _config = config;
        }
        
        #endregion

        public async Task<List<BloqueioTransito>> GetBloqueios()
        {

            var response = new List<BloqueioTransito>();

            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var query = @"SELECT * FROM BloqueioTransito(nolock)";
            response = (List<BloqueioTransito>)await connection.QueryAsync<BloqueioTransito>(query);

            return response;

        }
        public async Task<BloqueioTransito> GetBloqueio(int id_bloqueio)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));


            var param = new DynamicParameters();
            param.Add("id_bloqueio", id_bloqueio, direction: ParameterDirection.Input);

            var query = @"SELECT * FROM BloqueioTransito(nolock) WHERE id_bloqueio = @id_bloqueio";

            var response = await connection.QueryFirstAsync<BloqueioTransito>(query, param);

            return response;

        }
        public async Task<bool> CreateBloqueio(BloqueioTransito model)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var param = new DynamicParameters();

            param.Add("id_bloqueio", model.id_bloqueio, direction: ParameterDirection.Input);
            param.Add("id_placa", model.id_placa, direction: ParameterDirection.Input);
            param.Add("numero_placa", model.numero_placa, direction: ParameterDirection.Input);

            var Id = "(SELECT isnull(max(id_bloqueio),0)+1 AS id_bloqueio FROM BloqueioTransito)";
            var Placa = "(SELECT numero_placa FROM Placa WHERE id_placa = @id_placa))";

            var query = $@"INSERT INTO BloqueioTransito (id_bloqueio, id_placa, numero_placa)
                        VALUES
                        ({Id}, @id_placa, {Placa})";

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
        public async Task<bool> UpdateBloqueio(BloqueioTransito model)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var param = new DynamicParameters();

            param.Add("id_bloqueio", model.id_bloqueio, direction: ParameterDirection.Input);
            param.Add("id_placa", model.id_placa, direction: ParameterDirection.Input);
            param.Add("numero_placa", model.numero_placa, direction: ParameterDirection.Input);

            var query = @"UPDATE BloqueioTransito SET
                        id_bloqueio = @id_bloqueio,
                        id_placa = @id_placa,
                        numero_placa = @numero_placa
                        WHERE id_bloqueio = @id_bloqueio";

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
        public async Task<bool> DeleteBloqueio(BloqueioTransito model)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var param = new DynamicParameters();

            param.Add("id_bloqueio", model.id_bloqueio, direction: ParameterDirection.Input);
            param.Add("id_placa", model.id_placa, direction: ParameterDirection.Input);
            param.Add("numero_placa", model.numero_placa, direction: ParameterDirection.Input);

            var query = @"DELETE FROM BloqueioTransito 
                        WHERE id_bloqueio = @id_bloqueio";

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
