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
    public class TenantRepository : ITenantRepository
    {
        #region Injeção de Dependências

        private readonly IConfiguration _config;

        public TenantRepository(IConfiguration config)
        {
            _config = config;
        }
        
        #endregion

        public async Task<List<Tenant>> GetTenant()
        {

            var response = new List<Tenant>();

            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var query = @"SELECT * FROM Tenant(nolock)";
            response = (List<Tenant>)await connection.QueryAsync<Tenant>(query);

            return response;

        }

        public async Task<Tenant> GetTenant(int id_tenant)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));


            var param = new DynamicParameters();
            param.Add("id_tenant", id_tenant, direction: ParameterDirection.Input);

            var query = @"SELECT * FROM Tenant(nolock) WHERE id_tenant = @id_tenant";

            var response = await connection.QueryFirstAsync<Tenant>(query, param);

            return response;

        }
        public async Task<bool> CreateTenant(Tenant model)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var param = new DynamicParameters();

            param.Add("id_veiculo", model.id_veiculo, direction: ParameterDirection.Input);
            param.Add("id_tenant", model.id_tenant, direction: ParameterDirection.Input);
   

            var Id = "(SELECT isnull(max(id_tenant),0)+1 AS id_tenant FROM Tenant)";

            var query = $@"INSERT INTO Tenant (id_tenant, id_veiculo)
                        VALUES
                        ({Id}, @id_veiculo)";

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
