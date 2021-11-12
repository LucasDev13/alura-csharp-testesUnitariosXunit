using Xunit;
using Alura.LeilaoOnline.Core;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        
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

            foreach(var valor in ofertas)
            {
                leilao.RecebeLance(fulano, valor);
            }

            //Act - m�todo sob teste.
            leilao.TerminaPregao();

            //Assert
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //Arrange
            var leilao = new Leilao("Van Gogh");
            //Act
            leilao.TerminaPregao();
            //Assert
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
