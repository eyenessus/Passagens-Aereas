using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Google.Models;

namespace Google.Models
{
    public class Usuario //classe
    {
    //atributos da classe de Usuario
    //objtivo dessa classe e representa os atributos da classe, considerando a TIPAGEM (INT, STRING,DATE)
    //CRUD C - CREATE U- UPTDADE R- READ D-DELET
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}