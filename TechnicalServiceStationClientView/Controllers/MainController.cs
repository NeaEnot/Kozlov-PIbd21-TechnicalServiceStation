using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechnicalServiceStationBusinessLogic.BindingModels;
using TechnicalServiceStationBusinessLogic.BusinessLogic;
using TechnicalServiceStationBusinessLogic.Interfaces;
using TechnicalServiceStationBusinessLogic.ViewModels;

namespace TechnicalServiceStationClientView.Controllers
{
    public class MainController : Controller
    {
        private readonly IOrderLogic order;
        private readonly IServiceLogic service;
        private readonly MainLogic main;
        private readonly ReportLogic report;

        public MainController(IOrderLogic order, IServiceLogic service, MainLogic main, ReportLogic report)
        {
            this.order = order;
            this.service = service;
            this.main = main;
            this.report = report;
        }

        [HttpGet]
        public ActionResult CreateOrder(string[] services)
        {
            if (services.Length == 0)
            {
                return Redirect("/Main/Index?errMessage=" + "Для формирования заказа нужно выбрать по крайней мере одну работу");
            }

            string errMessage = "";

            List<ServiceViewModel> servicesList = null;

            try
            {
                servicesList = service.Read(null)?.Where(rec => services.Contains(rec.Id.ToString())).ToList();
            }
            catch (Exception e)
            {
                errMessage += e.Message + "\n";
            }

            try
            {
                main.CreateOrder(
                new OrderBindingModel
                {
                    UserId = getUserId(),
                    CreateDate = DateTime.Now.Date,
                    Price = servicesList.Sum(rec => rec.Price),
                    OrderServices = servicesList.ToDictionary(rec => rec.Id, rec => rec.Name)
                });
            }
            catch (Exception e)
            {
                errMessage += e.Message + "\n";
            }

            return Redirect("/Main/Index?errMessage=" + errMessage);
        }

        // GET: Main/Index
        [HttpGet]
        public ActionResult Index(string errMessage)
        {
            if (!HttpContext.Session.Keys.Contains("userId"))
            {
                return Redirect("/User/Enter");
            }

            try
            {
                ViewBag.orders = order.Read(new OrderBindingModel { UserId = getUserId() });
            }
            catch (Exception e)
            {
                errMessage += "\n" + e.Message;
                ViewBag.orders = null;
            }

            ViewBag.errMessage = errMessage;

            return View();
        }

        // GET: Main/NewOrder
        [HttpGet]
        public ActionResult NewOrder()
        {
            if (!HttpContext.Session.Keys.Contains("userId"))
            {
                return Redirect("/User/Enter");
            }

            ViewBag.services = service.Read(null)?.ToList();

            return View();
        }

        // GET: Main/Orders
        [HttpGet]
        public ActionResult Orders(string[] orders, string actionOption, string format)
        {
            if (!HttpContext.Session.Keys.Contains("userId"))
            {
                return Redirect("/User/Enter");
            }

            if (orders.Length == 0)
            {
                return Redirect("/Main/Index?errMessage=Выберите хотя бы один заказ");
            }

            string errMessage = "";

            if (actionOption == "Зарезервировать детали")
            {
                foreach (var orderId in orders)
                {
                    try
                    {
                        main.ReserveAutoparts(new OrderBindingModel { Id = int.Parse(orderId) });
                    }
                    catch (Exception e)
                    {
                        errMessage += orderId + " : " + e.Message + "\n";
                    }
                }
            }

            if (actionOption == "Отправить отчёт о работах на почту")
            {
                try
                {
                    List<int> ordersId = new List<int>();

                    foreach (var orderId in orders)
                    {
                        ordersId.Add(int.Parse(orderId));
                    }

                    if (format == "docx")
                    {
                        report.SendServicesWordFile(new ReportBindingModel
                        {
                            UserId = getUserId(),
                            UserEmail = getUserEmail(),
                            OrdersId = ordersId
                        });
                    }

                    if (format == "xmlx")
                    {
                        report.SendServicesExcelFile(new ReportBindingModel
                        {
                            UserId = getUserId(),
                            UserEmail = getUserEmail(),
                            OrdersId = ordersId
                        });
                    }
                }
                catch (Exception e)
                {
                    errMessage = e.Message;
                }
            }

            return Redirect("/Main/Index?errMessage=" + errMessage);
        }

