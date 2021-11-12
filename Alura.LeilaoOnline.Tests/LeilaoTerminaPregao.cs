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
        
        //[Fact]//Fact = Fato. Diz para o xUnit que s�o m�todos de teste
        [Theory]//Essa anota��o vai transformar nosso m�todo a partir de algumas coisa
        //precisa passar dados para esse teste
        [InlineData(1200, new double[] {800, 900, 1000, 1200})]//Os dados s�o passados para o construtor do InlineData
        [InlineData(1000,new double[] { 800, 900, 1000, 990 })]
        [InlineData(800, new double[] {800})]
        //os argumentos precisam ser injetados no m�todo para n�o dar erros no InlineData
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] ofertas)
        {
            //Arrange - Cen�rio
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

            //Act - m�todo sob teste.
            leilao.TerminaPregao();

            //Assert
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            //Arrange - cen�rio
            var leilao = new Leilao("Van Gogh");
            //Assert - verifica
            var excecaoObtida = Assert.Throws<System.InvalidOperationException>(
                //Act - m�todo sob teste
                () => leilao.TerminaPregao()
                );
            var msgEsperada = "N�o � poss�vel terminar o preg�o sem que ele tenha come�ado. Para isso utilize o m�todo IniciaPreg�o()";
            Assert.Equal(msgEsperada, excecaoObtida.Message);

        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //Arrange - cen�rio
            var leilao = new Leilao("Van Gogh");
            leilao.IniciaPregao();
            //Act - m�todo sob teste
            leilao.TerminaPregao();
            //Assert - verifica.
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
