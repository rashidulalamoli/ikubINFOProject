using System;
using System.ComponentModel.DataAnnotations;

namespace ikubINFO.Entity.Dtos
{
    public record UserDto(
        string UserGuid,
        string FirstName,
        string LastName,
        string Initials,
        string FullName,
        string Email,
        string UserName,
        string Phone,
        string Image,
        int RoleId);
    public record CreateUserDto(
        [Required, MaxLength(50)] string FirstName,
        [MaxLength(50)] string LastName,
        [MaxLength(50)] string Initials,
        [MaxLength(150)] string FullName,
        [Required, MaxLength(50), EmailAddress] string Email,
        [Required, MaxLength(50)] string UserName,
        [MaxLength(50)] string Phone,
        [Required, MaxLength(50)] string Password,
        string Image,
        int RoleId);
    public record UpdateUserDto(
        [Required, MaxLength(50)] string FirstName,
        [MaxLength(50)] string LastName,
        [MaxLength(50)] string Initials,
        [MaxLength(150)] string FullName,
        [Required, MaxLength(50), EmailAddress] string Email,
        [Required, MaxLength(50)] string UserName,
        [MaxLength(50)] string Phone,
        [Required, MaxLength(50)] string Password,
        string Image,
        int RoleId);
}