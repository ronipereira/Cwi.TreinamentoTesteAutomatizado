using System;

namespace Cwi.TreinamentoTesteAutomatizado.Models
{
    public class AuthenticationResponse
    {
        public DateTime Created { get; set; }

        public DateTime Expiration { get; set; }

        public string AccessToken { get; set; }
    }
}
