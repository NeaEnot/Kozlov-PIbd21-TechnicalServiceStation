using System;
using System.Collections.Generic;
using System.Linq;
using TechnicalServiceStationBusinessLogic.BindingModels;
using TechnicalServiceStationBusinessLogic.Interfaces;
using TechnicalServiceStationBusinessLogic.ViewModels;
using TechnicalServiceStationDatabaseImplement.Models;

namespace TechnicalServiceStationDatabaseImplement.Implements
{
    public class UserLogic : IUserLogic
    {
        public void CreateOrUpdate(UserBindingModel model)
        {
            using (var context = new TechnicalServiceStationDatabase())
            {
                User element = context.Users.FirstOrDefault(rec => rec.Email == model.Email && rec.Id != model.Id);

                if (element != null)
                {
                    throw new Exception("Уже есть пользователь с такой почтой");
                }

                if (model.Id.HasValue)
                {
                    element = context.Users.FirstOrDefault(rec => rec.Id == model.Id);

                    if (element == null)
                    {
                        throw new Exception("Пользователь не найден");
                    }
                }
                else
                {
                    element = new User();
                    context.Users.Add(element);
                }

                element.Email = model.Email;
                element.Password = model.Password;

                context.SaveChanges();
            }
        }

        public void Delete(UserBindingModel model)
        {
            using (var context = new TechnicalServiceStationDatabase())
            {
                User element = context.Users.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.Users.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Пользователь не найден");
                }
            }
        }

        public List<UserViewModel> Read(UserBindingModel model)
        {
            using (var context = new TechnicalServiceStationDatabase())
            {
                return context.Users
                .Where(
                    rec => model == null
                    || rec.Id == model.Id
                    || rec.Email == model.Email && rec.Password == model.Password
                )
                .Select(rec => new UserViewModel
                {
                    Id = rec.Id,
                    Email = rec.Email,
                    Password = rec.Password
                })
                .ToList();
            }
        }
    }
}
