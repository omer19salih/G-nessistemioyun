using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Günessistemioyun
{
    public class Soru
    {
        public string Metin { get; set; }
        public string[] Cevaplar { get; set; }
        public int DogruCevapIndex { get; set; }
        public string Resources { get; set; }
        // public string ResimDosyasi { get; set; } // İsteğe bağlı: Resim dosya adı için
    }
}