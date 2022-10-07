using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VeiculosFagron.Repository;

namespace VeiculosFagron.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TenantController : ControllerBase
    {
        #region Injeção de Dependências 

        // Configuração da conexão do banco com o microsserviço
        private readonly IConfiguration _config;

        // Configuração e injeção dos Log de erros usando a biblioteca "Serilog"
        private readonly ILogger<TenantController> _logger;

        // Declarando Repository dos veiculos para retornar logs
        private readonly ITenantRepository _tenantRepository;

        // Declarando e armazenando as configurações de dependências
        public TenantController(IConfiguration config, ILogger<TenantController> logger, ITenantRepository tenantRepository)
        {
            _config = config;
            _logger = logger;
            _tenantRepository = tenantRepository;
        }

        #endregion

        #region Tenant

        [HttpGet]
        [Route("getTenant")]
        public async Task<ActionResult<List<Tenant>>> getTenant()
        {
            try
            {
                var data = await _tenantRepository.GetTenant();
                return Ok(data);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetTenant: Erro na requisição dos dados");
                return new StatusCodeResult(500);
            }

        }

        [HttpGet]
        [Route("getTenant/{id_tenant}")]
        public async Task<ActionResult<Tenant>> getTenant(int id_tenant)
        {
            try
            {
                var data = await _tenantRepository.GetTenant(id_tenant);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetTenant: Erro na requisição dos dados");
                return new StatusCodeResult(500);
            }

        }

        #endregion

    }
}
