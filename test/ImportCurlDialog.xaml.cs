using System.Windows;

namespace ApiTester
{
    public partial class ImportCurlDialog : Window
    {
        public string CurlText { get; private set; } = string.Empty;

        public ImportCurlDialog()
        {
            InitializeComponent();
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            CurlText = CurlTextBox.Text;
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
