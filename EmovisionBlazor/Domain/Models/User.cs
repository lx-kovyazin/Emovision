using System;
using System.Collections.Generic;

namespace EmovisionBlazor.Domain.Models
{
    /// <summary>
    /// Таблица пользователя системы.
    /// </summary>
    public partial class User
        : Microsoft.AspNetCore.Identity.IdentityUser
    {
        public User()
        {
            Sessions = new HashSet<Session>();
        }

        public string Name { get; set; } = null!;
        public string PasswordSha256 { get; set; } = null!;
        public string? Email { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }
}
