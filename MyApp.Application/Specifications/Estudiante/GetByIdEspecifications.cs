using Ardalis.Specification;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Specifications.Estudiante
{
    public class GetByIdEspecifications : Specification<ClaseEstudianteEntity>
    {
        public GetByIdEspecifications(Guid EstudianteId)
        {
            Query.Where(x => x.EstudianteId == EstudianteId);
        }
    }
}
