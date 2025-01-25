namespace CriptografiaApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnGerarChave;
        private System.Windows.Forms.Button btnSelecionarChave;
        private System.Windows.Forms.Button btnCriptografar;
        private System.Windows.Forms.Button btnDescriptografar;
        private System.Windows.Forms.TextBox txtChave;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel pnlHeader;

        private void InitializeComponent()
        {
            this.btnGerarChave = new System.Windows.Forms.Button();
            this.btnSelecionarChave = new System.Windows.Forms.Button();
            this.btnCriptografar = new System.Windows.Forms.Button();
            this.btnDescriptografar = new System.Windows.Forms.Button();
            this.txtChave = new System.Windows.Forms.TextBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();

            this.SuspendLayout();

            // Definição de estilos globais
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

            // pnlHeader
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Size = new System.Drawing.Size(700, 80);
            this.pnlHeader.BackColor = System.Drawing.Color.Navy;
            this.pnlHeader.Paint += (sender, e) => {
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddLine(0, 0, this.pnlHeader.Width, 0);
                path.AddLine(this.pnlHeader.Width, 0, this.pnlHeader.Width, this.pnlHeader.Height - 10);
                path.AddArc(this.pnlHeader.Width - 20, this.pnlHeader.Height - 20, 20, 20, 0, 90);
                path.AddLine(this.pnlHeader.Width - 20, this.pnlHeader.Height, 20, this.pnlHeader.Height);
                path.AddArc(0, this.pnlHeader.Height - 20, 20, 20, 90, 90);
                path.AddLine(0, this.pnlHeader.Height - 10, 0, 0);
                this.pnlHeader.Region = new System.Drawing.Region(path);

                // Adicionando sombra maior
                System.Drawing.SolidBrush shadowBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(100, 0, 0, 0));
                e.Graphics.FillRectangle(shadowBrush, 0, this.pnlHeader.Height, this.pnlHeader.Width, 20);
            };

            // lblTitulo
            this.lblTitulo.Text = "CriptografiAPP";
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(30, 20);
            this.pnlHeader.Controls.Add(this.lblTitulo);

            // Layout dos botões (2x2 grid)
            int buttonWidth = 250;
            int buttonHeight = 40;
            int spacing = 20;
            int startX = 100;
            int startY = 100;

            this.btnGerarChave.Location = new System.Drawing.Point(startX, startY);
            this.btnGerarChave.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
            this.btnGerarChave.Text = "Gerar Chave";
            this.btnGerarChave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGerarChave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnGerarChave.ForeColor = System.Drawing.Color.White;
            this.btnGerarChave.Click += new System.EventHandler(this.btnGerarChave_Click);

            this.btnSelecionarChave.Location = new System.Drawing.Point(startX + buttonWidth + spacing, startY);
            this.btnSelecionarChave.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
            this.btnSelecionarChave.Text = "Selecionar Chave";
            this.btnSelecionarChave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecionarChave.BackColor = System.Drawing.Color.SeaGreen;
            this.btnSelecionarChave.ForeColor = System.Drawing.Color.White;
            this.btnSelecionarChave.Click += new System.EventHandler(this.btnSelecionarChave_Click);

            this.btnCriptografar.Location = new System.Drawing.Point(startX, startY + buttonHeight + spacing);
            this.btnCriptografar.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
            this.btnCriptografar.Text = "Criptografar";
            this.btnCriptografar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCriptografar.BackColor = System.Drawing.Color.OrangeRed;
            this.btnCriptografar.ForeColor = System.Drawing.Color.White;
            this.btnCriptografar.Click += new System.EventHandler(this.btnCriptografar_Click);

            this.btnDescriptografar.Location = new System.Drawing.Point(startX + buttonWidth + spacing, startY + buttonHeight + spacing);
            this.btnDescriptografar.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
            this.btnDescriptografar.Text = "Descriptografar";
            this.btnDescriptografar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDescriptografar.BackColor = System.Drawing.Color.DarkViolet;
            this.btnDescriptografar.ForeColor = System.Drawing.Color.White;
            this.btnDescriptografar.Click += new System.EventHandler(this.btnDescriptografar_Click);

            // txtChave
            this.txtChave.Location = new System.Drawing.Point(100, 250);
            this.txtChave.Size = new System.Drawing.Size(500, 30);
            this.txtChave.ReadOnly = true;
            this.txtChave.PlaceholderText = "Caminho da chave selecionada";
            this.txtChave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Form1
            this.ClientSize = new System.Drawing.Size(700, 350);
            this.MaximizeBox = false;
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.btnGerarChave);
            this.Controls.Add(this.btnSelecionarChave);
            this.Controls.Add(this.btnCriptografar);
            this.Controls.Add(this.btnDescriptografar);
            this.Controls.Add(this.txtChave);
            this.Text = "CriptografiAPP";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            
            this.ResumeLayout(false);
        }
    }
}
