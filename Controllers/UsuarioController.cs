using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Financas.Mvc.Web.Models;
using Senai.Financas.Mvc.Web.Repositorios;

namespace Senai.Financas.Mvc.Web.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult Cadastro(){
            return View();
        }

        [HttpPost]
        public ActionResult Cadastro(IFormCollection form){
            UsuarioModel usuarioModel = new UsuarioModel();

            usuarioModel.Nome = form["nome"];
            usuarioModel.Email = form["email"];
            usuarioModel.DataNascimento = DateTime.Parse(form["dataNascimento"]);
            usuarioModel.Senha = form["senha"];

            using(StreamWriter sw = new StreamWriter("usuarios.csv",true)){
                sw.WriteLine($"{usuarioModel.Nome};{usuarioModel.Email};{usuarioModel.Senha};{usuarioModel.DataNascimento}");
            }

            ViewBag.Mensagem = "Usuário Cadastrado";

            return View();
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(IFormCollection form)
        {
            
            //Pega os dados do POST
            UsuarioModel usuario = new UsuarioModel
            {
                Email = form["email"],
                Senha = form["senha"]
            };

            //Verificar se o usuário possuí acesso para realizazr login
            UsuarioRepositorio usuarioRep = new UsuarioRepositorio();
            
            UsuarioModel usuarioModel = usuarioRep.BuscarPorEmailESenha(usuario.Email, usuario.Senha);

            if (usuarioModel != null)
            {
                HttpContext.Session.SetString("idUsuario", usuarioModel.Email.ToString());

                ViewBag.Mensagem = "Login realizado com sucesso!";

                return RedirectToAction("Cadastrar", "Transacao");
            }
            else
            {
                ViewBag.Mensagem = "Acesso negado!";
            }

            return View();
        }


    }
}