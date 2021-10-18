using Importacao.Dados.Domain.Entities;
using Importacao.Dados.Domain.Interfaces.Applications;
using Importacao.Dados.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;


namespace Importacao.Dados.Application.Applications
{
    public class ProdutoApplication : ApplicationBase<Produto>, IProdutoApplication
    {
        private readonly IProdutoService produtoService;
        public ProdutoApplication(IProdutoService produtoService)
            :base(produtoService)
        {
            this.produtoService = produtoService;
        }

        public async Task<Produto> GetById(int Id)
        {
            try
            {
                return await produtoService.GetById(Id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public async Task ImportarArquivoExcel(IFormFile arquivoExcel)
        {
            try
            {
                if (arquivoExcel != null)
                {
                    string sFileExtension = Path.GetExtension(arquivoExcel.FileName).ToLower();
                    string path = Path.Combine(Environment.CurrentDirectory, "Uploads");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    if ((sFileExtension == ".xls") || (sFileExtension == ".xlsx"))
                    {
                        await ObterDadosPlanilha(arquivoExcel);
                    }
                    else
                    {
                        throw new Exception($"Arquivo Inválido!");
                    }

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        private bool ValidarProduto(ref StringBuilder erros, Produto produto, int linha)
        {
            
            if (produto.DataEntrega <= DateTime.Now)
                erros.Append($"O campo data de entrega não pode ser menor ou igual que o dia atual. Linha: {linha.ToString()}");
            if (produto.Nome.Length > 50)
                erros.Append($"O campo descrição precisa ter o tamanho máximo de 50 caracteres. Linha: {linha.ToString()}");
            if (produto.Quantidade == 0)
                erros.Append($" O campo quanƟdade tem que ser maior do que zero. Linha: {linha.ToString()}");
            if (produto.ValorUnitario == 0.00)
                erros.Append($@"O campo valor unitário deve ser maior que zero e suas casas decimais devem ser
                              arredondadas matematicamente para duas casas decimais. Linha: {linha.ToString()}");


            return erros.Length == 0;
        }

        private async Task ObterDadosPlanilha(IFormFile file)
        {
            try
            {

                StringBuilder erros = new StringBuilder();

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream).ConfigureAwait(false);

                    using (var package = new ExcelPackage(memoryStream))
                    {
                        for (int i = 1; i <= package.Workbook.Worksheets.Count; i++)
                        {
                            var totalRows = package.Workbook.Worksheets[i].Dimension?.Rows;
                            var totalCollumns = package.Workbook.Worksheets[i].Dimension?.Columns;
                            Produto produto = new Produto();
                            for (int j = 2; j <= totalRows.Value; j++)
                            {
                                produto.Id = 0;
                                produto.DataEntrega = Convert.ToDateTime(package.Workbook.Worksheets[i].Cells[j, 1].Value.ToString());
                                produto.Nome = package.Workbook.Worksheets[i].Cells[j, 2].Value.ToString();
                                produto.Quantidade = Convert.ToInt32(package.Workbook.Worksheets[i].Cells[j, 3].Value.ToString());
                                produto.ValorUnitario = Convert.ToDouble(package.Workbook.Worksheets[i].Cells[j, 4].Value.ToString());

                                if (ValidarProduto(ref erros, produto, j))
                                    Add(produto);
                            }
                        }
                    }
                }

                if (erros.Length > 0)
                    throw new Exception(erros.ToString());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }        
    }
}
