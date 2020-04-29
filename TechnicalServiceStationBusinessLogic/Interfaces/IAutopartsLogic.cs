using System.Collections.Generic;
using TechnicalServiceStationBusinessLogic.BindingModels;
using TechnicalServiceStationBusinessLogic.ViewModels;

namespace TechnicalServiceStationBusinessLogic.Interfaces
{
    public interface IAutopartsLogic
    {
        List<AutopartsViewModel> Read(AutopartsBindingModel model);

        void CreateOrUpdate(AutopartsBindingModel model);

        void Delete(AutopartsBindingModel model);
    }
}
