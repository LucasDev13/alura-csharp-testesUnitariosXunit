using Xunit;
using Alura.LeilaoOnline.Core;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1200, 1250, new double[] {800, 1150, 1400, 1250})]
        public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidade(
            double valorDestino, double valorEsperado, double[] ofertas)
        {
            //Arrange
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            for (int i = 0; i < ofertas.Length; i++)
            {
                if ((i % 2 == 0))
                {
                    leilao.RecebeLance(fulano, ofertas[i]);
                }
                else
                {
                    leilao.RecebeLance(maria, ofertas[i]);
                }
            }

            //Act
            leilao.TerminaPregao();

            //Assert
            Assert.Equal(valorEsperado, leilao.Ganhador.Valor);
        }
        
        //[Fact]//Fact = Fato. Diz para o xUnit que são métodos de teste
        [Theory]//Essa anotação vai transformar nosso método a partir de algumas coisa
        //precisa passar dados para esse teste
        [InlineData(1200, new double[] {800, 900, 1000, 1200})]//Os dados são passados para o construtor do InlineData
        [InlineData(1000,new double[] { 800, 900, 1000, 990 })]
        [InlineData(800, new double[] {800})]
        //os argumentos precisam ser injetados no método para não dar erros no InlineData
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] ofertas)
        {
            //Arrange - Cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();
            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if ((i % 2) == 0)
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }

            //Act - método sob teste.
            leilao.TerminaPregao();

            //Assert
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            //Arrange - cenário
            var leilao = new Leilao("Van Gogh");
            //Assert - verifica
            var excecaoObtida = Assert.Throws<System.InvalidOperationException>(
                //Act - método sob teste
                () => leilao.TerminaPregao()
                );
            var msgEsperada = "Não é possível terminar o pregão sem que ele tenha começado. Para isso utilize o método IniciaPregão()";
            Assert.Equal(msgEsperada, excecaoObtida.Message);

        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //Arrange - cenário
            var leilao = new Leilao("Van Gogh");
            leilao.IniciaPregao();
            //Act - método sob teste
            leilao.TerminaPregao();
            //Assert - verifica.
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
