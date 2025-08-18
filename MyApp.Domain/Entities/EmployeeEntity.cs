using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("Employees")]
    public class EmployeeEntity
    {
        public EmployeeEntity()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }


        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public required Guid OfficeId { get; set; }
        [ForeignKey("OfficeId")]
        public OfficeEntity? Office { get; set; }

        public bool IsDeleted { get; set; }
    }
}
