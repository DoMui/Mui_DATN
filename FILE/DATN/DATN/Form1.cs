using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using Microsoft.CSharp;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DATN.Source_files;
using System.Text.RegularExpressions;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace DATN   
{
    public partial class Form1 : Form 
    {
        private string selectedFilePath; // Biến lưu đường dẫn file .cs mà người dùng chọn.
        public Form1()
        {
            InitializeComponent();
        }

        private void txtChontep_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnChontep_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); 
            openFileDialog.Filter = "C# files (*.cs)|*.cs"; 
            openFileDialog.Title = "Chọn file C#";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFilePath = openFileDialog.FileName; 
                txtChontep.Text = selectedFilePath; 
            }
        }

        private void btnSinhtest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFilePath))
            {
                MessageBox.Show("Vui lòng chọn file .cs trước!");
                return;
            }


            string luaChon = cbbDL.SelectedItem.ToString();
            if (luaChon == "RANDOM")
            {
                string className = Path.GetFileNameWithoutExtension(selectedFilePath);
                // Lấy Assembly (dự án đang chạy) để truy xuất các class bên trong
                //Nó lấy toàn bộ các class, hàm, namespace, v.v. trong chương trình bạn đang chạy.

                // Biên dịch file .cs được chọn
                CSharpCodeProvider provider = new CSharpCodeProvider();
                CompilerParameters parameters = new CompilerParameters
                {
                    GenerateInMemory = true,
                    GenerateExecutable = false,
                    ReferencedAssemblies =
                {
                    "System.dll",
                    "System.Core.dll",        // Bổ sung để hỗ trợ System.Linq
                    "Microsoft.CSharp.dll"    // (nếu bạn dùng dynamic hoặc LINQ nâng cao)
                }
                };

                CompilerResults results = provider.CompileAssemblyFromFile(parameters, selectedFilePath);

                if (results.Errors.HasErrors)
                {
                    string errors = string.Join("\n", results.Errors.Cast<CompilerError>().Select(err => err.ToString()));
                    richTextBox1.Text = "Lỗi biên dịch file .cs:\n" + errors;
                    return;
                }

                Assembly asm = results.CompiledAssembly; // Assembly từ file được chọn

                //Assembly asm = Assembly.GetExecutingAssembly();
                Type targetType = asm.GetTypes().FirstOrDefault(t => t.Name == className);// Tìm class trong assembly có tên trùng với tên file

                if (targetType == null)
                {
                    MessageBox.Show($"Không tìm thấy class {className} trong project!");
                    return;
                }

                Random rand = new Random();
                int testCaseCount = trackBarTestCount.Value;

                StringBuilder testClassCode = new StringBuilder();// Tạo đối tượng StringBuilder để xây dựng nội dung mã test

                testClassCode.AppendLine("using NUnit.Framework;");
                // Thêm using lấy namespace chứa class gốc
                testClassCode.AppendLine($"using {targetType.Namespace};");
                testClassCode.AppendLine();
                testClassCode.AppendLine("namespace DATN.Tests");
                testClassCode.AppendLine("{");
                // Gắn thuộc tính [TestFixture] cho class test
                testClassCode.AppendLine("    [TestFixture]");
                // Đặt tên class test: VD CalculatorTests
                testClassCode.AppendLine($"    public class {targetType.Name}Tests");
                testClassCode.AppendLine("    {");

                // Hàm để lấy tên kiểu dữ liệu C# từ kiểu .NET  sang tên kiểu dữ liệu tương ứng trong ngôn ngữ C#.
                //Hàm này trả về một chuỗi (string), là tên kiểu trong cú pháp C# (như "int", "string"…).
                Func<Type, string> getCSharpTypeName = type =>
                {
                    if (type == typeof(int)) return "int";
                    if (type == typeof(double)) return "double";
                    if (type == typeof(float)) return "float";
                    if (type == typeof(string)) return "string";
                    if (type == typeof(bool)) return "bool";
                    if (type == typeof(char)) return "char";
                    return type.Name;
                };

                // Lấy danh sách các method hợp lệ để sinh test (các method public, của class đó, và chỉ nhận tham số đơn giản)
                var methods = targetType.GetMethods()
                .Where(m => m.IsPublic // method phải là public
                && m.DeclaringType == targetType // method thuộc về chính class đang xét
                && m.GetParameters().All(p => // tất cả tham số phải có kiểu hỗ trợ
                p.ParameterType == typeof(int)
                || p.ParameterType == typeof(double)
                || p.ParameterType == typeof(string)
                || p.ParameterType == typeof(bool)))
                .ToList(); // chuyển về danh sách

                //Vòng lặp sinh test case
                foreach (var method in methods)
                {
                    // Danh sách các dòng [TestCase(...)]
                    List<string> testCaseAttributes = new List<string>();
                    var paramInfos = method.GetParameters(); // Lấy danh sách tham số

                    for (int i = 0; i < testCaseCount; i++)// Sinh test case mỗi method theo testCaseCount
                    {
                        object[] inputs = new object[paramInfos.Length]; // Mảng lưu giá trị input của từng test

                        // Sinh dữ liệu tương ứng với từng kiểu tham số
                        for (int j = 0; j < paramInfos.Length; j++)
                        {
                            Type paramType = paramInfos[j].ParameterType;

                            if (paramType == typeof(int))
                                inputs[j] = rand.Next(-100, 100);
                            else if (paramType == typeof(double))
                                inputs[j] = Math.Round(rand.NextDouble() * 100, 2);
                            else if (paramType == typeof(bool))
                                inputs[j] = rand.Next(0, 2) == 0;
                            else if (paramType == typeof(string))
                            {
                                int option = rand.Next(5);
                                switch (option)
                                {
                                    case 0: inputs[j] = GenerateRandomString(rand.Next(1, 20)); break;
                                    case 1: inputs[j] = ""; break;
                                    case 2: inputs[j] = "   "; break;
                                    case 3: inputs[j] = "!@#TEST123"; break;// Chuỗi đặc biệt
                                    default: inputs[j] = "default"; break;// Chuỗi mặc định
                                }
                            }
                            else
                                inputs[j] = null;// Kiểu không hỗ trợ
                        }

                        object expected;
                        try
                        {
                            // Tạo một instance của class gốc
                            object instance = Activator.CreateInstance(targetType);
                            // Gọi method thực tế để lấy kết quả
                            expected = method.Invoke(instance, inputs);
                        }
                        catch
                        {
                            // Nếu có lỗi khi gọi method thì gán expected = null
                            expected = null;
                        }
                        // Chuyển inputs thành chuỗi dạng C#
                        string args = string.Join(", ", inputs.Select(val => FormatValue(val)));

                        // Tạo dòng test case
                        if (expected != null)
                        {
                            // Nếu kết quả là chuỗi thì thêm dấu nháy kép
                            string expectedStr = expected is string ? $"\"{expected}\"" : expected.ToString().ToLower();
                            testCaseAttributes.Add($"        [TestCase({args}, ExpectedResult = {expectedStr})]");
                        }
                        else
                        {
                            // Nếu lỗi khi chạy thì ghi chú lại
                            testCaseAttributes.Add($"        //[TestCase({args})] // lỗi khi chạy method");
                        }
                    }

                    // Thêm các dòng TestCase vào file test
                    foreach (var line in testCaseAttributes)
                    {
                        testClassCode.AppendLine(line);
                    }

                    // Tạo chữ ký của method test
                    string paramList = string.Join(", ", paramInfos.Select(p => $"{getCSharpTypeName(p.ParameterType)} {p.Name}"));
                    string argList = string.Join(", ", paramInfos.Select(p => p.Name));
                    string returnType = getCSharpTypeName(method.ReturnType);

                    // Tạo method test
                    testClassCode.AppendLine($"        public {returnType} {method.Name}Test({paramList})");
                    testClassCode.AppendLine("        {");
                    testClassCode.AppendLine($"            var obj = new {targetType.Name}();");
                    testClassCode.AppendLine($"            return obj.{method.Name}({argList});");
                    testClassCode.AppendLine("        }");
                    testClassCode.AppendLine();
                }

                // Kết thúc class và namespace
                testClassCode.AppendLine("    }");
                testClassCode.AppendLine("}");

                // Tạo đường dẫn lưu file test
                string folderPath = @"E:\DOANTOTNGHIEP\DATN\Tests";// Thư mục lưu file test
                string testFileName = className == "Calculator" ? "Calculator_Test.cs" : className + "_Test.cs";
                string filePath = Path.Combine(folderPath, testFileName);

                Directory.CreateDirectory(folderPath);// Tạo thư mục nếu chưa có
                File.WriteAllText(filePath, testClassCode.ToString());// Ghi file

                MessageBox.Show($"Đã tạo file test: {testFileName}!");// Thông báo

                richTextBox1.Clear();// Xoá nội dung cũ
                richTextBox1.Text = File.ReadAllText(filePath);// Hiển thị nội dung test vừa tạo
                return;
            }
            else if (luaChon == "NHẬP TỪ BÀN PHÍM")
            {
                using (var form2 = new Form2(selectedFilePath))
                {
                    if (form2.ShowDialog() == DialogResult.OK)
                    {
                        // Lấy mã test case Form2 vừa sinh và hiển thị lên richTextBox1 của Form1
                        richTextBox1.Text = form2.GeneratedTestCode;
                    }
                }
            }
            else if (luaChon == "TẢI DỮ LIỆU TỪ FILE")
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "CSV files (*.csv)|*.csv";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string csvPath = openFileDialog.FileName;

                    string className = Path.GetFileNameWithoutExtension(selectedFilePath);
                    CSharpCodeProvider provider = new CSharpCodeProvider();
                    CompilerParameters parameters = new CompilerParameters
                    {
                        GenerateInMemory = true,
                        GenerateExecutable = false,
                        ReferencedAssemblies =
            {
                "System.dll",
                "System.Core.dll",
                "Microsoft.CSharp.dll"
            }
                    };

                    CompilerResults results = provider.CompileAssemblyFromFile(parameters, selectedFilePath);

                    if (results.Errors.HasErrors)
                    {
                        string errors = string.Join("\n", results.Errors.Cast<CompilerError>().Select(err => err.ToString()));
                        richTextBox1.Text = "Lỗi biên dịch file .cs:\n" + errors;
                        return;
                    }

                    Assembly asm = results.CompiledAssembly;
                    Type targetType = asm.GetTypes().FirstOrDefault(t => t.Name == className);

                    if (targetType == null)
                    {
                        MessageBox.Show($"Không tìm thấy class {className} trong project!");
                        return;
                    }

                    // Khởi tạo StringBuilder để sinh mã test
                    StringBuilder testClassCode = new StringBuilder();
                    testClassCode.AppendLine("using System;");
                    testClassCode.AppendLine("using NUnit.Framework;");
                    testClassCode.AppendLine($"using {targetType.Namespace};");
                    testClassCode.AppendLine();
                    testClassCode.AppendLine("namespace DATN.Tests");
                    testClassCode.AppendLine("{");
                    testClassCode.AppendLine("    [TestFixture]");
                    testClassCode.AppendLine($"    public class {targetType.Name}Tests");
                    testClassCode.AppendLine("    {");

                    Func<Type, string> getCSharpTypeName = type =>
                    {
                        if (type == typeof(int)) return "int";
                        if (type == typeof(double)) return "double";
                        if (type == typeof(float)) return "float";
                        if (type == typeof(string)) return "string";
                        if (type == typeof(bool)) return "bool";
                        if (type == typeof(char)) return "char";
                        return type.Name;
                    };

                    var methods = targetType.GetMethods()
                        .Where(m => m.IsPublic
                            && m.DeclaringType == targetType
                            && m.GetParameters().All(p =>
                                p.ParameterType == typeof(int)
                                || p.ParameterType == typeof(double)
                                || p.ParameterType == typeof(string)
                                || p.ParameterType == typeof(bool)))
                        .ToList();

                    var lines = File.ReadAllLines(csvPath);

                    List<List<string>> testCases = new List<List<string>>();
                    string currentMethodName = null;

                    foreach (var line in lines)
                    {
                        var trimmedLine = line.Trim();
                        if (string.IsNullOrEmpty(trimmedLine))
                            continue;

                        if (trimmedLine.StartsWith("#"))
                        {
                            // Nếu có dữ liệu phương thức cũ thì xử lý nó
                            if (currentMethodName != null && testCases.Count > 0)
                            {
                                var method = methods.FirstOrDefault(m => m.Name == currentMethodName);
                                if (method != null)
                                {
                                    foreach (var caseData in testCases)
                                    {
                                        string expected = caseData.Last();
                                        var inputParams = caseData.Take(caseData.Count - 1);
                                        testClassCode.AppendLine($"        [TestCase({string.Join(", ", inputParams)}, ExpectedResult = {expected})]");
                                    }

                                    string paramList = string.Join(", ", method.GetParameters().Select(p => $"{getCSharpTypeName(p.ParameterType)} {p.Name}"));
                                    string methodName = currentMethodName + "Test";
                                    string argList = string.Join(", ", method.GetParameters().Select(p => p.Name));

                                    string returnType = getCSharpTypeName(method.ReturnType);

                                    testClassCode.AppendLine($"        public {returnType} {methodName}({paramList})");
                                    testClassCode.AppendLine("        {");
                                    testClassCode.AppendLine($"            var obj = new {targetType.Name}();");
                                    testClassCode.AppendLine($"            return obj.{currentMethodName}({argList});");
                                    testClassCode.AppendLine("        }");
                                    testClassCode.AppendLine();
                                }

                                testCases.Clear(); // reset sau khi xử lý
                            }

                            currentMethodName = trimmedLine.Substring(1).Trim();
                        }
                        else
                        {
                            // Dòng này là dữ liệu cho test case: input1,input2,...,expected
                            var values = trimmedLine.Split(',').Select(s => s.Trim()).ToList();
                            if (values.Count >= 2) // ít nhất có 1 input + 1 expected
                            {
                                testCases.Add(values);
                            }
                        }
                    }

                    // Xử lý phương thức cuối cùng
                    if (currentMethodName != null && testCases.Count > 0)
                    {
                        var method = methods.FirstOrDefault(m => m.Name == currentMethodName);
                        if (method != null)
                        {
                            foreach (var caseData in testCases)
                            {
                                string expected = caseData.Last();
                                var inputParams = caseData.Take(caseData.Count - 1);
                                testClassCode.AppendLine($"        [TestCase({string.Join(", ", inputParams)}, ExpectedResult = {expected})]");
                            }

                            string paramList = string.Join(", ", method.GetParameters().Select(p => $"{getCSharpTypeName(p.ParameterType)} {p.Name}"));
                            string methodName = currentMethodName + "Test";
                            string argList = string.Join(", ", method.GetParameters().Select(p => p.Name));

                            string returnType = getCSharpTypeName(method.ReturnType);

                            testClassCode.AppendLine($"        public {returnType} {methodName}({paramList})");
                            testClassCode.AppendLine("        {");
                            testClassCode.AppendLine($"            var obj = new {targetType.Name}();");
                            testClassCode.AppendLine($"            return obj.{currentMethodName}({argList});");
                            testClassCode.AppendLine("        }");
                            testClassCode.AppendLine();
                        }
                    }

                    testClassCode.AppendLine("    }");
                    testClassCode.AppendLine("}");

                    // Ghi lại vào file test
                    string folderPath = @"E:\DOANTOTNGHIEP\DATN\Tests";
                    string testFileName = className == "Calculator" ? "Calculator_Test.cs" : className + "_Test.cs";
                    string filePath = Path.Combine(folderPath, testFileName);

                    Directory.CreateDirectory(folderPath);
                    File.WriteAllText(filePath, testClassCode.ToString());

                    MessageBox.Show($"Đã tạo file test từ CSV: {testFileName}!");
                    richTextBox1.Clear();
                    richTextBox1.Text = File.ReadAllText(filePath);
                }
            }
        }

        // Hàm FormatValue: Chuyển đổi object thành chuỗi C# hợp lệ để đưa vào test case
        private string FormatValue(object val)// Định nghĩa hàm FormatValue, trả về kiểu string, nhận vào một object tên là val
        {
            if (val == null) return "null";
            if (val is string) return $"\"{val}\"";
            if (val is bool) return val.ToString().ToLower();
            return val.ToString();
        }

        // Hàm GenerateRandomString: Tạo chuỗi ngẫu nhiên gồm chữ cái và số, độ dài tùy chọn
        private string GenerateRandomString(int length)
            {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random rand = new Random();
            return new string(Enumerable.Repeat(chars, length)// Lặp lại chuỗi `chars` với số lần là `length`, tạo ra 1 tập hợp có `length` phần tử
                .Select(s => s[rand.Next(s.Length)])// Với mỗi phần tử, chọn ngẫu nhiên 1 ký tự trong `chars`
                .ToArray());// Chuyển kết quả thành mảng ký tự
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbbDL.SelectedIndex = 0;
            BoGocChoButton(btnChayTest, 70);
            BoGocChoButton(btnChontep, 50);
            BoGocChoButton(btnSinhtest, 70);
        }

        private async void btnChayTest_Click(object sender, EventArgs e)
        {
            string testProjectFolder = @"E:\DOANTOTNGHIEP\DATN\Tests";// Chuỗi đường dẫn thư mục chứa project NUnit test
            string testProjectCsprojPath = Path.Combine(testProjectFolder, "Tests.csproj");// Ghép đường dẫn đến file Tests.csproj


            // --- MỚI: GHI file test từ richTextBox1 ---
            string testFilePath = Path.Combine(
                testProjectFolder,
                Path.GetFileNameWithoutExtension(selectedFilePath) + "_Test.cs");
            Directory.CreateDirectory(testProjectFolder);
            File.WriteAllText(testFilePath, richTextBox1.Text);


            if (!File.Exists(testProjectCsprojPath))// Kiểm tra nếu file Tests.csproj không tồn tại
            {
                MessageBox.Show("File .csproj không tồn tại. Vui lòng tạo lại file .csproj.");
                return;
            }

            // ❗ Bắt buộc: Build lại project để cập nhật file test mới
            //Build lại project trước khi test
            Process buildProcess = new Process();// Tạo tiến trình mới (process) để build project
            buildProcess.StartInfo.FileName = "dotnet";// Thiết lập tên chương trình thực thi: "dotnet"
            buildProcess.StartInfo.Arguments = $"build \"{testProjectCsprojPath}\"";// Thêm tham số: build file project .csproj
            buildProcess.StartInfo.RedirectStandardOutput = true;
            buildProcess.StartInfo.RedirectStandardError = true;
            buildProcess.StartInfo.UseShellExecute = false;
            buildProcess.StartInfo.CreateNoWindow = true;

            buildProcess.Start();// Bắt đầu chạy tiến trình

            // Đọc kết quả đầu ra và lỗi sau khi build
            string buildOutput = await buildProcess.StandardOutput.ReadToEndAsync();// Đọc toàn bộ đầu ra chuẩn (Output)
            string buildError = await buildProcess.StandardError.ReadToEndAsync();// Đọc toàn bộ lỗi chuẩn (Error)
            await Task.Run(() => buildProcess.WaitForExit());// Đợi tiến trình build kết thúc

            if (buildProcess.ExitCode != 0)// Nếu mã thoát khác 0 => build lỗi
            {
                richTextBox1.Clear();// Xoá nội dung trong richTextBox1
                richTextBox1.AppendText("❌ Build thất bại\n\n");// Ghi dòng thông báo thất bại
                richTextBox1.AppendText("==== OUTPUT ====\n" + buildOutput + "\n");// Ghi phần đầu ra
                richTextBox1.AppendText("==== ERROR ====\n" + buildError + "\n");// Ghi phần lỗi
                return;
            }

            // ✅ Nếu build thành công thì tiếp tục chạy test
            // Lấy tên class từ file .cs người dùng đã chọn
            string className = Path.GetFileNameWithoutExtension(selectedFilePath); // Lấy tên file không có đuôi .cs → dùng làm tên class
            string testClassName = $"{className}Tests"; // Thêm hậu tố "Tests" để ra tên class test

            // Tạo tiến trình để chạy lệnh test bằng dotnet test
            Process process = new Process();// Tạo tiến trình mới để chạy test
            process.StartInfo.FileName = "dotnet";// Tên lệnh: dotnet
            // Tham số: test file csproj, không build lại, chỉ chạy test class cụ thể, hiển thị log chi tiết
            process.StartInfo.Arguments = $"test \"{testProjectCsprojPath}\" --no-build --filter \"FullyQualifiedName~{testClassName}\" --logger \"console;verbosity=detailed\"";
            process.StartInfo.RedirectStandardOutput = true;// Lấy đầu ra
            process.StartInfo.RedirectStandardError = true;// Lấy lỗi
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            process.Start();// Bắt đầu tiến trình test

            // Đọc kết quả test
            string output = await process.StandardOutput.ReadToEndAsync();// Đọc kết quả test từ output
            string error = await process.StandardError.ReadToEndAsync();// Đọc lỗi nếu có

            await Task.Run(() => process.WaitForExit());// Đợi tiến trình kết thúc

            richTextBox1.Clear();// Xoá nội dung cũ của richTextBox1

            // Phân tích kết quả test theo dòng
            string[] lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.None);// Tách output thành các dòng
            List<string> testCaseResults = new List<string>();
            int testCaseNumber = 1;
            int total = 0, passed = 0, failed = 0, skipped = 0;

            double totalTimeMs = 0; // tổng thời gian tính bằng milisecond

            foreach (string line in lines)
            {
                if (line.Trim().StartsWith("Passed ") || line.Trim().StartsWith("Failed "))
                {
                    string trimmed = line.Trim();
                    int methodStart = trimmed.IndexOf(' ') + 1;
                    int timeStart = trimmed.LastIndexOf('[');
                    int timeEnd = trimmed.LastIndexOf(']');

                    string methodWithArgs = trimmed.Substring(methodStart, timeStart - methodStart).Trim();
                    string timeTaken = trimmed.Substring(timeStart + 1, timeEnd - timeStart - 1);

                    string resultSymbol = trimmed.StartsWith("Passed") ? "✅ Passed" : "❌ Failed";

                    // Chuyển thời gian sang milisecond
                    // Có thể timeTaken là "12ms", "< 1 ms", "1 s", "500 ms"...
                    double timeMs = 0;
                    if (timeTaken.Contains("ms"))
                    {
                        // Loại bỏ chữ "ms" và khoảng trắng, parse số
                        string numberPart = timeTaken.Replace("ms", "").Replace("<", "").Replace(" ", "");
                        if (double.TryParse(numberPart, out double ms))
                            timeMs = ms;
                    }
                    else if (timeTaken.Contains("s"))
                    {
                        // Loại bỏ chữ "s" và khoảng trắng, parse số rồi nhân 1000
                        string numberPart = timeTaken.Replace("s", "").Replace(" ", "");
                        if (double.TryParse(numberPart, out double s))
                            timeMs = s * 1000;
                    }
                    else
                    {
                        // Trường hợp khác, mặc định 0
                        timeMs = 0;
                    }

                    totalTimeMs += timeMs;

                    string testCaseStr = $"TestCase {testCaseNumber}.";
                    testCaseStr = testCaseStr.PadRight(12);
                    string methodStr = $"{methodWithArgs}:";
                    methodStr = methodStr.PadRight(50);
                    string resultStr = resultSymbol.PadRight(10);
                    string timeStr = $"⏱ {timeTaken}";

                    testCaseResults.Add($"{testCaseStr}{methodStr}{resultStr}{timeStr}");
                    testCaseNumber++;
                }

                // Trích thông tin tổng số test
                if (line.Trim().StartsWith("Total tests:"))
                    total = int.Parse(line.Split(':')[1].Trim());
                else if (line.Trim().StartsWith("Passed:"))
                    passed = int.Parse(line.Split(':')[1].Trim());
                else if (line.Trim().StartsWith("Failed:"))
                    failed = int.Parse(line.Split(':')[1].Trim());
                else if (line.Trim().StartsWith("Skipped:"))
                    skipped = int.Parse(line.Split(':')[1].Trim());
            }

            richTextBox1.Clear();

            foreach (var result in testCaseResults)
                richTextBox1.AppendText(result + Environment.NewLine);

            richTextBox1.AppendText(Environment.NewLine);
            richTextBox1.AppendText($"❌ Failed : {failed} testcase{Environment.NewLine}");
            richTextBox1.AppendText($"✅ Passed : {passed} testcase{Environment.NewLine}");
            richTextBox1.AppendText($"⚠️ Skipped : {skipped} testcase{Environment.NewLine}");
            richTextBox1.AppendText($"📊 Total : {total} testcase{Environment.NewLine}");

            // In thêm dòng tổng thời gian (format về dạng "xx ms" hoặc "yy s" nếu >1000 ms)
            if (totalTimeMs >= 1000)
                richTextBox1.AppendText($"⏱ Total Time : {(totalTimeMs / 1000.0):0.###} s{Environment.NewLine}");
            else
                richTextBox1.AppendText($"⏱ Total Time : {totalTimeMs:0.###} ms{Environment.NewLine}");
        }



        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void trackBarTestCount_Scroll(object sender, EventArgs e)
        {
            lblTestCount.Text = $"Số lượng test case: {trackBarTestCount.Value}";
        }

        private void cbbDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbDL.Width = 250;   
            cbbDL.Height = 200;
            cbbDL.Font = new Font("Segoe UI", 10);
            cbbDL.ItemHeight = 200;
        }

        private void BoGocChoButton(Button btn, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(btn.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, btn.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            btn.Region = new Region(path);
            btn.FlatStyle = FlatStyle.Flat;
        }
    }
}
