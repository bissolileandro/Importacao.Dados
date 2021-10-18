using Importacao.Dados.Domain.Entities;
using Importacao.Dados.Domain.Interfaces.Repositories;
using Importacao.Dados.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Importacao.Dados.Service.Services
{
    public class ProdutoService : ServiceBase<Produto>, IProdutoService
    {
        public ProdutoService(IProdutoReposioy produtoRepository)
            :base(produtoRepository)
        {

        }
    }
}
