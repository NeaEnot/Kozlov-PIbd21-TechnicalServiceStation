using System;
using TechnicalServiceStationBusinessLogic.BindingModels;
using TechnicalServiceStationBusinessLogic.Enums;
using TechnicalServiceStationBusinessLogic.HelperModels;
using TechnicalServiceStationBusinessLogic.Interfaces;

namespace TechnicalServiceStationBusinessLogic.BusinessLogic
{
    public class MainLogic
    {
        private readonly IOrderLogic orderLogic;

        private readonly IUserLogic userLogic;

        public MainLogic(IOrderLogic orderLogic, IUserLogic userLogic)
        {
            this.orderLogic = orderLogic;
            this.userLogic = userLogic;
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

        public void TakeOrderInWork(OrderBindingModel model)
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
