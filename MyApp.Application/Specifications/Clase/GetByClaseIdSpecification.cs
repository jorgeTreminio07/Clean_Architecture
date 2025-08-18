using Ardalis.Specification;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Specifications.Clase
{
    public class GetByClaseIdSpecification : Specification<ClaseEstudianteEntity>
    {
        public GetByClaseIdSpecification(Guid claseId)
        {
            Query.Where(a => a.ClaseId == claseId);

        }
    }
}