        // GET: Main/Report
        [HttpGet]
        public ActionResult Report(string report, string errMessage, string dateFrom = "00.00.0000", string dateTo = "00.00.0000")
        {
            if (!HttpContext.Session.Keys.Contains("userId"))
            {
                return Redirect("/User/Enter");
            }

            List<ReportOrderAutopartsViewModel> reportList = new List<ReportOrderAutopartsViewModel>();
            if (report != null)
            {
                foreach (var r in report.Split(';'))
                {
                    reportList.Add(new ReportOrderAutopartsViewModel
                    {
                        Id = int.Parse(r.Split(',')[0]),
                        AutopartsName = r.Split(',')[1],
                        Count = int.Parse(r.Split(',')[2]),
                        Price = int.Parse(r.Split(',')[3])
                    });
                }
            }

            ViewBag.reportList = reportList;
            ViewBag.errMessage = errMessage;
            ViewBag.dateFrom = dateFrom;
            ViewBag.dateTo = dateTo;

            return View();
        }

        // GET: Main/GetReport
        [HttpGet]
        public ActionResult GetReport(string dateFrom, string dateTo, string actionOption)
        {
            if (!HttpContext.Session.Keys.Contains("userId"))
            {
                return Redirect("/User/Enter");
            }

            DateTime from;
            DateTime to;

            try
            {
                from = DateTime.Parse(dateFrom);
                to = DateTime.Parse(dateTo);
            }
            catch
            {
                return Redirect("/Main/Report?" +
                                "errMessage=Введите значения для даты начала и даты окочания периода" +
                                "&dateFrom=" + (dateFrom == null ? "00.00.0000" : dateFrom) + 
                                "&dateTo=" + (dateTo == null ? "00.00.0000" : dateTo));
            }

            if (from >= to)
            {
                return Redirect("/Main/Report?" +
                                "errMessage=Дата окончания периода должна быть больше даты начала" +
                                "&dateFrom=" + dateFrom +
                                "&dateTo=" + dateTo);
            }

            if (actionOption == "Вывести отчёт по деталям")
            {
                string reportMessage = "";
                string errMessage = "";

                try
                {
                    List<ReportOrderAutopartsViewModel> reportList = report.GetOrderAutoparts(new ReportBindingModel 
                    { 
                        UserId = getUserId(), 
                        UserEmail = getUserEmail(), 
                        DateFrom = from, 
                        DateTo = to 
                    })?.ToList();

                    foreach (var report in reportList)
                    {
                        reportMessage += report.Id + "," + report.AutopartsName + "," + report.Count + "," + report.Price + ";";
                    }
                    reportMessage = reportMessage.Substring(0, reportMessage.Length - 1);
                }
                catch (Exception e)
                {
                    errMessage = e.Message;
                }

                return Redirect("/Main/Report?" +
                                "report=" + reportMessage + 
                                "&errMessage=" + errMessage +
                                "&dateFrom=" + dateFrom +
                                "&dateTo=" + dateTo);
            }

            if (actionOption == "Отправить отчёт по деталям на почту (pdf)")
            {
                string errMessage = "";

                try
                {
                    report.SendAutopartsPdfFile(new ReportBindingModel 
                    {
                        UserId = getUserId(), 
                        UserEmail = getUserEmail(), 
                        DateFrom = from, 
                        DateTo = to 
                    });
                }
                catch (Exception e)
                {
                    errMessage = e.Message;
                }

                return Redirect("/Main/Index?errMessage=" + errMessage);
            }

            return Redirect("/Main/Index");
        }

        public int getUserId()
        {
            var bytes = HttpContext.Session.Get("userId");
            int userId = 0;

            for (int i = bytes.Length - 1; i >= 0; i--)
            {
                userId += bytes[i] * Convert.ToInt32((Math.Pow(byte.MaxValue, bytes.Length - 1 - i)));
            }

            return userId;
        }

        public string getUserEmail()
        {
            var bytes = HttpContext.Session.Get("userEmail");
            string answer = "";

            for (int i = 0; i < bytes.Length; i++)
            {
                answer += (char)bytes[i];
            }

            return answer;
        }
    }
}