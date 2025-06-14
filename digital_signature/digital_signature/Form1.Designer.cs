namespace digital_signature
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            btnCertPath = new Button();
            txtCertpath = new TextBox();
            txtRootPath = new TextBox();
            btnRootPath = new Button();
            txtFolderSource = new TextBox();
            btnFolderSource = new Button();
            txtFolderDestination = new TextBox();
            btnFolderDestination = new Button();
            label2 = new Label();
            txtX = new TextBox();
            txtY = new TextBox();
            label3 = new Label();
            txtFolderErr = new TextBox();
            button1 = new Button();
            txtFolderSucess = new TextBox();
            btnFolderSucces = new Button();
            listView1 = new ListView();
            btnSign = new Button();
            txtPin = new TextBox();
            label4 = new Label();
            txtLogo = new TextBox();
            btnLogo = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(514, 9);
            label1.Name = "label1";
            label1.Size = new Size(537, 38);
            label1.TabIndex = 0;
            label1.Text = "Welcome to sign PDF using USB TOKEN";
            // 
            // btnCertPath
            // 
            btnCertPath.Location = new Point(18, 69);
            btnCertPath.Name = "btnCertPath";
            btnCertPath.Size = new Size(150, 29);
            btnCertPath.TabIndex = 2;
            btnCertPath.Text = "CertPath";
            btnCertPath.UseVisualStyleBackColor = true;
            btnCertPath.Click += btnCertPath_Click;
            // 
            // txtCertpath
            // 
            txtCertpath.Location = new Point(190, 69);
            txtCertpath.Name = "txtCertpath";
            txtCertpath.Size = new Size(409, 27);
            txtCertpath.TabIndex = 3;
            // 
            // txtRootPath
            // 
            txtRootPath.Location = new Point(190, 123);
            txtRootPath.Name = "txtRootPath";
            txtRootPath.Size = new Size(409, 27);
            txtRootPath.TabIndex = 5;
            // 
            // btnRootPath
            // 
            btnRootPath.Location = new Point(18, 123);
            btnRootPath.Name = "btnRootPath";
            btnRootPath.Size = new Size(150, 29);
            btnRootPath.TabIndex = 4;
            btnRootPath.Text = "RootPath";
            btnRootPath.UseVisualStyleBackColor = true;
            btnRootPath.Click += btnRootPath_Click;
            // 
            // txtFolderSource
            // 
            txtFolderSource.Location = new Point(190, 176);
            txtFolderSource.Name = "txtFolderSource";
            txtFolderSource.Size = new Size(409, 27);
            txtFolderSource.TabIndex = 7;
            // 
            // btnFolderSource
            // 
            btnFolderSource.Location = new Point(18, 174);
            btnFolderSource.Name = "btnFolderSource";
            btnFolderSource.Size = new Size(150, 29);
            btnFolderSource.TabIndex = 6;
            btnFolderSource.Text = "Input Folder";
            btnFolderSource.UseVisualStyleBackColor = true;
            btnFolderSource.Click += btnFolderSource_Click;
            // 
            // txtFolderDestination
            // 
            txtFolderDestination.Location = new Point(190, 227);
            txtFolderDestination.Name = "txtFolderDestination";
            txtFolderDestination.Size = new Size(409, 27);
            txtFolderDestination.TabIndex = 9;
            // 
            // btnFolderDestination
            // 
            btnFolderDestination.Location = new Point(18, 227);
            btnFolderDestination.Name = "btnFolderDestination";
            btnFolderDestination.Size = new Size(150, 29);
            btnFolderDestination.TabIndex = 8;
            btnFolderDestination.Text = "Output Folder";
            btnFolderDestination.UseVisualStyleBackColor = true;
            btnFolderDestination.Click += btnFolderDestination_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 436);
            label2.Name = "label2";
            label2.Size = new Size(54, 20);
            label2.TabIndex = 10;
            label2.Text = "Coor X";
            // 
            // txtX
            // 
            txtX.Location = new Point(190, 429);
            txtX.Name = "txtX";
            txtX.Size = new Size(409, 27);
            txtX.TabIndex = 11;
            // 
            // txtY
            // 
            txtY.Location = new Point(190, 479);
            txtY.Name = "txtY";
            txtY.Size = new Size(409, 27);
            txtY.TabIndex = 13;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 486);
            label3.Name = "label3";
            label3.Size = new Size(53, 20);
            label3.TabIndex = 12;
            label3.Text = "Coor Y";
            // 
            // txtFolderErr
            // 
            txtFolderErr.Location = new Point(190, 277);
            txtFolderErr.Name = "txtFolderErr";
            txtFolderErr.Size = new Size(409, 27);
            txtFolderErr.TabIndex = 15;
            // 
            // button1
            // 
            button1.Location = new Point(18, 277);
            button1.Name = "button1";
            button1.Size = new Size(150, 29);
            button1.TabIndex = 14;
            button1.Text = "Folder Log err";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // txtFolderSucess
            // 
            txtFolderSucess.Location = new Point(190, 332);
            txtFolderSucess.Name = "txtFolderSucess";
            txtFolderSucess.Size = new Size(409, 27);
            txtFolderSucess.TabIndex = 17;
            // 
            // btnFolderSucces
            // 
            btnFolderSucces.Location = new Point(18, 332);
            btnFolderSucces.Name = "btnFolderSucces";
            btnFolderSucces.Size = new Size(150, 29);
            btnFolderSucces.TabIndex = 16;
            btnFolderSucces.Text = "Folder Log Success";
            btnFolderSucces.UseVisualStyleBackColor = true;
            btnFolderSucces.Click += btnFolderSucces_Click;
            // 
            // listView1
            // 
            listView1.Location = new Point(618, 69);
            listView1.Name = "listView1";
            listView1.Size = new Size(859, 516);
            listView1.TabIndex = 18;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // btnSign
            // 
            btnSign.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSign.ForeColor = SystemColors.ControlText;
            btnSign.Location = new Point(655, 612);
            btnSign.Name = "btnSign";
            btnSign.Size = new Size(237, 57);
            btnSign.TabIndex = 19;
            btnSign.Text = "START SIGN";
            btnSign.UseVisualStyleBackColor = true;
            btnSign.Click += btnSign_Click;
            // 
            // txtPin
            // 
            txtPin.Location = new Point(190, 530);
            txtPin.Name = "txtPin";
            txtPin.Size = new Size(409, 27);
            txtPin.TabIndex = 21;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(22, 537);
            label4.Name = "label4";
            label4.Size = new Size(29, 20);
            label4.TabIndex = 20;
            label4.Text = "Pin";
            // 
            // txtLogo
            // 
            txtLogo.Location = new Point(190, 385);
            txtLogo.Name = "txtLogo";
            txtLogo.Size = new Size(409, 27);
            txtLogo.TabIndex = 23;
            // 
            // btnLogo
            // 
            btnLogo.Location = new Point(18, 385);
            btnLogo.Name = "btnLogo";
            btnLogo.Size = new Size(150, 29);
            btnLogo.TabIndex = 22;
            btnLogo.Text = "Logo Path";
            btnLogo.UseVisualStyleBackColor = true;
            btnLogo.Click += btnLogo_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1489, 681);
            Controls.Add(txtLogo);
            Controls.Add(btnLogo);
            Controls.Add(txtPin);
            Controls.Add(label4);
            Controls.Add(btnSign);
            Controls.Add(listView1);
            Controls.Add(txtFolderSucess);
            Controls.Add(btnFolderSucces);
            Controls.Add(txtFolderErr);
            Controls.Add(button1);
            Controls.Add(txtY);
            Controls.Add(label3);
            Controls.Add(txtX);
            Controls.Add(label2);
            Controls.Add(txtFolderDestination);
            Controls.Add(btnFolderDestination);
            Controls.Add(txtFolderSource);
            Controls.Add(btnFolderSource);
            Controls.Add(txtRootPath);
            Controls.Add(btnRootPath);
            Controls.Add(txtCertpath);
            Controls.Add(btnCertPath);
            Controls.Add(label1);
            Name = "Form1";
            Text = "SIGN PDF";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnCertPath;
        private TextBox txtCertpath;
        private TextBox txtRootPath;
        private Button btnRootPath;
        private TextBox txtFolderSource;
        private Button btnFolderSource;
        private TextBox txtFolderDestination;
        private Button btnFolderDestination;
        private Label label2;
        private TextBox txtX;
        private TextBox txtY;
        private Label label3;
        private TextBox txtFolderErr;
        private Button button1;
        private TextBox txtFolderSucess;
        private Button btnFolderSucces;
        private ListView listView1;
        private Button btnSign;
        private TextBox txtPin;
        private Label label4;
        private TextBox txtLogo;
        private Button btnLogo;
    }
}
