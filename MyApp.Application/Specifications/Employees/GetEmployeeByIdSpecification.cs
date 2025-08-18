using Ardalis.Specification;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Specifications.Employees
{
    public class GetEmployeeByIdSpecification : Specification<EmployeeEntity>
    {
        public GetEmployeeByIdSpecification(Guid employeeId)
        {
            Query.Where(x => x.Id == employeeId && !x.IsDeleted);
            Query.Include(b => b.Office);
        }
    }
}
