using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ApiTester.Models;
using Newtonsoft.Json;

namespace ApiTester.Services
{
    public class StorageService
    {
        private readonly string _collectionsFolder;
        private readonly string _historyFile;

        public StorageService()
        {
            var appDataFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            var appFolder = Path.Combine(appDataFolder, "ApiTester");
            _collectionsFolder = Path.Combine(appFolder, "collections");
            _historyFile = Path.Combine(appFolder, "history.json");

            Directory.CreateDirectory(_collectionsFolder);
            Directory.CreateDirectory(appFolder);
        }

        public List<RequestCollection> LoadCollections()
        {
            var collections = new List<RequestCollection>();

            if (!Directory.Exists(_collectionsFolder))
            {
                return collections;
            }

            var files = Directory.GetFiles(_collectionsFolder, "*.json");
            foreach (var file in files)
            {
                try
                {
                    var json = File.ReadAllText(file);
                    var collection = JsonConvert.DeserializeObject<RequestCollection>(json);
                    if (collection != null)
                    {
                        collections.Add(collection);
                    }
                }
                catch { }
            }

            return collections.OrderByDescending(c => c.CreatedAt).ToList();
        }

        public void SaveCollection(RequestCollection collection)
        {
            var filePath = Path.Combine(_collectionsFolder, $"{collection.Id}.json");
            var json = JsonConvert.SerializeObject(collection, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public List<ApiRequest> LoadRequestHistory()
        {
            if (!File.Exists(_historyFile))
            {
                return new List<ApiRequest>();
            }

            try
            {
                var json = File.ReadAllText(_historyFile);
                var history = JsonConvert.DeserializeObject<List<ApiRequest>>(json);
                return history ?? new List<ApiRequest>();
            }
            catch
            {
                return new List<ApiRequest>();
            }
        }

        public void SaveRequestToHistory(ApiRequest request)
        {
            var history = LoadRequestHistory();
            
            // Add to beginning of list
            history.Insert(0, request);

            // Keep only last 100 requests
            if (history.Count > 100)
            {
                history = history.Take(100).ToList();
            }

            var json = JsonConvert.SerializeObject(history, Formatting.Indented);
            File.WriteAllText(_historyFile, json);
        }
    }
}
