using Ardalis.Specification;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Specifications.User
{
    public class GetUserByIdSpecification : Specification<UserEntity>
    {
        public GetUserByIdSpecification(Guid userId)
        {
            Query.Where(x => x.Id == userId && !x.IsDelete);
            Query.Include(c => c.Rol);
        }
    }
}
