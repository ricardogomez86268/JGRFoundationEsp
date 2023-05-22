using JGRFoundation.Shared.Entities;

namespace JGRFoundation.Shared.DTOs
{
    public class TokenDTO
    {
        public string Token { get; set; } = null!;

        public DateTime Expiration { get; set; }

    }
}
