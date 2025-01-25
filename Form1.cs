using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace CriptografiaApp
{
    public partial class Form1 : Form
    {
        private const string logFilePath = "criptografia_log.txt";
        private const string outputFilePath = "arquivos_descriptografados.txt";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGerarChave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Key Files (*.key)|*.key",
                Title = "Escolha o local para salvar a chave"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (Aes aes = Aes.Create())
                {
                    File.WriteAllBytes(saveFileDialog.FileName, aes.Key);
                    MessageBox.Show($"Chave salva em '{saveFileDialog.FileName}'.");
                    RegistrarLog("Chave gerada", saveFileDialog.FileName, "N/A");
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
            if (string.IsNullOrEmpty(txtChave.Text) || !File.Exists(txtChave.Text))
            {
                MessageBox.Show("Por favor, selecione uma chave válida.");
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Selecione o arquivo para criptografar"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Arquivo Criptografado (*.enc)|*.enc",
                    Title = "Salvar arquivo criptografado"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] chave = File.ReadAllBytes(txtChave.Text);
                    byte[] dados = File.ReadAllBytes(openFileDialog.FileName);
                    byte[] dadosCriptografados = CriptografarDados(dados, chave);

                    File.WriteAllBytes(saveFileDialog.FileName, dadosCriptografados);
                    MessageBox.Show("Arquivo criptografado com sucesso!");

                    RegistrarLog("Arquivo criptografado", openFileDialog.FileName, Convert.ToBase64String(dados));
                }
            }
        }

        private void btnDescriptografar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtChave.Text) || !File.Exists(txtChave.Text))
            {
                MessageBox.Show("Por favor, selecione uma chave válida.");
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Arquivo Criptografado (*.enc)|*.enc",
                Title = "Selecione o arquivo criptografado"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] chave = File.ReadAllBytes(txtChave.Text);
                byte[] dadosCriptografados = File.ReadAllBytes(openFileDialog.FileName);
                byte[] dadosDescriptografados = DescriptografarDados(dadosCriptografados, chave);

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Title = "Salvar arquivo descriptografado"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(saveFileDialog.FileName, dadosDescriptografados);
                    MessageBox.Show("Arquivo descriptografado com sucesso!");
                    RegistrarLog("Arquivo descriptografado", openFileDialog.FileName, Convert.ToBase64String(dadosDescriptografados));
                    SalvarDescriptografia(saveFileDialog.FileName, dadosDescriptografados);
                }
            }
        }

        private void RegistrarLog(string operacao, string arquivo, string conteudo)
        {
            string logMensagem = $"[{DateTime.Now}] {operacao} - Arquivo: {arquivo} - Conteúdo (Base64): {conteudo}\n";
            File.AppendAllText(logFilePath, logMensagem);
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

        private void SalvarDescriptografia(string caminhoArquivo, byte[] dados)
        {
            string conteudo = Encoding.UTF8.GetString(dados);
            File.AppendAllText(outputFilePath, $"Arquivo: {caminhoArquivo}\n{conteudo}\n\n");
        }
    }
}
