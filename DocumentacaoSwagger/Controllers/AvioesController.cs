using DocumentacaoSwagger.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentacaoSwagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvioesController : ControllerBase
    {
        private readonly Contexto _contexto;

        public AvioesController(Contexto contexto)
        {
            _contexto = contexto;
        }


        /// <summary>
        /// Pega todos os aviões cadastrados
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição
        /// GET /Avioes
        /// </remarks>
        /// <returns>Lista de aviões cadastrados</returns>
        /// <response code="200">Lista de aviões cadastrados</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<Aviao>>> PegarTodosAsync()
        {
            return await _contexto.Avioes.ToListAsync();
        }

        /// <summary>
        /// Salva o avião recebido
        /// </summary>
        /// <param name="aviao">Avião a ser salvo no banco de dados</param>
        /// <remarks>
        /// Exemplo de requisição
        /// POST /Avioes
        /// {
        ///     "nomeProdutor" : "Boeing",
        ///     "nomeAviao" : "B787",
        ///     "qtdPassageiros" : 350
        /// }
        /// </remarks>
        /// <returns>Mensagem de confirmação que o avião foi salvo</returns>
        /// <response code="200">O avião foi salvo corretamente</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Aviao>> SalvarAsync(Aviao aviao)
        {
            if (ModelState.IsValid)
            {
                await _contexto.AddAsync(aviao);
                await _contexto.SaveChangesAsync();
                return Ok(new
                {
                    mensagem = $"O avião {aviao.NomeProdutor} {aviao.NomeAviao} foi salvo corretamente"
                });
            }

            return BadRequest(new
            {
                mensagem = "Dados inválidos"
            });

        }

        /// <summary>
        /// Pega um avião baseado em seu id
        /// </summary>
        /// <param name="aviaoId">Id usado para pegar o avião</param>
        /// <remarks>
        /// Exemplo de requisição
        /// GET Avioes/90
        /// {
        ///     "aviaoId" : 90,
        ///     "nomeProdutor" : "Boeing",
        ///     "nomeAviao" : "B737",
        ///     "qtdPassageiros" : 200
        /// }
        /// </remarks>
        /// <returns> Avião com base em seu identificador</returns>
        /// <response code="200"> Retorna avião pego no banco de dados</response>
        /// <response code="400"> Identificador inválido</response>
        /// /// <response code="404"> Avião não encontrado</response>
        [HttpGet("{aviaoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Aviao>> PegarPeloIdAsync(string aviaoId)
        {
            if (string.IsNullOrEmpty(aviaoId))
            {
                return BadRequest(new
                {
                    mensagem = $"O identificador é inválido"
                });
            }

            else
            {
                if(!int.TryParse(aviaoId, out int value))
                {
                    return BadRequest(new
                    {
                        mensagem = $"O identificador {aviaoId} é inválido"
                    });
                }

                else
                {
                    int id = int.Parse(aviaoId);
                    Aviao aviao = await _contexto.Avioes.FindAsync(id);

                    if(aviao != null)
                    {
                        return aviao;
                    }

                    return NotFound(new
                    {
                        mensagem = $"O avião com identificador {id} não foi encontrado ou nunca existiu"
                    });
                }
            }
        }

        /// <summary>
        /// Atualiza o avião recebido
        /// </summary>
        /// <param name="aviao">Avião a ser atualizado</param>
        /// <remarks>
        /// Exemplo de requisição
        /// PUT /Avioes
        /// {
        ///     "aviaoId" : 90,
        ///     "nomeProdutor" : "Boeing",
        ///     "nomeAviao" : "B737",
        ///     "qtdPassageiros" : 200
        /// }
        /// </remarks>
        /// <returns> Avião atualizado</returns>
        /// <response code="200"> Avião atualizado corretamente</response>
        /// <response code="400"> Erro ao atualizar o avião recebido</response>        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Aviao>> AtualizarAsync(Aviao aviao)
        {
            if (ModelState.IsValid)
            {
                _contexto.Avioes.Update(aviao);
                await _contexto.SaveChangesAsync();

                return Ok(new
                {
                    mensagem = $"O avião {aviao.NomeProdutor} {aviao.NomeAviao} foi atualizado corretamente"
                });
            }

            return BadRequest(new
            {
                mensagem = $"Houve algum erro ao tentar atualizar o avião {aviao.NomeProdutor} {aviao.NomeAviao}"
            });
        }

        /// <summary>
        /// Exclui um avião baseado em seu id
        /// </summary>
        /// <param name="aviaoId">Id usado para pegar o avião</param>
        /// <remarks>
        /// Exemplo de requisição
        /// DELETE Avioes/90
        /// {
        ///     "aviaoId" : 90    
        /// }
        /// </remarks>
        /// <returns> Avião excluído corretamente</returns>
        /// <response code="200"> Avião excluído</response>
        /// <response code="400"> Identificador inválido</response>
        /// <response code="404"> Avião não encontrado</response>
        [HttpDelete("{aviaoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Aviao>> ExcluirAsync(string aviaoId)
        {
            if (string.IsNullOrEmpty(aviaoId))
            {
                return BadRequest(new
                {
                    mensagem = $"O identificador é inválido"
                });
            }

            else
            {
                if (!int.TryParse(aviaoId, out int value))
                {
                    return BadRequest(new
                    {
                        mensagem = $"O identificador {aviaoId} é inválido"
                    });
                }

                else
                {
                    int id = int.Parse(aviaoId);
                    Aviao aviao = await _contexto.Avioes.FindAsync(id);

                    if (aviao != null)
                    {
                        _contexto.Avioes.Remove(aviao);
                        await _contexto.SaveChangesAsync();

                        return Ok(new
                        {
                            mensagem = $"O avião {aviao.NomeProdutor} {aviao.NomeAviao} foi excluído corretamente"
                        });
                    }

                    return NotFound(new
                    {
                        mensagem = $"O avião com identificador {id} não foi encontrado ou nunca existiu"
                    });
                }
            }
        }


    }
}
