using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MD5Sifreleme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = MD5(textBox1.Text);
            label5.Text = SHA1(textBox1.Text);
            label7.Text = SHA256(textBox1.Text);
            label9.Text = SHA384(textBox1.Text);
            label11.Text = SHA512(textBox1.Text);
            MessageBox.Show(DESSifrele(textBox1.Text));
        }
        //HASH ŞİFRELEME YÖNTEMLERİ****************************************************************************************************
        public static byte[] ByteDonustur(string deger)
        {

            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            return ByteConverter.GetBytes(deger);

        }

        public static byte[] Byte8(string deger)
        {
            char[] arrayChar = deger.ToCharArray();
            byte[] arrayByte = new byte[arrayChar.Length];
            for (int i = 0; i < arrayByte.Length; i++)
            {
                arrayByte[i] = Convert.ToByte(arrayChar[i]);
            }
            return arrayByte;
        }

        //MD5 Şifreleme
        public string MD5(string strGiris)
        {
            if (strGiris == "" || strGiris == null)
            {
                throw new ArgumentNullException("Şifreşenecek veri yok!");
            }
            else
            {
                MD5CryptoServiceProvider sifre = new MD5CryptoServiceProvider();
                byte[] arySifre = ByteDonustur(strGiris);
                byte[] aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash);
            }
        }


        //SHA1 Şifreleme
        public string SHA1(string strGiris)
        {
            if (strGiris == "" || strGiris == null)
            {
                throw new ArgumentNullException("Şifreşenecek veri yok!");
            }
            else
            {
                SHA1CryptoServiceProvider sifre = new SHA1CryptoServiceProvider();
                byte[] arySifre = ByteDonustur(strGiris);
                byte[] aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash);
            }
        }




        //SHA256 Şifreleme
        public string SHA256(string strGiris)
        {
            if (strGiris == "" || strGiris == null)
            {
                throw new ArgumentNullException("Şifreşenecek veri yok!");
            }
            else
            {
                SHA256Managed sifre = new SHA256Managed();
                byte[] arySifre = ByteDonustur(strGiris);
                byte[] aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash);
            }
        }


        //SHA384 Şifreleme
        public string SHA384(string strGiris)
        {
            if (strGiris == "" || strGiris == null)
            {
                throw new ArgumentNullException("Şifreşenecek veri yok!");
            }
            else
            {
                SHA384Managed sifre = new SHA384Managed();
                byte[] arySifre = ByteDonustur(strGiris);
                byte[] aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash);
            }
        }



        //SHA512 Şifreleme
        public string SHA512(string strGiris)
        {
            if (strGiris == "" || strGiris == null)
            {
                throw new ArgumentNullException("Şifreşenecek veri yok!");
            }
            else
            {
                SHA512Managed sifre = new SHA512Managed();
                byte[] arySifre = ByteDonustur(strGiris);
                byte[] aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash);
            }
        }


        //SİMETRİK ŞİFRELEME*********************************************************************************************************************


        //DES Şifreleme Yöntemi
        public string DESSifrele(string strGiris)
        {
            string sonuc = "";
            if (strGiris == "" || strGiris == null)
            {
                throw new ArgumentNullException("Şifrelenecek veri yok");
            }
            else
            {
                byte[] aryKey = Byte8("12345678"); // BURAYA 8 bit string DEĞER GİRİN
                byte[] aryIV = Byte8("12345678"); // BURAYA 8 bit string DEĞER GİRİN
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(aryKey, aryIV), CryptoStreamMode.Write);
                StreamWriter writer = new StreamWriter(cs);
                writer.Write(strGiris);
                writer.Flush();
                cs.FlushFinalBlock();
                writer.Flush();
                sonuc = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
                writer.Dispose();
                cs.Dispose();
                ms.Dispose();
            }
            return sonuc;
        }

        public string DESCoz(string strGiris)
        {
            string strSonuc = "";
            if (strGiris == "" || strGiris == null)
            {
                throw new ArgumentNullException("Şifrelenecek veri yok.");
            }
            else
            {
                byte[] aryKey = Byte8("12345678");
                byte[] aryIV = Byte8("12345678");
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream(Convert.FromBase64String(strGiris));
                CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(aryKey, aryIV), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(cs);
                strSonuc = reader.ReadToEnd();
                reader.Dispose();
                cs.Dispose();
                ms.Dispose();
            }
            return strSonuc;
        }



    }
}
