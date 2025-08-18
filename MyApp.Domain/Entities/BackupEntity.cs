using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("Backups")]
    public class BackupEntity
    {
        public BackupEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
        public Guid Id { get; set; }
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Url { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
