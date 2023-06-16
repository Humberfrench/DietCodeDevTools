using DietCode.PublicServices.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietCode.PublicServices.CoreServices
{
    public class DocumentService : IDocumentService
    {
         private readonly Random random;
       public DocumentService() 
        {
            random = new Random();
        }

        #region Gerador de documentos


        //implements a random cpf generator

        public async Task<string> GerarCPF(bool formatado = false)
        {
            int[] cpfArray = new int[9];

            // Gera os primeiros 9 dígitos do CPF de forma assíncrona
            await Task.Run(() =>
            {
                for (int i = 0; i < 9; i++)
                {
                    cpfArray[i] = random.Next(0, 9);
                }
            });

            // Calcula o primeiro dígito verificador do CPF
            int digito1 = await CalcularDigitoVerificadorCPF(cpfArray, 10);

            // Calcula o segundo dígito verificador do CPF
            cpfArray[9] = digito1;
            int digito2 = await CalcularDigitoVerificadorCPF(cpfArray, 11);

            // Concatena os dígitos para formar o CPF completo
            var cpf = string.Concat(cpfArray) + digito1 + digito2;

            if (formatado)
            {
                cpf = string.Format("{0:000\\.000\\.000\\-00}", cpf);
            }

            return cpf;
        }

        private async Task<int> CalcularDigitoVerificadorCPF(int[] cpfArray, int pesoInicial)
        {
            int soma = 0;

            for (int i = 0; i < cpfArray.Length; i++)
            {
                soma += cpfArray[i] * pesoInicial;
                pesoInicial--;
            }

            int resto = soma % 11;
            int digito = 11 - resto;

            if (digito >= 10)
            {
                digito = 0;
            }

            await Task.Delay(100); // Simula uma operação assíncrona

            return digito;
        }

        public async Task<string> GerarCNPJ(bool formatado=false)
        {
            int[] cnpjArray = new int[12];

            // Gera os primeiros 12 dígitos do CNPJ de forma assíncrona
            await Task.Run(() =>
            {
                for (int i = 0; i < 12; i++)
                {
                    cnpjArray[i] = random.Next(0, 9);
                }
            });

            // Calcula o primeiro dígito verificador do CNPJ
            int digito1 = await CalcularDigitoVerificadorCNPJ(cnpjArray, 5, 9);

            // Calcula o segundo dígito verificador do CNPJ
            int digito2 = await CalcularDigitoVerificadorCNPJ(cnpjArray, 6, 9);

            // Concatena os dígitos para formar o CNPJ completo
            string cnpj = string.Concat(cnpjArray) + digito1 + digito2;

            if (formatado)
            {
                cnpj = string.Format("{0:00\\.000\\.000\\/0000\\-00}", cnpj);
            }

            return cnpj;
        }

        private async Task<int> CalcularDigitoVerificadorCNPJ(int[] cnpjArray, int pesoInicial, int pesoFinal)
        {
            int soma = 0;
            int multiplicador = pesoInicial;

            for (int i = 0; i < pesoFinal; i++)
            {
                soma += cnpjArray[i] * multiplicador;
                multiplicador--;

                if (multiplicador < 2)
                {
                    multiplicador = 9;
                }
            }

            int resto = soma % 11;
            int digito = resto < 2 ? 0 : 11 - resto;

            await Task.Delay(100); // Simula uma operação assíncrona

            return digito;
        }
        #endregion

        #region Validador de documentos
        public async Task<bool> ValidarCpf(string cpf)
        {
            return await Task.Run(() =>
            {
                var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                cpf = cpf.Trim().Replace(".", "").Replace("-", "");
                if (cpf.Length != 11)
                {
                    return false;
                }

                for (var j = 0; j < 10; j++)
                {
                    if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    {
                        return false;
                    }
                }

                var tempCpf = cpf.Substring(0, 9);
                var soma = 0;

                for (var i = 0; i < 9; i++)
                {
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                }

                var resto = soma % 11;
                if (resto < 2)
                {
                    resto = 0;
                }
                else
                {
                    resto = 11 - resto;
                }

                var digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (var i = 0; i < 10; i++)
                {
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                }

                resto = soma % 11;
                if (resto < 2)
                {
                    resto = 0;
                }
                else
                {
                    resto = 11 - resto;
                }

                digito = digito + resto.ToString();

                return cpf.EndsWith(digito);
            });
        }

        public async Task<bool> ValidarCnpj(string cnpj)
        {
            return await Task.Run(() =>
            {
                var multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                var multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

                cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
                if (cnpj.Length != 14)
                {
                    return false;
                }

                var tempCnpj = cnpj.Substring(0, 12);
                var soma = 0;

                for (var i = 0; i < 12; i++)
                {
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
                }

                var resto = (soma % 11);
                if (resto < 2)
                {
                    resto = 0;
                }
                else
                {
                    resto = 11 - resto;
                }

                var digito = resto.ToString();
                tempCnpj = tempCnpj + digito;
                soma = 0;
                for (var i = 0; i < 13; i++)
                {
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
                }

                resto = (soma % 11);
                if (resto < 2)
                {
                    resto = 0;
                }
                else
                {
                    resto = 11 - resto;
                }

                digito = digito + resto.ToString();

                return cnpj.EndsWith(digito);
            });
        }

        #endregion
    }
}
