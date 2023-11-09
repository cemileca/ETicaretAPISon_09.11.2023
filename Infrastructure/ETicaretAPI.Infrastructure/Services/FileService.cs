using ETicaretAPI.Application.Services;
using ETicaretAPI.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Buffers;

namespace ETicaretAPI.Infrastructure.Services
{
    public class FileService : IFileService
    {
        readonly private IWebHostEnvironment _webHostEnvironment;

        #region CONSTRUCTOR
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion


        string path1 = "resource/ProductImages";

        public async Task<bool> UploadAsync(string path, IFormFileCollection filess)
        {
            try
            {

                string uploadFilePath = Path.Combine(_webHostEnvironment.WebRootPath, path); //wwwwroot'un yolunu çektik (_webHostEnvironment.WebRootPath) bunun sayesinde, path ile kombine yaptık ve dosyalarımızı kaydedeceğimiz Kalsörümüzün tam yolunu belirledik
                                                                                             //  string uploadFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), path); //wwwwroot'un yolunu çektik (_webHostEnvironment.WebRootPath) bunun sayesinde, path ile kombine yaptık ve dosyalarımızı kaydedeceğimiz Kalsörümüzün tam yolunu belirledik

                await BuKalsorYoluYoksaOlustur(uploadFilePath); //Control: Gösterilen isimde Klasör mevcutmudur? Değilse oluştur!

                AppDomain.CurrentDomain.SetData(path, uploadFilePath);
                var setPath = AppDomain.CurrentDomain.GetData(path);

                foreach (IFormFile file in filess)
                {
                    string newFileName = NameOperation.CharacterRegulatory(file.FileName); //ele alınan file isminde Türkçe ve Sembol varsa düzelt!
                    await DosyayiKlasoreKayitEt(newFileName, setPath.ToString(), file);
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #region METOD-DosyayiKlasoreKayitEt
        private async Task<bool> DosyayiKlasoreKayitEt(string yeniDosyaIsmi, string setPath, IFormFile file)
        {
            bool BuDosyaIsmiTekrarMi = await BuIsisimdeDosyaVarmi(setPath, yeniDosyaIsmi);// ele alınan file isminin aynısı belirlenmiş Klasörde mevcut mu?

            if (!BuDosyaIsmiTekrarMi)
            {
                await BuDosyaIsminiVerilenKlasorYolunaEkle(setPath, yeniDosyaIsmi, file);
            }
            else
            {
                yeniDosyaIsmi = await TekrariOlanDosyaIsmininSonunaKopyaRakaminiEkle(yeniDosyaIsmi);
                await DosyayiKlasoreKayitEt(yeniDosyaIsmi, setPath, file);
            }
            return true;
        }

        #endregion

        #region METOD-TekrariOlanDosyaIsmininSonunaKopyaRakaminiEkle

        private async Task<string> TekrariOlanDosyaIsmininSonunaKopyaRakaminiEkle(string tekrariOlanDosyaIsmi)
        {
            int KopyaSayisi = await BuFileIsmiKacinciKopyaOldugunuBul(tekrariOlanDosyaIsmi);

            string uzanti = tekrariOlanDosyaIsmi.Substring(tekrariOlanDosyaIsmi.LastIndexOf("."));
            string yeniDosyaIsmi;
            if (KopyaSayisi != -1)
            {
                yeniDosyaIsmi = tekrariOlanDosyaIsmi.Remove(tekrariOlanDosyaIsmi.LastIndexOf("(")) + $"({++KopyaSayisi})" + uzanti;
            }
            else
            {
                yeniDosyaIsmi = tekrariOlanDosyaIsmi.Remove(tekrariOlanDosyaIsmi.LastIndexOf(".")) + "(1)" + uzanti;
            }


            return yeniDosyaIsmi;
        }

        #endregion

        #region METOD-BuFileIsmiKacinciKopyaOldugunuBul
        public async Task<int> BuFileIsmiKacinciKopyaOldugunuBul(string TekrariOlanKelime)
        {
            int ParantezarasiRakam;
            int SonuncuKapalıParantezIndexi = TekrariOlanKelime.LastIndexOf(')');
            int SonuncuAcikParantezIndexi = TekrariOlanKelime.LastIndexOf('(');
            string IkiParantezArasindakiYaziyiAl = "";
            string uzantisizDosyaIsmi = Path.GetFileNameWithoutExtension(TekrariOlanKelime);

            if (uzantisizDosyaIsmi.Substring(uzantisizDosyaIsmi.Length - 1) == ")" && SonuncuAcikParantezIndexi != -1)
            {
                IkiParantezArasindakiYaziyiAl = uzantisizDosyaIsmi.Substring(SonuncuAcikParantezIndexi + 1, SonuncuKapalıParantezIndexi - SonuncuAcikParantezIndexi - 1);
            }

            return int.TryParse(IkiParantezArasindakiYaziyiAl, out ParantezarasiRakam) ? (ParantezarasiRakam > 0 ? ParantezarasiRakam : -1) : -1;

        }

        #endregion

        #region METOD-BuDosyaIsminiVerilenKlasorYolunaEkle

        private async Task<bool> BuDosyaIsminiVerilenKlasorYolunaEkle(string setPath, string yeniDosyaAdi, IFormFile formFile)
        {
            string path = Path.Combine(setPath, yeniDosyaAdi);
            using FileStream fileStream = new(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, 1024 * 1024, useAsync: false);
            await formFile.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            return true;
        }

        #endregion

        #region METOD-BuIsisimdeDosyaVarmi
        private async Task<bool> BuIsisimdeDosyaVarmi(string path, string newFileName)
        {
            bool dosyaVar = false;
            if (File.Exists($"{path}\\{newFileName}"))
            {
                dosyaVar = true;
            }
            else
            {
                dosyaVar = false;
            }
            return dosyaVar;
        }

        #endregion

        #region METOD-BuKalsorYoluYoksaOlustur
        public async Task<bool> BuKalsorYoluYoksaOlustur(string path)
        {
            if (!Directory.Exists(path))  //Control: Aynı isimde dosya mevcutmudur? Değilse oluştur!
                Directory.CreateDirectory(path);
            return true;
        }

        #endregion

        #region METOD-DosyaIsmiVeUzantisi

        public async Task<(string extention, string oldNameWithOutExtention)> DosyaIsmiVeUzantisi(string fileFullName)
        {
            string extention = Path.GetExtension(fileFullName);  // file'in uzantısını alıyor
            string oldName = Path.GetFileNameWithoutExtension(fileFullName); // file'in uzantı dışındakı ismini alıyor
            return (extention, oldName);
        }

        #endregion



    }
}
