using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeMvcApiClient.Models
{
    public class Personeller
    {
        public int PersonelId { get; set; }
        public string PAdsoyad { get; set; }
        public string PCinsiyet { get; set; }
        public string PYas { get; set; }
    }
}
