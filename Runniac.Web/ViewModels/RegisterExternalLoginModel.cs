using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Runniac.Web.ViewModels
{
    public class RegisterExternalLoginModel : AbstractRegisterModel
    {
        public string ExternalLoginData { get; set; }
    }
}