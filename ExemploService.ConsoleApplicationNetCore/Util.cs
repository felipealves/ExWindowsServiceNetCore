using System;
using System.IO;
using System.Threading.Tasks;

namespace ExemploService.ConsoleApplicationNetCore
{
    internal class Util
    {
        public static string VerificarSeExisteArquivo()
        {
            string path = @"c:\tmp";
            string filename = $@"{path}\MyService.txt";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (!File.Exists(filename))
            {
                File.Create(filename);
            }

            return filename;
        }

        public static async Task SalvarDataArquivo(string status)
        {
            string filename = Util.VerificarSeExisteArquivo();
            await File.AppendAllTextAsync(filename, $"{DateTime.Now} {status}.{Environment.NewLine}");
        }
    }
}
