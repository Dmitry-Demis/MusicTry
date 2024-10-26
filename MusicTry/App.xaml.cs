using System.Configuration;
using System.Data;
using System.Windows;

namespace MusicTry
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Инициализация базы данных
            using (var context = new MusicContext())
            {
                context.Database.EnsureCreated(); // Проверка и создание базы данных при необходимости
            }
        }
    }

}
