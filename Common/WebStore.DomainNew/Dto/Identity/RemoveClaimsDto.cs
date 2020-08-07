using System.Collections.Generic;
using System.Security.Claims;

namespace WebStore.DomainNew.Dto.Identity
{
    public class RemoveClaimsDto
    {
        public Entities.User User { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }
}