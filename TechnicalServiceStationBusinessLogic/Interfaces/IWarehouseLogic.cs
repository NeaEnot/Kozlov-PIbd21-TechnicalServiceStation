using System.Collections.Generic;
using TechnicalServiceStationBusinessLogic.BindingModels;
using TechnicalServiceStationBusinessLogic.ViewModels;

namespace TechnicalServiceStationBusinessLogic.Interfaces
{
    public interface IWarehouseLogic
    {
        List<WarehouseViewModel> Read(WarehouseBindingModel model);

        void CreateOrUpdate(WarehouseBindingModel model);

        void Delete(WarehouseBindingModel model);

        void ReserveAutoparts(List<ServiceAutopartsBindingModel> autoparts);

        void WriteOffAutoparts(List<ServiceAutopartsBindingModel> autoparts);
    }
}
