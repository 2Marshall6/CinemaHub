using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IJwtService
    {
        public string GenerateJwt(Guid userId, string userName, IList<string> roles);
    }
}
