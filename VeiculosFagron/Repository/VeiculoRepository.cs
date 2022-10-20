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
    public class VeiculoRepository : IVeiculoRepository
    {
        #region Injeção de Dependências

        private readonly IConfiguration _config;

        public VeiculoRepository(IConfiguration config)
        {
            _config = config;
        }
        
        #endregion

        public async Task<List<VeiculoCompleto>> GetVeiculos()
        {

            var response = new List<VeiculoCompleto>();

            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var query = @"SELECT id_veiculo,
                        v.id_placa,
                        v.id_modelo,
                        v.id_cor,
                        v.data_cadastro, 
                        v.km, 
                        m.descricao_modelo, 
                        p.numero_placa, 
                        p.modelo_placa, 
                        c.nome_cor
                        FROM Veiculo v 
                        LEFT OUTER JOIN Modelo m ON v.id_modelo = m.id_modelo
                        LEFT OUTER JOIN Placa p ON v.id_placa = p.id_placa
                        LEFT OUTER JOIN Cor c ON v.id_cor = c.id_cor
                        ";
            response = (List<VeiculoCompleto>)await connection.QueryAsync<VeiculoCompleto>(query);

            return response;

        }
        public async Task<Veiculo> GetVeiculo(int id_veiculo)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));


            var param = new DynamicParameters();
            param.Add("id_veiculo", id_veiculo, direction: ParameterDirection.Input);

            var query = @"SELECT * FROM Veiculo(nolock) WHERE id_veiculo = @id_veiculo";

            var response = await connection.QueryFirstAsync<Veiculo>(query, param);
            
            return response;

        }
        public async Task<bool> CreateVeiculo(Veiculo model)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var param = new DynamicParameters();

            param.Add("id_veiculo", model.id_veiculo, direction: ParameterDirection.Input);
            param.Add("data_cadastro", model.data_cadastro, direction: ParameterDirection.Input);
            param.Add("id_placa", model.id_placa, direction: ParameterDirection.Input);
            param.Add("id_cor", model.id_cor, direction: ParameterDirection.Input);
            param.Add("km", model.km, direction: ParameterDirection.Input);
            param.Add("id_modelo", model.id_modelo, direction: ParameterDirection.Input);

            var Id = "(SELECT isnull(max(id_veiculo),0)+1 AS id_veiculo FROM Veiculo)";

            var query = $@"INSERT INTO Veiculo (id_veiculo, data_cadastro, id_placa, id_cor, km, id_modelo)
                        VALUES
                        ({Id}, @data_cadastro, @id_placa, @id_cor, @km, @id_modelo)";

            var response = await connection.ExecuteAsync(query, param);

            if(response > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> UpdateVeiculo(Veiculo model)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var param = new DynamicParameters();

            param.Add("id_veiculo", model.id_veiculo, direction: ParameterDirection.Input);
            param.Add("data_cadastro", model.data_cadastro, direction: ParameterDirection.Input);
            param.Add("id_placa", model.id_placa, direction: ParameterDirection.Input);
            param.Add("id_cor", model.id_cor, direction: ParameterDirection.Input);
            param.Add("km", model.km, direction: ParameterDirection.Input);
            param.Add("id_modelo", model.id_modelo, direction: ParameterDirection.Input);

            var query = @"UPDATE Veiculo SET
                        id_veiculo = @id_veiculo,
                        id_placa = @id_placa, 
                        id_modelo = @id_modelo, 
                        id_cor = @id_cor,
                        data_cadastro = @data_cadastro,
                        km = @km 
                        WHERE id_veiculo = @id_veiculo AND
                        id_placa = @id_placa AND
                        id_modelo = @id_modelo";

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
        public async Task<bool> DeleteVeiculo(Veiculo model)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var param = new DynamicParameters();

            param.Add("id_veiculo", model.id_veiculo, direction: ParameterDirection.Input);
            param.Add("data_cadastro", model.data_cadastro, direction: ParameterDirection.Input);
            param.Add("id_placa", model.id_placa, direction: ParameterDirection.Input);
            param.Add("id_cor", model.id_cor, direction: ParameterDirection.Input);
            param.Add("km", model.km, direction: ParameterDirection.Input);
            param.Add("id_modelo", model.id_modelo, direction: ParameterDirection.Input);

            var query = @"DELETE FROM Veiculo 
                        WHERE id_veiculo = @id_veiculo 
                        AND id_placa = @id_placa 
                        AND id_modelo = @id_modelo";

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
