using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ApiTester.Models;
using ApiTester.Services;

namespace ApiTester.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly HttpService _httpService;
        private readonly StorageService _storageService;
        private readonly EnvironmentService _environmentService;

        private string _url = string.Empty;
        private string _selectedMethod = "GET";
        private string _headersText = string.Empty;
        private string _requestBody = string.Empty;
        private string _responseBody = string.Empty;
        private string _statusText = "Ready";
        private bool _isLoading;
        private string _curlImportText = string.Empty;

        public MainViewModel()
        {
            _httpService = new HttpService();
            _storageService = new StorageService();
            _environmentService = new EnvironmentService();

            LoadData();
            InitializeCommands();
        }

        #region Properties

        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }

        public string SelectedMethod
        {
            get => _selectedMethod;
            set => SetProperty(ref _selectedMethod, value);
        }

        public string HeadersText
        {
            get => _headersText;
            set => SetProperty(ref _headersText, value);
        }

        public string RequestBody
        {
            get => _requestBody;
            set => SetProperty(ref _requestBody, value);
        }

        public string ResponseBody
        {
            get => _responseBody;
            set => SetProperty(ref _responseBody, value);
        }

        public string StatusText
        {
            get => _statusText;
            set => SetProperty(ref _statusText, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public string CurlImportText
        {
            get => _curlImportText;
            set => SetProperty(ref _curlImportText, value);
        }

        public ObservableCollection<string> AvailableMethods { get; } = new()
        {
            "GET", "POST", "PUT", "DELETE", "PATCH", "HEAD", "OPTIONS"
        };

        public ObservableCollection<RequestCollection> Collections { get; } = new();
        public ObservableCollection<ApiRequest> RequestHistory { get; } = new();
        public ObservableCollection<Environment> Environments { get; } = new();

        #endregion

        #region Commands

        public ICommand? SendRequestCommand { get; private set; }
        public ICommand? SaveRequestCommand { get; private set; }
        public ICommand? ClearResponseCommand { get; private set; }
        public ICommand? NewCollectionCommand { get; private set; }
        public ICommand? NewEnvironmentCommand { get; private set; }
        public ICommand? ImportCurlCommand { get; private set; }

        private void InitializeCommands()
        {
            SendRequestCommand = new RelayCommand(async _ => await SendRequest());
            SaveRequestCommand = new RelayCommand(_ => SaveRequest());
            ClearResponseCommand = new RelayCommand(_ => ClearResponse());
            NewCollectionCommand = new RelayCommand(_ => CreateNewCollection());
            NewEnvironmentCommand = new RelayCommand(_ => CreateNewEnvironment());
            ImportCurlCommand = new RelayCommand(_ => ImportCurl());
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            // Load collections
            var collections = _storageService.LoadCollections();
            Collections.Clear();
            foreach (var collection in collections)
            {
                Collections.Add(collection);
            }

            // Load environments
            var environments = _environmentService.LoadEnvironments();
            Environments.Clear();
            foreach (var env in environments)
            {
                Environments.Add(env);
            }

            // Load request history (last 20)
            var history = _storageService.LoadRequestHistory().Take(20);
            RequestHistory.Clear();
            foreach (var request in history)
            {
                RequestHistory.Add(request);
            }
        }

        private async System.Threading.Tasks.Task SendRequest()
        {
            try
            {
                IsLoading = true;
                StatusText = "Sending request...";

                var request = BuildRequest();
                var response = await _httpService.SendRequestAsync(request);

                ResponseBody = response.Body;
                StatusText = $"Status: {(int)response.StatusCode} {response.StatusCode} | Time: {response.Duration.TotalMilliseconds:F0}ms | Size: {response.ContentLength} bytes";

                // Add to history
                RequestHistory.Insert(0, request);
                _storageService.SaveRequestToHistory(request);
            }
            catch (Exception ex)
            {
                ResponseBody = $"Error: {ex.Message}";
                StatusText = "Request failed";
                MessageBox.Show($"Error sending request: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private ApiRequest BuildRequest()
        {
            var request = new ApiRequest
            {
                Name = $"{SelectedMethod} {Url}",
                Url = _environmentService.ReplaceVariables(Url),
                Method = new System.Net.Http.HttpMethod(SelectedMethod),
                Body = _environmentService.ReplaceVariables(RequestBody)
            };

            // Parse headers
            if (!string.IsNullOrWhiteSpace(HeadersText))
            {
                var headerLines = HeadersText.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in headerLines)
                {
                    var parts = line.Split(':', 2);
                    if (parts.Length == 2)
                    {
                        var key = parts[0].Trim();
                        var value = _environmentService.ReplaceVariables(parts[1].Trim());
                        request.Headers[key] = value;
                    }
                }
            }

            return request;
        }

        private void SaveRequest()
        {
            var request = BuildRequest();
            
            if (Collections.Count == 0)
            {
                CreateNewCollection();
            }

            if (Collections.Count > 0)
            {
                var collection = Collections[0];
                collection.Requests.Add(request);
                _storageService.SaveCollection(collection);
                StatusText = $"Request saved to collection: {collection.Name}";
            }
        }

        private void ClearResponse()
        {
            ResponseBody = string.Empty;
            StatusText = "Ready";
        }

        private void CreateNewCollection()
        {
            var collection = new RequestCollection
            {
                Name = $"Collection {Collections.Count + 1}"
            };
            Collections.Add(collection);
            _storageService.SaveCollection(collection);
            StatusText = $"Created new collection: {collection.Name}";
        }

        private void CreateNewEnvironment()
        {
            var environment = new Environment
            {
                Name = $"Environment {Environments.Count + 1}",
                Variables = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "base_url", "https://api.example.com" },
                    { "api_key", "your-api-key-here" }
                }
            };
            Environments.Add(environment);
            _environmentService.SaveEnvironment(environment);
            StatusText = $"Created new environment: {environment.Name}";
        }

        private void ImportCurl()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(CurlImportText))
                {
                    MessageBox.Show("Please paste a cURL command first.", "Import cURL", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var curlParser = new CurlParser();
                var request = curlParser.ParseCurl(CurlImportText);

                Url = request.Url;
                SelectedMethod = request.Method.ToString();
                RequestBody = request.Body;
                HeadersText = string.Join("\n", request.Headers.Select(h => $"{h.Key}: {h.Value}"));

                CurlImportText = string.Empty;
                StatusText = "cURL imported successfully";
                MessageBox.Show("cURL command imported successfully!", "Import cURL", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to import cURL: {ex.Message}", "Import Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SelectEnvironment(string environmentId)
        {
            var environment = Environments.FirstOrDefault(e => e.Id == environmentId);
            if (environment != null)
            {
                _environmentService.SetActiveEnvironment(environment);
                StatusText = $"Active environment: {environment.Name}";
            }
        }

        public void LoadRequestFromHistory(ApiRequest request)
        {
            Url = request.Url;
            SelectedMethod = request.Method.ToString();
            RequestBody = request.Body;
            HeadersText = string.Join("\n", request.Headers.Select(h => $"{h.Key}: {h.Value}"));
            StatusText = $"Loaded request from history: {request.Name}";
        }

        #endregion
    }
}
