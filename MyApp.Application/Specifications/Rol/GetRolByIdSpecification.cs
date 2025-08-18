using Ardalis.Specification;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Specifications.Rol
{
    public class GetRolByIdSpecification : Specification<RolEntity>
    {
        public GetRolByIdSpecification(Guid rolId)
        {
            Query.Where(x => x.Id == rolId && !x.IsDeleted);
        }
    }
}
