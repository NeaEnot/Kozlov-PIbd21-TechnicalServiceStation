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
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
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

                        foreach (var wa in model.WarehouseAutoparts)
                        {
                            var warehouseAutoparts = context.WarehouseAutoparts.FirstOrDefault(rec => rec.Id == wa.Key);

                            if (warehouseAutoparts != null)
                            {
                                warehouseAutoparts.Count = wa.Value.Item2;
                                warehouseAutoparts.Reserved = wa.Value.Item3;
                            }
                            else
                            {
                                context.WarehouseAutoparts.Add(new WarehouseAutoparts
                                {
                                    Count = wa.Value.Item2,
                                    Reserved = wa.Value.Item3,
                                    AutopartsId = wa.Key,
                                    WarehouseId = element.Id
                                });
                            }

                            context.SaveChanges();
                        }

                        context.SaveChanges();
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
                    Name = rec.Name,
                    WarehouseAutoparts =
                        context.WarehouseAutoparts
                        .Include(recWA => recWA.Autoparts)
                        .Where(recWA => recWA.WarehouseId == rec.Id)
                        .ToDictionary(recWA => recWA.AutopartsId, recWA => (recWA.Autoparts?.Name, recWA.Count, recWA.Reserved))
                })
                .ToList();
            }
        }

        public void ReserveAutoparts(List<ServiceAutopartsBindingModel> autoparts)
        {
            using (var context = new TechnicalServiceStationDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var autopart in autoparts)
                        {
                            foreach (var wa in context.WarehouseAutoparts)
                            {
                                if (wa.Count > 0)
                                {
                                    int debited = Math.Min(wa.Count, autopart.Count);
                                    wa.Count -= debited;
                                    wa.Reserved += debited;
                                    autopart.Count -= debited;
                                }
                            }

                            if (autopart.Count > 0)
                            {
                                throw new Exception("На складах недостаточно деталей");
                            }
                            
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

        public void WriteOffAutoparts(List<ServiceAutopartsBindingModel> autoparts)
        {
            using (var context = new TechnicalServiceStationDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var autopart in autoparts)
                        {
                            foreach (var wa in context.WarehouseAutoparts)
                            {
                                if (wa.Count > 0)
                                {
                                    int debited = Math.Min(wa.Reserved, autopart.Count);
                                    wa.Reserved -= debited;
                                    autopart.Count -= debited;
                                }
                            }

                            if (autopart.Count > 0)
                            {
                                throw new Exception("Зарезервировано недостаточно деталей");
                            }

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
    }
}
