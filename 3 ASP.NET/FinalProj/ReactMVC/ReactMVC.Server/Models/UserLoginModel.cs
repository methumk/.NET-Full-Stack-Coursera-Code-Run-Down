using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReactMVC.Server.Models;

public partial class UserLoginModel
{
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

}
