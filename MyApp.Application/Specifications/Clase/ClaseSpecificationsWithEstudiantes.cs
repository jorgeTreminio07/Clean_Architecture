using Ardalis.Specification;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Specifications.Clase
{
    public class ClaseSpecificationsWithEstudiantes : Specification<ClaseEntity>
    {
        public ClaseSpecificationsWithEstudiantes()
        {
            Query.Include(a => a.Estudiantes.Where(b => !b.Estudiantes!.IsDeleted))
            .ThenInclude(c => c.Estudiantes);

            Query.Where(d => !d.IsDeleted);
        }
    }
}
