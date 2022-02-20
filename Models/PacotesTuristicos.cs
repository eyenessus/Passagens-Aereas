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
    public class PacotesTuristicos
    {
         public int Id { get; set; }
         public string Nome { get; set; }
         public string Origem { get; set; }
         public string Destino { get; set; }
         public string Atrativos { get; set; }
         public DateTime Saida { get; set; }
         public DateTime Retorno { get; set; }
         public int Usuario { get; set; }
    }
}
