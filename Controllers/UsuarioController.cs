using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Google.Models;
using Microsoft.AspNetCore.Http;

namespace Google.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Login(){
            return View();
        }
        [HttpPost]
        public IActionResult Login(Usuario u){
        UsuarioRepository ur = new UsuarioRepository();
        Usuario userEncontrado = ur.ValidarLogin(u);
           if(userEncontrado==null){ // o if vai verificar se tal usuario e tal senha sao iguais se caso a senha nao bate ficara como null e apresentara a o texto falha no login
                ViewBag.Mensagem = "Falha no login!";
                return View();
            }else {
                //ViewBag.Mensagem = "Você está logado";
                //registrar na sessao ID E NOME DO USUARIO LOGADO
                //set server para atribuir a tal tipagem STRING OU INT UMA INFORMACAO
                HttpContext.Session.SetInt32("IdUsuario",userEncontrado.Id);// registra o ID do usaurio logado na sessao
                //HttpContext.Session.SetInt32("Nomedassesao",userEncontrado.Id(pegando o ID do usuario encontrado e sendo atribuido ao IDUSUARIO));
                HttpContext.Session.SetString("NOMEUSUARIO",userEncontrado.Nome);//REGISTRA NOME DO USUARIO
                //NOME QUALQUER E ONDE ESTA SENDO ENCONTRADO
                return RedirectToAction("Listagem","Usuario");
            }
        }
          public IActionResult Logout() {
                HttpContext.Session.Clear();//limpa todos os dados registrado na sessao quando o usuario logou
                return RedirectToAction("Login","Usuario");//redirecionamento para a pagina de login apos ter saido ação // usuario
            }
        
        public IActionResult Listagem(){

            if( HttpContext.Session.GetInt32("IdUsuario") == null ){//valida se o usaurio esta logado (se caso tiver registro)
                return RedirectToAction("Login","Usuario");// redirecionamento para a pagina login da classe CONTROLE
            }
            UsuarioRepository ur =new UsuarioRepository();


            List<Usuario> Lista = ur.Listar();//Definição de  tipo lista com LIST<>

            

            return View(Lista); // retorna uma lista de usuario Na view
        }

        //rotas editar e excluir

        
    public IActionResult Excluir(int Id){
       
        if( HttpContext.Session.GetInt32("IdUsuario") == null ){//valida se o usaurio esta logado (se caso tiver registro)
                return RedirectToAction("Login","Usuario");// redirecionamento para a pagina login da classe CONTROLE
            }
        
            UsuarioRepository ur = new UsuarioRepository();
        
            Usuario userEncontrado = ur.BuscarPorId(Id); //buscarPorId passa todas informações obtida para userencontrado que são infor da classe Usuario e abaixo e chamado uma ação no ur que vem do banco de dados a ação excluir o objto userencontrado que no caso ja estao com as informações passsada pelo buscarporId no final e retornado a listagem que vem da controle Usuario

            ur.Excluir(userEncontrado); //metodo excluir recebe ojbto exemplo Id

            return RedirectToAction ("Listagem", "Usuario"); //(Listagem) ACTION, (Usuario)

        }
        public IActionResult Editar(int Id){ //aqui e passado o id que vem do formulario
        if( HttpContext.Session.GetInt32("IdUsuario") == null ){//valida se o usaurio esta logado (se caso tiver registro)
                return RedirectToAction("Login","Usuario");// redirecionamento para a pagina login da classe CONTROLE
            }

            UsuarioRepository ur =new UsuarioRepository();

            Usuario userEncontrado = ur.BuscarPorId(Id); //  ur.BuscarPorId(Id) vai no banco de dados pega todos dados do Id passado e armazena no objeto userencontrado que são dos atributos da classe Usuario e no return vai retorna a view com as informações gravada no userEncontrado

             // Classe usuario com o nome do objto userencontrao, ur.BuscarPorId(Id) recebe o numero 2 e retorna um ojbto Usuario


            return View(userEncontrado);


            //este metodo faz com que retorne as informações encontrada no formulario ainda para fazer a edição
            //ja no metodo POST e feito o envio de novas informações novas que retorna para listagem
        }
        
        [HttpPost]
    public IActionResult Editar(Usuario userFor){

        UsuarioRepository ur =new UsuarioRepository();

            ur.Editar(userFor); //foi colocado no parametro um objto q  e da classe Usuario


            return RedirectToAction("Listagem", "Usuario"); //(Listagem) ACTION, (Usuario) CONTROLE uma ação dentro de uma controle chamada Usuario
        }


        public IActionResult Cadastro(){
        return View();
    }
    public IActionResult FaleConosco(){
        return View();
    }

[HttpPost]
    public IActionResult Cadastro(Usuario userFor){
        UsuarioRepository ur =new UsuarioRepository();
            ur.Cadastrar(userFor); //foi colocado no parametro um objto q  e da classe Usuario
            return RedirectToAction("Listagem", "Usuario"); //(Listagem) ACTION, (Usuario) CONTROLE uma ação dentro de uma controle chamada Usuario
        }

    }

    
}