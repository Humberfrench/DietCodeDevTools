using Dietcode.Core.DomainValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietCode.PublicServices.Domain.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        ValidationResult SaveChanges();
        Task<ValidationResult> SaveChangesAsync();

    }
}
