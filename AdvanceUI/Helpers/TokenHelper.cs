using AdvanceUI.Models.DTO.UserInfo;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace AdvanceUI.Helpers
{
	public class TokenHelper
	{
		public static UserInfoDTO GetUserInfoFromToken(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

			if (jwtToken != null)
			{
				var claims = jwtToken.Claims;
				var idClaim = claims.FirstOrDefault(c => c.Type == "ID");
				var nameClaim=claims.FirstOrDefault(c => c.Type == "Name");
				var surnameClaim = claims.FirstOrDefault(c => c.Type == "Surname");
				var emailClaim = claims.FirstOrDefault(c => c.Type == "Email");
				var titleNameClaim = claims.FirstOrDefault(c => c.Type == "TitleName");
				var titleIDClaim = claims.FirstOrDefault(c => c.Type == "TitleID");
				if (idClaim != null || nameClaim!=null)
				{
					string idValue = idClaim.Value;
					string nameValue = nameClaim.Value;
					string surnameValue= surnameClaim.Value;
					string emailValue = emailClaim.Value;
					string titleNameValue= titleNameClaim.Value;
					string titleIdValue = titleIDClaim.Value;
					return new UserInfoDTO
					{
						ID=idValue, Name=nameValue,Email=emailValue,Surname=surnameValue,TitleName=titleNameValue,TitleID=titleIdValue
					};
				}
			}

			return null;
		}
	}
}
