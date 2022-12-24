using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeMvcApiClient.Models
{
    public class Subeler
    {
        public int SubeId { get; set; }
        public string SubeAd { get; set; }
        public int DepoId { get; set; }
        [ForeignKey("DepoId")]
        public Depolar Depolar { get; set; }
    }
}
