using Ay.Framework.WPF.MVC;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StockConcept.Views
{
    /// <summary>
    /// StartConcept.xaml 的交互逻辑
    /// </summary>
    public partial class StartConcept : Window
    {
        public string dbPath = ("Data Source =" + Environment.CurrentDirectory + "/zyg.db");

        public StartConcept()
        {
            InitializeComponent();
            this.local_program_version.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            SQLiteConnection connection = null;
            SQLiteDataReader reader = null;
            try
            {
                
                connection = new SQLiteConnection(this.dbPath);
                connection.Open();
                reader = new SQLiteCommand("select content from version where id=1", connection).ExecuteReader();

                reader.Read();
                this.local_db_version.Text = reader["content"].ToString();
                reader.Close();
                connection.Close();
            }
            catch (Exception)
            {
                this.local_db_version.Text = "取版本号错";
            }

            MySqlConnection connection1 = new MySqlConnection("Database=zyg;Data Source=sirius0427.jios.org;User Id=cst;Password=q1w2e3r4t5Y^U&I*O(P);pooling=false;CharSet=utf8;port=3307");
            connection1.Open();
            MySqlDataReader reader1 = null;
            try
            {
                reader1 = new MySqlCommand("select content from version order by id", connection1).ExecuteReader();
                reader1.Read();
                this.server_program_version.Text = reader1["content"].ToString();
                reader1.Read();
                this.server_db_version.Text = reader1["content"].ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("取不到数据库版本号");
            }
            finally
            {
                reader1.Close();
                connection1.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //new AYUIApplication<_ViewStart>(new Global()).Run();
            _ViewStart ViewStart = new _ViewStart();
            ViewStart.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void program_update_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection("Database=zyg;Data Source=sirius0427.jios.org;User Id=cst;Password=q1w2e3r4t5Y^U&I*O(P);pooling=false;CharSet=utf8;port=3307");
            connection.Open();
            MySqlDataReader reader = new MySqlCommand("select content from version where id=1", connection).ExecuteReader();
            try
            {
                reader.Read();
                this.server_program_version.Text = reader["content"].ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("取不到数据库版本号");
            }
            finally
            {
                reader.Close();
                connection.Close();
            }
            if (this.server_program_version.Text.Equals(this.local_program_version.Text))
            {
                MessageBox.Show("您的版本已是最新版，无需升级");
                //this.program_update_notice.Visibility = Visibility.Visible;
            }
            else
            {
                if (MessageBox.Show("点击确定按键开始升级程序", "程序升级", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    Process.Start("ConceptUpdate.exe");
                    this.Dispatcher.Invoke(() => Application.Current.Shutdown());
                }
                //connection.Open();
                //reader = new MySqlCommand("select content from version where id=3", connection).ExecuteReader();
                //try
                //{
                //    reader.Read();
                //    string str = reader["content"].ToString();
                //    this.program_update_label.Visibility = Visibility.Visible;
                //    this.program_update_ProcessBar.Visibility = Visibility.Visible;
                //    this.program_update.IsEnabled = false;
                //    if (this.HttpFileExist(str))
                //    {
                //        this.DownloadHttpFile_program(str, "setup.exe");
                //    }
                //}
                //catch (Exception)
                //{
                //    MessageBox.Show("下载文件出错");
                //}
                //finally
                //{
                //    reader.Close();
                //    connection.Close();
                //}
            }

        }

        private void database_update_Click(object sender, RoutedEventArgs e)
        {
            if (this.server_db_version.Text.Equals(this.local_db_version.Text))
            {
                MessageBox.Show("您的版本已是最新版，无需升级");
                //this.db_update_notice.Visibility = Visibility.Visible;
            }
            else
            {
                if (MessageBox.Show("点击确定按键开始升级数据库", "数据库升级", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    Process.Start("DBUpdate.exe");
                    this.Dispatcher.Invoke(() => Application.Current.Shutdown());
                }
                //MySqlConnection connection = new MySqlConnection("Database=zyg;Data Source=sirius0427.jios.org;User Id=cst;Password=q1w2e3r4t5Y^U&I*O(P);pooling=false;CharSet=utf8;port=3307");
                //connection.Open();
                //MySqlDataReader reader = new MySqlCommand("select content from version where id=4", connection).ExecuteReader();
                //try
                //{
                //    reader.Read();
                //    string str = reader["content"].ToString();
                //    this.db_update_label.Visibility = Visibility.Visible;
                //    this.db_update_ProcessBar.Visibility = Visibility.Visible;
                //    this.database_update.IsEnabled = false;
                //    reader.Close();
                //    connection.Close();
                //    if (this.HttpFileExist(str))
                //    {
                //        this.DownloadHttpFile_db(str, "zyg.db");
                //    }
                //}
                //catch (Exception)
                //{
                //    MessageBox.Show("下载文件出错");
                //}
                //finally
                //{
                //    reader.Close();
                //    connection.Close();
                //}
            }

        }

        //private bool HttpFileExist(string http_file_url)
        //{
        //    WebResponse response = null;
        //    bool flag = false;
        //    try
        //    {
        //        response = WebRequest.Create(http_file_url).GetResponse();
        //        flag = response != null;
        //    }
        //    catch (Exception)
        //    {
        //        flag = false;
        //    }
        //    finally
        //    {
        //        if (response != null)
        //        {
        //            response.Close();
        //        }
        //    }
        //    return flag;
        //}

        //public void DownloadHttpFile_program(string http_url, string save_url)
        //{
        //    WebResponse response = null;
        //    response = WebRequest.Create(http_url).GetResponse();
        //    if (response != null)
        //    {
        //        this.program_update_ProcessBar.Maximum = response.ContentLength;
        //        ThreadPool.QueueUserWorkItem(delegate (object obj)
        //        {
        //            Stream responseStream = response.GetResponseStream();
        //            Stream stream2 = new FileStream(save_url, FileMode.Create);
        //            byte[] buffer = new byte[0x400];
        //            long num = 0L;
        //            for (int j = responseStream.Read(buffer, 0, buffer.Length); j > 0; j = responseStream.Read(buffer, 0, buffer.Length))
        //            {
        //                stream2.Write(buffer, 0, j);
        //                num += j;
        //                object[] args = new object[] { num };
        //                this.program_update_ProcessBar.Dispatcher.BeginInvoke(new ProgressBarSetter(this.SetProgressBar_program), args);
        //            }
        //            responseStream.Close();
        //            stream2.Close();
        //            //this.close_update_button.Dispatcher.Invoke(() => this.close_update_button.Visibility = Visibility.Visible);
        //            this.program_update.Dispatcher.Invoke(() => this.program_update.Visibility = Visibility.Collapsed);
        //            this.program_update_label.Dispatcher.Invoke(() => this.program_update_label.Content = "升级完成");
        //        }, null);
        //    }
        //}

        //public void DownloadHttpFile_db(string http_url, string save_url)
        //{
        //    WebResponse response = null;
        //    response = WebRequest.Create(http_url).GetResponse();
        //    if (response != null)
        //    {
        //        this.db_update_ProcessBar.Maximum = response.ContentLength;
        //        ThreadPool.QueueUserWorkItem(delegate (object obj) {
        //            Stream responseStream = response.GetResponseStream();
        //            Stream stream2 = new FileStream(save_url, FileMode.Create);
        //            byte[] buffer = new byte[0x400];
        //            long num = 0L;
        //            for (int j = responseStream.Read(buffer, 0, buffer.Length); j > 0; j = responseStream.Read(buffer, 0, buffer.Length))
        //            {
        //                stream2.Write(buffer, 0, j);
        //                num += j;
        //                object[] args = new object[] { num };
        //                this.db_update_ProcessBar.Dispatcher.BeginInvoke(new ProgressBarSetter(this.SetProgressBar_db), args);
        //            }
        //            responseStream.Close();
        //            stream2.Close();
        //            this.database_update.Dispatcher.Invoke(() => this.database_update.IsEnabled = true);
        //            this.db_update_label.Dispatcher.Invoke(() => this.db_update_label.Content = "升级完成");
        //        }, null);
        //    }
        //}


        //public delegate void ProgressBarSetter(double value);

        //public void SetProgressBar_program(double value)
        //{
        //    this.program_update_ProcessBar.Value = value;
        //    this.program_update_label.Content = ((int)((value / this.program_update_ProcessBar.Maximum) * 100.0)) + "%";
        //}

        //public void SetProgressBar_db(double value)
        //{
        //    this.db_update_ProcessBar.Value = value;
        //    this.db_update_label.Content = ((int)((value / this.db_update_ProcessBar.Maximum) * 100.0)) + "%";
        //    if (this.db_update_label.Content.ToString() == "100%")
        //    {
        //        this.db_update_label.Visibility = Visibility.Collapsed;
        //        this.db_update_ProcessBar.Visibility = Visibility.Collapsed;
        //        this.database_update.IsEnabled = true;
        //    }
        //}





    }
}
