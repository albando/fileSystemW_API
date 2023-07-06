using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft;
using System.Security.Policy;
using RestSharp;
using static System.Net.WebRequestMethods;
using FileUploadDownloadAPI;
using System.Buffers.Text;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        #region Boş Değişken Tanımları
        public static string _url = "http://localhost:5077";
        public string dosyaYolu = "";
        public string dosyaAdi = "";
        public string dosyaBoyutu = "";
        public string dosyaTuru = "";
        public string dosyaOlusturulmaTarihi = "";
        public string userInformation = "";
        public string paneldosyaAdi = "";
        public string paneldosyaYuklenmeTarihi = "";
        public string kesikliDosyaAdi = "";
        public string dosyaYolu2 = "";
        public string dosyaAdi2 = "";
        public string dosyaTuru2 = "";
        #endregion
        static double ConvertBytesToMegabytes(long bytes)
        {
            return Math.Round((bytes / 1024f) / 1024f, 2);
        }

        public static byte[] ConvertToByteArray(string filePath)
        {
            byte[] fileByteArray = System.IO.File.ReadAllBytes(filePath);
            return fileByteArray;
        }

        public MainForm()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {


        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "RAR Dosyası|*.rar| ZIP Dosyası |*.zip";
            file.FilterIndex = 1;
            if (file.ShowDialog() == DialogResult.OK)
            {
                dosyaYolu = file.FileName;
                FileInfo fi = new FileInfo(dosyaYolu);

                dosyaAdi = fi.Name;
                dosyaAdiResult.Text = fi.Name;

                //double byteToMega = fi.Length * Convert.ToDouble(Math.Pow(10, -6));
                double byt = ConvertBytesToMegabytes(fi.Length);
                dosyaBoyutuResult.Text = byt.ToString() + " MB";
                dosyaBoyutu = byt.ToString();

                dosyaTuruResult.Text = fi.Extension.ToString();
                dosyaTuru = fi.Extension.ToString();

                dosyaOlusturulmaResult.Text = fi.CreationTime.ToString();
                dosyaOlusturulmaTarihi = fi.CreationTime.ToString();
            }
        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void dosyaSecPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "RAR Dosyası|*.rar| ZIP Dosyası |*.zip";
            file.FilterIndex = 1;
            if (file.ShowDialog() == DialogResult.OK)
            {
                dosyaYolu = file.FileName;
                FileInfo fi = new FileInfo(dosyaYolu);
                dosyaAdi = fi.Name;
                dosyaAdiResult.Text = fi.Name;

                //double byteToMega = fi.Length * Convert.ToDouble(Math.Pow(10, -6));
                double byt = ConvertBytesToMegabytes(fi.Length);
                dosyaBoyutuResult.Text = byt.ToString() + " MB";
                dosyaBoyutu = byt.ToString();

                dosyaTuruResult.Text = fi.Extension.ToString();
                dosyaTuru = fi.Extension.ToString();

                dosyaOlusturulmaResult.Text = fi.CreationTime.ToString();
                dosyaOlusturulmaTarihi = fi.CreationTime.ToString();
            }
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {

            if (dosyaAdi == "")
            {
                MessageBox.Show("Herhangi bir dosya seçmediniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (kullaniciAdi_textbox.Text == "" || sifre_textbox.Text == "")
                {
                    MessageBox.Show("Kullanıcı adı veya şifre kısmını boş bırakamazsınız!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    Upload r = new Upload();
                    r.UserInformation = Convert.ToBase64String(Encoding.UTF8.GetBytes(kullaniciAdi_textbox.Text + ":" + sifre_textbox.Text));
                    r.FileName = dosyaAdi;
                    var fileByteArray = ConvertToByteArray(dosyaYolu);
                    r.File = fileByteArray;

                    var options = new RestClientOptions(@"http://localhost:5077")
                    {
                        MaxTimeout = -1,
                    };
                    var client = new RestClient(options);
                    var request = new RestRequest(@"/api/Upload", Method.Post);
                    request.AddHeader("Content-Type", "application/json");
                    var body = Newtonsoft.Json.JsonConvert.SerializeObject(r);
                    request.AddStringBody(body, DataFormat.Json);
                    RestResponse response = client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        MessageBox.Show("Seçtiğiniz dosya başarılı bir şekilde yüklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Hata oluştu!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
        }

        
        private void gosterButton_Click(object sender, EventArgs e)
        {
            //GetFiles getFiles = new GetFiles();



            GetFiles getFiles = new GetFiles();
            this.userInformation= Convert.ToBase64String(Encoding.UTF8.GetBytes(kullaniciAdi_textbox1.Text + ":" + sifre_textbox1.Text));
            getFiles.userInformation = this.userInformation;

            if (kullaniciAdi_textbox1.Text == "" || sifre_textbox1.Text == "")
            {
                MessageBox.Show("Kullanıcı adı veya şifre kısmını boş bırakamazsınız!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (AllFilesCheck.Checked == true)
                {
                    getFiles.publicDirectory = true;
                }
                else
                {
                    getFiles.publicDirectory = false;
                }
                if (MyFilesCheck.Checked == true)
                {
                    getFiles.myDirectory = true;

                }
                else
                {
                    getFiles.myDirectory = false;
                }




                var options = new RestClientOptions("http://localhost:5077")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest(@"/api/GetFiles", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                var body = Newtonsoft.Json.JsonConvert.SerializeObject(getFiles);
                request.AddStringBody(body, DataFormat.Json);
                RestResponse response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show("Kullanıcı adı veya şifreyi yanlış girdiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //var XX = response.Content.Replace(@"\", "");
                    //XX = XX.Substring(1);
                    //XX = XX.Substring(0, XX.Length - 1);
                    Root_ dosyalar = Newtonsoft.Json.JsonConvert.DeserializeObject<Root_>(response.Content);

                    sonuclarDataGrid.DataSource = dosyalar.files;
                }
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void goster3_Click(object sender, EventArgs e)
        {



            GetFiles getFiles = new GetFiles();
            getFiles.userInformation = Convert.ToBase64String(Encoding.UTF8.GetBytes(kad3.Text + ":" + ksifre3.Text));
            getFiles.myDirectory = true;
            getFiles.publicDirectory = false;
            if (kad3.Text == "" || ksifre3.Text == "")
            {
                MessageBox.Show("Kullanıcı adı veya şifre kısmını boş bırakamazsınız!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                var options = new RestClientOptions("http://localhost:5077")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest(@"/api/GetFiles", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                var body = Newtonsoft.Json.JsonConvert.SerializeObject(getFiles);
                request.AddStringBody(body, DataFormat.Json);
                RestResponse response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show("Kullanıcı adı veya şifreyi yanlış girdiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //var XX = response.Content.Replace(@"\", "");
                    //XX = XX.Substring(1);
                    //XX = XX.Substring(0, XX.Length - 1);
                    Root_ dosyalar = Newtonsoft.Json.JsonConvert.DeserializeObject<Root_>(response.Content);

                }
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Request r1 = new Request();
            if (kad3.Text == "" || ksifre3.Text == "")
            {
                MessageBox.Show("Kullanıcı adı veya şifre kısmını boş bırakamazsınız!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                r1.UserInformation = Convert.ToBase64String(Encoding.UTF8.GetBytes(kad3.Text + ":" + ksifre3.Text));
                if (f_IDTxt.Text == "")
                {
                    MessageBox.Show("Dosya ID kısmını boş bırakamazsınız!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                r1.FileID = Convert.ToInt32(f_IDTxt.Text);

                if (publicRadio.Checked == true)
                {
                    r1.FileStatus = true;
                }
                else
                {
                    r1.FileStatus = false;

                }
                var options = new RestClientOptions(@"http://localhost:5077")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var _request = new RestRequest(@"/api/FileStatusChange", Method.Post);
                _request.AddHeader("Content-Type", "application/json");
                string body = Newtonsoft.Json.JsonConvert.SerializeObject(r1);
                MessageBox.Show("Başarılı bir şekilde güncellendi", "İşlem Gerçekleştirildi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _request.AddStringBody(body, DataFormat.Json);
                RestResponse response = client.Execute(_request);
            }

        }



        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Request request = new Request();

            request.UserInformation = Convert.ToBase64String(Encoding.UTF8.GetBytes(kad3.Text + ":" + ksifre3.Text));
            request.FileID = Convert.ToInt32(f_IDTxt.Text);

            if (publicRadio.Checked == true)
            {
                request.FileStatus = true;
            }
            else
            {
                request.FileStatus = false;

            }
            var options = new RestClientOptions(@"http://localhost:5077")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var _request = new RestRequest(@"/api/FileStatusChange", Method.Post);
            _request.AddHeader("Content-Type", "application/json");
            string body = Newtonsoft.Json.JsonConvert.SerializeObject(request);

            _request.AddStringBody(body, DataFormat.Json);
            RestResponse response = client.Execute(_request);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                MessageBox.Show("Kullanıcı adı veya şifrenizi yanlış girdiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                MessageBox.Show("Bu dosya size ait değil!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Başarılı bir şekilde güncellendi!", "İşlem Gerçekleştirildi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void dIndir_button_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.OverwritePrompt = false;
            save.CreatePrompt = true;
            save.Title = paneldosyaAdi;
            save.Filter = "RAR Files(*.rar)|*.rar";
            if (dkullaniciAdi_text.Text == "" || dSifre_text.Text == "")
            {
                MessageBox.Show("Kullanıcı adı veya şifre kısmını boş bırakamazsınız!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                Download d = new Download();
                d.UserInformation = Convert.ToBase64String(Encoding.UTF8.GetBytes(dkullaniciAdi_text.Text + ":" + dSifre_text.Text));
                if (dFileID_text.Text == "") {
                    MessageBox.Show("File ID kısmını boş bırakamazsınız!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                d.FileID = Convert.ToInt32(dFileID_text.Text.ToString());
                var options = new RestClientOptions(@"http://localhost:5077")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest(@"/api/DownloadFile", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                var body = Newtonsoft.Json.JsonConvert.SerializeObject(d);
                request.AddStringBody(body, DataFormat.Json);
                RestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show("Kullanıcı adı veya şifrenizi yanlış girdiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (save.ShowDialog() == DialogResult.OK)
                    {


                        DownloadResponse responseD = Newtonsoft.Json.JsonConvert.DeserializeObject<DownloadResponse>(response.Content);
                        System.IO.File.WriteAllBytes(save.FileName, responseD.File);

                    }
                }

            }
        }

        private void silButonu_Click_1(object sender, EventArgs e)
        {
            Delete df = new Delete();
            if (sKullaniciAdi_text.Text == "" || sSifre_text.Text == "")
            {
                MessageBox.Show("Kullanıcı adı veya şifre kısmını boş bırakamazsınız!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                df.UserInformation = Convert.ToBase64String(Encoding.UTF8.GetBytes(sKullaniciAdi_text.Text + ":" + sSifre_text.Text));
                df.FileID = Convert.ToInt32(sFileID_text.Text.ToString());
                var options = new RestClientOptions(@"http://localhost:5077")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest(@"/api/DeleteFile", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                var body = Newtonsoft.Json.JsonConvert.SerializeObject(df);
                request.AddStringBody(body, DataFormat.Json);
                RestResponse response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show("Kullanıcı adı veya şifrenizi yanlış girdiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    string orijinalString = response.Content;
                    string yeniString = orijinalString.Replace("\"", "");

                    if (yeniString == "File is public!")
                    {
                        MessageBox.Show("Dosya herkese açık olduğu için silinemez!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                        MessageBox.Show("Bu dosya mevcut değil veya size ait değil!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    return;
                }
                else
                {
                    MessageBox.Show("Başarılı bir şekilde silindi", "İşlem Gerçekleştirildi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }
    }
}
