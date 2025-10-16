using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ApiTester.ViewModels;
using ApiTester.Models;

namespace ApiTester
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;
        }

        private void ImportCurl_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ImportCurlDialog();
            if (dialog.ShowDialog() == true && !string.IsNullOrWhiteSpace(dialog.CurlText))
            {
                try
                {
                    var curlParser = new Services.CurlParser();
                    var request = curlParser.ParseCurl(dialog.CurlText);
                    
                    // Load data directly into ViewModel
                    _viewModel.Url = request.Url;
                    _viewModel.SelectedMethod = request.Method.ToString();
                    _viewModel.RequestBody = request.Body;
                    _viewModel.HeadersText = string.Join("\n", request.Headers.Select(h => $"{h.Key}: {h.Value}"));
                    
                    MessageBox.Show("cURL imported successfully!", "Import cURL", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Failed to import cURL: {ex.Message}", "Import Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Environment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox?.SelectedValue is string environmentId)
            {
                _viewModel.SelectEnvironment(environmentId);
            }
        }

        private void History_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox?.SelectedItem is ApiRequest request)
            {
                _viewModel.LoadRequestFromHistory(request);
            }
        }
    }
}
