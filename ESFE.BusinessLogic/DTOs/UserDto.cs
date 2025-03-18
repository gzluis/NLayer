using ESFE.Entities;

namespace ESFE.BusinessLogic.DTOs;


public class UserResponse
{
    public int UserId { get; set; }

    public int RolId { get; set; }

    public string? UserName { get; set; }

    public string? UserNickname { get; set; }

    public bool? UserStatus { get; set; }

    public string? RolName { get; set; }
}


public class CreateUserRequest
{
    public int RolId { get; set; }

    public string? UserName { get; set; }

    public string? UserNickname { get; set; }

    public string? UserPassword { get; set; }

    public bool? UserStatus { get; set; }

    public DateOnly? RegistrationDate { get; set; }
}


public class UpdateUserRequest
{
    public int UserId { get; set; }

    public int RolId { get; set; }

    public string? UserName { get; set; }

    public string? UserNickname { get; set; }

    public string? UserPassword { get; set; }

    public bool? UserStatus { get; set; }

    public DateOnly? RegistrationDate { get; set; }
}

public class RoleResponse
{
    public int RolId { get; set; }

    public string? RolName { get; set; }
}


public class UserByIdResponse
{
    public int UserId { get; set; }

    public int RolId { get; set; }

    public string? UserName { get; set; }

    public string? UserNickname { get; set; }

    public string? UserPassword { get; set; }

    public bool? UserStatus { get; set; }

    public DateOnly? RegistrationDate { get; set; } 
}
