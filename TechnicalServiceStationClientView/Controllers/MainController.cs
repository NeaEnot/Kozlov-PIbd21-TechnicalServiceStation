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
        public List<ServiceViewModel> GetServices(ServiceBindingModel model) => service.Read(model)?.ToList();

        [HttpGet]
        public List<OrderViewModel> GetOrders(int userId)
            => order.Read(new OrderBindingModel { UserId = userId });

        [HttpPost]
        public void CreateOrder(OrderBindingModel model)
            => main.CreateOrder(model);

        [HttpGet]
        public List<ReportOrderAutopartsViewModel> GetReportOrderAutoparts(DateTime dateFrom, DateTime dateTo)
            => report.GetOrderAutoparts(new ReportBindingModel { DateFrom = dateFrom, DateTo = dateTo })?.ToList();

        [HttpPost]
        public void SendReportPdf(int userId, DateTime dateFrom, DateTime dateTo)
            => report.SendAutopartsPdfFile(new UserBindingModel { Id = userId }, new ReportBindingModel { DateFrom = dateFrom, DateTo = dateTo });

        [HttpPost]
        public void SendReportWord(int userId, DateTime dateFrom, DateTime dateTo)
            => report.SendServicesWordFile(new UserBindingModel { Id = userId }, new ReportBindingModel { DateFrom = dateFrom, DateTo = dateTo });

        [HttpPost]
        public void SendReportExcel(int userId, DateTime dateFrom, DateTime dateTo)
            => report.SendServicesExcelFile(new UserBindingModel { Id = userId }, new ReportBindingModel { DateFrom = dateFrom, DateTo = dateTo });

        // --------------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        // GET: Main/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Main/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Main/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Main/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Main/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Main/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Main/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}