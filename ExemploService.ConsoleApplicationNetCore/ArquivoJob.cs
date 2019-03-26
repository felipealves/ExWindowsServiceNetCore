using Quartz;
using System.Threading.Tasks;

namespace ExemploService.ConsoleApplicationNetCore
{
    internal class ArquivoJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Util.SalvarDataArquivo("iniciado").GetAwaiter();
        }
    }
}
