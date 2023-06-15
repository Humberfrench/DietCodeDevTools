using DietCode.PublicServices.Domain.Interfaces.Services;
using System.Text.RegularExpressions;

namespace DietCode.PublicServices.CoreServices
{
    public class ValidatorService : IValidatorService
    {

        #region Documentos
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
    }
}