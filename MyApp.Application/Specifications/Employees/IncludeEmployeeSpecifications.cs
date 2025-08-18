using Ardalis.Specification;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Specifications.Employees
{
    public class IncludeEmployeeSpecifications : Specification<EmployeeEntity>
    {
        public IncludeEmployeeSpecifications()
        {
            Query.Include(employee => employee.Office);
            Query.Where(employee => !employee.IsDeleted); 
        }
    }
}
