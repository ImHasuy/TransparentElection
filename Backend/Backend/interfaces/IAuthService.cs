using Backend.DTOs;

namespace Backend.interfaces;

public interface IAuthService
{
    Task<string> AuthenticateAsyncForVoter(FirstLayerLoginDto firstLayerLoginDto);
}