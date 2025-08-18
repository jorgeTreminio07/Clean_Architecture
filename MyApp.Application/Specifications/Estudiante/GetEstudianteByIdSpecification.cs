using Ardalis.Specification;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Specifications.Estudiante
{
    public class GetEstudianteByIdSpecification : Specification<EstudianteEntity>
    {
        public GetEstudianteByIdSpecification(Guid estudianteId)
        {
            Query.Where(x => x.Id == estudianteId && !x.IsDeleted);
            Query.Include(a => a.Clases.Where(b => !b.Clases!.IsDeleted)).ThenInclude(c => c.Clases);

        }
    }
}
