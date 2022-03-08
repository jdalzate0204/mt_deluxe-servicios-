using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.Security.Claims;
using Utilitarios;
using Logica;

namespace ApiServicios.Seguridad
{
    //Clase que genera el token
    internal static class TokenGeneratorCo
    {
        public static string GenerateTokenJwt(Conductor conductor)
        {
            //TODO: appsetting for Demo JWT - protect correctly this settings
            //Llaves para permitir manipulación o cambios
            var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
            var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
            var expireTime = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];

            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // create a claimsIdentity 
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, conductor.Usuario),
                new Claim(ClaimTypes.Role, conductor.Rol.ToString())
            });

            // create token to the user 
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler(); //Se crea objeto que tiene el estandar JWT
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.Now, //fecha de creación del token
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(expireTime)), //fecha de expiración de token
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);

            ConductorTokenLogin token = new ConductorTokenLogin();
            token.FechaGenerado = DateTime.Now;
            token.FechaVigencia = DateTime.Now.AddMinutes(Convert.ToInt32(expireTime));
            token.IdConductor = conductor.IdConductor;
            token.Token = jwtTokenString;
            new LConductor().guardarToken(token);
            return jwtTokenString;
        }
    }
}
