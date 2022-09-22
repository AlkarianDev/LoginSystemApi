using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618
namespace AllegicWebApi.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required, EmailAddress]
        
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordConfirm { get; set; }
    }
}
