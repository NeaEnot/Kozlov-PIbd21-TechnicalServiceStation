using System;
using System.Collections.Generic;
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

        public ReportLogic(IServiceLogic serviceLogic, IAutopartsLogic autopartTypeLogic, IOrderLogic orderLogic)
        {
            this.serviceLogic = serviceLogic;
            this.autopartsLogic = autopartTypeLogic;
            this.orderLogic = orderLogic;
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

                    foreach (var autoparts in service.Autoparts)
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

        public void SaveServicesToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список работ",
                Orders = GetServices(model)
            });
        }

        public void SaveServicesToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список работ",
                Orders = GetServices(model)
            });
        }

        public void SaveAutopartssToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов с расшифровкой по запчастям",
                OrderAutoparts = GetOrderAutoparts(model)
            });
        }
    }
}
