using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.App.Messages
{
    public record ProjectCreatedMessage (Guid projectId)
    {
    }
}
