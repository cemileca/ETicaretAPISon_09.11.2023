using System.Text.RegularExpressions;

namespace ETicaretAPI.Infrastructure.Operations
{
    public static class StringControlOperations
    {
    
        public static bool IsThisStringNumeric(string kontorlEdilecekKelime)
        {
            bool BuYaziNumerikmi=false;
            Regex reg = new Regex(RegexDesenleri_Karakterler.PatternTumYaziNumeriktir);
            if (reg.IsMatch(kontorlEdilecekKelime))
            {
                BuYaziNumerikmi = true;
            }
            else
            {
                BuYaziNumerikmi=false;
            }
            return BuYaziNumerikmi;
        }
        public static string WhatsIsThisStringFormat(string kontrolEdilecekKelime)
        {
            return "";
        }
    }
}
