using DietCode.PublicServices.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietCode.PublicServices.CoreServices
{
    public class DocGeneratoratorService : IDocGeneratoratorService
    {
         private readonly Random random;
       public DocGeneratoratorService() 
        {
            random = new Random();
        }

        //implements a random cpf generator

        public async Task<string> GerarCPF()
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
            return string.Concat(cpfArray) + digito1 + digito2;
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

        public async Task<string> GerarCNPJ()
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
    }
}
