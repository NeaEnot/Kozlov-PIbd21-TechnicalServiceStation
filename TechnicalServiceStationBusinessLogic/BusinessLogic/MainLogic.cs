using DocumentFormat.OpenXml.EMMA;
using System;
using System.Collections.Generic;
using System.Linq;
using TechnicalServiceStationBusinessLogic.BindingModels;
using TechnicalServiceStationBusinessLogic.Enums;
using TechnicalServiceStationBusinessLogic.HelperModels;
using TechnicalServiceStationBusinessLogic.Interfaces;
using TechnicalServiceStationBusinessLogic.ViewModels;

namespace TechnicalServiceStationBusinessLogic.BusinessLogic
{
    public class MainLogic
    {
        private readonly IOrderLogic orderLogic;
        private readonly IUserLogic userLogic;
        private readonly IServiceLogic serviceLogic;
        private readonly IWarehouseLogic warehouseLogic;

        public MainLogic(IOrderLogic orderLogic, IUserLogic userLogic, IServiceLogic serviceLogic, IWarehouseLogic warehouseLogic)
        {
            this.orderLogic = orderLogic;
            this.userLogic = userLogic;
            this.serviceLogic = serviceLogic;
            this.warehouseLogic = warehouseLogic;
        }

        public void CreateOrder(OrderBindingModel model)
        {
            model.Status = OrderStatus.Принят;

            orderLogic.CreateOrUpdate(model);

            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = userLogic.Read(new UserBindingModel { Id = model.UserId })?[0]?.Email,
                Subject = $"Новый заказ",
                Text = $"Заказ принят."
            });
        }

        public void ReserveAutoparts(OrderBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel { Id = model.Id })?[0];

            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }

            if (order.Status != OrderStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }

            List<ServiceAutopartsBindingModel> serviceAutoparts = new List<ServiceAutopartsBindingModel>();

            foreach (var os in order.OrderServices)
            {
                serviceAutoparts
                    .AddRange(
                        serviceLogic
                        .Read(new ServiceBindingModel { Id = os.ServiceId })?[0]
                        .ServiceAutoparts
                        .Select(
                            rec => new ServiceAutopartsBindingModel
                            {
                                AutopartsId = rec.Key,
                                Count = rec.Value.Item2
                            }));
            }

            warehouseLogic.ReserveAutoparts(serviceAutoparts);

            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                Price = order.Price,
                Status = OrderStatus.Зарезервирован,
                CreateDate = order.CreateDate,
                UserId = order.UserId
            });

            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = userLogic.Read(new UserBindingModel { Id = order.UserId })?[0]?.Email,
                Subject = $"Заказ №{order.Id}",
                Text = $"Запчасти по заказу №{order.Id} зарезервированы."
            });
        }

        public void TakeOrderInWork(OrderBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel { Id = model.Id })?[0];

            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }

            if (order.Status != OrderStatus.Зарезервирован)
            {
                throw new Exception("Заказ не в статусе \"Зарезервирован\"");
            }

            List<ServiceAutopartsBindingModel> serviceAutoparts = new List<ServiceAutopartsBindingModel>();

            foreach (var os in order.OrderServices)
            {
                serviceAutoparts
                    .AddRange(
                        serviceLogic
                        .Read(new ServiceBindingModel { Id = os.ServiceId })?[0]
                        .ServiceAutoparts
                        .Select(
                            rec => new ServiceAutopartsBindingModel
                            {
                                AutopartsId = rec.Key,
                                Count = rec.Value.Item2
                            }));
            }

            warehouseLogic.WriteOffAutoparts(serviceAutoparts);

            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                Price = order.Price,
                Status = OrderStatus.Выполняется,
                CreateDate = order.CreateDate,
                UserId = order.UserId
            });

            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = userLogic.Read(new UserBindingModel { Id = order.UserId })?[0]?.Email,
                Subject = $"Заказ №{order.Id}",
                Text = $"Заказ №{order.Id} передан в работу."
            });
        }

        public void FinishOrder(OrderBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel { Id = model.Id })?[0];

            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }

            if (order.Status != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }

            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                Price = order.Price,
                Status = OrderStatus.Готов,
                DeliveryDate = DateTime.Now,
                CreateDate = order.CreateDate,
                UserId = order.UserId
            });

            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = userLogic.Read(new UserBindingModel { Id = order.UserId })?[0]?.Email,
                Subject = $"Заказ №{order.Id}",
                Text = $"Заказ №{order.Id} готов."
            });

        }

        public void PayOrder(OrderBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel { Id = model.Id })?[0];

            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }

            if (order.Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }

            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                Price = order.Price,
                Status = OrderStatus.Оплачен,
                DeliveryDate = DateTime.Now,
                CreateDate = order.CreateDate,
                UserId = order.UserId
            });

            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = userLogic.Read(new UserBindingModel { Id = order.UserId })?[0]?.Email,
                Subject = $"Заказ №{order.Id}",
                Text = $"Заказ №{order.Id} оплачен."
            });
        }
    }
}
