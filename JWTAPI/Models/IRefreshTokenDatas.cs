namespace JWTAPI.Models
{
    public interface IRefreshTokenDatas
    {
        List<RefreshTokenModel> RefreshTokens { get; set; }

        bool AddToken(RefreshTokenModel newtoken);
        bool RemoveToken(RefreshTokenModel oldtoken);
        bool Update(RefreshTokenModel storedToken);
    }
}