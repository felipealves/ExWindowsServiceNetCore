using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;
using System.ServiceProcess;

namespace ExemploService.ConsoleApplicationNetCore
{
    internal class ScheduleServices : ServiceBase
    {
        private readonly IScheduler scheduler;

        public ScheduleServices()
        {
            ServiceName = "ScheduleService";

            NameValueCollection props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" },
                { "quartz.scheduler.instanceName", "MyScheduler" },
                { "quartz.jobStore.type", "Quartz.Simpl.RAMJobStore, Quartz" },
                { "quartz.threadPool.threadCount", "3" }
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            scheduler = factory.GetScheduler().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public void onDebug()
        {
            Util.VerificarSeExisteArquivo2();
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            scheduler.Start().ConfigureAwait(false).GetAwaiter().GetResult();
            ScheduleJobs();
        }

        protected override void OnStop()
        {
            Util.SalvarDataArquivo("parado").GetAwaiter();
            StopJobs();
        }

        private void ScheduleJobs()
        {
            //Identificamos o nosso Job que será executado.
            IJobDetail job = JobBuilder.Create<ArquivoJob>()
                .WithIdentity("job1", "group1")
                .Build();

            //Criareamos as configurações do agendamento
            //EX: WithIntervalInSeconds - Intevalo em segundos em que o serviço será executado.
            //    RepeatForever - Será repetido até o serviço ser parado.
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            //Agendamento do Job.
            scheduler.ScheduleJob(job, trigger).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public void StopJobs()
        {
            scheduler.Shutdown().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
