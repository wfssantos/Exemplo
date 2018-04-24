using Model;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class TarefaBLL : IDisposable
    {
        public IEnumerable<Tarefa> Lista()
        {
            return new Tarefa[] {
                new Tarefa() { Id = 1, Titulo = "Teste Titulo 1", Descricao = "Descrição bla bla bla 1", DataExecucao = DateTime.Now },
                new Tarefa() { Id = 2, Titulo = "Teste Titulo 2", Descricao = "Descrição xpto xpto xpto xpto xpto xpto 2", DataExecucao = DateTime.Now.AddDays(-10)
            } };
        }

        public bool Add(Tarefa model)
        {
            return true;
        }

        public bool Remove(int id)
        {
            return true;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
