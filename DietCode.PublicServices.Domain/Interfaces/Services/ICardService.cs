﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietCode.PublicServices.Domain.Interfaces.Services
{
    public interface ICardService
    {
        Task<string> ValidaBandeira(string card);
        Task<bool> IsValidCreditCardNumber(string cc);
    }
}
