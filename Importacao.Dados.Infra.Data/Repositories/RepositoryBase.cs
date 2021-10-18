using Importacao.Dados.Domain.Interfaces.Repositories;
using Importacao.Dados.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Importacao.Dados.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected ImportacaoDadosContext db;

        public RepositoryBase(ImportacaoDadosContext context)
        {
            db = context;
        }
        public void Add(TEntity obj)
        {
            try
            {
                db.Set<TEntity>().Add(obj);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao persistir os dados: {e.Message}");
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                return db.Set<TEntity>().ToList();
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao consultar os dados: {e.Message}");
            }
        }
        public void Dispose()
        {

        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                return await db.Set<TEntity>().ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao consultar os dados: {e.Message}");
            }
        }
    }
}
