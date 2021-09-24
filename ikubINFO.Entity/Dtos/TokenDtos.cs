using System;
using System.ComponentModel.DataAnnotations;

namespace ikubINFO.Entity.Dtos
{
    public record JwtTokenInfo(string Grant_type, string email, string Password, string Refreshtoken);
    public record TokenResult([Required] string Access_token, DateTimeOffset? Expiration, string UserEmail, int StatusCode,string Message);
    public record TokenInfo(string UserName, string UserGuid, int StatusCode,string Message);
}