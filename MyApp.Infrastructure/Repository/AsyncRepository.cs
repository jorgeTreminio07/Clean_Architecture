using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Repository
{
    public class AsyncRepository<T> : RepositoryBase<T>, IAsyncRepository<T>  where T : class 
    {
        private readonly ApplicationDbContext _dbContext;

        public AsyncRepository(ApplicationDbContext dbContext) : base(dbContext) 
        {
            this._dbContext = dbContext;
        }

        public async Task<List<string>> GetPermisosByRoleId(Guid roleId)
        {
            return await _dbContext.RolPermisos
                .Where(rp => rp.RolId == roleId)
                .Include(rp => rp.Permisos)
                .Select(rp => rp.Permisos!.Name)
                .ToListAsync();
        }

    }
}
