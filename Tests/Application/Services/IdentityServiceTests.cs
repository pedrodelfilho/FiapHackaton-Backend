using Application.Services;
using Domain.Entities;
using Entities.Request;
using Infra.Mail;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Tests.Application.Services
{

    public class IdentityServiceTests
    {
        private readonly SignInManager<UserIdentity> _signInManager;
        private readonly Mock<UserManager<UserIdentity>> _userManagerMock;
        private readonly IOptions<JwtOptions> _jwtOptions;
        private readonly Mock<IEnvioEmail> _emailServiceMock;
        private readonly IdentityService _identityService;

        public IdentityServiceTests()
        {
            var userStore = new Mock<IUserStore<UserIdentity>>();
            _userManagerMock = new Mock<UserManager<UserIdentity>>(
                userStore.Object, null, null, null, null, null, null, null, null);

            var contextAccessor = new Mock<IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<UserIdentity>>();
            var optionsIdentity = Options.Create(new IdentityOptions());
            var logger = new Mock<ILogger<SignInManager<UserIdentity>>>();
            var schemes = new Mock<IAuthenticationSchemeProvider>();
            var confirmation = new Mock<IUserConfirmation<UserIdentity>>();

            _signInManager = new SignInManager<UserIdentity>(
                _userManagerMock.Object,
                contextAccessor.Object,
                claimsFactory.Object,
                optionsIdentity,
                logger.Object,
                schemes.Object);

            _jwtOptions = Options.Create(new JwtOptions
            {
                Issuer = "test-issuer",
                Audience = "test-audience",
                AccessTokenExpiration = 60
            });

            _emailServiceMock = new Mock<IEnvioEmail>();

            _identityService = new IdentityService(
                _signInManager,
                _userManagerMock.Object,
                _jwtOptions,
                _emailServiceMock.Object);
        }

        [Fact]
        [Trait("Categoria", "IdentityService - Cadastro")]
        public async Task CadastrarUsuario_DeveRetornarSucesso_QuandoUsuarioValido()
        {
            // Arrange
            var request = GetUsuarioCadastroRequest();

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<UserIdentity>(), request.Senha))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(x => x.FindByNameAsync(request.Email))
                .ReturnsAsync(new UserIdentity { Email = request.Email });

            _userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<UserIdentity>(), "Paciente"))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(x => x.GenerateEmailConfirmationTokenAsync(It.IsAny<UserIdentity>()))
                .ReturnsAsync("fake-token");

            // Act
            var result = await _identityService.CadastrarUsuario(request);

            // Assert
            Assert.True(result.Sucesso);
            Assert.Contains("Link de confirmação", result.Message);
        }

        [Fact]
        [Trait("Categoria", "IdentityService - Cadastro")]
        public async Task CadastrarUsuario_DeveRetornarErro_QuandoCriacaoFalhar()
        {
            // Arrange
            var request = GetUsuarioCadastroRequest();

            var erros = IdentityResult.Failed(new IdentityError { Description = "Erro simulado" });

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<UserIdentity>(), request.Senha))
                .ReturnsAsync(erros);

            // Act
            var result = await _identityService.CadastrarUsuario(request);

            // Assert
            Assert.False(result.Sucesso);
            Assert.Contains("Erro simulado", result.Erros[0]);
        }

        private UsuarioCadastroRequest GetUsuarioCadastroRequest()
        {
            return new UsuarioCadastroRequest
            {
                Email = "teste@exemplo.com",
                Nome = "Teste",
                Cpf = "12345678900",
                Crm = "123456",
                DataNascimento = DateTime.Now.AddYears(-30),
                IdEspecialidade = 1,
                Senha = "Senha@123"
            };
        }
    }

}
