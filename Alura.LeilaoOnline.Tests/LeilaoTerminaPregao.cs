using Xunit;
using Alura.LeilaoOnline.Core;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        
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

            foreach(var valor in ofertas)
            {
                leilao.RecebeLance(fulano, valor);
            }

            //Act - método sob teste.
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
