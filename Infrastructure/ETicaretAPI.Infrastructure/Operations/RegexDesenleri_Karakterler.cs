using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Operations
{
    public static class RegexDesenleri_Karakterler
    {
        public static string TurkceKarakterler = @"[ğüşiöçĞÜŞİÖÇ]";
        public static string OzelKarakterler = @"[/*-+\,`;~&!^'é%]";
        public static string SayilarEvrensel = @"[0-9]"; // \d- Regex dilie bu da Rakamsal sayısal demektir.
        public static string PatternTumYaziNumeriktir = "^[0-9]+$"; //Evrensel
        //public static string PatternTumYaziNumeriktirArapcaDahil = "^[0-9]+$"; //Evrensel
    }

}
