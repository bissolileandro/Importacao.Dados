using Importacao.Dados.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Importacao.Dados.Domain.Interfaces.Applications
{
    public interface IProdutoApplication : IApplicationBase<Produto>
    {
        Task ImportarArquivoExcel(IFormFile arquivoExcel);
        Task<Produto> GetById(int Id);
    }
}
