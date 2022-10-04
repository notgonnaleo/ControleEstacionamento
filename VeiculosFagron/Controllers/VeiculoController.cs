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
    public class VeiculoController : ControllerBase
    {
        #region Injeção de Dependências 

        // Configuração da conexão do banco com o microsserviço
        private readonly IConfiguration _config;

        // Configuração e injeção dos Log de erros usando a biblioteca "Serilog"
        private readonly ILogger<VeiculoController> _logger;

        // Declarando Repository dos veiculos para retornar logs
        private readonly IVeiculoRepository _veiculoRepository;

        // Declarando e armazenando as configurações de dependências
        public VeiculoController(IConfiguration config, ILogger<VeiculoController> logger, IVeiculoRepository veiculoRepository)
        {
            _config = config;
            _logger = logger;
            _veiculoRepository = veiculoRepository;
        }

        #endregion

        #region Veículos

        [HttpGet]
        [Route("getVeiculos")]
        public async Task<ActionResult<List<Veiculo>>> getVeiculos()
        {
            try
            {
                var data = await _veiculoRepository.GetVeiculos();
                return Ok(data);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetVeiculos: Erro na requisição dos dados");
                return new StatusCodeResult(500);
            }

        }

        [HttpGet]
        [Route("getVeiculo/{id_veiculo}")]
        public async Task<ActionResult<Veiculo>> getVeiculo(int id_veiculo)
        {
            try
            {
                var data = await _veiculoRepository.GetVeiculo(id_veiculo);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetVeiculo: Erro na requisição dos dados");
                return new StatusCodeResult(500);
            }

        }

        [HttpPost]
        [Route("createVeiculo")]
        public async Task<ActionResult<bool>> createVeiculo(Veiculo model)
        {
            try
            {
                var data = await _veiculoRepository.CreateVeiculo(model);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateVeiculo: Erro na requisição dos dados");
                return false;
            }

        }

        [HttpPut]
        [Route("updateVeiculo")]
        public async Task<ActionResult<bool>> updateVeiculo(Veiculo model)
        {
            try
            {
                var data = await _veiculoRepository.UpdateVeiculo(model);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateVeiculo: Erro na requisição dos dados");
                return false;
            }

        }

        [HttpDelete]
        [Route("deleteVeiculo")]
        public async Task<ActionResult<bool>> deleteVeiculo(Veiculo model)
        {
            try
            {
                var data = await _veiculoRepository.DeleteVeiculo(model);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteVeiculo: Erro na requisição dos dados");
                return false;
            }

        }

        #endregion

    }
}
