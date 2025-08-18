using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interface.Security
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string roleId, int time = 8);

        bool ValidateToken(string token);

        string? GetUserIdFromToken(string token);
    }
}
