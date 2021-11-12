using Alura.LeilaoOnline.Core;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeOferta
    {
        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimoLance()
        {
            //Arrange - Cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            leilao.IniciaPregao();
            leilao.RecebeLance(fulano, 800);

            //Act - método sob teste.
            leilao.RecebeLance(fulano, 1000);

            //Assert
            var qtdEsperada = 1; 
            var qtdObtido = leilao.Lances.Count();
            Assert.Equal(qtdEsperada, qtdObtido);
        } 

         [Theory]
         [InlineData(2, new double[] {800, 900 })]
         [InlineData(4, new double[] {1000, 1200, 1400, 1300})]
         public void NaoPermiteNovosLancesDadoLeilaoFinalizado(
             int qtdEsperada, double[] ofertas )
        {
            //Arrange - Cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao(); 
            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if ((i%2)==0)
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
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
