using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace AbstractGarmentFactoryRestApi.Services
{
    public class WorkImplementer
    {
        private readonly IMainService _service;

        private readonly IImplementerService _serviceImplementer;

        private readonly int _implementerId;

        private readonly int _indentId;
        // семафор   
        static Semaphore _sem = new Semaphore(3, 3); 

        Thread myThread;

        public WorkImplementer(IMainService service, IImplementerService serviceImplementer, int implementerId, int indentId)
        {
            _service = service;
            _serviceImplementer = serviceImplementer;
            _implementerId = implementerId;
            _indentId = indentId;
            try
            {
                _service.TakeIndentInWork(new IndentBindingModel
                {
                    Id = _indentId,
                    ImplementerId = _implementerId
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            myThread = new Thread(Work);
            myThread.Start();
        }

        public void Work()
        {
            try
            {
                // забиваем мастерскую       
                _sem.WaitOne();
                // Типа выполняем             
                Thread.Sleep(5000);
                _service.FinishIndent(new IndentBindingModel 
                {
                    Id = _indentId
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // освобождаем мастерскую      
                _sem.Release();
            }
        }
    }
}