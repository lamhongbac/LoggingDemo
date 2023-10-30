﻿namespace JWTAPI.Models
{
    public class RefreshTokenDatas
    {
        public RefreshTokenDatas()
        {
            RefreshTokens = new List<RefreshTokenModel>();
        }
        public List<RefreshTokenModel> RefreshTokens { get; set; }

        public void AddToken(RefreshTokenModel token)
        {
            if (!RefreshTokens.Contains(token))
            {
                RefreshTokens.Add(token);
            }
        }
        public void RemoveToken(RefreshTokenModel token)
        {
            if (RefreshTokens.Contains(token))
            {
                RefreshTokens.Remove(token);    
            }
        }
    }
}
