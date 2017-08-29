using System;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace DBUpdate
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MySqlConnection connection = new MySqlConnection("Database=zyg;Data Source=sirius0427.jios.org;User Id=cst;Password=q1w2e3r4t5Y^U&I*O(P);pooling=false;CharSet=utf8;port=3307");
            connection.Open();
            MySqlDataReader reader = new MySqlCommand("select content from version where id=4", connection).ExecuteReader();
            try
            {
                reader.Read();
                string str = reader["content"].ToString();

                reader.Close();
                connection.Close();
                if (this.HttpFileExist(str))
                {
                    this.DownloadHttpFile_program(str, "zyg.db");
                }

            }
            catch
            {
                MessageBox.Show("下载文件出错");
            }
            finally
            {

            }

        }

        private bool HttpFileExist(string http_file_url)
        {
            WebResponse response = null;
            bool flag = false;
            try
            {
                response = WebRequest.Create(http_file_url).GetResponse();
                flag = response != null;
            }
            catch (Exception)
            {
                flag = false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return flag;
        }

        public void DownloadHttpFile_program(string http_url, string save_url)
        {
            WebResponse response = null;
            response = WebRequest.Create(http_url).GetResponse();
            if (response != null)
            {
                this.program_update_ProcessBar.Maximum = response.ContentLength;
                ThreadPool.QueueUserWorkItem(delegate (object obj)
                {
                    Stream responseStream = response.GetResponseStream();
                    Stream stream2 = new FileStream(save_url, FileMode.Create);
                    byte[] buffer = new byte[0x400];
                    long num = 0L;
                    for (int j = responseStream.Read(buffer, 0, buffer.Length); j > 0; j = responseStream.Read(buffer, 0, buffer.Length))
                    {
                        stream2.Write(buffer, 0, j);
                        num += j;
                        object[] args = new object[] { num };
                        this.program_update_ProcessBar.Dispatcher.BeginInvoke(new ProgressBarSetter(this.SetProgressBar_program), args);
                    }
                    responseStream.Close();
                    stream2.Close();
                    //this.close_update_button.Dispatcher.Invoke(() => this.close_update_button.Visibility = Visibility.Visible);
                    this.program_update_label.Dispatcher.Invoke(() => this.program_update_label.Content = "升级完成");
                    Process.Start("StockConcept.exe");
                    this.Dispatcher.Invoke(() => Application.Current.Shutdown());
                    //Application.Current.Shutdown();
                }, null);
            }
        }

        public delegate void ProgressBarSetter(double value);

        public void SetProgressBar_program(double value)
        {
            this.program_update_ProcessBar.Value = value;
            this.program_update_label.Content = ((int)((value / this.program_update_ProcessBar.Maximum) * 100.0)) + "%";
        }
    }
}
