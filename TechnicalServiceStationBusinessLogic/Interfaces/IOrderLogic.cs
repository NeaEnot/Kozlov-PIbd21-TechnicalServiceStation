using System.Collections.Generic;
using TechnicalServiceStationBusinessLogic.BindingModels;
using TechnicalServiceStationBusinessLogic.ViewModels;

namespace TechnicalServiceStationBusinessLogic.Interfaces
{
    public interface IOrderLogic
    {
        List<OrderViewModel> Read(OrderBindingModel model);

        void CreateOrUpdate(OrderBindingModel model);

        void Delete(OrderBindingModel model);
    }
}
