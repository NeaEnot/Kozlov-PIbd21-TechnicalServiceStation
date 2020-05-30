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
        private readonly IAutopartsLogic autopartsLogic;
        private readonly IServiceLogic serviceLogic;
        private readonly IOrderLogic orderLogic;
        private readonly IUserLogic userLogic;

        public ReportLogic(IServiceLogic serviceLogic, IAutopartsLogic autopartTypeLogic, IOrderLogic orderLogic, IUserLogic userLogic)
        {
            this.serviceLogic = serviceLogic;
            this.autopartsLogic = autopartTypeLogic;
            this.orderLogic = orderLogic;
            this.userLogic = userLogic;
        }

        public Dictionary<int, List<ReportServiceViewModel>> GetServices(ReportBindingModel model)
        {
            var orders = 
                orderLogic
                .Read(
                    new OrderBindingModel
                    {
                        DateFrom = model.DateFrom,
                        DateTo = model.DateTo
                    }
                );

            Dictionary<int, List<ReportServiceViewModel>> answer = new Dictionary<int, List<ReportServiceViewModel>>();

            foreach (var order in orders)
            {
                var list = new List<ReportServiceViewModel>();

                foreach (var os in order.OrderServices)
                {
                    var record = new ReportServiceViewModel
                    {
                        OrderCreateDate = order.CreateDate,
                        ServiceName = os.Value.Item1,
                        Price = os.Value.Item2
                    };

                    list.Add(record);
                }

                answer.Add(order.Id, list);
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
                        DateFrom = model.DateFrom,
                        DateTo = model.DateTo
                    }
                );

            var list = new List<ReportOrderAutopartsViewModel>();

            foreach (var order in orders)
            {
                foreach (var os in order.OrderServices)
                {
                    var service =
                        serviceLogic
                        .Read(
                            new ServiceBindingModel
                            {
                                Id = os.Key
                            }
                        )?[0];

                    foreach (var autoparts in service.ServiceAutoparts)
                    {
                        var record = new ReportOrderAutopartsViewModel
                        {
                            OrderCreateDate = order.CreateDate,
                            AutopartsName = autoparts.Value.Item1,
                            Count = autoparts.Value.Item2,
                            Price = autoparts.Value.Item3
                        };

                        list.Add(record);
                    }
                }
            }

            return list;
        }

        public void SendServicesWordFile(UserBindingModel userModel, ReportBindingModel model)
        {
            Random rnd = new Random();
            string fileName = ConfigurationManager.AppSettings["TempFilesPath"] + rnd.Next().ToString() + ".docx";

            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список работ",
                Orders = GetServices(model)
            });

            UserViewModel user = userLogic.Read(userModel)?[0];

            MailLogic.MailSendAsync(
                new MailSendInfo
                {
                    MailAddress = user.Email,
                    Subject = "Список Работ",
                    Text = "Отчёт в прикреплённом файле",
                    Attachment = fileName
                });

            File.Delete(fileName);
        }

        public void SendServicesExcelFile(UserBindingModel userModel, ReportBindingModel model)
        {
            Random rnd = new Random();
            string fileName = ConfigurationManager.AppSettings["TempFilesPath"] + rnd.Next().ToString() + ".xlsx";

            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = fileName,
                Title = "Список работ",
                Orders = GetServices(model)
            });

            UserViewModel user = userLogic.Read(userModel)?[0];

            MailLogic.MailSendAsync(
                new MailSendInfo
                {
                    MailAddress = user.Email,
                    Subject = "Список работ",
                    Text = "Отчёт в прикреплённом файле",
                    Attachment = fileName
                });

            File.Delete(fileName);
        }

        public void SendAutopartsPdfFile(UserBindingModel userModel, ReportBindingModel model)
        {
            Random rnd = new Random();
            string fileName = ConfigurationManager.AppSettings["TempFilesPath"] + rnd.Next().ToString() + ".pdf";

            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = fileName,
                Title = "Список заказов с расшифровкой по запчастям",
                OrderAutoparts = GetOrderAutoparts(model)
            });

            UserViewModel user = userLogic.Read(userModel)?[0];

            MailLogic.MailSendAsync(
                new MailSendInfo
                {
                    MailAddress = user.Email,
                    Subject = "Список заказов с расшифровкой по запчастям",
                    Text = "Отчёт в прикреплённом файле",
                    Attachment = fileName
                });

            File.Delete(fileName);
        }
    }
}
