using AbstractGarmentFactoryModel;
using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Configuration;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using AbstractGarmentFactoryServiceDAL.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Configuration;


namespace AbstractGarmentFactoryServiceImplementDataBase.Implementations
{
    public class MainServiceDB : IMainService
    {
        private AbstractDBScope context;

        public MainServiceDB(AbstractDBScope context)
        {
            this.context = context;
        }

        public List<IndentViewModel> GetList()
        {
            List<IndentViewModel> result = context.Indents.Select(rec => new IndentViewModel
            {
                Id = rec.Id,
                CustomerId = rec.CustomerId,
                FabricId = rec.FabricId,
                ImplementerId = rec.ImplementerId,
                DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " + SqlFunctions.DateName("mm", rec.DateCreate) + " " + SqlFunctions.DateName("yyyy", rec.DateCreate),
                DateImplement = rec.DateImplement == null ? "" : SqlFunctions.DateName("dd", rec.DateImplement.Value) + " " + SqlFunctions.DateName("mm", rec.DateImplement.Value) + " " + SqlFunctions.DateName("yyyy", rec.DateImplement.Value),
                Condition = rec.Condition.ToString(),
                Amount = rec.Amount,
                Total = rec.Total,
                CustomerFIO = rec.Customer.CustomerFIO,
                FabricName = rec.Fabric.FabricName,
                ImplementerName = rec.Implementer.ImplementerFIO
            }).ToList();
            return result;
        }

        public void CreateIndent(IndentBindingModel model)
        {
            var indent = new Indent
            {
                CustomerId = model.CustomerId,
                FabricId = model.FabricId,
                ImplementerId = model.ImplementerId,
                DateCreate = DateTime.Now,
                Amount = model.Amount,
                Total = model.Total,
                Condition = IndentCondition.Принят
            };
         //   context.Indents.Add(indent);

            var customer = context.Customers.FirstOrDefault(x => x.Id == model.CustomerId);
            SendEmail(customer.Mail, "Оповещение по заказам", 
                string.Format("Заказ №{0} от {1} создан успешно",
                indent.Id, indent.DateCreate.ToShortDateString()));

            context.SaveChanges();

        }

        public void TakeIndentInWork(IndentBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                Indent element = context.Indents
                    .FirstOrDefault(rec => rec.Id == model.Id);
                try
                {
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.Condition != IndentCondition.Принят && element.Condition != IndentCondition.НедостаточноРесурсов)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }
                    var fabricStockings = context.FabricStockings
                        .Include(rec => rec.Stocking)
                        .Where(rec => rec.FabricId == element.FabricId).ToList();
                    // списываем          
                    foreach (var fabricStocking in fabricStockings)
                    {
                        int amountOnStorages = fabricStocking.Amount * element.Amount;
                        var storageStockings = context.StorageStockings
                            .Where(rec => rec.StockingId == fabricStocking.StockingId).ToList();
                        foreach (var storageStocking in storageStockings)
                        {
                            // компонентов на одном слкаде может не хватать     
                            if (storageStocking.Amount >= amountOnStorages)
                            {
                                storageStocking.Amount -= amountOnStorages;
                                amountOnStorages = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                amountOnStorages -= storageStocking.Amount;
                                storageStocking.Amount = 0;
                                context.SaveChanges();
                            }
                        }
                        if (amountOnStorages > 0)
                        {
                            throw new Exception("Не достаточно компонента " + fabricStocking.Stocking.StockingName + " требуется " + fabricStocking.Amount + ", не хватает " + amountOnStorages);
                        }
                    }
                    element.ImplementerId = model.ImplementerId;
                    element.DateImplement = DateTime.Now;
                    element.Condition = IndentCondition.Выполняется;
                    context.SaveChanges();
                    SendEmail(element.Customer.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} передеан в работу", element.Id, element.DateCreate.ToShortDateString()));
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    element.Condition = IndentCondition.НедостаточноРесурсов;
                    context.SaveChanges();
                    transaction.Commit();
                    throw;
                }
            }
        }

        public void FinishIndent(IndentBindingModel model)
        {
            Indent element = context.Indents.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Condition != IndentCondition.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Condition = IndentCondition.Готов;
            context.SaveChanges();
            SendEmail(element.Customer.Mail, "Оповещение по заказам", 
                string.Format("Заказ №{0} от {1} передан на оплату", 
                element.Id, element.DateCreate.ToShortDateString()));
        }

        public void PayIndent(IndentBindingModel model)
        {
            Indent element = context.Indents.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Condition != IndentCondition.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Condition = IndentCondition.Оплачен;
            context.SaveChanges();
            SendEmail(element.Customer.Mail, "Оповещение по заказам", 
                string.Format("Заказ №{0} от {1} оплачен успешно",
                element.Id, element.DateCreate.ToShortDateString()));
        }

        public void PutStockingOnStorage(StorageStockingBindingModel model)
        {
            StorageStocking element = context.StorageStockings.FirstOrDefault(rec => rec.StorageId == model.StorageId && rec.StockingId == model.StockingId);
            if (element != null)
            {
                element.Amount += model.Amount;
            }
            else
            {
                context.StorageStockings.Add(new StorageStocking
                {
                    StorageId = model.StorageId,
                    StockingId = model.StockingId,
                    Amount = model.Amount
                });
            }
            context.SaveChanges();
        }

        public List<IndentViewModel> GetFreeIndents()
        {       
            List<IndentViewModel> result = context.Indents
                .Where(x => x.Condition == IndentCondition.Принят || x.Condition == IndentCondition.НедостаточноРесурсов)
                .Select(rec => new IndentViewModel
                {
                    Id = rec.Id
                }).ToList();
            return result;
        }

        private void SendEmail(string mailAddress, string subject, string text)
        {
            MailMessage objMailMessage = new MailMessage();
            SmtpClient objSmtpClient = null;

            try
            {
                objMailMessage.From = new MailAddress(ConfigurationManager.AppSettings["MailLogin"]);
                objMailMessage.To.Add(new MailAddress(mailAddress));
                objMailMessage.Subject = subject;
                objMailMessage.Body = text;
                objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;

                objSmtpClient = new SmtpClient("smtp.gmail.com", 587);
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailLogin"], 
                    ConfigurationManager.AppSettings["MailPassword"]);

                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMailMessage = null;
                objSmtpClient = null;
            }
        }
    }
}
