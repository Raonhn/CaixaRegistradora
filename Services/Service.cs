using System;
using Microsoft.AspNetCore.Http.HttpResults;
using CaixaRegistradora.Classes;
using CaixaRegistradora.Errors;

namespace CaixaRegistradora.Services{
    public class Service{
        private readonly List<Nota> Caixa;

        public Service(List<Nota> caixa){
            this.Caixa = caixa;
        }

        public List<Nota> AddNota(Nota nota){
            if(nota.Valor <= 0){
                throw new HttpException(400, "Erro no Body", new List<string>{ "Nota com valor igual ou inferior a 0 não existe" });
            } else if(nota.Qtd <= 0){
                throw new HttpException(400, "Erro no Body", new List<string>{ "A quantidade de notas deve ser ao minimo 1" });
            }
            if(Caixa.Any(notas => notas.Valor == nota.Valor)){
                var notaExistente = Caixa.FirstOrDefault(notas => notas.Valor == nota.Valor);
                notaExistente!.Qtd = notaExistente.Qtd;
            } else {
                Caixa.Add(new Nota(nota.Valor, nota.Qtd));
                Caixa.Sort((nota1,nota2) => nota2.Valor.CompareTo(nota1.Valor));
            }
            return new List<Nota>{nota};
        }

        public void RemoverNota(Nota nota){
            var notaExistente = Caixa.FirstOrDefault(notas => notas.Valor == nota.Valor);
            if(notaExistente!.Qtd > 0){
                notaExistente!.Qtd -= nota.Qtd;
            }
        }

        public bool ContarNota(Nota nota){
            var notaExistente = Caixa.FirstOrDefault(notas => notas.Valor == nota.Valor);
            if(notaExistente!.Qtd > 0){
                return true;
            }
            return false;
        }

        public List<Nota> VerCaixa() {
            if(Caixa.Count < 1){
                throw new HttpException(204, "NoContent", new List<string> { "Caixa Registradora sem Notas" });
            }
             return Caixa;
        }

        public bool isEmpyt(){
            return Caixa.Count() <= 0;
        }

        public decimal valorTotal(){
            return Caixa.Sum(it => it.Valor*it.Qtd);
        }

        public List<Nota> calculaTroco(Transacao transacao){
            if(transacao.Valor <= 0){
                throw new HttpException(400, "Erro no Body", new List<string> { "Não existem transacoes zeradas ou negativas"});
            } else {
                foreach(var nota in transacao.NotasEnviado){
                    if(nota.Valor <= 0){
                        throw new HttpException(400, "Erro no Body", new List<string> { "Nota com valor igual ou menor que 0 não existe." });
                    } else if(nota.Qtd <= 0){
                        throw new HttpException(400, "Erro no Body", new List<string> { "A quantidade de notas deve ser ao minimo 1" });
                    }
                }
            }
            var troco = transacao.NotasEnviado.Sum(it => it.Valor*it.Qtd) - transacao.Valor;
            if(troco < 0){
                throw new HttpException(406, "Dinheiro Insuficiente para essa transacao", new List<string> { "As notas enviadas estão com valor inferior ao valor total da transacao" });
            }
            var trocoNotas = new List<Nota>();

            foreach(var nota in Caixa){
                if(nota.Valor <= troco){
                    var i = nota.Qtd;
                    while(trocoNotas.Sum(it => it.Valor*it.Qtd)+nota.Valor <= troco && i > 0){
                        i--;
                        if(ContarNota(nota)){
                            if(trocoNotas.Sum(it => it.Valor*it.Qtd)+nota.Valor <= troco){
                                if(Caixa.Any(notas => notas.Valor == nota.Valor)){
                                    var notaExistente = trocoNotas.FirstOrDefault(notas => notas.Valor == nota.Valor);
                                    notaExistente!.Qtd++;
                                } else {
                                    trocoNotas.Add(new Nota(nota.Valor, 1));
                                }
                            } else {
                                break;
                            }
                        } else{
                            break;
                        }
                    }
                }
            }
            if(trocoNotas.Sum(it => it.Valor*it.Qtd) < troco){
                throw new HttpException(500, "Erro do Servidor", new List<string> { "Não temos troco o suficiente para concluir essa transacao" });
            }
            foreach(var nota in trocoNotas){
                RemoverNota(nota);
            }
            foreach(var nota in transacao.NotasEnviado){
                AddNota(nota);
            }
            return trocoNotas;
        }
    }
}