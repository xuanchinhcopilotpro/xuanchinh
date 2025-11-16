using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.CSharp.ProjectDecompiler;
using ICSharpCode.Decompiler.Metadata;

namespace PluginDecompiler
{
    public partial class MainForm : Form
    {
        private TextBox? txtDllPath;
        private TextBox? txtOutputPath;
        private Button? btnBrowseDll;
        private Button? btnBrowseOutput;
        private Button? btnDecompile;
        private RichTextBox? txtLog;
        private ProgressBar? progressBar;
        private CheckBox? chkCreateCombined;
        private CheckBox? chkOpenOutput;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form setup
            this.Text = "Dynamics 365 Plugin Decompiler";
            this.Size = new Size(850, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(850, 650);
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Title
            var lblTitle = new Label
            {
                Text = "🔧 Dynamics 365 Plugin Decompiler",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(800, 40),
                ForeColor = Color.FromArgb(0, 120, 215)
            };
            this.Controls.Add(lblTitle);

            var lblSubtitle = new Label
            {
                Text = "Decompile plugin DLL files to readable C# source code",
                Font = new Font("Segoe UI", 10),
                Location = new Point(20, 60),
                Size = new Size(800, 25),
                ForeColor = Color.Gray
            };
            this.Controls.Add(lblSubtitle);

            // DLL Path
            var lblDllPath = new Label
            {
                Text = "Plugin DLL File:",
                Location = new Point(20, 105),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            this.Controls.Add(lblDllPath);

            txtDllPath = new TextBox
            {
                Location = new Point(20, 128),
                Size = new Size(680, 25),
                Font = new Font("Segoe UI", 9),
                BorderStyle = BorderStyle.FixedSingle
            };
            txtDllPath.AllowDrop = true;
            txtDllPath.DragEnter += TxtDllPath_DragEnter;
            txtDllPath.DragDrop += TxtDllPath_DragDrop;
            txtDllPath.TextChanged += (s, e) => ValidateInputs();
            this.Controls.Add(txtDllPath);

            btnBrowseDll = new Button
            {
                Text = "Browse...",
                Location = new Point(710, 126),
                Size = new Size(100, 28),
                Font = new Font("Segoe UI", 9),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnBrowseDll.FlatAppearance.BorderSize = 0;
            btnBrowseDll.Click += BtnBrowseDll_Click;
            this.Controls.Add(btnBrowseDll);

            // Output Path
            var lblOutputPath = new Label
            {
                Text = "Output Folder:",
                Location = new Point(20, 170),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            this.Controls.Add(lblOutputPath);

            txtOutputPath = new TextBox
            {
                Location = new Point(20, 193),
                Size = new Size(680, 25),
                Font = new Font("Segoe UI", 9),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(txtOutputPath);

            btnBrowseOutput = new Button
            {
                Text = "Browse...",
                Location = new Point(710, 191),
                Size = new Size(100, 28),
                Font = new Font("Segoe UI", 9),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnBrowseOutput.FlatAppearance.BorderSize = 0;
            btnBrowseOutput.Click += BtnBrowseOutput_Click;
            this.Controls.Add(btnBrowseOutput);

            // Options
            chkCreateCombined = new CheckBox
            {
                Text = "Create combined source file",
                Location = new Point(20, 235),
                Size = new Size(250, 20),
                Checked = true,
                Font = new Font("Segoe UI", 9)
            };
            this.Controls.Add(chkCreateCombined);

            chkOpenOutput = new CheckBox
            {
                Text = "Open output folder after decompilation",
                Location = new Point(280, 235),
                Size = new Size(300, 20),
                Checked = true,
                Font = new Font("Segoe UI", 9)
            };
            this.Controls.Add(chkOpenOutput);

            // Decompile Button
            btnDecompile = new Button
            {
                Text = "🔄 Decompile Plugin",
                Location = new Point(20, 270),
                Size = new Size(790, 45),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(16, 124, 16),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnDecompile.FlatAppearance.BorderSize = 0;
            btnDecompile.Click += BtnDecompile_Click;
            this.Controls.Add(btnDecompile);

            // Progress Bar
            progressBar = new ProgressBar
            {
                Location = new Point(20, 330),
                Size = new Size(790, 25),
                Style = ProgressBarStyle.Continuous,
                Visible = false
            };
            this.Controls.Add(progressBar);

            // Log
            var lblLog = new Label
            {
                Text = "Decompilation Log:",
                Location = new Point(20, 365),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            this.Controls.Add(lblLog);

            txtLog = new RichTextBox
            {
                Location = new Point(20, 388),
                Size = new Size(790, 200),
                Font = new Font("Consolas", 9),
                ReadOnly = true,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.FromArgb(220, 220, 220),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(txtLog);

            this.ResumeLayout(false);
        }

        private void TxtDllPath_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void TxtDllPath_DragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetData(DataFormats.FileDrop) is string[] files && files.Length > 0)
            {
                if (txtDllPath != null)
                    txtDllPath.Text = files[0];
            }
        }

        private void ValidateInputs()
        {
            if (btnDecompile != null && txtDllPath != null)
            {
                btnDecompile.Enabled = !string.IsNullOrWhiteSpace(txtDllPath.Text) && 
                                       File.Exists(txtDllPath.Text);
            }
        }

        private void BtnBrowseDll_Click(object? sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "DLL Files (*.dll)|*.dll|All Files (*.*)|*.*";
                dialog.Title = "Select Plugin DLL File";

                if (dialog.ShowDialog() == DialogResult.OK && txtDllPath != null)
                {
                    txtDllPath.Text = dialog.FileName;
                    
                    // Auto-set output path
                    if (txtOutputPath != null && string.IsNullOrWhiteSpace(txtOutputPath.Text))
                    {
                        var dllDir = Path.GetDirectoryName(dialog.FileName);
                        var dllName = Path.GetFileNameWithoutExtension(dialog.FileName);
                        txtOutputPath.Text = Path.Combine(dllDir ?? "", $"{dllName}_Decompiled");
                    }
                }
            }
        }

        private void BtnBrowseOutput_Click(object? sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select Output Folder";
                
                if (dialog.ShowDialog() == DialogResult.OK && txtOutputPath != null)
                {
                    txtOutputPath.Text = dialog.SelectedPath;
                }
            }
        }

        private async void BtnDecompile_Click(object? sender, EventArgs e)
        {
            if (txtDllPath == null || string.IsNullOrWhiteSpace(txtDllPath.Text) || !File.Exists(txtDllPath.Text))
            {
                MessageBox.Show("Please select a valid DLL file.", "Invalid Input", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string outputPath = txtOutputPath != null && !string.IsNullOrWhiteSpace(txtOutputPath.Text) 
                ? txtOutputPath.Text
                : Path.Combine(Path.GetDirectoryName(txtDllPath.Text) ?? "", "Decompiled");

            // Disable controls
            SetControlsEnabled(false);
            if (progressBar != null)
            {
                progressBar.Visible = true;
                progressBar.Style = ProgressBarStyle.Marquee;
            }
            if (txtLog != null)
                txtLog.Clear();

            try
            {
                LogMessage("Starting decompilation...", Color.LightBlue);
                LogMessage($"Input: {txtDllPath.Text}", Color.White);
                LogMessage($"Output: {outputPath}\n", Color.White);

                bool createCombined = chkCreateCombined?.Checked ?? true;
                await System.Threading.Tasks.Task.Run(() => 
                    DecompileDll(txtDllPath.Text, outputPath, createCombined));

                LogMessage("\n✅ Decompilation completed successfully!", Color.LightGreen);
                
                if ((chkOpenOutput?.Checked ?? true) && Directory.Exists(outputPath))
                {
                    System.Diagnostics.Process.Start("explorer.exe", outputPath);
                }

                MessageBox.Show("Decompilation completed successfully!", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LogMessage($"\n❌ Error: {ex.Message}", Color.Red);
                MessageBox.Show($"Error during decompilation:\n{ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetControlsEnabled(true);
                if (progressBar != null)
                    progressBar.Visible = false;
            }
        }

        private void SetControlsEnabled(bool enabled)
        {
            if (btnDecompile != null) btnDecompile.Enabled = enabled && !string.IsNullOrWhiteSpace(txtDllPath?.Text);
            if (btnBrowseDll != null) btnBrowseDll.Enabled = enabled;
            if (btnBrowseOutput != null) btnBrowseOutput.Enabled = enabled;
            if (txtDllPath != null) txtDllPath.Enabled = enabled;
            if (txtOutputPath != null) txtOutputPath.Enabled = enabled;
        }

        private void DecompileDll(string dllPath, string outputPath, bool createCombined)
        {
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

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

            using (var peFile = new PEFile(dllPath))
            {
                LogMessage($"📦 Assembly: {peFile.Name}", Color.Cyan);
                LogMessage($"🎯 Target framework: {peFile.DetectTargetFrameworkId()}", Color.Cyan);

                var resolver = new UniversalAssemblyResolver(dllPath, false, peFile.DetectTargetFrameworkId());
                var decompiler = new WholeProjectDecompiler(decompilerSettings, resolver, null, null);

                LogMessage("🔄 Decompiling to project structure...", Color.Yellow);
                decompiler.DecompileProject(peFile, outputPath);
                LogMessage("✓ Project files created", Color.LightGreen);

                if (createCombined)
                {
                    LogMessage("📝 Creating combined source file...", Color.Yellow);
                    CreateCombinedSourceFile(dllPath, outputPath, decompilerSettings);
                    LogMessage("✓ Combined file created", Color.LightGreen);
                }
            }
        }

        private void CreateCombinedSourceFile(string dllPath, string outputPath, DecompilerSettings settings)
        {
            var decompiler = new CSharpDecompiler(dllPath, settings);
            
            var types = decompiler.TypeSystem.MainModule.TypeDefinitions
                .Where(t => !t.Name.StartsWith("<"))
                .OrderBy(t => t.Namespace)
                .ThenBy(t => t.Name);

            string combinedFileName = Path.GetFileNameWithoutExtension(dllPath) + "_Combined.cs";
            string combinedFilePath = Path.Combine(outputPath, combinedFileName);

            using (StreamWriter writer = new StreamWriter(combinedFilePath))
            {
                writer.WriteLine("// ============================================");
                writer.WriteLine($"// Combined decompiled source from: {Path.GetFileName(dllPath)}");
                writer.WriteLine($"// Generated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                writer.WriteLine($"// Decompiled by: Dynamics 365 Plugin Decompiler");
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
        }

        private void LogMessage(string message, Color color)
        {
            if (txtLog == null) return;

            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() => LogMessage(message, color)));
                return;
            }

            txtLog.SelectionStart = txtLog.TextLength;
            txtLog.SelectionLength = 0;
            txtLog.SelectionColor = color;
            txtLog.AppendText(message + Environment.NewLine);
            txtLog.SelectionColor = txtLog.ForeColor;
            txtLog.ScrollToCaret();
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
