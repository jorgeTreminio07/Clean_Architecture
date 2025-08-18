using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Dtos.Office
{
    public class UpdateOfficeReqDto
    {
        [Required]
        public required Guid Id { get; set; }
        [Required]
        public required string Name { get; set; }
    }
}
