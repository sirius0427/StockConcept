using System.Windows;
using Ay.Framework.WPF.MVC;
using System;

namespace StockConcept
{
    public class Global : AYUIGlobal
    {
        public override void Application_Start(StartupEventArgs e, Application appliation)
        {
            appliation.AddExceptionSimply();
        }
        public override void RegisterResourceDictionary(ClientResourceDictionaryCollection resources)
        {
            resources.Add(AyExtension.CreateResourceDictionary("Contents/Styles/AYUIProjectDictionary.xaml"));
        }


    }

}

