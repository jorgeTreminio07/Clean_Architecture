using MyApp.Application.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Dtos.Office
{
    public class OfficeDtoWithEmployees
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } = string.Empty;

        public List<EmployeeDtoWithoutOffice> Employees { get; set; } = [];
    }
}
