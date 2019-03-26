using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;

namespace ExemploService.ConsoleApplicationNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //Variavel para verificar se esta em modo Debug a execução do serviço.
            var isService = !(Debugger.IsAttached || args.Contains("--console"));

            using (var servico = new ScheduleServices())
            {
                if (isService)
                    ServiceBase.Run(servico);
                else
                    servico.onDebug();
            }
        }
    }
}
