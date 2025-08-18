using Ardalis.Specification;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Specifications.Clase
{
    public class GetClaseByIdSpecification : Specification<ClaseEntity>
    {
        public GetClaseByIdSpecification(Guid claseId)
        {
            Query.Where(x => x.Id == claseId && !x.IsDeleted);
            Query.Include(a => a.Estudiantes.Where(b => !b.Estudiantes!.IsDeleted)).ThenInclude(c => c.Estudiantes);
        }
    }
}
