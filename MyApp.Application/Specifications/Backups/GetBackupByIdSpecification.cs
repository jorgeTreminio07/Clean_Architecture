using Ardalis.Specification;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Specifications.Backups
{
    public class GetBackupByIdSpecification : Specification<BackupEntity>
    {
        public GetBackupByIdSpecification(Guid backupId)
        {
            Query.Where(x => x.Id == backupId);
        }
    }
}
