using System.Collections.Generic;
using Senai.Financas.Mvc.Web.Models;

namespace Senai_Financas_Web_Mvc_Tarde.Interfaces {
    public interface IUsuario {
        //Interface - o que eu vou fazer?
        //Classes - como eu vou fazer?
        //"Ordem" - Model, Interface, Repositório, Controller/View
        UsuarioModel Cadastrar (UsuarioModel usuario);

        List<UsuarioModel> Listar ();

        void Excluir (int id);

        UsuarioModel Editar (UsuarioModel usuario);

        UsuarioModel BuscarPorEmailESenha(string email, string senha);

        UsuarioModel BuscarPorId(int Id);
    }
}