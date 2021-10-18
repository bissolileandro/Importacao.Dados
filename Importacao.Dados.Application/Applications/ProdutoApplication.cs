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

namespace Importacao.Dados.Application.Applications
{
    public class ProdutoApplication : ApplicationBase<Produto>, IProdutoApplication
    {
        public ProdutoApplication(IProdutoService produtoService)
            :base(produtoService)
        {

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

        private async Task ObterDadosPlanilha(IFormFile file)
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
                            
                            Add(produto);
                        }
                    }
                }
            }
        }
       
    }
}
