using Ardalis.Specification;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Specifications.Office
{
    public class GetOfficeByIdSpecification : Specification<OfficeEntity>
    {
        public GetOfficeByIdSpecification(Guid officeId)
        {
            Query.Where(x => x.Id == officeId && !x.IsDeleted);
            Query.Include(a => a.Employees.Where(b => !b.IsDeleted));
        }
    }
}
