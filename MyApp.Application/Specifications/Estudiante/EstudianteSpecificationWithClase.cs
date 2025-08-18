using Ardalis.Specification;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Specifications.Estudiante
{
    public class EstudianteSpecificationWithClase : Specification<EstudianteEntity>
    {
        public EstudianteSpecificationWithClase()
        {
            Query.Include(a => a.Clases.Where(b => !b.Clases!.IsDeleted))
                .ThenInclude(c => c.Clases);
            Query.Where(d => !d.IsDeleted);
        }
    }
}
