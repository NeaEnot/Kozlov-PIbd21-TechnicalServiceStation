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
    public class ServiceLogic : IServiceLogic
    {
        public void CreateOrUpdate(ServiceBindingModel model)
        {
            using (var context = new TechnicalServiceStationDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Service element = context.Services.FirstOrDefault(rec => rec.Name == model.Name && rec.Id != model.Id);

                        if (element != null)
                        {
                            throw new Exception("Уже есть элемент с таким названием");
                        }

                        if (model.Id.HasValue)
                        {
                            element = context.Services.FirstOrDefault(rec => rec.Id == model.Id);

                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                        }
                        else
                        {
                            element = new Service();
                            context.Services.Add(element);
                        }

                        element.Name = model.Name;
                        element.Price = model.Price;

                        context.SaveChanges();

                        if (model.Id.HasValue)
                        {
                            var serviceAutoparts = context.ServiceAutoparts.Where(rec => rec.ServiceId == model.Id.Value).ToList();
                            context.ServiceAutoparts.RemoveRange(serviceAutoparts.Where(rec => !model.ServiceAutoparts.ContainsKey(rec.AutopartsId)).ToList());

                            context.SaveChanges();

                            foreach (var updateAutoparts in serviceAutoparts)
                            {
                                updateAutoparts.Count = model.ServiceAutoparts[updateAutoparts.AutopartsId].Item2;

                                model.ServiceAutoparts.Remove(updateAutoparts.AutopartsId);
                            }

                            context.SaveChanges();
                        }

                        foreach (var sa in model.ServiceAutoparts)
                        {
                            context.ServiceAutoparts.Add(new ServiceAutoparts
                            {
                                ServiceId = element.Id,
                                AutopartsId = sa.Key,
                                Count = sa.Value.Item2
                            });

                            context.SaveChanges();
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(ServiceBindingModel model)
        {
            using (var context = new TechnicalServiceStationDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.ServiceAutoparts.RemoveRange(context.ServiceAutoparts.Where(rec => rec.ServiceId == model.Id));
                        Service element = context.Services.FirstOrDefault(rec => rec.Id == model.Id);

                        if (element != null)
                        {
                            context.Services.Remove(element);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<ServiceViewModel> Read(ServiceBindingModel model)
        {
            using (var context = new TechnicalServiceStationDatabase())
            {
                return context.Services
                .Where(rec => model == null || rec.Id == model.Id)
                .ToList()
                .Select(rec => new ServiceViewModel
                {
                    Id = rec.Id,
                    Name = rec.Name,
                    Price = rec.Price,
                    ServiceAutoparts = 
                        context.ServiceAutoparts
                        .Include(recSA => recSA.Autoparts)
                        .Where(recSA => recSA.ServiceId == rec.Id)
                        .ToDictionary(recSA => recSA.AutopartsId, recSA => (recSA.Autoparts?.Name, recSA.Count, recSA.Autoparts.Price * recSA.Count))
                })
                .ToList();
            }
        }
    }
}