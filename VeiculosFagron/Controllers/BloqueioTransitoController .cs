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
    public class BloqueioTransitoController : ControllerBase
    {
        #region Injeção de Dependências 

        // Configuração da conexão do banco com o microsserviço
        private readonly IConfiguration _config;

        // Configuração e injeção dos Log de erros usando a biblioteca "Serilog"
        private readonly ILogger<BloqueioTransitoController> _logger;

        // Declarando Repository dos veiculos para retornar logs
        private readonly IBloqueioTransitoRepository _bloqueioTransitoRepository;

        // Declarando e armazenando as configurações de dependências
        public BloqueioTransitoController(IConfiguration config, ILogger<BloqueioTransitoController> logger, IBloqueioTransitoRepository bloqueioTransitoRepository)
        {
            _config = config;
            _logger = logger;
            _bloqueioTransitoRepository = bloqueioTransitoRepository;
        }

        #endregion

        #region Placas

        [HttpGet]
        [Route("getBloqueios")]
        public async Task<ActionResult<List<BloqueioTransito>>> getBloqueios()
        {
            try
            {
                var data = await _bloqueioTransitoRepository.GetBloqueios();
                return Ok(data);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetBloqueios: Erro na requisição dos dados");
                return new StatusCodeResult(500);
            }

        }

        [HttpGet]
        [Route("getBloqueio/{id_bloqueio}")]
        public async Task<ActionResult<BloqueioTransito>> getBloqueio(int id_bloqueio)
        {
            try
            {
                var data = await _bloqueioTransitoRepository.GetBloqueio(id_bloqueio);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetBloqueio: Erro na requisição dos dados");
                return new StatusCodeResult(500);
            }

        }

        [HttpPost]
        [Route("createBloqueio")]
        public async Task<ActionResult<bool>> createBloqueio(BloqueioTransito model)
        {
            try
            {
                var data = await _bloqueioTransitoRepository.CreateBloqueio(model);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateBloqueio: Erro na requisição dos dados");
                return false;
            }

        }


        [HttpPut]
        [Route("updateBloqueio")]
        public async Task<ActionResult<bool>> updateBloqueio(BloqueioTransito model)
        {
            try
            {
                var data = await _bloqueioTransitoRepository.UpdateBloqueio(model);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateBloqueio: Erro na requisição dos dados");
                return false;
            }

        }

        [HttpDelete]
        [Route("deleteBloqueio")]
        public async Task<ActionResult<bool>> deleteBloqueio(BloqueioTransito model)
        {
            try
            {
                var data = await _bloqueioTransitoRepository.DeleteBloqueio(model);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteBloqueio: Erro na requisição dos dados");
                return false;
            }

        }

        #endregion

    }
}
