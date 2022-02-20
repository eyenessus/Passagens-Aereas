using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Google.Models;
using MySqlConnector; // necessario adicionar para ter a conexao que,  vem do package // pacote conjunto de classe
namespace Google.Models
{
    public class PacotesTuristicosRepository
    {
        private const string Dadosdeconexao = "Database=senac; Data Source=localhost; User Id=root;"; // criando uma variavel para estabelização de conexao



        public void TestarConexao(){

            MySqlConnection Conexao = new MySqlConnection (Dadosdeconexao);
            Conexao.Open();
            Console.WriteLine("Olá eu sou uma maquina, e estou funcionando.");
            Conexao.Close();
        }
        public void Cadastrar(PacotesTuristicos user){
            MySqlConnection Conexao = new MySqlConnection(Dadosdeconexao);
            Conexao.Open();
            String Query = "insert into PacotesTuristicos (Nome,Origem,Destino,Atrativos,Saida,Retorno,Usuario) values (@Nome,@Origem,@Destino,@Atrativos,@Saida,@Retorno,@Usuario)";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);
            Comando.Parameters.AddWithValue("@Nome",user.Nome);
            Comando.Parameters.AddWithValue("@Origem",user.Origem);
            Comando.Parameters.AddWithValue("@Destino",user.Destino);
            Comando.Parameters.AddWithValue("@Atrativos",user.Atrativos);
            Comando.Parameters.AddWithValue("@Saida",user.Saida);
            Comando.Parameters.AddWithValue("@Retorno",user.Retorno);
            Comando.Parameters.AddWithValue("@Usuario",user.Usuario);
            Comando.ExecuteNonQuery();
            Conexao.Close();
        }
        public void Excluir (PacotesTuristicos pacote){
            MySqlConnection Conexao = new MySqlConnection (Dadosdeconexao);
            Conexao.Open();
            String Query = "DELETE FROM PacotesTuristicos WHERE id=@Id";
            MySqlCommand Comando = new MySqlCommand(Query,Conexao);
            Comando.Parameters.AddWithValue("@Id", pacote.Id);
            Comando.ExecuteNonQuery();
            Conexao.Close();
        }
        public void Editar (PacotesTuristicos user){
            MySqlConnection Conexao = new MySqlConnection(Dadosdeconexao);
            Conexao.Open();
                     String Query = "UPDATE PacotesTuristicos SET Id=@Id,Nome=@Nome, Origem=@Origem,Destino=@Destino,Atrativos=@Atrativos,Saida=@Saida,Retorno=@Retorno WHERE Id=@Id";


            MySqlCommand Comando = new MySqlCommand(Query, Conexao);


            //tratamento em SQL INJECTION (SEGURANÇA)
            Comando.Parameters.AddWithValue("@Id",user.Id);
            Comando.Parameters.AddWithValue("@Nome",user.Nome);
            Comando.Parameters.AddWithValue("@Origem",user.Origem);
            Comando.Parameters.AddWithValue("@Destino",user.Destino);
            Comando.Parameters.AddWithValue("@Atrativos",user.Atrativos);
            Comando.Parameters.AddWithValue("@Saida",user.Saida);
            Comando.Parameters.AddWithValue("@Retorno",user.Retorno);
            Comando.Parameters.AddWithValue("@Usuario",user.Usuario);
            Comando.ExecuteNonQuery();
            Conexao.Close();
        }
        public PacotesTuristicos Buscar(int Id){
            MySqlConnection Conexao = new MySqlConnection (Dadosdeconexao);
            Conexao.Open();
            String Query = "select * from PacotesTuristicos where id=@Id";
            MySqlCommand Comando = new MySqlCommand(Query,Conexao);
            Comando.Parameters.AddWithValue("@Id",Id);
            MySqlDataReader Reader = Comando.ExecuteReader();
            PacotesTuristicos User = new PacotesTuristicos();

            if (Reader.Read()){
               User.Id = Reader.GetInt32("Id");
    
                if (!Reader.IsDBNull(Reader.GetOrdinal("Nome")))
                User.Nome =Reader.GetString("Nome");

                if (!Reader.IsDBNull(Reader.GetOrdinal("Origem")))
                User.Origem =Reader.GetString("Origem");
                 

                if (!Reader.IsDBNull(Reader.GetOrdinal("Destino")))
                User.Destino =Reader.GetString("Destino");

                  if (!Reader.IsDBNull(Reader.GetOrdinal("Atrativos")))
                User.Destino =Reader.GetString("Atrativos");

            }
            Conexao.Close();
            return (User);
        }
        public List<PacotesTuristicos> Listar(){
            MySqlConnection Conexao = new MySqlConnection (Dadosdeconexao);
            Conexao.Open();
            String Query = "select * from PacotesTuristicos";
            MySqlCommand comando = new MySqlCommand(Query, Conexao);
            MySqlDataReader Reader = comando.ExecuteReader();
            List<PacotesTuristicos> Listar = new List<PacotesTuristicos>();
            while (Reader.Read()) {
                PacotesTuristicos User = new PacotesTuristicos();
                User.Id = Reader.GetInt16("Id");

                User.Nome=Reader.GetString("Nome");
                if (!Reader.IsDBNull(Reader.GetOrdinal("Nome")))//verificador se tal campo e esta nullo ou nao,ser nao for nulo e feito a adicao

                User.Origem=Reader.GetString("Origem");
                if (!Reader.IsDBNull(Reader.GetOrdinal("Origem")))

                User.Destino=Reader.GetString("Destino");
                if (!Reader.IsDBNull(Reader.GetOrdinal("Destino")))//verificador se tal campo e esta nullo ou nao,ser nao for nulo e feito a adicao

                User.Atrativos=Reader.GetString("Atrativos");
                if (!Reader.IsDBNull(Reader.GetOrdinal("Atrativos")))

                User.Usuario=Reader.GetInt16("Usuario");
                if (!Reader.IsDBNull(Reader.GetOrdinal("Usuario")))
                Listar.Add(User);
            }

            Conexao.Close();
            return (Listar);

        }
    }
}