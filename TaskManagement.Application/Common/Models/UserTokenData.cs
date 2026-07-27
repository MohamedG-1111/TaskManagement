using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Common.Models
{
    public sealed record UserTokenData(
        string UserId,
        string Email,
        string? UserName = null,
        IReadOnlyCollection<string>? Roles = null
    );
}
