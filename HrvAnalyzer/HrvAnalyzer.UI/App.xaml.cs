using System.Windows;

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
        }

        /// <summary>
        /// Создает главное окно приложения
        /// </summary>
        /// <returns>Главное окно приложения</returns>
        protected override Window CreateShell()
        {
            var window = Container.Resolve<MainWindow>();

            return window;
        }
    }
}
