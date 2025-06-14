using System.Text;
using System.Windows.Forms;

namespace digital_signature
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.SetOut(new ListViewWriter(listView1));
            txtCertpath.Text = "D:\\1628124307224.crt";
            txtRootPath.Text = "D:\\ROOT1628124288103.crt";
            txtX.Text = "420";
            txtY.Text = "780";
            txtFolderSource.Text = "D:\\";
            txtFolderDestination.Text = "D:\\";
            txtFolderErr.Text = "D:\\";
            txtFolderSucess.Text = "D:\\";
            txtPin.Text = "12345678";
        }

        #region check

        private void btnCertPath_Click(object sender, EventArgs e)
        {
            string InitialDirectory = "D:\\";
            string Filter = "Certificate files (*.crt)|*.crt|All files (*.*)|*.*";
            openDialog(InitialDirectory, Filter, txtCertpath);
        }

        private void btnLogo_Click(object sender, EventArgs e)
        {
            string InitialDirectory = @"D:\";
            string Filter = "Image files (*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All files (*.*)|*.*";
            openDialog(InitialDirectory, Filter, txtLogo);
        }

        public void openDialog(string InitialDirectory, string Filter, TextBox textBox)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = InitialDirectory;
                openFileDialog.Filter = Filter;
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = openFileDialog.FileName;

                    textBox.Text = selectedFile;

                    Console.WriteLine("Selected file: " + selectedFile);
                }
            }
        }


        public class ListViewWriter : TextWriter
        {
            private ListView _listView;
            private delegate void SafeWrite(string value);

            public ListViewWriter(ListView listView)
            {
                _listView = listView;
                if (_listView.Columns.Count == 0)
                {
                    _listView.Columns.Add("LOG SESSION", _listView.Width - 10);
                    _listView.View = View.Details;
                    _listView.FullRowSelect = true;
                }
            }

            public override Encoding Encoding => Encoding.UTF8;

            public override void WriteLine(string value)
            {
                if (_listView.InvokeRequired)
                {
                    _listView.Invoke(new SafeWrite(WriteLine), value);
                }
                else
                {
                    _listView.Items.Add(new ListViewItem(value));
                    _listView.EnsureVisible(_listView.Items.Count - 1);
                }
            }

            public override void Write(char value) { }
        }

        private void btnRootPath_Click(object sender, EventArgs e)
        {
            string InitialDirectory = "D:\\";
            string Filter = "Certificate files (*.crt)|*.crt|All files (*.*)|*.*";
            openDialog(InitialDirectory, Filter, txtRootPath);
        }

        private void btnFolderSource_Click(object sender, EventArgs e)
        {
            string InitialDirectory = "D:\\";
            openFolderDialog(InitialDirectory, txtFolderSource);
        }

        public void openFolderDialog(string InitialDirectory, TextBox textBox)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Chọn thư mục";
                folderDialog.SelectedPath = InitialDirectory;
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFolder = folderDialog.SelectedPath;

                    textBox.Text = selectedFolder;

                    Console.WriteLine("Selected folder: " + selectedFolder);
                }
            }
        }

        private void btnFolderDestination_Click(object sender, EventArgs e)
        {
            string InitialDirectory = "D:\\";
            openFolderDialog(InitialDirectory, txtFolderDestination);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string InitialDirectory = "D:\\";
            openFolderDialog(InitialDirectory, txtFolderErr);
        }

        private void btnFolderSucces_Click(object sender, EventArgs e)
        {
            string InitialDirectory = "D:\\";
            openFolderDialog(InitialDirectory, txtFolderSucess);
        }

        #endregion

        #region sign

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCertpath.Text) || !File.Exists(txtCertpath.Text))
            {
                MessageBox.Show("Vui lòng chọn file Intermediate Cert hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtRootPath.Text) || !File.Exists(txtRootPath.Text))
            {
                MessageBox.Show("Vui lòng chọn file Root Cert hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLogo.Text) || !File.Exists(txtLogo.Text))
            {
                MessageBox.Show("Vui lòng chọn file logo hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!float.TryParse(txtX.Text, out float x))
            {
                MessageBox.Show("Giá trị X không hợp lệ. Vui lòng nhập số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!float.TryParse(txtY.Text, out float y))
            {
                MessageBox.Show("Giá trị Y không hợp lệ. Vui lòng nhập số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(txtFolderSource.Text))
            {
                MessageBox.Show("Thư mục nguồn không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(txtFolderDestination.Text))
            {
                MessageBox.Show("Thư mục đích không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(txtFolderErr.Text))
            {
                MessageBox.Show("Thư mục lưu log lỗi không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(txtFolderSucess.Text))
            {
                MessageBox.Show("Thư mục lưu log thành công không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtPin.Text))
            {
                MessageBox.Show("Vui lòng nhập mã Pin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Console.WriteLine("Tất cả kiểm tra hợp lệ. Bắt đầu ký...");
            if (sign())
            {
                Console.WriteLine("Hoàn tất ký");
            }
        }



        public bool sign()
        {
            bool ret = true;
            string pkcs11Path = @"C:\Windows\System32\eTPKCS11.dll";

            var loader = new CertificateLoader();
            try
            {
                var cert = loader.LoadCertificateFromToken(pkcs11Path, txtPin.Text);
                Console.WriteLine("✔️ Tải chứng thư thành công!");
                Console.WriteLine("🔹 Subject: " + cert.SubjectDN);
                Console.WriteLine("🔹 Issuer : " + cert.IssuerDN);
                Console.WriteLine("🔹 Serial : " + cert.SerialNumber);
                string folderPath = txtFolderSource.Text;
                string outputFolder = txtFolderDestination.Text;
                string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");
                var service = new PdfSignerService();
                string timestamp = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string logSuccessPath = Helpers.InitLogFile(txtFolderSucess.Text, "LogSuccess", timestamp);
                string logErrPath = Helpers.InitLogFile(txtFolderSucess.Text, "LogErr", timestamp);

                foreach (string inputFile in pdfFiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(inputFile);
                    string outputFile = Path.Combine(outputFolder, fileName + "_signed.pdf");

                    service.SignPdf(inputFile, outputFile, pkcs11Path, txtPin.Text, float.Parse(txtX.Text), float.Parse(txtY.Text), logErrPath, logSuccessPath, txtCertpath.Text, txtRootPath.Text, $"{fileName}_signed.pdf", txtLogo.Text);
                    Console.WriteLine($"✔️ Đã ký: {outputFile}");
                }
            }
            catch (Exception ex)
            {
                ret = false;
                Console.WriteLine("❌ Lỗi: " + ex.Message);
            }
            return ret;
        }

        #endregion

       
    }
}
