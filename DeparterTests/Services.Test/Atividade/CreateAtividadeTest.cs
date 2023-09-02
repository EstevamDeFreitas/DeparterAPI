using Services.DTO;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeparterTests.Services.Test.Atividade
{
    [TestFixture]
    public class CreateAtividadeTest : DeparterBaseTest
    {
        [SetUp]
        public void Setup()
        {
            var funcionario = new FuncionarioDTO
            {
                Apelido = "Funcionario Teste",
                Email = "Teste@teste.com",
                IsAdmin = true,
                Nome = "TESTE",
                Senha = "teste123",
                Imagem = ""
            };
            serviceWrapper.FuncionarioService.CreateFuncionario(funcionario);

        }

        [Test]
        public void NormalCreateTest()
        {
            Assert.IsTrue(true);
        }
    }
}
