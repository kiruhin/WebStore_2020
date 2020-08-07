namespace WebStore.DomainNew.Dto.Identity
{
    public class PasswordHashDto
    {
        public Entities.User User { get; set; }
        public string Hash { get; set; }
    }
}