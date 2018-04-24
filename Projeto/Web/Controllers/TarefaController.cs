using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Model;

namespace Web.Controllers
{
    public class TarefaController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var model = new TarefaViewModel();
            string apiUrl = "http://localhost:9849/api/Tarefa/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    model.Lista = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Tarefa>>(data);
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Novo(Tarefa tarefa)
        {
            string apiUrl = "http://localhost:9849/api/Tarefa/";

            var myContent = JsonConvert.SerializeObject(tarefa);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);            
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            using (var client = new HttpClient())
            {
                var result = client.PostAsync(apiUrl, byteContent).Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
            
        }

        [HttpPost]
        public async Task<PartialViewResult> Pesquisa(string titulo, string descricao)
        {
            var model = new TarefaViewModel();
            string apiUrl = "http://localhost:9849/api/Tarefa/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var listaCompleta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Tarefa>>(data);
                    model.Lista = listaCompleta.Where(w => (w.Titulo.Contains(titulo) || string.IsNullOrEmpty(titulo)) && (w.Descricao.Contains(descricao) || string.IsNullOrEmpty(descricao))).ToList();
                }
            }

            return PartialView("_Grid", model);

        }

        [HttpPost]
        public async Task<bool> Delete(int id)
        {
            var retorno = false;
            string apiUrl = "http://localhost:9849/api/Tarefa/" + id;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    retorno = Convert.ToBoolean(await response.Content.ReadAsStringAsync());
                }
            }

            return retorno;
        }
    }
}