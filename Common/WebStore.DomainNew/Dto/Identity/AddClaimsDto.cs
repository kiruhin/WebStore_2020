using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace WebStore.DomainNew.Dto.Identity
{
    public class AddClaimsDto
    {
        public Entities.User User { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }
}
