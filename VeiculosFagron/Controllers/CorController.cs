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
    public class CorController : ControllerBase
    {
        #region Injeção de Dependências 

        // Configuração da conexão do banco com o microsserviço
        private readonly IConfiguration _config;

        // Configuração e injeção dos Log de erros usando a biblioteca "Serilog"
        private readonly ILogger<CorController> _logger;

        // Declarando Repository dos veiculos para retornar logs
        private readonly ICorRepository _corRepository;

        // Declarando e armazenando as configurações de dependências
        public CorController(IConfiguration config, ILogger<CorController> logger, ICorRepository corRepository)
        {
            _config = config;
            _logger = logger;
            _corRepository = corRepository;
        }

        #endregion

        #region Cor

        [HttpGet]
        [Route("getCor")]
        public async Task<ActionResult<List<Cor>>> getCor()
        {
            try
            {
                var data = await _corRepository.GetCor();
                return Ok(data);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetCor: Erro na requisição dos dados");
                return new StatusCodeResult(500);
            }

        }

        #endregion

    }
}
