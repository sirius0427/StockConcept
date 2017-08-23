using Ay.Framework.WPF.MVC;
using System;

namespace StockConcept
{
    public class Startup
    {
        [STAThread]
        static void Main()
        {

             new AYUIApplication<_ViewStart>(new Global()).Run();

        }

    }
}
