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
    public class PlacaController : ControllerBase
    {
        #region Injeção de Dependências 

        // Configuração da conexão do banco com o microsserviço
        private readonly IConfiguration _config;

        // Configuração e injeção dos Log de erros usando a biblioteca "Serilog"
        private readonly ILogger<PlacaController> _logger;

        // Declarando Repository dos veiculos para retornar logs
        private readonly IPlacaRepository _placaRepository;

        // Declarando e armazenando as configurações de dependências
        public PlacaController(IConfiguration config, ILogger<PlacaController> logger, IPlacaRepository placaRepository)
        {
            _config = config;
            _logger = logger;
            _placaRepository = placaRepository;
        }

        #endregion

        #region Placas

        [HttpGet]
        [Route("getPlacas")]
        public async Task<ActionResult<List<Placa>>> getPlacas()
        {
            try
            {
                var data = await _placaRepository.GetPlacas();
                return Ok(data);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetPlacas: Erro na requisição dos dados");
                return new StatusCodeResult(500);
            }

        }

        [HttpGet]
        [Route("getModelo/{id_modelo}")]
        public async Task<ActionResult<Placa>> getPlaca(int id_placa)
        {
            try
            {
                var data = await _placaRepository.GetPlaca(id_placa);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetPlaca: Erro na requisição dos dados");
                return new StatusCodeResult(500);
            }

        }

        [HttpPost]
        [Route("createModelo")]
        public async Task<ActionResult<bool>> createPlaca(Placa model)
        {
            try
            {
                var data = await _placaRepository.CreatePlaca(model);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreatePlaca: Erro na requisição dos dados");
                return false;
            }

        }


        [HttpPut]
        [Route("updateModelo")]
        public async Task<ActionResult<bool>> updatePlaca(Placa model)
        {
            try
            {
                var data = await _placaRepository.UpdatePlaca(model);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdatePlaca: Erro na requisição dos dados");
                return false;
            }

        }

        [HttpDelete]
        [Route("deleteModelo")]
        public async Task<ActionResult<bool>> deletePlaca(Placa model)
        {
            try
            {
                var data = await _placaRepository.DeletePlaca(model);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeletePlaca: Erro na requisição dos dados");
                return false;
            }

        }

        #endregion

    }
}
