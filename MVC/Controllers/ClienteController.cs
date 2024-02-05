using Microsoft.AspNetCore.Mvc;
using Projeto.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace MVC.Controllers
{
    public class ClienteController : Controller
    {
        private readonly string url = "https://localhost:44392/api/Cliente/";

        public ActionResult Index()
        {
            using var client = new HttpClient(Certifcado.HabilitarSSL());
            client.BaseAddress = new Uri(url);

            var responseTask = client.GetAsync("Listar");
            responseTask.Wait();
            var result = responseTask.Result;

            IEnumerable<ClienteDTO> clientes;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IEnumerable<ClienteDTO>>();
                readTask.Wait();
                clientes = readTask.Result;
            }
            else
            {
                clientes = Enumerable.Empty<ClienteDTO>();
                ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
            }
            return View(clientes);
        }

        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Incluir(ClienteDTO cliente)
        {
            try
            {
                using (var client = new HttpClient(Certifcado.HabilitarSSL()))
                {
                    client.BaseAddress = new Uri(url);
                    var postTask = client.PostAsJsonAsync<ClienteDTO>("Adicionar", cliente);
                    postTask.Wait();
                    var result = postTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "Erro no Servidor. Contacte o Administrador.");
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
        public ActionResult Detalhe(int id)
        {
            return View(RetonarClientePorId(id));
        }

        public ActionResult Edit(int id)
        {
            return View(RetonarClientePorId(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClienteDTO cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient(Certifcado.HabilitarSSL()))
                    {
                        client.BaseAddress = new Uri(url);
                        var putTask = client.PutAsJsonAsync<ClienteDTO>("Alterar", cliente);
                        putTask.Wait();
                        var result = putTask.Result;

                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    ModelState.AddModelError(string.Empty, "Erro no Servidor. Contacte o Administrador.");
                }
            }
            catch
            {
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            using var client = new HttpClient(Certifcado.HabilitarSSL());
            client.BaseAddress = new Uri(url);

            var responseTask = client.GetAsync("ListarPorId/" + id);
            responseTask.Wait();
            var result = responseTask.Result;

            ClienteDTO cliente;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<ClienteDTO>();
                readTask.Wait();
                cliente = readTask.Result;
            }
            else
            {
                cliente = null;
                ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ClienteDTO cliente)
        {
            try
            {
                using (var client = new HttpClient(Certifcado.HabilitarSSL()))
                {
                    client.BaseAddress = new Uri(url);
                    var deleteTask = client.DeleteAsync("Excluir/" + id.ToString());
                    deleteTask.Wait();
                    var result = deleteTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "Erro no Servidor. Contate o Administrador.");
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        private ClienteDTO RetonarClientePorId(int id)
        {
            using var client = new HttpClient(Certifcado.HabilitarSSL());
            client.BaseAddress = new Uri(url);

            var responseTask = client.GetAsync("ListarPorId/" + id);
            responseTask.Wait();
            var result = responseTask.Result;

            ClienteDTO cliente;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<ClienteDTO>();
                readTask.Wait();
                cliente = readTask.Result;
            }
            else
            {
                cliente = null;
                ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
            }
            return cliente;
        }
    }
}
