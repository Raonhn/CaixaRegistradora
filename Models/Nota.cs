using System.ComponentModel.DataAnnotations;

namespace CaixaRegistradora.Classes{

    public class Nota{
        public decimal Valor { get; set; }

        public int Qtd { get; set; }

        public Nota(decimal valor, int qtd) {
             Valor = valor;
             Qtd = qtd;
        }
    }
}