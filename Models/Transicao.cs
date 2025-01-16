using System.ComponentModel.DataAnnotations;
using System.Xml;
using Microsoft.Extensions.Options;

namespace CaixaRegistradora.Classes{

    public class Transacao {
        public decimal Valor { get; set; }

        public List<Nota> NotasEnviado { get; set; }

        public Transacao(decimal valor, List<Nota> notasEnviado) {
            Valor = valor;
            NotasEnviado = notasEnviado;
        }
    }
}