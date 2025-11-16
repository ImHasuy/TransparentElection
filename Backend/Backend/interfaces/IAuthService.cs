using Backend.DTOs;

namespace Backend.interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> AuthenticateAsyncForVoter(FirstLayerLoginDto firstLayerLoginDto);
}