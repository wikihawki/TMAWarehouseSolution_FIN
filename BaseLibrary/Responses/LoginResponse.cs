

namespace BaseLibrary.Responses
{
    public record class LoginResponse(bool Flag, string Message = null!, string Token = null!, string RefreshToken = null!);

}
