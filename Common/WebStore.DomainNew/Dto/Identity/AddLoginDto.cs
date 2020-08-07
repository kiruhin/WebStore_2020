using Microsoft.AspNetCore.Identity;

namespace WebStore.DomainNew.Dto.Identity
{
    public class AddLoginDto
    {
        public Entities.User User { get; set; }
        public UserLoginInfo UserLoginInfo { get; set; }
    }
}