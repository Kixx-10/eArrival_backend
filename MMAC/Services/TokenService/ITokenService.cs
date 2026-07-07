namespace MMAC.Services.TokenService
{
    public interface ITokenService
    {
        Task<String> CreateToken(Guid travellerId);
    }
}
