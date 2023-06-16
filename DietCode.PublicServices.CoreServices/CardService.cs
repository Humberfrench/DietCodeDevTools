using DietCode.PublicServices.Domain.Interfaces.Services;
using System.Text.RegularExpressions;

namespace DietCode.PublicServices.CoreServices
{
    public class CardService : ICardService
    {

        #region Bandeira
        public async Task<string> ValidaBandeira(string card)
        {
            return await Task.Run(() =>
            {
                var bandeira = "ND";
                var visa = new Regex("^4[0-9]{12}(?:[0-9]{3})");
                var mastercard = new Regex("^5[1-5][0-9]{14}");
                var amex = new Regex("^3[47][0-9]{13}");
                var diners = new Regex("^3(?:0[0-5]|[68][0-9])[0-9]{11}");
                var discover = new Regex("^6(?:011|5[0-9]{2})[0-9]{12}");
                var elo = new Regex("^((((636368)|(438935)|(504175)|(451416)|(636297))[0-9]{10})|((5067)|(4576)|(4011))[0-9]{12})");
                var hipercard = new Regex("^(606282)|(3841)$/");

                if (visa.Match(card).Success)
                {
                    bandeira = "VISA";
                }
                else if (mastercard.Match(card).Success)
                {
                    bandeira = "MASTERCARD";
                }
                else if (amex.Match(card).Success)
                {
                    bandeira = "AMEX";
                }
                else if (diners.Match(card).Success)
                {
                    bandeira = "DINERS";
                }
                else if (discover.Match(card).Success)
                {
                    bandeira = "DISCOVER";
                }
                else if (elo.Match(card).Success)
                {
                    bandeira = "ELO";
                }
                else if (hipercard.Match(card).Success)
                {
                    bandeira = "HIPERCARD";
                }

                return bandeira;
            });
        }

        #endregion

        #region Validar Cartão
        private async Task<bool> IsValidLuhnn(string val)
        {
            return await Task.Run(() =>
            {
                int currentDigit;
                int valSum = 0;
                int currentProcNum = 0;

                for (int i = val.Length - 1; i >= 0; i--)
                {
                    //parse to int the current rightmost digit, if fail return false (not-valid id)
                    if (!int.TryParse(val.Substring(i, 1), out currentDigit))
                    {
                        return false;
                    }

                    currentProcNum = currentDigit << (1 + i & 1);
                    //summarize the processed digits
                    valSum += (currentProcNum > 9 ? currentProcNum - 9 : currentProcNum);

                }

                // if digits sum is exactly divisible by 10, return true (valid), else false (not-valid)
                // valSum must be greater than zero to avoid validate 0000000...00 value
                return (valSum > 0 && valSum % 10 == 0);
            });
        }

        public async Task<bool> IsValidCreditCardNumber(string card)
        {
            // rule #1, must be only numbers
            if (!card.All(char.IsDigit))
            {
                return false;
            }
            // rule #2, must have at least 12 and max of 19 digits
            if (12 > card.Length || card.Length > 19)
            {
                return false;
            }
            // rule #3, must pass Luhnn Algorithm
            return await IsValidLuhnn(card);

        }
        #endregion

        #region Gerar Cartao

        public async Task<string> GerarNumeroCartaoCreditoAsync(string bandeira)
        {
            Random random = new Random();
            string numeroCartao = "";

            switch (bandeira)
            {
                case "Visa":
                    numeroCartao += "4";
                    for (int i = 0; i < 15; i++)
                    {
                        int digito = random.Next(0, 10);
                        numeroCartao += digito.ToString();
                    }
                    break;
                case "Mastercard":
                    numeroCartao += "5";
                    int segundoDigito = random.Next(1, 6);
                    numeroCartao += segundoDigito.ToString();
                    for (int i = 0; i < 13; i++)
                    {
                        int digito = random.Next(0, 10);
                        numeroCartao += digito.ToString();
                    }
                    break;
                // Adicione outros casos para bandeiras adicionais
                default:
                    throw new ArgumentException("Bandeira de cartão inválida");
            }

            int digitoVerificador = await CalcularDigitoVerificadorAsync(numeroCartao);
            numeroCartao += digitoVerificador.ToString();

            return numeroCartao;
        }

        public async Task<int> CalcularDigitoVerificadorAsync(string numeroCartao)
        {
            int soma = 0;
            bool dobrar = false;

            for (int i = numeroCartao.Length - 1; i >= 0; i--)
            {
                int digito = int.Parse(numeroCartao[i].ToString());

                if (dobrar)
                {
                    digito *= 2;

                    if (digito > 9)
                        digito -= 9;
                }

                soma += digito;
                dobrar = !dobrar;
            }

            int digitoVerificador = (10 - (soma % 10)) % 10;

            // Simulando uma operação assíncrona com Task.Delay
            await Task.Delay(100);

            return digitoVerificador;
        }

        #endregion
    }
}