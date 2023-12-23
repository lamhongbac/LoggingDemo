namespace JWTAPI.Models
{
    public class RefreshTokenDatas
    {
        public RefreshTokenDatas()
        {
            RefreshTokens = new List<RefreshTokenModel>();
        }
        public List<RefreshTokenModel> RefreshTokens { get; set; }

        /// <summary>
        /// usage:
        /// khi tao ra 1 refresh token moi thi goi ham nay de add vao csdl
        /// </summary>
        /// <param name="newtoken"></param>
        /// <exception cref="Exception"></exception>
        public void AddToken(RefreshTokenModel newtoken)
        {
            if (RefreshTokens!=null)
            {
                var exist= RefreshTokens.FirstOrDefault(x=>x.Id==newtoken.Id);
                if (exist!=null)
                {
                    throw new Exception("token is exist");
                }

            }
            else
            {
                RefreshTokens = new List<RefreshTokenModel>();
            }
            RefreshTokens.Add(newtoken);


                RefreshTokens.Add(newtoken);
            
        }
        public void RemoveToken(RefreshTokenModel token)
        {
            if (RefreshTokens.Contains(token))
            {
                RefreshTokens.Remove(token);    
            }
        }

        internal void Update(RefreshTokenModel storedToken)
        {
            throw new NotImplementedException();
        }
    }
}
