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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            UsuarioRepository ur= new UsuarioRepository(); //ur e o nome qualquer do objto que vem de UsuarioRepository


            ur.TestarConexao();
            // chamando novamente o ur que e o objto acima, chamo o metodo que esta na classe UsuarioRepository TestarConexao();


            return View();
        }
        public IActionResult Privacy()
        {
            
            return View();
        }
        public IActionResult FaleConosco(){
            return View();
        }
        [HttpPost]
        public IActionResult FaleConosco(FaleConosco fc){//recebe os dados preenchidos no formulario
             //atribuir o ID da sessao do usuario loga (abaixo)
            fc.Usuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            FaleConoscoRepository ur = new FaleConoscoRepository();
            ur.Cadastrar(fc);
            return RedirectToAction("Cadastro", "Usuario");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
