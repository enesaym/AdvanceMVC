using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace AdvanceUI.Helpers
{
	public class TokenHelper
	{
		public static string GetIdFromToken(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

			if (jwtToken != null)
			{
				var claims = jwtToken.Claims;
				var idClaim = claims.FirstOrDefault(c => c.Type == "ID");

				if (idClaim != null)
				{
					string idValue = idClaim.Value;
					return idValue;
				}
			}

			return null;
		}
	}
}
