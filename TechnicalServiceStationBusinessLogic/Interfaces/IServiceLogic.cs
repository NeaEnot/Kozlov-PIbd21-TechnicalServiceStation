using System.Collections.Generic;
using TechnicalServiceStationBusinessLogic.BindingModels;
using TechnicalServiceStationBusinessLogic.ViewModels;

namespace TechnicalServiceStationBusinessLogic.Interfaces
{
    public interface IServiceLogic
    {
        List<ServiceViewModel> Read(ServiceBindingModel model);

        void CreateOrUpdate(ServiceBindingModel model);

        void Delete(ServiceBindingModel model);
    }
}
