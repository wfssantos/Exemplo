using Model;
using System.Collections.Generic;

namespace Web.Model
{
    public class TarefaViewModel
    {
        public TarefaViewModel()
        {
            Tarefa = new Tarefa();
            Lista = new List<Tarefa>();
        }

        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Tarefa Tarefa { get; set; }
        public List<Tarefa> Lista { get; set; }
    }
}