using DB;
using System.Configuration;
using System.Windows.Forms;

namespace Question_App
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["mssqlDB"].ConnectionString;
                Database.Open(connectionString);
            }
            catch (System.Exception)
            {
                MessageBox.Show(
                    text: "Не удалось подключиться к базе данных.",
                    caption: "Ошибка",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error
                );
                Application.Exit();
            }
        }
    }
}
