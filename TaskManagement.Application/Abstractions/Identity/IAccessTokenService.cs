using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common.Models;

namespace TaskManagement.Application.Abstractions.Identity
{
    public interface IAccessTokenService
    {
        AccessTokenResponse GenerateToken(UserTokenData user);
    }
}
