using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Abstractions.Identity
{
    public sealed record UserTokenData(
        string UserId,
        string Email,
        string? UserName = null,
        IReadOnlyCollection<string>? Roles = null
    );
}
