using Ardalis.Specification;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Specifications.User
{
    public class GetAllUserSpecifications : Specification<UserEntity>
    {
        public GetAllUserSpecifications()
        {
            Query.Where(x => !x.IsDelete);
            Query.Include(x => x.Rol);
        }
    }
}
