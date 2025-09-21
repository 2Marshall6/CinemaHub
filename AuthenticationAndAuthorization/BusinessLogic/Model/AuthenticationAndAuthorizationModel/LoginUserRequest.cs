using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public record LoginUserRequest
    (
        [Required] string Email,
        [Required] string Password
    );
}
