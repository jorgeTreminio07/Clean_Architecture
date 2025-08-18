using Ardalis.Specification;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Specifications.Rol
{
    public class GetAllRolesSpecifications : Specification<RolEntity>
    {
        public GetAllRolesSpecifications()
        {
            Query.Where(x => !x.IsDeleted);
            Query.Include(a => a.UserEntities.Where(b => !b.IsDelete));
            Query.Include(c => c.Permisos).ThenInclude(d => d.Permisos);
        }
    }
}
