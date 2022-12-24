using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeMvcApiClient.Models
{
    public class Urunler
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public int UrunAdeti { get; set; }
        public string UrunAciklama { get; set; }
        public int KategoriId { get; set; }
        [ForeignKey("KategoriId")] //bir kategorinin birden fazla ürünü olabilir.
        public Kategoriler Kategoriler { get; set; }
    }
}
