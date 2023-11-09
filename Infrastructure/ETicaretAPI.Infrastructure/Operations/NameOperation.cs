using System.Text.RegularExpressions;

namespace ETicaretAPI.Infrastructure.Operations
{
    public static class NameOperation
    {
        public static string CharacterRegulatory(string name)
        {
            string TurkceKarakterler = @"[ğüşiöçĞÜŞİÖÇ]";
            string OzelKarakterler = @"[/*-+\,`;~&!^'é%]";
            MatchCollection Dogrumu = Regex.Matches(name, TurkceKarakterler);


            //TurkceHarfiDegistir
            if (Dogrumu != null)
            {
                name = Regex.Replace(name, "ğ", "g");
                name = Regex.Replace(name, "Ğ", "G");
                name = Regex.Replace(name, "Ü", "U");
                name = Regex.Replace(name, "ü", "u");
                name = Regex.Replace(name, "Ş", "S");
                name = Regex.Replace(name, "ş", "s");
                name = Regex.Replace(name, "İ", "I");
                name = Regex.Replace(name, "ı", "i");
                name = Regex.Replace(name, "Ö", "O");
                name = Regex.Replace(name, "ö", "o");
                name = Regex.Replace(name, "Ç", "C");
                name = Regex.Replace(name, "ç", "c");
            }
            name = Regex.Replace(name, OzelKarakterler, "");
            name = Regex.Replace(name, " ", "-");

            return name;

        }

    }
}
