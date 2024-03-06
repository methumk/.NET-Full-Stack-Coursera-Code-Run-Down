using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReactMVC.Server.Models;

public partial class UserDataModel : UserLoginModel
{
    [Required]
    public string Username { get; set; } = null!;
}
