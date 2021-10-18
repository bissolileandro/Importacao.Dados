using Importacao.Dados.Domain.Interfaces.Applications;
using Importacao.Dados.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Importacao.Dados.Application.Applications
{
    public class ApplicationBase<TEntity> : IDisposable, IApplicationBase<TEntity> where TEntity : class
    {
        private readonly IServiceBase<TEntity> _serviceBase;

        public ApplicationBase(IServiceBase<TEntity> serviceBase)
        {
            _serviceBase = serviceBase;
        }
        public void Add(TEntity obj)
        {
            try
            {
                _serviceBase.Add(obj);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
                       

        }
        public IEnumerable<TEntity> GetAll()
        {
            return _serviceBase.GetAll();
        }
        public void Dispose()
        {

        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                return await _serviceBase.GetAllAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao consultar os dados: {e.Message}");
            }
        }
    }
}
