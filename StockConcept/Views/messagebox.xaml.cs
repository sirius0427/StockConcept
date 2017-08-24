using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace StockConcept.Views
{
    /// <summary>
    /// messagebox.xaml 的交互逻辑
    /// </summary>
    public partial class messagebox : Window
    {
            // Fields
        private DispatcherTimer timer_show = new DispatcherTimer();

        // Methods
        public messagebox(string Message)
        {
            this.InitializeComponent();
            this.richtextbox_message.AppendText(Message);
            this.timer_show.Interval = TimeSpan.FromMilliseconds(1);
            this.timer_show.Tick += new EventHandler(this.dispatcherTimer_Tick);
            base.Left = SystemParameters.WorkArea.Width - base.Width;
            base.Top = SystemParameters.PrimaryScreenHeight;
            this.StopTop = SystemParameters.WorkArea.Height - base.Height;
            this.timer_show.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            while (true)
            {
                if (base.Top <= this.StopTop)
                {
                    break;
                }
                base.Top -= 2;
            }
            this.timer_show.Stop();
        }


        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]

        // Properties
        public double StopLeft { get; set; }

        public double StopTop { get; set; }
    }
}
