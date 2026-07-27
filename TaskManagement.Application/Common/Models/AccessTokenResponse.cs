using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Common.Models
{
    public sealed record AccessTokenResponse(
        string AccessToken,
        DateTime ExpiresAtUtc
    );
}
