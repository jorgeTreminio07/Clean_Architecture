using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    [Table("Users")]
    public class UserEntity
    {

        public UserEntity()
        {
            Id = Guid.NewGuid();
            IsDelete = false;
        }

        public Guid Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }

        [Required]
        public required Guid RolId { get; set; }
        [ForeignKey("RolId")]
        public RolEntity? Rol { get; set; }
        public string? Avatar { get; set; }
        public bool IsDelete { get; set; }
    }
}
