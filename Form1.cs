using CriptografiaApp;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace CriptografiaApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGerarChave_Click(object sender, EventArgs e)
        {
            string senha = Microsoft.VisualBasic.Interaction.InputBox("Digite uma senha para proteger a chave:", "Definir Senha", "");
            if (string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Senha não pode ser vazia.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Key Files (*.key)|*.key",
                Title = "Escolha o local para salvar a chave"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (Aes aes = Aes.Create())
                {
                    byte[] salt = GenerateSalt();
                    byte[] chaveDerivada = DerivarChave(senha, salt);

                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        fs.Write(salt, 0, salt.Length);
                        fs.Write(chaveDerivada, 0, chaveDerivada.Length);
                    }

                    MessageBox.Show($"Chave protegida por senha salva em '{saveFileDialog.FileName}'.");
                }
            }
        }

        private void btnSelecionarChave_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Key Files (*.key)|*.key",
                Title = "Selecione a chave de criptografia"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtChave.Text = openFileDialog.FileName;
            }
        }

        private void btnCriptografar_Click(object sender, EventArgs e)
        {
            byte[] chave = ObterChave();
            if (chave == null) return;

            OpenFileDialog openFileDialog = new OpenFileDialog { Title = "Selecione o arquivo para criptografar" };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Arquivo Criptografado (*.enc)|*.enc",
                    Title = "Salvar arquivo criptografado"
                };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] dados = File.ReadAllBytes(openFileDialog.FileName);
                    byte[] dadosCriptografados = CriptografarDados(dados, chave);
                    File.WriteAllBytes(saveFileDialog.FileName, dadosCriptografados);
                    MessageBox.Show("Arquivo criptografado com sucesso!");
                }
            }
        }

        private void btnDescriptografar_Click(object sender, EventArgs e)
        {
            byte[] chave = ObterChave();
            if (chave == null) return;

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Arquivo Criptografado (*.enc)|*.enc",
                Title = "Selecione o arquivo criptografado"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] dadosCriptografados = File.ReadAllBytes(openFileDialog.FileName);
                byte[] dadosDescriptografados = DescriptografarDados(dadosCriptografados, chave);

                SaveFileDialog saveFileDialog = new SaveFileDialog { Title = "Salvar arquivo descriptografado" };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(saveFileDialog.FileName, dadosDescriptografados);
                    MessageBox.Show("Arquivo descriptografado com sucesso!");
                }
            }
        }

        private byte[] ObterChave()
        {
            if (string.IsNullOrEmpty(txtChave.Text) || !File.Exists(txtChave.Text))
            {
                MessageBox.Show("Por favor, selecione uma chave válida.");
                return null;
            }

            string senha = Microsoft.VisualBasic.Interaction.InputBox("Digite a senha da chave:", "Autenticação", "");
            byte[] chaveArmazenada = File.ReadAllBytes(txtChave.Text);
            byte[] salt = new byte[16];
            Array.Copy(chaveArmazenada, salt, salt.Length);
            byte[] chaveDerivada = DerivarChave(senha, salt);

            for (int i = 0; i < 32; i++)
            {
                if (chaveArmazenada[16 + i] != chaveDerivada[i])
                {
                    MessageBox.Show("Senha incorreta.");
                    return null;
                }
            }
            return chaveDerivada;
        }

        private byte[] CriptografarDados(byte[] dados, byte[] chave)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = chave;
                aes.GenerateIV();
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(aes.IV, 0, aes.IV.Length);
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(dados, 0, dados.Length);
                    }
                    return ms.ToArray();
                }
            }
        }

        private byte[] DescriptografarDados(byte[] dados, byte[] chave)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = chave;
                byte[] iv = new byte[16];
                Array.Copy(dados, iv, iv.Length);
                aes.IV = iv;
                using (MemoryStream ms = new MemoryStream(dados, 16, dados.Length - 16))
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (MemoryStream output = new MemoryStream())
                {
                    cs.CopyTo(output);
                    return output.ToArray();
                }
            }
        }

        private byte[] GenerateSalt() => RandomNumberGenerator.GetBytes(16);

        private byte[] DerivarChave(string senha, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(senha, salt, 100000, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(32);
            }
        }

    }
}
