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

    public class FaleConoscoRepository
    {
        private const string Dadosdeconexao = "Database=senac; Data Source=localhost; User Id=root;"; // criando uma variavel para estabelização de conexao
        public void Cadastrar(FaleConosco user){
            
            
             MySqlConnection Conexao = new MySqlConnection(Dadosdeconexao);  //ESTABELECER CONEXAO
             

            Conexao.Open();
            //Abertura de conexao com o banco de dados



            //Query em SQL para inserção : INSERT INTO TABELA (campos) values (informacao)
            String Query = "Insert into FaleConosco (Mensagem,Usuario) values (@Mensagem,@Usuario)";
            //Query em SQL para insert into tabela (campos) values (informacoes)


            MySqlCommand Comando = new MySqlCommand(Query, Conexao); //preparando o comando pasasando a query para o objto comando


            //tratamento em SQL INJECTION (SEGURANÇA)
            Comando.Parameters.AddWithValue("@Mensagem",user.Mensagem);
            Comando.Parameters.AddWithValue("@Usuario",user.Usuario);


            Comando.ExecuteNonQuery(); //execução de comando


            Conexao.Close();
            //Fechamento de conexao com o banco de dados
        }
    }
}