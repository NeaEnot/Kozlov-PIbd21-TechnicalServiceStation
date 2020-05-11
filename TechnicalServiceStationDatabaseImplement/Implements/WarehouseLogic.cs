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
    public class WarehouseLogic : IWarehouseLogic
    {
        public void CreateOrUpdate(WarehouseBindingModel model)
        {
            using (var context = new TechnicalServiceStationDatabase())
            {
                Warehouse element = context.Warehouses.FirstOrDefault(rec => rec.Name == model.Name && rec.Id != model.Id);

                if (element != null)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }

                if (model.Id.HasValue)
                {
                    element = context.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);

                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Warehouse();
                    context.Warehouses.Add(element);
                }

                element.Name = model.Name;

                context.SaveChanges();
            }
        }

        public void Delete(WarehouseBindingModel model)
        {
            using (var context = new TechnicalServiceStationDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.WarehouseAutoparts.RemoveRange(context.WarehouseAutoparts.Where(rec => rec.WarehouseId == model.Id));
                        Warehouse element = context.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);

                        if (element != null)
                        {
                            context.Warehouses.Remove(element);
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

        public List<WarehouseViewModel> Read(WarehouseBindingModel model)
        {
            using (var context = new TechnicalServiceStationDatabase())
            {
                return context.Warehouses
                .Where(rec => model == null || rec.Id == model.Id)
                .ToList()
                .Select(rec => new WarehouseViewModel
                {
                    Id = rec.Id,
                    WarehouseName = rec.Name,
                    WarehouseAutoparts = 
                        context.WarehouseAutoparts
                        .Include(recWA => recWA.Autoparts)
                        .Where(recWA => recWA.WarehouseId == rec.Id)
                        .ToDictionary(recWA => recWA.AutopartsId, recWA => (recWA.Autoparts?.Name, recWA.Count, recWA.Reserved))
                })
                .ToList();
            }
        }
    }
}
