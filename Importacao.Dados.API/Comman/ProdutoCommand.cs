using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Importacao.Dados.API.Comman
{
    public class ProdutoCommand
    {
        public IFormFile ArquivoExcel { get; set; }
    }
}
