using BLL;
using Model;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Api.Controllers
{
    [EnableCors(origins: "http://localhost:9798", headers: "*", methods: "*")]
    public class TarefaController : ApiController
    {
        public IEnumerable<Tarefa> Get()
        {
            IEnumerable<Tarefa> _resultado = null;
            using (var _blo = new TarefaBLL())
                _resultado = _blo.Lista();
            return _resultado;
        }

        public bool Post([FromBody]Tarefa model)
        {
            var _resultado = false;
            using (var _blo = new TarefaBLL())
                _resultado = _blo.Add(model);
            return _resultado;

        }

        public bool Delete(int id)
        {
            var _resultado = false;
            using (var _blo = new TarefaBLL())
                _resultado = _blo.Remove(id);
            return _resultado;
        }
    }
}