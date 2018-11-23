using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Senai.Financas.Mvc.Web.Models;
using Senai_Financas_Web_Mvc_Tarde.Interfaces;

namespace Senai_Financas_Web_Mvc_Tarde.Repositorios {
    public class UsuarioRepositorioSerializacao : IUsuario {

        /// <summary>
        /// Lista que armazena todos os usuários cadastrados no sistema
        /// </summary>
        /// <value></value>
        private List<UsuarioModel> UsuariosSalvos { get; set; }

        public UsuarioRepositorioSerializacao () {

            //O método construtor é uma ótima alternativapara instânciar seus objetos

            //Verificando se já existe um arquivo serializado...
            if (File.Exists ("usuarios.dat")) {
                //Ler o arquivo
                UsuariosSalvos = LerArquivoSerializado ();
            } else {
                UsuariosSalvos = new List<UsuarioModel> ();
            }
        }

        public UsuarioModel BuscarPorEmailESenha (string email, string senha) {
            throw new System.NotImplementedException ();
        }

        public UsuarioModel BuscarPorId (int Id) {
            //Percorre todos os usuários buscando pelo Id...
            foreach (UsuarioModel usuario in UsuariosSalvos) {
                if (Id == usuario.ID) {
                    return usuario;
                }
            }
            //Caso não encontre o usuário pelo id
            return null;
        }

        public UsuarioModel Cadastrar (UsuarioModel usuario) {
            //Adiciona o usuário na lista
            usuario.ID = UsuariosSalvos.Count + 1;
            UsuariosSalvos.Add (usuario);

            //Serializando a lista com todos os usuários cadastrados:

            EscreverNoArquivo ();

            return usuario;
        }

        private void EscreverNoArquivo () {
            //Vai guardar os bytes da serializaçaõ
            MemoryStream memoria = new MemoryStream ();

            //Objeto qur fará a serialização            
            BinaryFormatter serializadora = new BinaryFormatter ();

            serializadora.Serialize (memoria, UsuariosSalvos);

            //Pegando os bytes salvos na memória
            byte[] bytes = memoria.ToArray ();

            File.WriteAllBytes ("usuarios.dat", bytes);
        }

        public UsuarioModel Editar (UsuarioModel usuario) {
            throw new System.NotImplementedException ();
        }

        public void Excluir (int id) {
            //Buscando usuário por id
            UsuarioModel usuarioBuscado = BuscarPorId (id);

            //Caso o usuário buscado tenha sido encontrado...
            if (usuarioBuscado != null) {
                UsuariosSalvos.Remove (usuarioBuscado);

                //Temos que atualizar o arquivo com a lista sem o objeto
                EscreverNoArquivo ();
            }
        }

        public List<UsuarioModel> Listar () {

            return UsuariosSalvos;
        }

        private List<UsuarioModel> LerArquivoSerializado () {
            //Lê os bytes do arquivo
            byte[] bytesSerializados = File.ReadAllBytes ("usuarios.dat");

            //Cria o fluxo de memória com os bytes do arquivo serializado
            MemoryStream memoria = new MemoryStream (bytesSerializados);

            BinaryFormatter serializador = new BinaryFormatter ();

            //ClassCastException - erro na classe
            return (List<UsuarioModel>) serializador.Deserialize (memoria);

        }
    }
}