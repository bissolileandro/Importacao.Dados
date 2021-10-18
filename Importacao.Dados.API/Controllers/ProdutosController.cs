﻿using Importacao.Dados.API.Comman;
using Importacao.Dados.Domain.Interfaces.Applications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Importacao.Dados.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoApplication produtoApplication;
        

        public ProdutosController(IProdutoApplication produtoApplication)
        {
            this.produtoApplication = produtoApplication;
        }

        [HttpPost]
        [Route("ImportarDadosExcel")]
        public async Task<IActionResult> ImportarDadosExcel(IFormFile produtoCommand)
        {
            try
            {
                await produtoApplication.ImportarArquivoExcel(produtoCommand);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest($"Erro: {e.Message}");
            }
        }
    }
}