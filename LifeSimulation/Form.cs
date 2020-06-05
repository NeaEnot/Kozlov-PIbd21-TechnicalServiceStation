using System;
using System.Linq;
using System.Threading;
using TechnicalServiceStationBusinessLogic.BindingModels;
using TechnicalServiceStationBusinessLogic.BusinessLogic;
using TechnicalServiceStationBusinessLogic.Enums;
using TechnicalServiceStationBusinessLogic.Interfaces;

namespace LifeSimulation
{
    public partial class Form : System.Windows.Forms.Form
    {
        private MainLogic mainLogic;
        private IOrderLogic orderLogic;
        private IWarehouseLogic warehouseLogic;
        private IAutopartsLogic autopartsLogic;

        public Form(MainLogic mainLogic, IOrderLogic orderLogic, IWarehouseLogic warehouseLogic, IAutopartsLogic autopartsLogic)
        {
            this.mainLogic = mainLogic;
            this.orderLogic = orderLogic;
            this.warehouseLogic = warehouseLogic;
            this.autopartsLogic = autopartsLogic;

            InitializeComponent();

            while (true)
            {
                DoWork();
                Thread.Sleep(30000);
            }
        }

        private void DoWork()
        {
            try
            {
                var orders = orderLogic.Read(null).Where(rec => rec.Status != OrderStatus.Принят && rec.Status != OrderStatus.Оплачен).ToList();

                if (orders != null)
                {
                    Random rnd = new Random();
                    var order = orders[rnd.Next(0, orders.Count)];

                    switch (order.Status)
                    {
                        case OrderStatus.Зарезервирован:
                            mainLogic.TakeOrderInWork(new OrderBindingModel { Id = order.Id });
                            break;
                        case OrderStatus.Выполняется:
                            mainLogic.FinishOrder(new OrderBindingModel { Id = order.Id });
                            break;
                        case OrderStatus.Готов:
                            mainLogic.PayOrder(new OrderBindingModel { Id = order.Id });

                            int cash = (int)(order.Price * 1.5);

                            var warehouses = warehouseLogic.Read(null);
                            var warehouse = warehouses[rnd.Next(0, warehouses.Count)];

                            int waId = warehouse.WarehouseAutoparts.Keys.ToArray()[rnd.Next(0, warehouse.WarehouseAutoparts.Keys.Count)];
                            var wa = warehouse.WarehouseAutoparts[waId];
                            var autoparts = autopartsLogic.Read(new AutopartsBindingModel { Id = waId })[0];

                            wa.Item2 += cash / autoparts.Price;
                            warehouseLogic.CreateOrUpdate(new WarehouseBindingModel
                            {
                                Id = warehouse.Id,
                                Name = warehouse.Name,
                                WarehouseAutoparts = warehouse.WarehouseAutoparts
                            });

                            break;
                    }
                }
            }
            catch
            {

            }
        }
    }
}
