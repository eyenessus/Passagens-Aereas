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
    public class PacotesController : Controller
    {
        public IActionResult ListaPacotes()
        {
            if( HttpContext.Session.GetInt32("IdUsuario") == null ){//valida se o usaurio esta logado (se caso tiver registro)
                return RedirectToAction("Login","Usuario");// redirecionamento para a pagina login da classe CONTROLE
            }
            PacotesTuristicosRepository ob = new PacotesTuristicosRepository();
            List<PacotesTuristicos> Lista = ob.Listar();
            return View(Lista);
        }
        public IActionResult Excluir (int Id){
            if( HttpContext.Session.GetInt32("IdUsuario") == null ){//valida se o usaurio esta logado (se caso tiver registro)
                return RedirectToAction("Login","Usuario");// redirecionamento para a pagina login da classe CONTROLE
            }
            PacotesTuristicosRepository ob = new PacotesTuristicosRepository();
            PacotesTuristicos userEncontrado = ob.Buscar(Id);
            ob.Excluir(userEncontrado);
            return RedirectToAction("ListaPacotes", "Pacotes");
        }
        public IActionResult Cadastro()
        {
            if( HttpContext.Session.GetInt32("IdUsuario") == null ){//valida se o usaurio esta logado (se caso tiver registro)
                return RedirectToAction("Login","Usuario");// redirecionamento para a pagina login da classe CONTROLE
            }
            return View();
        }
        [HttpPost]
        public IActionResult Cadastro(PacotesTuristicos user)
        {
            PacotesTuristicosRepository ob = new PacotesTuristicosRepository();
            ob.Cadastrar(user);
            return RedirectToAction("ListaPacotes", "Pacotes");
        
        }
        public IActionResult Editar(int Id){
            PacotesTuristicosRepository ob = new PacotesTuristicosRepository();
            PacotesTuristicos userEncontrado = ob.Buscar(Id);
            return View(userEncontrado);

        }
        [HttpPost]
         public IActionResult Editar(PacotesTuristicos user){
            PacotesTuristicosRepository ob = new PacotesTuristicosRepository();
            ob.Editar(user);
            return RedirectToAction("ListaPacotes", "Pacotes");

        }
    }
}
