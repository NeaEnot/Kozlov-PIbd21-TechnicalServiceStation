using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using TechnicalServiceStationBusinessLogic.BindingModels;
using TechnicalServiceStationBusinessLogic.HelperModels;
using TechnicalServiceStationBusinessLogic.Interfaces;
using TechnicalServiceStationBusinessLogic.ViewModels;

namespace TechnicalServiceStationBusinessLogic.BusinessLogic
{
    public class ReportLogic
    {
        private readonly IServiceLogic serviceLogic;
        private readonly IOrderLogic orderLogic;

        public ReportLogic(IServiceLogic serviceLogic, IOrderLogic orderLogic)
        {
            this.serviceLogic = serviceLogic;
            this.orderLogic = orderLogic;
        }

        public List<ReportServiceViewModel> GetServices(ReportBindingModel model)
        {
            var answer = new List<ReportServiceViewModel>();

            foreach (var orderId in model.OrdersId)
            {
                var order = orderLogic.Read(new OrderBindingModel { Id = orderId })[0];

                answer.Add(new ReportServiceViewModel
                {
                    OrderId = orderId,
                    Services = order.OrderServices.Select(rec => new ValueTuple<string, int>(rec.ServiceName, rec.Price)).ToList()
                }) ;
            }

            return answer;
        }

        public List<ReportOrderAutopartsViewModel> GetOrderAutoparts(ReportBindingModel model)
        {
            var orders = 
                orderLogic
                .Read(
                    new OrderBindingModel
                    {
                        UserId = model.UserId,
                        DateFrom = model.DateFrom,
                        DateTo = model.DateTo
                    }
                );

            var list = new List<ReportOrderAutopartsViewModel>();

            foreach (var order in orders)
            {
                foreach (var os in order.OrderServices)
                {
                    list.AddRange (serviceLogic
                    .Read(new ServiceBindingModel { Id = os.ServiceId } )?[0].ServiceAutoparts
                    .Select (recSA => new ReportOrderAutopartsViewModel
                    {
                        Id = order.Id,
                        AutopartsName = recSA.Value.Item1,
                        Count = recSA.Value.Item2,
                        Price = recSA.Value.Item3 * recSA.Value.Item2
                    }));
                }
            }

            return list;
        }

        public void SendServicesWordFile(ReportBindingModel model)
        {
            Random rnd = new Random();
            string fileName = ConfigurationManager.AppSettings["TempFilesPath"] + rnd.Next().ToString() + ".docx";

            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = fileName,
                Title = "Список работ",
                Report = GetServices(model)
            });

            MailLogic.MailSendAsync(
                new MailSendInfo
                {
                    MailAddress = model.UserEmail,
                    Subject = "Список Работ",
                    Text = "Отчёт в прикреплённом файле",
                    Attachment = fileName
                });
        }

        public void SendServicesExcelFile(ReportBindingModel model)
        {
            Random rnd = new Random();
            string fileName = ConfigurationManager.AppSettings["TempFilesPath"] + rnd.Next().ToString() + ".xlsx";

            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = fileName,
                Title = "Список работ",
                Report = GetServices(model)
            });

            MailLogic.MailSendAsync(
                new MailSendInfo
                {
                    MailAddress = model.UserEmail,
                    Subject = "Список работ",
                    Text = "Отчёт в прикреплённом файле",
                    Attachment = fileName
                });
        }

        public void SendAutopartsPdfFile(ReportBindingModel model)
        {
            Random rnd = new Random();
            string fileName = ConfigurationManager.AppSettings["TempFilesPath"] + rnd.Next().ToString() + ".pdf";

            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = fileName,
                Title = "Список заказов с расшифровкой по запчастям",
                OrderAutoparts = GetOrderAutoparts(model)
            });

            MailLogic.MailSendAsync(
                new MailSendInfo
                {
                    MailAddress = model.UserEmail,
                    Subject = "Список заказов с расшифровкой по запчастям",
                    Text = "Отчёт в прикреплённом файле",
                    Attachment = fileName
                });
        }
    }
}
