using Ardalis.Specification;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Specifications.Rol
{
    public class GetRolByNameSpecification : Specification<RolEntity>
    {
        public GetRolByNameSpecification(string rolName)
        {
            Query.Where(x => x.Name == rolName && !x.IsDeleted);
        }
    }
}
