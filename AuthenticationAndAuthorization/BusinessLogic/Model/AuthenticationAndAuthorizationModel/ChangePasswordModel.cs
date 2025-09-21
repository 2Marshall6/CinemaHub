using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model.AuthenticationAndAuthorizationModel
{
    public record ChangePasswordModel
    (
        [Required] string Email,
        [Required] string OldPassword,
        [Required] string NewPassword
    );
}
