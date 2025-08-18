using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("Office")]
    public class OfficeEntity
    {
        public OfficeEntity()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        public required string Name { get; set; }

        public bool IsDeleted { get; set; }

        public List<EmployeeEntity> Employees { get; set; } = [];
    }
}
