using System;
using System.Collections.Generic;
using System.IO;
using Senai.Financas.Mvc.Web.Models;

namespace Senai.Financas.Mvc.Web.Repositorios
{
    public class UsuarioRepositorio
    {
        public UsuarioModel BuscarPorEmailESenha(string email, string senha)
        {
            List<UsuarioModel> usuariosCadastrados = CarregarDoCSV();

            //Percorro cada usuário da lista do CSV...
            foreach (UsuarioModel usuario in usuariosCadastrados)
            {
                if (usuario.Email == email && usuario.Senha == senha)
                {
                    return usuario;
                }
            }

            //Caso  sistema não encontre nenhuma combinação de email e senha retorna nulls
            return null;
        }

        /// <summary>
        /// Carrega a lista de usuários com os dados do CSV
        /// </summary>
        private List<UsuarioModel> CarregarDoCSV()
        {
            List<UsuarioModel> lsUsuarios = new List<UsuarioModel>();

            //Abre o stream de leitura do arquivo
            string[] linhas = File.ReadAllLines("usuarios.csv");

            //Lê cada registro no CSV
            foreach (string linha in linhas)
            {
                //Separa os dados da linha
                string[] dadosDaLinha = linha.Split(';');

                //Cria o objeto com os dados da linha do CSV
                UsuarioModel usuario = new UsuarioModel
                {
                    Nome = dadosDaLinha[0],
                    Email = dadosDaLinha[1],
                    Senha = dadosDaLinha[2],
                    DataNascimento = DateTime.Parse(dadosDaLinha[3])
                };

                //Adicionando o usuário na lista
                lsUsuarios.Add(usuario);
            }       
            return lsUsuarios;     
        }
    }
}