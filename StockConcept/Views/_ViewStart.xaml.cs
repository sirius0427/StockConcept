using Ay.Framework.WPF.Controls;
using CefSharp;
using MySql.Data.MySqlClient;
using StockConcept.Views;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Media;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace StockConcept
{
    /// <summary>
    /// _ViewStart.xaml 的交互逻辑
    /// </summary>
    /// 

    public partial class _ViewStart : AyWindow
    {
        public string dbPath = "Data Source =" + Environment.CurrentDirectory + "/zyg.db";
        private Timer hudong_Thread_Time;
        private long hudong_lasttime;

        public _ViewStart()
        {
            InitializeComponent();

            index.Visibility = Visibility.Visible;
            F10ticai.Visibility = Visibility.Collapsed;
            gudong.Visibility = Visibility.Collapsed;
            xingwen.Visibility = Visibility.Collapsed;
            hudong.Visibility = Visibility.Collapsed;
            shengji.Visibility = Visibility.Collapsed;

        }

        private void ZygUrl_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.zhuayaogu.com/");
        }

        private void 首页_Click(object sender, RoutedEventArgs e)
        {
            index.Visibility = Visibility.Visible;
            F10ticai.Visibility = Visibility.Collapsed;
            gudong.Visibility = Visibility.Collapsed;
            xingwen.Visibility = Visibility.Collapsed;
            hudong.Visibility = Visibility.Collapsed;
            shengji.Visibility = Visibility.Collapsed;
            nothing.Visibility = Visibility.Collapsed;
        }

        private void F10题材搜索_Click(object sender, RoutedEventArgs e)
        {
            index.Visibility = Visibility.Collapsed;
            F10ticai.Visibility = Visibility.Visible;
            gudong.Visibility = Visibility.Collapsed;
            xingwen.Visibility = Visibility.Collapsed;
            hudong.Visibility = Visibility.Collapsed;
            shengji.Visibility = Visibility.Collapsed;
            nothing.Visibility = Visibility.Collapsed;

            if (listBox_ticai.Items.Count == 0)
            {
                F10ticai.Visibility = Visibility.Collapsed;
                nothing.Visibility = Visibility.Visible;
            }
        }

        private void 股东搜索_Click(object sender, RoutedEventArgs e)
        {
            index.Visibility = Visibility.Collapsed;
            F10ticai.Visibility = Visibility.Collapsed;
            gudong.Visibility = Visibility.Visible;
            xingwen.Visibility = Visibility.Collapsed;
            hudong.Visibility = Visibility.Collapsed;
            shengji.Visibility = Visibility.Collapsed;
            nothing.Visibility = Visibility.Collapsed;

            if (listBox_gudong.Items.Count == 0)
            {
                gudong.Visibility = Visibility.Collapsed;
                nothing.Visibility = Visibility.Visible;
            }
        }

        private void 新闻搜索_Click(object sender, RoutedEventArgs e)
        {
            index.Visibility = Visibility.Collapsed;
            F10ticai.Visibility = Visibility.Collapsed;
            gudong.Visibility = Visibility.Collapsed;
            xingwen.Visibility = Visibility.Visible;
            hudong.Visibility = Visibility.Collapsed;
            shengji.Visibility = Visibility.Collapsed;
            nothing.Visibility = Visibility.Collapsed;

            if (ListView_xinwen.Items.Count == 0)
            {
                xingwen.Visibility = Visibility.Collapsed;
                nothing.Visibility = Visibility.Visible;
            }
        }

        private void 互动搜索_Click(object sender, RoutedEventArgs e)
        {
            index.Visibility = Visibility.Collapsed;
            F10ticai.Visibility = Visibility.Collapsed;
            gudong.Visibility = Visibility.Collapsed;
            xingwen.Visibility = Visibility.Collapsed;
            hudong.Visibility = Visibility.Visible;
            shengji.Visibility = Visibility.Collapsed;
            nothing.Visibility = Visibility.Collapsed;
            KeyWord.Visibility = Visibility.Visible;
            search_hudong(sender, e);

        }

        private void 软件升级_Click(object sender, RoutedEventArgs e)
        {
            index.Visibility = Visibility.Collapsed;
            F10ticai.Visibility = Visibility.Collapsed;
            gudong.Visibility = Visibility.Collapsed;
            xingwen.Visibility = Visibility.Collapsed;
            hudong.Visibility = Visibility.Collapsed;
            shengji.Visibility = Visibility.Visible;
            nothing.Visibility = Visibility.Collapsed;
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox_ticai.SelectedIndex != -1)
            {
                SQLiteConnection conn = null;

                conn = new SQLiteConnection(dbPath);//创建数据库实例，指定文件位置  
                conn.Open();//打开数据库，若文件不存在会自动创建  

                //string sql = "SELECT point FROM dfcf_concept where sourcename = '" + listBox_ticai.SelectedItem.ToString() + "' and point like '%" + KeyWord.Text + "%' order by id";
                string sql = "SELECT point FROM dfcf_concept where sourcename = '" + listBox_ticai.SelectedItem.ToString() + "' order by id";
                SQLiteCommand cmdQ = new SQLiteCommand(sql, conn);

                SQLiteDataReader reader = cmdQ.ExecuteReader();

                richTextBox_ticai.Document.Blocks.Clear();

                if (reader.HasRows)
                {
                    richTextBox_ticai.AppendText("东财概念：");
                    richTextBox_ticai.AppendText(Environment.NewLine);
                }
                // 读取所有数据，添加到集合中.
                while (reader.Read())
                {
                    richTextBox_ticai.AppendText(reader["point"].ToString());
                    richTextBox_ticai.AppendText(Environment.NewLine);
                }

                //sql = "SELECT point FROM ths_concept where sourcename = '" + listBox_ticai.SelectedItem.ToString() + "' and point like '%" + KeyWord.Text + "%' order by id";
                sql = "SELECT point FROM ths_concept where sourcename = '" + listBox_ticai.SelectedItem.ToString() + "' order by id";
                cmdQ = new SQLiteCommand(sql, conn);
                reader = cmdQ.ExecuteReader();

                if (reader.HasRows)
                {
                    richTextBox_ticai.AppendText(Environment.NewLine + "同花顺概念：" + Environment.NewLine);
                }
                while (reader.Read())
                {
                    richTextBox_ticai.AppendText(reader["point"].ToString());
                    richTextBox_ticai.AppendText(Environment.NewLine);
                }
                reader.Close();

                ChangeColor(Color.FromRgb(0, 0, 255), richTextBox_ticai, "东财概念：");
                ChangeColor(Color.FromRgb(0, 0, 255), richTextBox_ticai, "同花顺概念：");
                if (KeyWord.Text.Length > 0)
                {
                    ChangeColor(Color.FromRgb(255, 0, 0), richTextBox_ticai, KeyWord.Text);
                }
                conn.Close();
            }
        }

        public TextPointer selecta(Color l, RichTextBox richTextBox1, int selectLength, TextPointer tpStart, TextPointer tpEnd)
        {
            TextRange range = richTextBox1.Selection;
            range.Select(tpStart, tpEnd);
            //高亮选择         

            range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(l));
            range.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);

            return tpEnd.GetNextContextPosition(LogicalDirection.Forward);
        }

        public void ChangeColor(Color l, RichTextBox richBox, string keyword)
        {
            //设置文字指针为Document初始位置           
            //richBox.Document.FlowDirection           
            TextPointer position = richBox.Document.ContentStart;
            while (position != null)
            {
                //向前搜索,需要内容为Text       
                if (position.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    //拿出Run的Text        
                    string text = position.GetTextInRun(LogicalDirection.Forward);
                    //可能包含多个keyword,做遍历查找           
                    int index = 0;
                    index = text.IndexOf(keyword, 0);
                    if (index != -1)
                    {
                        TextPointer start = position.GetPositionAtOffset(index);
                        TextPointer end = start.GetPositionAtOffset(keyword.Length);
                        position = selecta(l, richBox, keyword.Length, start, end);
                    }
                }
                //文字指针向前偏移   
                position = position.GetNextContextPosition(LogicalDirection.Forward);

            }
        }

        private void KeyWord_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                if (Equals(KeyWord.Text, ""))
                {
                    MessageBox.Show("请在输入框输入需要查询的关键词，否则可能会有太多结果");
                }
                else
                {
                    begin_search(sender, e);
                }

            }
        }

        private void begin_search(object sender, RoutedEventArgs e)
        {
            if (this.F10题材搜索.IsChecked.Value)
            {
                this.search_F10(sender, e);
                F10ticai.Visibility = Visibility.Visible;
                nothing.Visibility = Visibility.Collapsed;
            }
            else if (this.股东搜索.IsChecked.Value)
            {
                this.search_gudong(sender, e);
                gudong.Visibility = Visibility.Visible;
                nothing.Visibility = Visibility.Collapsed;
            }
            else if (this.新闻搜索.IsChecked.Value)
            {
                this.search_xinwen(sender, e);
                xingwen.Visibility = Visibility.Visible;
                nothing.Visibility = Visibility.Collapsed;
            }
            else if (this.互动搜索.IsChecked.Value)
            {
                this.search_hudong(sender, e);
                hudong.Visibility = Visibility.Visible;
                nothing.Visibility = Visibility.Collapsed;
            }
        }

        private void search_F10(object sender, RoutedEventArgs e)
        {
            int Count1 = 0;

            SQLiteConnection conn = null;

            conn = new SQLiteConnection(dbPath);//创建数据库实例，指定文件位置  
            conn.Open();//打开数据库，若文件不存在会自动创建  

            //string sql = "select distinct(sourcename) from (SELECT id,sourcename FROM dfcf_concept where point like '%" + KeyWord.Text + "%' union all SELECT id,sourcename FROM ths_concept where point like '%" + KeyWord.Text + "%') order by id";
            string sql = "select distinct(sourcename) from (SELECT id,sourcename FROM dfcf_concept where sourcename like '%" + KeyWord.Text + "%' union all SELECT id,sourcename FROM ths_concept where sourcename like '%" + KeyWord.Text + "%') order by id";
            SQLiteCommand cmdQ = new SQLiteCommand(sql, conn);

            SQLiteDataReader reader = cmdQ.ExecuteReader();

            // 清空listBox列表
            listBox_ticai.Items.Clear();
            // 读取所有数据，添加到集合中
            while (reader.Read())
            {
                listBox_ticai.Items.Add(reader["sourcename"]);
                Count1++;
            }
            reader.Close();

            sql = "select distinct(sourcename) from (SELECT id,sourcename FROM dfcf_concept where point like '%" + KeyWord.Text + "%' union all SELECT id,sourcename FROM ths_concept where point like '%" + KeyWord.Text + "%') order by id";
            cmdQ = new SQLiteCommand(sql, conn);

            reader = cmdQ.ExecuteReader();

            // 读取所有数据，添加到集合中
            while (reader.Read())
            {
                listBox_ticai.Items.Add(reader["sourcename"]);
                Count1++;
            }
            reader.Close();

            if (Count1 > 0)
            {
                listBox_ticai.Focus();
                listBox_ticai.SelectedIndex = 0;
            }
            if (KeyWord.Text.Length > 0)
            {
                ChangeColor(Color.FromRgb(255, 0, 0), richTextBox_ticai, KeyWord.Text);
            }
            conn.Close();
        }

        private void search_gudong(object sender, RoutedEventArgs e)
        {
            int Count1 = 0;

            SQLiteConnection conn = null;

            conn = new SQLiteConnection(dbPath);//创建数据库实例，指定文件位置  
            conn.Open();//打开数据库，若文件不存在会自动创建  

            string sql = "select distinct(sourcename) from holding where name like '%" + KeyWord.Text + "%' order by id";
            SQLiteCommand cmdQ = new SQLiteCommand(sql, conn);

            SQLiteDataReader reader = cmdQ.ExecuteReader();

            // 清空listBox列表
            listBox_gudong.Items.Clear();
            // 读取所有数据，添加到集合中
            while (reader.Read())
            {
                listBox_gudong.Items.Add(reader["sourcename"]);
                Count1++;
            }
            reader.Close();
            if (Count1 > 0)
            {
                listBox_gudong.Focus();
                listBox_gudong.SelectedIndex = 0;
            }
            if (KeyWord.Text.Length > 0)
            {
                ChangeColor(Color.FromRgb(255, 0, 0), richTextBox_gudong, KeyWord.Text);
            }
            conn.Close();
        }

        private void listBox_gudong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox_gudong.SelectedIndex != -1)
            {
                SQLiteConnection conn = null;

                conn = new SQLiteConnection(dbPath);//创建数据库实例，指定文件位置  
                conn.Open();//打开数据库，若文件不存在会自动创建  

                string sql = "SELECT name FROM holding where sourcename = '" + listBox_gudong.SelectedItem.ToString() + "' and style=1 order by id";
                SQLiteCommand cmdQ = new SQLiteCommand(sql, conn);

                SQLiteDataReader reader = cmdQ.ExecuteReader();

                richTextBox_gudong.Document.Blocks.Clear();

                if (reader.HasRows)
                {
                    richTextBox_gudong.AppendText("参股控股公司:");
                    richTextBox_gudong.AppendText(Environment.NewLine);
                }
                // 读取所有数据，添加到集合中.
                while (reader.Read())
                {
                    richTextBox_gudong.AppendText(reader["name"].ToString());
                    richTextBox_gudong.AppendText(Environment.NewLine);
                }
                reader.Close();


                sql = "SELECT name FROM holding where sourcename = '" + listBox_gudong.SelectedItem.ToString() + "' and style=2 order by id";
                cmdQ = new SQLiteCommand(sql, conn);
                reader = cmdQ.ExecuteReader();

                if (reader.HasRows)
                {
                    richTextBox_gudong.AppendText("十大流通股东:" + Environment.NewLine);
                }
                while (reader.Read())
                {
                    richTextBox_gudong.AppendText(reader["name"].ToString());
                    richTextBox_gudong.AppendText(Environment.NewLine);
                }
                reader.Close();

                sql = "SELECT name FROM holding where sourcename = '" + listBox_gudong.SelectedItem.ToString() + "' and style=3 order by id";
                cmdQ = new SQLiteCommand(sql, conn);
                reader = cmdQ.ExecuteReader();

                if (reader.HasRows)
                {
                    richTextBox_gudong.AppendText("十大股东:" + Environment.NewLine);
                }
                while (reader.Read())
                {
                    richTextBox_gudong.AppendText(reader["name"].ToString());
                    richTextBox_gudong.AppendText(Environment.NewLine);
                }
                reader.Close();

                ChangeColor(Color.FromRgb(0, 0, 255), richTextBox_gudong, "参股控股公司:");
                ChangeColor(Color.FromRgb(0, 0, 255), richTextBox_gudong, "十大流通股东:");
                ChangeColor(Color.FromRgb(0, 0, 255), richTextBox_gudong, "十大股东:");
                if (KeyWord.Text.Length > 0)
                {
                    ChangeColor(Color.FromRgb(255, 0, 0), richTextBox_gudong, KeyWord.Text);
                }
                conn.Close();
            }
        }

        private void search_xinwen(object sender, RoutedEventArgs e)
        {
            if (Equals(KeyWord.Text, ""))
            {
                MessageBox.Show("请在输入框输入需要查询的关键词，否则可能会有太多结果");
            }
            else
            {
                String mysqlStr = "Database=zyg;Data Source=sirius0427.jios.org;User Id=cst;Password=q1w2e3r4t5Y^U&I*O(P);pooling=false;CharSet=utf8;port=3307";
                MySqlConnection mysql = new MySqlConnection(mysqlStr);

                mysql.Open();
                String sqlSearch = "select from_unixtime(collectiontime/1000) as time,title,sourceaddr,sourcename from newsinfo where title like '%" + KeyWord.Text + "%' and articletype='1' order by collectiontime desc limit 200";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlSearch, mysql);

                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                try
                {
                    ListView_xinwen.Items.Clear();
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            MyData data = new MyData();
                            data.time = reader["time"].ToString();
                            data.title = reader["title"].ToString();
                            data.url = reader["sourceaddr"].ToString();
                            data.sourcename = reader["sourcename"].ToString();
                            ListView_xinwen.Items.Add(data);
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("查询失败了！");
                }
                finally
                {
                    reader.Close();
                    mysql.Close();
                }
            }
        }

        private void ListView_xinwen_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MyData data = new MyData();
            data = (MyData)ListView_xinwen.SelectedItem;
            if (data.url != "")
            {
                System.Diagnostics.Process.Start(data.url);
            }

        }

        //public ObservableCollection<MyData> dataset_hudong;
        List<MyData> dataset_hudong = new List<MyData>();

        private void search_hudong(object sender, RoutedEventArgs e)
        {
            //List<MyData> dataset_hudong = new List<MyData>();
            dataset_hudong.Clear();
            String mysqlStr = "Database=zyg;Data Source=sirius0427.jios.org;User Id=cst;Password=q1w2e3r4t5Y^U&I*O(P);pooling=false;CharSet=utf8;port=3307";
            MySqlConnection mysql = new MySqlConnection(mysqlStr);
            mysql.Open();
            String sqlSearch = "select max(collectiontime) as time from newsinfo where articletype='2'";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlSearch, mysql);
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            reader.Read();
            hudong_lasttime = reader["time"].ToLong();
            reader.Close();
            sqlSearch = "select from_unixtime(collectiontime/1000) as time,title,digest,sourcename from newsinfo where (title like '%" + KeyWord.Text + "%' or digest like '%" + KeyWord.Text + "%') and articletype='2' order by collectiontime desc limit 100";
            mySqlCommand = new MySqlCommand(sqlSearch, mysql);
            reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        int startIndex = 0;
                        int num2 = 0;
                        string str = "";
                        string str2 = "";
                        MyData item = new MyData
                        {
                            time = reader["time"].ToString()
                        };
                        string str3 = reader["title"].ToString().Replace("\n", "");
                        if (reader["sourcename"].Equals("深证互动"))
                        {
                            startIndex = str3.IndexOf("问", 0) + 2;
                            num2 = str3.IndexOf("):", 0) + 2;
                            str = str3.Substring(startIndex, (num2 - startIndex) - 1);
                            str2 = str3.Substring(num2, str3.Length - num2);
                        }
                        else if (reader["sourcename"].Equals(" 上证互动"))
                        {
                            startIndex = str3.IndexOf(":", 0) + 1;
                            num2 = str3.IndexOf(")", 0) + 1;
                            str = str3.Substring(startIndex, num2 - startIndex);
                            str2 = str3.Substring(num2, str3.Length - num2);
                        }
                        str.Trim();
                        str2.Trim();
                        string[] textArray1 = new string[] { "　　问：", str2, Environment.NewLine, "　　答：", reader["digest"].ToString() };
                        item.title = string.Concat(textArray1);
                        item.sourcename = str + "\n" + reader["time"].ToString();
                        //int num3 = 0;
                        //if (hudong_filter.IsChecked.Value)
                        //{
                        //    foreach (string str4 in Regex.Split(hudong_filtertext.Text, ","))
                        //    {
                        //        if (item.title.IndexOf(str4) >= 0)
                        //        {
                        //            num3 = 1;
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    num3 = 1;
                        //}
                        //if (num3 == 1)
                        //{
                        dataset_hudong.Add(item);
                        //}
                    }


                }


            }
            catch (Exception)
            {
                MessageBox.Show("查询失败了！" + reader["title"].ToString().Replace("\n", ""));
            }
            finally
            {
                ListView_hudong.ItemsSource = null;
                ListView_hudong.ItemsSource = dataset_hudong;
                reader.Close();
                mysql.Close();
            }

        }


        private void Layout_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }


        private void ListView_xinwen_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.C)//用户是否按下了Ctrl键
            {
                MyData data = new MyData();
                data = (MyData)ListView_xinwen.SelectedItem;

                Clipboard.SetText(data.time + Environment.NewLine + data.title + Environment.NewLine + data.url);
                MessageBox.Show("选择的项已拷贝至粘贴板");
            }
        }

        private void hudong_autofresh_Checked(object sender, RoutedEventArgs e)
        {
            hudong_Thread_Time = new System.Threading.Timer(new TimerCallback(hudong_Thread_Timer_Method), null, 0, 5000);
        }

        private void hudong_autofresh_Unchecked(object sender, RoutedEventArgs e)
        {
            hudong_Thread_Time.Dispose();
        }

        private void hudong_Thread_Timer_Method(object o)
        {
            long hudong_maxtime;
            MySqlConnection connection = new MySqlConnection("Database=zyg;Data Source=sirius0427.jios.org;User Id=cst;Password=q1w2e3r4t5Y^U&I*O(P);pooling=false;CharSet=utf8;port=3307");
            connection.Open();
            MySqlDataReader reader = new MySqlCommand("select max(collectiontime) as time from newsinfo where articletype='2'", connection).ExecuteReader();
            reader.Read();
            hudong_maxtime = reader["time"].ToLong();
            reader.Close();
            object[] objArray1 = new object[] { "select from_unixtime(collectiontime/1000) as time,title,digest,sourcename from newsinfo where articletype='2' and collectiontime >", hudong_lasttime, " and collectiontime <=", hudong_maxtime, " order by collectiontime" };
            reader = new MySqlCommand(string.Concat(objArray1), connection).ExecuteReader();
            try
            {
                int insertflag = 0;
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        int startIndex = 0;
                        int num2 = 0;
                        string str = "";
                        string str2 = "";
                        MyData data = new MyData
                        {
                            time = reader["time"].ToString()
                        };
                        string str3 = reader["title"].ToString().Replace("\n", "");
                        if (reader["sourcename"].Equals("深证互动"))
                        {
                            startIndex = str3.IndexOf("问", 0) + 2;
                            num2 = str3.IndexOf("):", 0) + 2;
                            str = str3.Substring(startIndex, (num2 - startIndex) - 1);
                            str2 = str3.Substring(num2, str3.Length - num2);
                        }
                        else if (reader["sourcename"].Equals(" 上证互动"))
                        {
                            startIndex = str3.IndexOf(":", 0) + 1;
                            num2 = str3.IndexOf(")", 0) + 1;
                            str = str3.Substring(startIndex, num2 - startIndex);
                            str2 = str3.Substring(num2, str3.Length - num2);
                        }
                        str.Trim();
                        str2.Trim();
                        string[] textArray1 = new string[] { "　　问：", str2, Environment.NewLine, "　　答：", reader["digest"].ToString() };
                        data.title = string.Concat(textArray1);
                        data.sourcename = str + "\n" + reader["time"].ToString();
                        string[] textArray2 = new string[] { str, "\t", reader["time"].ToString(), "\n问：", str2, Environment.NewLine, "\n答：", reader["digest"].ToString() };
                        string parameter = string.Concat(textArray2);
                        //Thread thread1 = new Thread(new ParameterizedThreadStart(new StartMessageBox().CreateCounterWindowThread));
                        //thread1.SetApartmentState(ApartmentState.STA);
                        //thread1.IsBackground = true;
                        //thread1.Start(parameter);

                        //base.Dispatcher.BeginInvoke((Action)delegate
                        //{
                        //    NotifyData data1 = new NotifyData();
                        //    data1.Title = "抓妖股实时互动";
                        //    data1.Content = parameter;

                        //    NotificationWindow dialog = new NotificationWindow();//new 一个通知
                        //    dialog.Closed += Dialog_Closed;
                        //    dialog.TopFrom = GetTopFrom();
                        //    _dialogs.Add(dialog);
                        //    dialog.DataContext = data1;//设置通知里要显示的数据
                        //    dialog.Show();
                        //}, null);

                        Dispatcher.BeginInvoke((Action)delegate
                        {
                            if (hudong_filter.IsChecked.Value)
                            {
                                insertflag = 0;
                                foreach (string str1 in Regex.Split(hudong_filtertext.Text, ",", RegexOptions.IgnoreCase))
                                {
                                    if (data.title.IndexOf(str1) >= 0)
                                    {
                                        insertflag += 1;

                                    }
                                    else
                                    {
                                        insertflag += 0;
                                    }
                                }
                            }
                            else
                            {
                                insertflag = 1;
                            }
                            if (insertflag >= 1)
                            {
                                dataset_hudong.Insert(0, data);
                                new SoundPlayer("ring.wav").Play();
                                NotifyData data1 = new NotifyData();
                                data1.Title = "抓妖股实时互动" + insertflag.ToString();
                                data1.Content = parameter;

                                NotificationWindow dialog = new NotificationWindow();//new 一个通知
                                dialog.Closed += Dialog_Closed;
                                dialog.TopFrom = GetTopFrom();
                                _dialogs.Add(dialog);
                                dialog.DataContext = data1;//设置通知里要显示的数据
                                dialog.Show();
                            }
                        }, null);

                    }
                }
                hudong_lasttime = hudong_maxtime;
                Dispatcher.BeginInvoke((Action)delegate ()
                {

                    ListView_hudong.ItemsSource = dataset_hudong;
                }, null);
            }
            catch (Exception)
            {
                MessageBox.Show("查询失败了！" + reader["title"].ToString().Replace("\n", ""));
            }
            finally
            {
                reader.Close();
                connection.Close();
            }
        }

        //public class StartMessageBox
        //{
        //    // Fields
        //    private messagebox MsgBox;

        //    // Methods
        //    public void CreateCounterWindowThread(object obj)
        //    {
        //        string message = Convert.ToString(obj);
        //        MsgBox = new messagebox(message);
        //        MsgBox.Topmost = true;
        //        MsgBox.Show();
        //        MsgBox.Closed += (s, e) => MsgBox.Dispatcher.BeginInvokeShutdown(DispatcherPriority.Background);
        //        StartCloseTimer();
        //        Dispatcher.Run();
        //    }

        //    private void StartCloseTimer()
        //    {
        //        DispatcherTimer timer1 = new DispatcherTimer
        //        {
        //            Interval = TimeSpan.FromSeconds(30.0)
        //        };
        //        timer1.Tick += new EventHandler(TimerTick);
        //        timer1.Start();
        //    }

        //    private void TimerTick(object sender, EventArgs e)
        //    {
        //        DispatcherTimer timer1 = (DispatcherTimer)sender;
        //        timer1.Stop();
        //        timer1.Tick -= new EventHandler(TimerTick);
        //        MsgBox.Close();
        //    }
        //}

        public static List<NotificationWindow> _dialogs = new List<NotificationWindow>();
        private void Dialog_Closed(object sender, EventArgs e)
        {
            var closedDialog = sender as NotificationWindow;
            _dialogs.Remove(closedDialog);
        }
        double GetTopFrom()
        {
            //屏幕的高度-底部TaskBar的高度。
            double topFrom = System.Windows.SystemParameters.WorkArea.Bottom - 10;
            bool isContinueFind = _dialogs.Any(o => o.TopFrom == topFrom);

            while (isContinueFind)
            {
                topFrom = topFrom - 210;//此处100是NotifyWindow的高 110-100剩下的10  是通知之间的间距
                isContinueFind = _dialogs.Any(o => o.TopFrom == topFrom);
            }

            if (topFrom <= 0)
                topFrom = System.Windows.SystemParameters.WorkArea.Bottom - 10;

            return topFrom;
        }

        //public class messagebox : Window, IComponentConnector
        //{
        //    // Fields
        //    private bool _contentLoaded;
        //    internal RichTextBox richtextbox_message;
        //    private DispatcherTimer timer_show = new DispatcherTimer();

        //    // Methods
        //    public messagebox(string Message)
        //    {
        //        this.InitializeComponent();
        //        this.richtextbox_message.AppendText(Message);
        //        this.timer_show.Interval = TimeSpan.FromMilliseconds(5000);
        //        this.timer_show.Tick += new EventHandler(this.dispatcherTimer_Tick);
        //        base.Left = SystemParameters.WorkArea.Width - base.Width;
        //        base.Top = SystemParameters.PrimaryScreenHeight;
        //        this.StopTop = SystemParameters.WorkArea.Height - base.Height;
        //        this.timer_show.Start();
        //    }

        //    private void dispatcherTimer_Tick(object sender, EventArgs e)
        //    {
        //        while (true)
        //        {
        //            if (base.Top <= this.StopTop)
        //            {
        //                break;
        //            }
        //            base.Top -= 0.0273;
        //        }
        //        this.timer_show.Stop();
        //    }

        //    [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //    public void InitializeComponent()
        //    {
        //        if (!this._contentLoaded)
        //        {
        //            this._contentLoaded = true;
        //            Uri resourceLocator = new Uri("/StockConcept;component/views/messagebox.xaml", UriKind.Relative);
        //            Application.LoadComponent(this, resourceLocator);
        //        }
        //    }

        //    [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        //    void IComponentConnector.Connect(int connectionId, object target)
        //    {
        //        if (connectionId == 1)
        //        {
        //            this.richtextbox_message = (RichTextBox)target;
        //        }
        //        else
        //        {
        //            this._contentLoaded = true;
        //        }
        //    }

        //    // Properties
        //    public double StopLeft { get; set; }

        //    public double StopTop { get; set; }
        //}

        private void ListView_hudong_KeyDown(object sender, KeyEventArgs e)
        {
            if (((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) && (e.Key == Key.C))
            {
                Clipboard.SetText(this.ListView_hudong.Items[this.ListView_hudong.SelectedIndex].ToString());
                MessageBox.Show("选择的项已拷贝至粘贴板");
            }
        }

        private void AyWindow_Closed(object sender, EventArgs e)
        {

            Application.Current.Shutdown();
        }

        public static void SetCefCookies(string url, CookieCollection cookies)
        {
            Cef.GetGlobalCookieManager().SetStoragePath(Environment.CurrentDirectory, true);
            foreach (System.Net.Cookie c in cookies)
            {
                var cookie = new CefSharp.Cookie
                {
                    Creation = DateTime.Now,
                    Domain = c.Domain,
                    Name = c.Name,
                    Value = c.Value,
                    Expires = c.Expires
                };
                Task<bool> task = Cef.GetGlobalCookieManager().SetCookieAsync(url, cookie);
                while (!task.IsCompleted)
                {
                    continue;
                }
                bool b = task.Result;
            }
        }
    }

    public class MyData
    {
        public string time { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string sourcename { get; set; }
    }



    public class ZygRadioButton : RadioButton
    {
        public ImageSource StaticImg
        {
            get { return (ImageSource)GetValue(StaticImageProperty); }
            set { SetValue(StaticImageProperty, value); }
        }

        public ImageSource CheckedImg
        {
            get { return (ImageSource)GetValue(CheckedImageProperty); }
            set { SetValue(CheckedImageProperty, value); }
        }

        public static readonly DependencyProperty StaticImageProperty = DependencyProperty.Register("StaticImg", typeof(ImageSource), typeof(ZygRadioButton), new PropertyMetadata(null));
        public static readonly DependencyProperty CheckedImageProperty = DependencyProperty.Register("CheckedImg", typeof(ImageSource), typeof(ZygRadioButton), new PropertyMetadata(null));
    }
}
