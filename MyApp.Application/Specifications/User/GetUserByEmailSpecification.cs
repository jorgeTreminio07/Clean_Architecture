using Ardalis.Specification;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Specifications.User
{
    public class GetUserByEmailSpecification : Specification<UserEntity>
    {
        public GetUserByEmailSpecification(string email)
        {
            Query.Where(x => x.Email == email && !x.IsDelete);
        }
    }
}
