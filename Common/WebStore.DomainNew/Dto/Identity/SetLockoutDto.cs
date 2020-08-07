using System;

namespace WebStore.DomainNew.Dto.Identity
{
    public class SetLockoutDto
    {
        public Entities.User User { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}