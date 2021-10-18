using Importacao.Dados.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Importacao.Dados.API.Models
{
    public class ProdutoModel
    {
        public int Id { get; set; }
        public DateTime DataEntrega { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public double ValorUnitario { get; set; }
        public double ValorTotal
        { get
            {
                return GetTotal();
            }
        }

        private double GetTotal()
        {
            return Quantidade * ValorUnitario;
        }

        public static IEnumerable<ProdutoModel> EntityToModel(IEnumerable<Produto> entities)
        {
            List<ProdutoModel> models = new List<ProdutoModel>();
            foreach (var item in entities)
            {
                models.Add(EntityToModel(item));
            }

            return models;
        }
        public static ProdutoModel EntityToModel(Produto entity)
        {
            return new ProdutoModel() 
            { 
                Id = entity.Id,
                DataEntrega = entity.DataEntrega,
                Nome = entity.Nome,
                Quantidade = entity.Quantidade,
                ValorUnitario = entity.ValorUnitario
            };
        }

        public static IEnumerable<Produto> ModelToEntity(IEnumerable<ProdutoModel> models)
        {
            List<Produto> entities = new List<Produto>();
            foreach (var item in models)
            {
                entities.Add(ModelToEntity(item));
            }
            return entities;
        }

        public static Produto ModelToEntity(ProdutoModel model)
        {
            return new Produto()
            {
                Id = model.Id,
                DataEntrega = model.DataEntrega,
                Nome = model.Nome,
                Quantidade = model.Quantidade,
                ValorUnitario = model.ValorUnitario
            };
        }
    }
}
