using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Financas.Mvc.Web.Models;
using Senai.Financas.Mvc.Web.Repositorios;

namespace Senai.Financas.Mvc.Web.Controllers {
    public class UsuarioController : Controller {
        [HttpGet]
        public ActionResult Cadastro () {
            return View ();
        }

        [HttpPost]
        public ActionResult Cadastro (IFormCollection form) {
            UsuarioModel usuarioModel = new UsuarioModel ();

            usuarioModel.Nome = form["nome"];
            usuarioModel.Email = form["email"];
            usuarioModel.DataNascimento = DateTime.Parse (form["dataNascimento"]);
            usuarioModel.Senha = form["senha"];

            //Aplicando o ID
            if (System.IO.File.Exists ("usuarios.csv"))
                usuarioModel.ID = System.IO.File.ReadAllLines ("usuarios.csv").Length + 1;
            else
                usuarioModel.ID = 1;

            using (StreamWriter sw = new StreamWriter ("usuarios.csv", true)) {
                sw.WriteLine ($"{usuarioModel.ID};{usuarioModel.Nome};{usuarioModel.Email};{usuarioModel.Senha};{usuarioModel.DataNascimento}");
            }

            ViewBag.Mensagem = "Usuário Cadastrado";

            return View ();

        }

        [HttpGet]
        public IActionResult Login () => View ();

        [HttpPost]
        public IActionResult Login (IFormCollection form) {

            //Pega os dados do POST
            UsuarioModel usuario = new UsuarioModel {
                Email = form["email"],
                Senha = form["senha"]
            };

            //Verificar se o usuário possuí acesso para realizazr login
            UsuarioRepositorio usuarioRep = new UsuarioRepositorio ();

            UsuarioModel usuarioModel = usuarioRep.BuscarPorEmailESenha (usuario.Email, usuario.Senha);

            if (usuarioModel != null) {
                HttpContext.Session.SetString ("idUsuario", usuarioModel.Email.ToString ());

                ViewBag.Mensagem = "Login realizado com sucesso!";

                return RedirectToAction ("Cadastrar", "Transacao");
            } else {
                ViewBag.Mensagem = "Acesso negado!";
            }

            return View ();
        }

        //Quem faz o POST - Cliente
        //Quem faz o GET - Programador

        /// <summary>
        /// Lista todos os usuários cadastrados no sistema
        /// </summary>
        /// <returns>A view da listagem de usuário</returns>

        [HttpGet]
        public IActionResult Listar () {

            UsuarioRepositorio rep = new UsuarioRepositorio ();

            //Buscando os dados do resositório e aplicandao no view bag
            // ViewBag.Usuarios = rep.Listar();
            ViewData["Usuarios"] = rep.Listar ();

            return View ();
        }

        [HttpGet]

        public IActionResult Excluir (int id) {
            UsuarioRepositorio rep = new UsuarioRepositorio ();
            rep.Excluir (id);
            return RedirectToAction("Listar");
        }

    }
}