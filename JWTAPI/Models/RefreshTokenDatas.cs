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
        public bool AddToken(RefreshTokenModel newtoken)
        {
            if (RefreshTokens!=null)
            {
                var exist= RefreshTokens.FirstOrDefault(x=>x.Id==newtoken.Id);
                if (exist!=null)
                {
                    throw new Exception("new token is exist");
                    //fasle
                }
                else
                {
                    RefreshTokens.Add(newtoken);
                    return true;
                }
            }
            else
            {
                RefreshTokens = new List<RefreshTokenModel>();
                RefreshTokens.Add(newtoken);
                return true;
            }
           


              
            
        }
        /// <summary>
        /// Remove token kg con gia tri su dung
        /// </summary>
        /// <param name="token"></param>
        public bool RemoveToken(RefreshTokenModel oldtoken)
        {
            if (RefreshTokens != null)
            {
                var exist = RefreshTokens.FirstOrDefault(x => x.Id == oldtoken.Id);
                int index= RefreshTokens.IndexOf(exist);
                if (index != -1)
                {
                    RefreshTokens.RemoveAt(index);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                //RefreshTokens = new List<RefreshTokenModel>();

                throw new Exception("old token is not exist");
            }
        }

        public bool Update(RefreshTokenModel storedToken)
        {
            if (RefreshTokens != null)
            {
                var exist = RefreshTokens.FirstOrDefault(x => x.Id == storedToken.Id);
                int index = RefreshTokens.IndexOf(exist);
                if (index != -1)
                {
                    RefreshTokens[index] = storedToken;
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                //RefreshTokens = new List<RefreshTokenModel>();
                throw new Exception("old token is not exist");
            }
        }
    }
}
