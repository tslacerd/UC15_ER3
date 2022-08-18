using ChapterTeste.Controllers;
using ChapterTeste.Interfaces;
using ChapterTeste.Models;
using ChapterTeste.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.IdentityModel.Tokens.Jwt;

namespace TestIntegracao.Controllers
{
    public class LoginControllerTeste
    {
        [Fact]
        public void LoginController_Retornar_Usuario_Invalido()
        {

            //Arrange
            var repositorioEspelhado = new Mock<IUsuarioRepository>();

            repositorioEspelhado.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns((Usuario)null);

            LoginViewModel dados = new LoginViewModel();
            dados.Email = "tslacerd@mail.com";
            dados.Senha = "12345";

            var controller = new LoginController(repositorioEspelhado.Object);


            //Act
            var resultado = controller.Login(dados);

            //Assert
            Assert.IsType<UnauthorizedObjectResult>(resultado);
        }

        [Fact]
        public void LoginController_Retornar_Token()
        { 
            //Arrange
            Usuario usuarioRetorno = new Usuario();
            usuarioRetorno.Email = "tslacerd@mail.com";
            usuarioRetorno.Senha = "12345";
            usuarioRetorno.Tipo = "1";
            usuarioRetorno.Id = 1;

            var repositorioEspelhado = new Mock<IUsuarioRepository>();

            repositorioEspelhado.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(usuarioRetorno);

            string issuerValido = "chapter.webapi";

            LoginViewModel dados = new LoginViewModel();
            dados.Email = "tslacerd@mail.com";
            dados.Senha = "12345";

            var controller = new LoginController(repositorioEspelhado.Object);


            //Act
            SOkObjectResult resultado = (OkObjectResult)controller.Login(dados);

            string tokenString = resultado.Value.ToString().Split(' ')[3];

            var jwtHandler = new JwtSecurityTokenHandler();
            var tokenJwt = jwtHandler.ReadJwtToken(tokenString);

            //Assert
            Assert.Equal(issuerValido, tokenJwt.Issuer);

        }
    }
}