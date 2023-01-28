using Api.Utills.Execution;

namespace Api.Utills.SaveFile
{
    public class SaveSourceCodeToTmpDirDocker : ISaveFileStrategy<SourceCode>
    {
        private static readonly string tmpDir = @"C:\Users\Anirut\Desktop\CompilerAPI\Container\tmp";

        private static readonly SaveSourceCodeToTmpDirDocker _instant = new();
        private SaveSourceCodeToTmpDirDocker() { }
        public static SaveSourceCodeToTmpDirDocker Instant { get => _instant; }

        public void Save(in SourceCode sourceCode, out string fileName, string extension = "code")
        {
            fileName = $"code_{HashString(sourceCode.Code)}.{extension}";
            string filePath = Path.Join(tmpDir, fileName);
            using StreamWriter file = new(filePath);
            file.Write(sourceCode.Code);
            file.Close();
        }

        public string HashString(string text, string salt = "")
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text + salt);
            byte[] hashBytes = System.Security.Cryptography.SHA256.HashData(textBytes);
            string hash = BitConverter
                .ToString(hashBytes)
                .Replace("-", string.Empty);
            return hash;
        }

    }
}
