using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;

namespace FinalAutorization.Servivces.JWTData
{

    public class AuthenticateController:ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        public AuthenticateController(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        
    }


}
