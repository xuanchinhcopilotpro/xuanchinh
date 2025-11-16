using System;
using System.IO;
using System.Linq;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.CSharp.ProjectDecompiler;
using ICSharpCode.Decompiler.Metadata;

namespace DllDecompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("===========================================");
            Console.WriteLine("  Dynamics 365 Plugin DLL Decompiler");
            Console.WriteLine("===========================================\n");

            if (args.Length == 0)
            {
                ShowUsage();
                return;
            }

            string dllPath = args[0];
            string outputPath = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(dllPath) ?? ".", "Decompiled");

            try
            {
                if (!File.Exists(dllPath))
                {
                    Console.WriteLine($"❌ Error: File not found: {dllPath}");
                    return;
                }

                if (!dllPath.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("⚠️  Warning: The file does not have a .dll extension.");
                }

                Console.WriteLine($"📂 Input DLL: {dllPath}");
                Console.WriteLine($"📁 Output folder: {outputPath}\n");

                DecompileDll(dllPath, outputPath);

                Console.WriteLine("\n✅ Decompilation completed successfully!");
                Console.WriteLine($"📄 Decompiled source code is in: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Error during decompilation: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static void DecompileDll(string dllPath, string outputPath)
        {
            Console.WriteLine("🔄 Starting decompilation...\n");

            // Create output directory if it doesn't exist
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            // Set up decompiler settings
            var decompilerSettings = new DecompilerSettings
            {
                ThrowOnAssemblyResolveErrors = false,
                ShowXmlDocumentation = true,
                UseDebugSymbols = true,
                RemoveDeadCode = true,
                RemoveDeadStores = true,
                AlwaysUseBraces = true,
                UseSdkStyleProjectFormat = true
            };

            // Load the module
            using (var peFile = new PEFile(dllPath))
            {
                var resolver = new UniversalAssemblyResolver(dllPath, false, peFile.DetectTargetFrameworkId());
                
                Console.WriteLine($"📦 Assembly: {peFile.Name}");
                Console.WriteLine($"🎯 Target framework: {peFile.DetectTargetFrameworkId()}\n");

                // Create WholeProjectDecompiler for better output
                var decompiler = new WholeProjectDecompiler(decompilerSettings, resolver, null, null);
                
                // Decompile to project
                decompiler.DecompileProject(peFile, outputPath);
                
                // Also create a single combined file for easier viewing
                CreateCombinedSourceFile(dllPath, outputPath, decompilerSettings);
            }
        }

        static void CreateCombinedSourceFile(string dllPath, string outputPath, DecompilerSettings settings)
        {
            try
            {
                var decompiler = new CSharpDecompiler(dllPath, settings);
                
                // Get all types in the assembly
                var types = decompiler.TypeSystem.MainModule.TypeDefinitions
                    .Where(t => !t.Name.StartsWith("<"))  // Skip compiler-generated types
                    .OrderBy(t => t.Namespace)
                    .ThenBy(t => t.Name);

                string combinedFileName = Path.GetFileNameWithoutExtension(dllPath) + "_Combined.cs";
                string combinedFilePath = Path.Combine(outputPath, combinedFileName);

                using (StreamWriter writer = new StreamWriter(combinedFilePath))
                {
                    writer.WriteLine("// ============================================");
                    writer.WriteLine($"// Combined decompiled source from: {Path.GetFileName(dllPath)}");
                    writer.WriteLine($"// Generated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    writer.WriteLine("// ============================================");
                    writer.WriteLine();

                    foreach (var type in types)
                    {
                        try
                        {
                            writer.WriteLine($"// ============================================");
                            writer.WriteLine($"// Type: {type.FullName}");
                            writer.WriteLine($"// ============================================");
                            
                            var code = decompiler.DecompileTypeAsString(type.FullTypeName);
                            writer.WriteLine(code);
                            writer.WriteLine();
                            writer.WriteLine();
                        }
                        catch (Exception ex)
                        {
                            writer.WriteLine($"// Error decompiling {type.FullName}: {ex.Message}");
                            writer.WriteLine();
                        }
                    }
                }

                Console.WriteLine($"📝 Combined source file created: {combinedFileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Warning: Could not create combined source file: {ex.Message}");
            }
        }

        static void ShowUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  DllDecompiler.exe <path-to-dll> [output-folder]");
            Console.WriteLine();
            Console.WriteLine("Parameters:");
            Console.WriteLine("  <path-to-dll>     : Path to the DLL file to decompile (required)");
            Console.WriteLine("  [output-folder]   : Output folder for decompiled source (optional)");
            Console.WriteLine("                      Default: ./Decompiled folder next to the DLL");
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine("  DllDecompiler.exe MyPlugin.dll");
            Console.WriteLine("  DllDecompiler.exe C:\\Plugins\\MyPlugin.dll C:\\Output");
            Console.WriteLine("  DllDecompiler.exe \"D:\\Dynamics365\\My Custom Plugin.dll\"");
            Console.WriteLine();
            Console.WriteLine("Note: This tool is designed for decompiling Dynamics 365 CRM/CE plugins");
            Console.WriteLine("      and other .NET assemblies for educational and debugging purposes.");
        }
    }
}
