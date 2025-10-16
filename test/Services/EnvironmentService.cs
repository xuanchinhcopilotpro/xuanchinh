using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ApiTester.Models;
using Newtonsoft.Json;

namespace ApiTester.Services
{
    public class EnvironmentService
    {
        private readonly string _environmentsFolder;
        private Environment? _activeEnvironment;

        public EnvironmentService()
        {
            var appDataFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            _environmentsFolder = Path.Combine(appDataFolder, "ApiTester", "environments");
            Directory.CreateDirectory(_environmentsFolder);
        }

        public List<Environment> LoadEnvironments()
        {
            var environments = new List<Environment>();

            if (!Directory.Exists(_environmentsFolder))
            {
                return environments;
            }

            var files = Directory.GetFiles(_environmentsFolder, "*.json");
            foreach (var file in files)
            {
                try
                {
                    var json = File.ReadAllText(file);
                    var environment = JsonConvert.DeserializeObject<Environment>(json);
                    if (environment != null)
                    {
                        environments.Add(environment);
                        if (environment.IsActive)
                        {
                            _activeEnvironment = environment;
                        }
                    }
                }
                catch { }
            }

            return environments;
        }

        public void SaveEnvironment(Environment environment)
        {
            var filePath = Path.Combine(_environmentsFolder, $"{environment.Id}.json");
            var json = JsonConvert.SerializeObject(environment, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public void SetActiveEnvironment(Environment environment)
        {
            // Deactivate all environments
            var allEnvironments = LoadEnvironments();
            foreach (var env in allEnvironments)
            {
                env.IsActive = false;
                SaveEnvironment(env);
            }

            // Activate selected environment
            environment.IsActive = true;
            SaveEnvironment(environment);
            _activeEnvironment = environment;
        }

        public string ReplaceVariables(string text)
        {
            if (string.IsNullOrWhiteSpace(text) || _activeEnvironment == null)
            {
                return text;
            }

            // Replace {{variable_name}} with actual values
            var result = text;
            var matches = Regex.Matches(text, @"\{\{(.+?)\}\}");
            
            foreach (Match match in matches)
            {
                var variableName = match.Groups[1].Value.Trim();
                if (_activeEnvironment.Variables.ContainsKey(variableName))
                {
                    result = result.Replace(match.Value, _activeEnvironment.Variables[variableName]);
                }
            }

            return result;
        }
    }
}
