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
    public class ModeloController : ControllerBase
    {
        #region Injeção de Dependências 

        // Configuração da conexão do banco com o microsserviço
        private readonly IConfiguration _config;

        // Configuração e injeção dos Log de erros usando a biblioteca "Serilog"
        private readonly ILogger<ModeloController> _logger;

        // Declarando Repository dos veiculos para retornar logs
        private readonly IModeloRepository _modeloRepository;

        // Declarando e armazenando as configurações de dependências
        public ModeloController(IConfiguration config, ILogger<ModeloController> logger, IModeloRepository modeloRepository)
        {
            _config = config;
            _logger = logger;
            _modeloRepository = modeloRepository;
        }

        #endregion

        #region Veículos

        [HttpGet]
        [Route("getModelos")]
        public async Task<ActionResult<List<Modelo>>> GetModelos()
        {
            try
            {
                var data = await _modeloRepository.GetModelos();
                return Ok(data);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetModelos: Erro na requisição dos dados");
                return new StatusCodeResult(500);
            }

        }

        #endregion

    }
}
