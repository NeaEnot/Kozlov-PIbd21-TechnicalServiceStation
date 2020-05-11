using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TechnicalServiceStationBusinessLogic.BindingModels;
using TechnicalServiceStationBusinessLogic.Interfaces;
using TechnicalServiceStationBusinessLogic.ViewModels;
using TechnicalServiceStationDatabaseImplement.Models;

namespace TechnicalServiceStationDatabaseImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        public void CreateOrUpdate(OrderBindingModel model)
        {
            using(var context = new TechnicalServiceStationDatabase())
            {
                Order element;

                if (model.Id.HasValue)
                {
                    element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);

                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Order();
                    context.Orders.Add(element);
                }

                element.UserId = model.UserId.Value;
                element.Price = model.Price;
                element.Status = model.Status;
                element.CreateDate = model.CreateDate;
                element.DeliveryDate = model.DeliveryDate;

                context.SaveChanges();
            }
        }

        public void Delete(OrderBindingModel model)
        {
            using (var context = new TechnicalServiceStationDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.Orders.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            using (var context = new TechnicalServiceStationDatabase())
            {
                return 
                    context.Orders
                    .Where(
                        rec => model == null
                        || rec.Id == model.Id && model.Id.HasValue
                        || model.DateFrom.HasValue && model.DateTo.HasValue && rec.CreateDate >= model.DateFrom && rec.CreateDate <= model.DateTo
                        || model.UserId.HasValue && rec.UserId == model.UserId
                    )
                    .Include(rec => rec.OrderServices)
                    .Include(rec => rec.User)
                    .Select(rec => new OrderViewModel
                    {
                        Id = rec.Id,
                        Price = rec.Price,
                        Status = rec.Status,
                        CreateDate = rec.CreateDate,
                        DeliveryDate = rec.DeliveryDate,
                        UserId = rec.UserId,
                        UserEmail = rec.User.Email,
                        OrderServices = 
                            context.OrderServices
                            .Include(recOS => recOS.Service)
                            .Where(recOS => recOS.OrderId == rec.Id)
                            .ToDictionary(recOS => recOS.Id, recOS => new ValueTuple<string, int>(recOS.Service.Name, recOS.Service.Price))
                    })
                    .ToList();
            }
        }
    }
}
