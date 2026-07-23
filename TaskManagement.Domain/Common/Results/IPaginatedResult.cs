using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Common.Results
{
    public interface IPaginatedResult
    {
        object Items { get; }

        int PageNumber { get; }

        int PageSize { get; }

        int TotalCount { get; }
    }
}
