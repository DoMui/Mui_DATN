using System;
using System.CodeDom.Compiler;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Microsoft.CSharp;
using System.Collections.Generic;

namespace DATN
{
    public partial class Form2 : Form
    {
        // Sau cùng, chúng sẽ được lưu ở đây
        public string GeneratedTestCode { get; private set; }
        private readonly string _sourceFile;
        private Type _targetType;
        public Form2(string sourceFilePath)
        {
            InitializeComponent();
            this.rtbDSPT.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rtbDSPT_MouseClick);
            _sourceFile = sourceFilePath;
            LoadMethods();
        }

        private void LoadMethods()
        {
            var provider = new CSharpCodeProvider();
            var parameters = new CompilerParameters
            {
                GenerateInMemory = true,
                GenerateExecutable = false
            };
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Core.dll");
            parameters.ReferencedAssemblies.Add("Microsoft.CSharp.dll");

            var results = provider.CompileAssemblyFromFile(parameters, _sourceFile);
            if (results.Errors.HasErrors)
            {
                MessageBox.Show("Lỗi biên dịch:\n" +
                    string.Join("\n", results.Errors.Cast<CompilerError>().Select(err => err.ErrorText)),
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            var asm = results.CompiledAssembly;
            string className = System.IO.Path.GetFileNameWithoutExtension(_sourceFile);
            _targetType = asm.GetTypes().FirstOrDefault(t => t.Name == className);
            if (_targetType == null)
            {
                MessageBox.Show($"Không tìm thấy class {className}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            var methods = _targetType.GetMethods()
                .Where(m => m.IsPublic
                         && m.DeclaringType == _targetType
                         && m.GetParameters().All(p =>
                                p.ParameterType == typeof(int) ||
                                p.ParameterType == typeof(double) ||
                                p.ParameterType == typeof(string) ||
                                p.ParameterType == typeof(bool)))
                .ToList();

            var sb = new StringBuilder();
            sb.AppendLine("Các phương thức có thể test:");
            foreach (var m in methods)
            {
                var sig = $"{m.ReturnType.Name} {m.Name}({string.Join(", ", m.GetParameters().Select(p => p.ParameterType.Name + " " + p.Name))})";
                sb.AppendLine("- " + sig);
            }
            rtbDSPT.Text = sb.ToString();
        }

            private void txtDLDV_TextChanged(object sender, EventArgs e)
            {

            }

        private void btnSinhTest_Click(object sender, EventArgs e)
        {
            if (_targetType == null) return;

            // 1. Đọc các dòng nhập tay
            var lines = txtDLDV.Lines
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => l.Trim())
                .ToList();

            if (lines.Count == 0)
            {
                MessageBox.Show("Chưa nhập test case nào!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Lấy tên method từ dòng đầu tiên nếu có dạng # MethodName
            string methodNameFilter = null;
            Dictionary<string, List<string>> methodCases = new Dictionary<string, List<string>>();  // Lưu trữ các test case theo tên phương thức

            foreach (var line in lines)
            {
                if (line.StartsWith("#"))
                {
                    methodNameFilter = line.Substring(1).Trim(); // Lấy tên method sau dấu #
                    if (!methodCases.ContainsKey(methodNameFilter))
                    {
                        methodCases[methodNameFilter] = new List<string>(); // Khởi tạo danh sách cho phương thức mới
                    }
                }
                else
                {
                    if (methodNameFilter != null)
                    {
                        methodCases[methodNameFilter].Add(line); // Thêm test case vào phương thức tương ứng
                    }
                }
            }

            if (methodCases.Count == 0)
            {
                MessageBox.Show("Chưa nhập tên phương thức và test case!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Lấy danh sách method cần test
            var allMethods = _targetType.GetMethods()
                .Where(m => m.IsPublic && m.DeclaringType == _targetType
                         && m.GetParameters().All(p =>
                                p.ParameterType == typeof(int) ||
                                p.ParameterType == typeof(double) ||
                                p.ParameterType == typeof(string) ||
                                p.ParameterType == typeof(bool)))
                .ToList();

            // 4. Chuẩn bị sinh mã
            var sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using NUnit.Framework;");
            sb.AppendLine($"using {_targetType.Namespace};");
            sb.AppendLine();
            sb.AppendLine("namespace DATN.Tests");
            sb.AppendLine("{");
            sb.AppendLine("    [TestFixture]");
            sb.AppendLine($"    public class {_targetType.Name}Tests");
            sb.AppendLine("    {");

            // 5. Sinh test cho từng phương thức
            foreach (var methodCase in methodCases)
            {
                var methodName = methodCase.Key;
                var testCases = methodCase.Value;

                var method = allMethods.FirstOrDefault(m => m.Name.Equals(methodName, StringComparison.OrdinalIgnoreCase));
                if (method == null)
                {
                    MessageBox.Show($"Không tìm thấy hàm '{methodName}' trong class {_targetType.Name}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }

                var ps = method.GetParameters();
                string testMethodName = methodName + "Test";

                Func<Type, string> alias = t =>
                     t == typeof(int) ? "int" :
                     t == typeof(double) ? "double" :
                     t == typeof(string) ? "string" :
                     t == typeof(bool) ? "bool" :
                     t.Name;
                string paramList = string.Join(", ", ps.Select(p => $"{alias(p.ParameterType)} {p.Name}"));

                foreach (var line in testCases)
                {
                    var parts = line.Split(',').Select(p => p.Trim()).ToArray();
                    if (parts.Length != ps.Length + 1)
                    {
                        MessageBox.Show(
                            $"Dòng '{line}' phải có {ps.Length} tham số + 1 kết quả.",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var args = string.Join(", ", parts.Take(parts.Length - 1));
                    var expected = parts.Last();
                    sb.AppendLine($"        [TestCase({args}, {expected})]");
                }

                string fullParamList = paramList + (paramList.Length > 0 ? ", " : "") + $"{alias(method.ReturnType)} expected";

                sb.AppendLine($"        public void {testMethodName}({fullParamList})");
                sb.AppendLine("        {");
                sb.AppendLine($"            var obj = new {_targetType.Name}();");
                sb.AppendLine($"            var result = obj.{method.Name}({string.Join(", ", ps.Select(p => p.Name))});");
                sb.AppendLine("            Assert.AreEqual(expected, result);");
                sb.AppendLine("        }");
                sb.AppendLine();
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            // Lưu kết quả test
            GeneratedTestCode = sb.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private void rtbDSPT_MouseClick(object sender, MouseEventArgs e)
        {
            // Lấy vị trí dòng đang được click
            int index = rtbDSPT.GetCharIndexFromPosition(e.Location);
            int line = rtbDSPT.GetLineFromCharIndex(index);
            string clickedLine = rtbDSPT.Lines[line];

            // Kiểm tra xem có phải dòng có phương thức không (bắt đầu bằng "- ")
            if (clickedLine.StartsWith("- "))
            {
                // Tách để lấy tên phương thức
                var parts = clickedLine.Substring(2).Split(' ');
                if (parts.Length >= 2)
                {
                    string methodName = parts[1].Split('(')[0];

                    // Nếu txtDLDV chưa có nội dung, thì không cần xuống dòng trước
                    if (txtDLDV.Text.Length == 0)
                    {
                        txtDLDV.AppendText($"# {methodName}\r\n");
                    }
                    else
                    {
                        // Thêm tên phương thức mới vào dòng mới
                        txtDLDV.AppendText($"\r\n# {methodName}\r\n");
                    }
                }
            }
        }

        private void rtbDSPT_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
