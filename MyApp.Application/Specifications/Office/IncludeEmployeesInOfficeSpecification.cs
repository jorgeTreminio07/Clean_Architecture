using Ardalis.Specification;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Specifications.Office
{
    public class IncludeEmployeesInOfficeSpecification : Specification<OfficeEntity>
    {
        public IncludeEmployeesInOfficeSpecification()
        {
            Query.Where(a => !a.IsDeleted);
            Query.Include(a => a.Employees.Where(b => !b.IsDeleted));
        }
    }
}
