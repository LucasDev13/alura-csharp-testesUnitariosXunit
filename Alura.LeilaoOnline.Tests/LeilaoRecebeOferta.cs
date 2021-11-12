using Alura.LeilaoOnline.Core;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeOferta
    {
         [Theory]
         [InlineData(2, new double[] {800, 900 })]
         [InlineData(4, new double[] {1000, 1200, 1400, 1300})]
         public void NaoPermiteNovosLancesDadoLeilaoFinalizado(
             int qtdEsperada, double[] ofertas )
        {
            //Arrange - Cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            foreach(var valor in ofertas)
            {
                leilao.RecebeLance(fulano, valor);
            }
            leilao.TerminaPregao();

            //Act - método sob teste.
            leilao.RecebeLance(fulano, 1000);

            //Assert
            var qtdObtido = leilao.Lances.Count();
            Assert.Equal(qtdEsperada, qtdObtido);
        }
    }
}
