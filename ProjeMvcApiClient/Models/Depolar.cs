using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeMvcApiClient.Models
{
    public class Depolar
    {
        public int DepoId { get; set; }
        public string DepoAdi { get; set; }
        public string DepoBolge { get; set; }
    }
}
