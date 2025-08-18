using Microsoft.AspNetCore.Authorization;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Security
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IRolRepository _rolRepository;

        public PermissionAuthorizationHandler(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var userRoleIdClaim = context.User.FindFirst("roleId");

            if (userRoleIdClaim == null || !Guid.TryParse(userRoleIdClaim.Value, out var roleId))
            {
                context.Fail();
                return;
            }

            var permisos = await _rolRepository.GetPermisosByRoleId(roleId);

            if (permisos.Contains(requirement.PermissionName))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
