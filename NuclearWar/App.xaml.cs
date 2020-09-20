using Autofac;
using NuclearWar.Controls;
using NuclearWar.Farm;
using NuclearWar.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NuclearWar
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ContainerBuilder();
            builder.RegisterType<UIHelper>();
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<MainWindow>();
            builder.RegisterType<HtmlAgilityPackWindow>();
            var container = builder.Build();
            //container.Resolve<MainWindow>().Show();
            container.Resolve<HtmlAgilityPackWindow>().Show();
        }
    }
}
