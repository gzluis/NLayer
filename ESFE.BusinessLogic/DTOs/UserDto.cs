using ESFE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFE.BusinessLogic.DTOs;

public class UserResponse
{
    public int UserId { get; set; } 

    public string? UserName { get; set; }

    public string? UserNickname { get; set; }

    public string? RolName { get; set; }
}
