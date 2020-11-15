using System.Windows;

using HrvAnalyzer.UI.Services;
using HrvAnalyzer.UI.ViewModels;
using HrvAnalyzer.UI.Views;

using Prism.Ioc;
using Prism.Unity;

namespace HrvAnalyzer.UI
{
    public partial class App : PrismApplication
    {
        /// <summary>
        /// Регистрирует все зависимости приложения
        /// </summary>
        /// <param name="containerRegistry">Регистрирующий контейнер</param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IOpenFileService, OpenFileService>();
            containerRegistry.Register<IFileDetailViewModel, FileDetailViewModel>();
        }

        /// <summary>
        /// Создает главное окно приложения
        /// </summary>
        /// <returns>Главное окно приложения</returns>
        protected override Window CreateShell()
        {
            var window = Container.Resolve<MainView>();

            return window;
        }
    }
}
