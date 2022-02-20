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
    public class UsuarioRepository
    {
        //objetivo desta model: é criar as funcionalidades ou metodos que manipulam os atributos da classe usuario
        //Cadastrar,Listar,Editar,Excluir

        //Obter as credencias do banco de dados (exemplos em qual ip esta rodando os banco de dados (ip,user,) *credenciais

        //Definir as credenciais do banco de dados
        //metodo privado pois nao deve ser acessado por fora, sendo uma constante que não deve mudar nunca

        //Operações que manipulam os atributos da classe usuario

        private const string DadosConexao = "Database=senac; Data Source=localhost; User Id=root;";
        // public para que ele fique visivel para ver e void pois nao vai retornar nada

        public Usuario ValidarLogin(Usuario user){ //recebe um objto de usuario


            MySqlConnection Conexao = new MySqlConnection(DadosConexao);


            Conexao.Open();


            String Query = "Select * from Usuario where Login=@Login and Senha=@Senha";



            MySqlCommand Comando = new MySqlCommand(Query, Conexao);



            Comando.Parameters.AddWithValue("@Login", user.Login);

            Comando.Parameters.AddWithValue("@Senha", user.Senha);


            MySqlDataReader Reader = Comando.ExecuteReader();



            Usuario UsuarioEncontrado = null; // aqui esta o pulo do gato!! 2 // mandara null para nao receber vazio


            if (Reader.Read())
            {
                UsuarioEncontrado = new Usuario();// aqui pulo do gaato 

                UsuarioEncontrado.Id = Reader.GetInt32("Id");

                if (!Reader.IsDBNull(Reader.GetOrdinal("Nome")))
                    //verificador se tal campo e esta nullo ou nao,ser nao for nulo e feito a adicao
                    //se Reader não for nulo (sem informacao) adicione caso contrario não.
                    UsuarioEncontrado.Nome = Reader.GetString("Nome"); // ("Nome") campo



                if (!Reader.IsDBNull(Reader.GetOrdinal("Login")))
                    //verificador se tal campo e esta nullo ou nao,ser nao for nulo e feito a adicao

                    UsuarioEncontrado.Login = Reader.GetString("Login");


                if (!Reader.IsDBNull(Reader.GetOrdinal("Senha")))
                    //verificador se tal campo e esta nullo ou nao,ser nao for nulo e feito a adicao  
                    UsuarioEncontrado.Senha = Reader.GetString("Senha");

                UsuarioEncontrado.DataNascimento = Reader.GetDateTime("DataNascimento");
            }
            Conexao.Close();
            return UsuarioEncontrado;
        }
        public void TestarConexao(){//metodo
            //Esta clase MySqlConnection vem da classe using my_sql_connector

            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            //conexao e um objeto que recebe as credenciais de DadosConexao para estabelecer a conexao
            //necessario por no parametro a string DadosConexao que ja tem valor passada
            // esta classse vem de mysqlconnector, passando para dentro do parametro a string para fazer a conexao. * definicao de conexao

            Conexao.Open(); //Abrir conexao com o banco de dados



            Console.WriteLine("O banco de dados está respondendo"); //Mensagem



            Conexao.Close(); //Close conexao com o banco de dados
            

            // E obrigatorio o fechamento de conexao com o banco de dados 

        } 

        public void Editar(Usuario user){

             MySqlConnection Conexao = new MySqlConnection(DadosConexao); 
             
        

            Conexao.Open();
            //Abertura de conexao com o banco de dados



            //Query em SQL para alteração update tabela set campo1=infor1 whrere condicao
            String Query = "update Usuario set Nome=@Nome, Login=@Login,Senha=@Senha,DataNascimento=DataNascimento where Id=@Id";


            MySqlCommand Comando = new MySqlCommand(Query, Conexao);


            //tratamento em SQL INJECTION (SEGURANÇA)
            Comando.Parameters.AddWithValue("@Id",user.Id);
            Comando.Parameters.AddWithValue("@Nome",user.Nome);
            Comando.Parameters.AddWithValue("@Login",user.Login);
            Comando.Parameters.AddWithValue("@Senha",user.Senha);
            Comando.Parameters.AddWithValue("@DataNascimento",user.DataNascimento);


            Comando.ExecuteNonQuery(); //execução de comando


            Conexao.Close();
            //Fechamento de conexao com o banco de dados
        }

        //metodo
        public void Excluir(Usuario user){ 
        //deve ter um nome por ter chamado a classe Usuario


        
        MySqlConnection Conexao = new MySqlConnection(DadosConexao); //definicão de conexao //ESTABELECER CONEXAO

            Conexao.Open();
            //Conexao e apenas um objto de MySqlConnection



            //Query em SQL para exclusão delet from Usuario where (condição)

            String Query = "delete from Usuario where id=@Id"; //definicao de query // comando para banco de dados
            //@Id e um tratamento em sql Injection (filtragem)
            //Comando para executacao no banco de dados mas ainda nao ativa, foi colocado nos parametro a Query conjunto da conexao para ir ambos (conexao = objeto de conexao) *preparação de rota (preparando o sql(QUERY) para objeto de conexao(Conexao) )


            //preparando o sql(Query) para o objto de conexao(Conexao)
            
            MySqlCommand Comando = new MySqlCommand(Query,Conexao); // preparação de comando em sql para dentro do banco de dados, nao e uma execução


            Comando.Parameters.AddWithValue("@Id",user.Id);


            Comando.ExecuteNonQuery();  // Esse comando executa a Query no banco de dados ele que fara a execução //executa a query no banco de dados


            Conexao.Close(); //fechamento de conexao
        }
        public void Cadastrar(Usuario user){
            
             MySqlConnection Conexao = new MySqlConnection(DadosConexao);  //ESTABELECER CONEXAO
             

            Conexao.Open();
            //Abertura de conexao com o banco de dados



            //Query em SQL para inserção : INSERT INTO TABELA (campos) values (informacao)
            String Query = "Insert into Usuario (Nome,Login,Senha,DataNascimento) values (@Nome,@Login,@Senha,@DataNascimento)";
            //Query em SQL para insert into tabela (campos) values (informacoes)


            MySqlCommand Comando = new MySqlCommand(Query, Conexao); //preparando o comando pasasando a query para o objto comando


            //tratamento em SQL INJECTION (SEGURANÇA)
            Comando.Parameters.AddWithValue("@Nome",user.Nome);
            Comando.Parameters.AddWithValue("@Login",user.Login);
            Comando.Parameters.AddWithValue("@Senha",user.Senha);
            Comando.Parameters.AddWithValue("@DataNascimento",user.DataNascimento);


            Comando.ExecuteNonQuery(); //execução de comando


            Conexao.Close();
            //Fechamento de conexao com o banco de dados
        }

        public Usuario BuscarPorId(int Id){ //buscarPorId so retorna apenas um usuario

            MySqlConnection Conexao = new MySqlConnection(DadosConexao); 
             

            Conexao.Open();
            //Abertura de conexao com o banco de dados



            //Query em SQL para FILTRAR SELECT * FROM tabela wherer Id....
            String Query = "select * from Usuario  where Id=@Id";




            MySqlCommand Comando = new MySqlCommand(Query, Conexao);



            Comando.Parameters.AddWithValue("@Id",Id); //tratamento de SQL


            //Objeto reader ira receber todos registros executado no banco de dados (Comando.ExecuteReader();
            MySqlDataReader Reader = Comando.ExecuteReader();//Aqui esta a execução do Comando
            //objto reader ira receber todos os registros executados no banco de dados pelo (Comando.ExecuteReader();  // aqui esta a execucao do comando  Mysqldata reader e definidio por receber muitos registros
            // o msqlcommand armazena no Reader que depoiis o reader e percorrido pelo while

            Usuario UsuarioEncontrado = new Usuario();

            if (Reader.Read())

            //Reader (Banco de dados) recebe as informacoes executada do Comando.ExecuteReader();, abaixo o nosso objetivo objtivo e recbeer os dados do abnco de dados  e alimenta o nosso objto UsuarioEncontrado
            //set envia , get recebe

            {
                //se ele encontrar algum registro devemos atribuir para o objto UsuarioEncontrado


                UsuarioEncontrado.Id = Reader.GetInt32("Id");
    
                if (!Reader.IsDBNull(Reader.GetOrdinal("Nome")))
                 //verificador se tal campo e esta nullo ou nao,ser nao for nulo e feito a adicao
                 //se Reader não for nulo (sem informacao) adicione caso contrario não.
                UsuarioEncontrado.Nome =Reader.GetString("Nome"); // ("Nome") campo
                 


                if (!Reader.IsDBNull(Reader.GetOrdinal("Login")))
                 //verificador se tal campo e esta nullo ou nao,ser nao for nulo e feito a adicao

                UsuarioEncontrado.Login =Reader.GetString("Login");
                 

                if (!Reader.IsDBNull(Reader.GetOrdinal("Senha")))
                 //verificador se tal campo e esta nullo ou nao,ser nao for nulo e feito a adicao  
                UsuarioEncontrado.Senha =Reader.GetString("Senha");
                 
            }

            Conexao.Close(); //fechamento de conexao


            return (UsuarioEncontrado);//retornando o usuario localizado
        }

        public List<Usuario> Listar(){


            MySqlConnection Conexao = new MySqlConnection(DadosConexao); 
             

            Conexao.Open();
            //Abertura de conexao com o banco de dados



            //Query em SQL para listagem: select * from TABELA 
            String Query = "select * from Usuario";




            MySqlCommand Comando = new MySqlCommand(Query, Conexao); //preparando o comando pasasando a query para o objto comando

            //Objeto reader ira receber todos registros executado no banco de dados (Comando.ExecuteReader();
            MySqlDataReader Reader = Comando.ExecuteReader();//Aqui esta a execução do Comando
            //objto reader ira receber todos os registros executados no banco de dados pelo (Comando.ExecuteReader();  // aqui esta a execucao do comando  Mysqldata reader e definidio por receber muitos registros
            // o msqlcommand armazena no Reader que depoiis o reader e percorrido pelo while


            List<Usuario> Lista = new List<Usuario>(); // Criando uma lista da Classe Usuario Vázia


            
            //Percursos nos registro do banco de dados e preciso usar uma estrutura de repetição
            //Percusos nos registro de banco de dados - e preciso usar uma estrutura de repetição de foreach que vem um registro atras do outro.
            
            while (Reader.Read()){ //e percorrido pelo Reader

                Usuario userEncontrado = new Usuario(); //instancia de objto

                userEncontrado.Id= Reader.GetInt16("Id"); //GetString ou GetInt e preciso para trazer informações do banco de dados


                userEncontrado.Nome=Reader.GetString("Nome");
                if (!Reader.IsDBNull(Reader.GetOrdinal("Nome")))//verificador se tal campo e esta nullo ou nao,ser nao for nulo e feito a adicao

                userEncontrado.Login=Reader.GetString("Login");
                if (!Reader.IsDBNull(Reader.GetOrdinal("Nome")))//verificador se tal campo e esta nullo ou nao,ser nao for nulo e feito a adicao

                userEncontrado.Senha=Reader.GetString("Senha");
                if (!Reader.IsDBNull(Reader.GetOrdinal("Nome")))//verificador se tal campo e esta nullo ou nao,ser nao for nulo e feito a adicao

                Lista.Add(userEncontrado);
            }

            Conexao.Close();
            //Fechamento de conexao com o banco de dados


            return (Lista); //retornando  a lista de Usuario
            //return deve ser depois do fechamento de banco de dados quando deve ser retornado, jamais deve ser retornado antes do fechamento da conexao com o banco de dados
        }

    }
}
//1 DEFINI A CONEXAO 2 ABRE E FECHA A CONEXAO, PREPARA A CONEXAO, PREPARA O COMANDO QUE VAI RECEBER A QUERY, 