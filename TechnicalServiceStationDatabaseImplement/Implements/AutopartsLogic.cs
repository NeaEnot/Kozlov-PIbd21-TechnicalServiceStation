using System;
using System.Collections.Generic;
using System.Linq;
using TechnicalServiceStationBusinessLogic.BindingModels;
using TechnicalServiceStationBusinessLogic.Interfaces;
using TechnicalServiceStationBusinessLogic.ViewModels;
using TechnicalServiceStationDatabaseImplement.Models;

namespace TechnicalServiceStationDatabaseImplement.Implements
{
    public class AutopartsLogic : IAutopartsLogic
    {
        public void CreateOrUpdate(AutopartsBindingModel model)
        {
            using (var context = new TechnicalServiceStationDatabase())
            {
                Autoparts element = context.Autoparts.FirstOrDefault(rec => rec.Name == model.Name && rec.Id != model.Id);

                if (element != null)
                {
                    throw new Exception("Уже есть запчасти с таким названием");
                }

                if (model.Id.HasValue)
                {
                    element = context.Autoparts.FirstOrDefault(rec => rec.Id == model.Id);

                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Autoparts();
                    context.Autoparts.Add(element);
                }

                element.Name = model.Name;
                element.Price = model.Price;

                context.SaveChanges();
            }
        }

        public void Delete(AutopartsBindingModel model)
        {
            using (var context = new TechnicalServiceStationDatabase())
            {
                Autoparts element = context.Autoparts.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.Autoparts.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<AutopartsViewModel> Read(AutopartsBindingModel model)
        {
            using (var context = new TechnicalServiceStationDatabase())
            {
                return context.Autoparts
                .Where(rec => model == null || rec.Id == model.Id)
                .Select(rec => new AutopartsViewModel
                {
                    Id = rec.Id,
                    Name = rec.Name,
                    Price = rec.Price
                })
                .ToList();
            }
        }
    }
}
