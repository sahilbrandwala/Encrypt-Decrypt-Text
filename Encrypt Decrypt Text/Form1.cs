using System.Security.Cryptography;
using System.Text;

namespace Encrypt_Decrypt_Text
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            byte[] data = Encoding.UTF8.GetBytes(this.textBox1.Text);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(Encoding.UTF8.GetBytes(this.textBox3.Text));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider
                {
                    Key = keys,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    this.textBox2.Text = Convert.ToBase64String(results, 0, results.Length).ToString();
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                string result = "";
                byte[] data = Convert.FromBase64String(this.textBox1.Text);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(Encoding.UTF8.GetBytes(this.textBox3.Text));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider
                    {
                        Key = keys,
                        Mode = CipherMode.ECB,
                        Padding = PaddingMode.PKCS7
                    })
                    {
                        ICryptoTransform transform = tripDes.CreateDecryptor();
                        try
                        {
                            byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                            result = Encoding.UTF8.GetString(results);
                        }
                        catch
                        {
                            result = "Incorrect Password";
                        }
                    }
                }
                this.textBox2.Text = result;
            }
        }
    }
}
