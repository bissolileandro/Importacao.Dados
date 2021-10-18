using Importacao.Dados.Domain.Entities;
using Importacao.Dados.Domain.Interfaces.Repositories;
using Importacao.Dados.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Importacao.Dados.Infra.Data.Repositories
{
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ImportacaoDadosContext context)
            :base(context)
        {

        }

        public async Task<Produto> GetById(int Id)
        {
            return db.Set<Produto>().FirstOrDefault(x => x.Id == Id);
        }
    }
}
