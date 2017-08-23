using Ay.Framework.WPF.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Data.SQLite;
using System.Windows.Documents;
using MySql.Data.MySqlClient;

namespace StockConcept
{
    /// <summary>
    /// _ViewStart.xaml 的交互逻辑
    /// </summary>
    public partial class _ViewStart : AyWindow
    {
        public string dbPath = "Data Source =" + Environment.CurrentDirectory + "/zyg.db";

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

            if ( listBox_ticai.Items.Count == 0 )
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

            if ( listBox_gudong.Items.Count == 0 )
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

            if ( ListView_xinwen.Items.Count == 0)
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

            if (ListBox_hudong.Items.Count == 0)
            {
                hudong.Visibility = Visibility.Collapsed;
                nothing.Visibility = Visibility.Visible;
            }
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

                string sql = "SELECT point FROM dfcf_concept where sourcename = '" + listBox_ticai.SelectedItem.ToString() + "' and point like '%" + KeyWord.Text + "%' order by id";
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

                sql = "SELECT point FROM ths_concept where sourcename = '" + listBox_ticai.SelectedItem.ToString() + "' and point like '%" + KeyWord.Text + "%' order by id";
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
            search_F10(sender, e);
            search_gudong(sender, e);
            search_xinwen(sender, e);
            search_hudong(sender, e);
            if ((bool) F10题材搜索.IsChecked )
            {
                F10题材搜索_Click(sender, e);
            }
            else if ((bool)股东搜索.IsChecked )
            {
                股东搜索_Click(sender, e);
            }
            else if ((bool)新闻搜索.IsChecked )
            {
                新闻搜索_Click(sender, e);
            }
            else if ((bool)互动搜索.IsChecked)
            {
                互动搜索_Click(sender, e);
            }

        }

        private void search_F10(object sender, RoutedEventArgs e)
        {
            int Count1 = 0;

            SQLiteConnection conn = null;

            conn = new SQLiteConnection(dbPath);//创建数据库实例，指定文件位置  
            conn.Open();//打开数据库，若文件不存在会自动创建  

            string sql = "select distinct(sourcename) from (SELECT id,sourcename FROM dfcf_concept where point like '%" + KeyWord.Text + "%' union all SELECT id,sourcename FROM ths_concept where point like '%" + KeyWord.Text + "%') order by id";
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
            int rowcount = 0;
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

        private void search_hudong(object sender, RoutedEventArgs e)
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
                String sqlSearch = "select from_unixtime(collectiontime/1000) as time,title,digest,sourcename from newsinfo where title like '%" + KeyWord.Text + "%' and articletype='2' order by collectiontime desc limit 200";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlSearch, mysql);

                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                try
                {
                    ListBox_hudong.Items.Clear();
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            //string title = reader["title"].ToString();
                            ListBox_hudong.Items.Add(reader["time"] + "  " + reader["sourcename"] + Environment.NewLine + reader["title"].ToString().Replace(" \n", "") + Environment.NewLine + reader["digest"] + Environment.NewLine);
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


        private void Layout_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ListBox_hudong_KeyDown(object sender, KeyEventArgs e)
        {
            if ( (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key==Key.C)//用户是否按下了Ctrl键
            {
                Clipboard.SetText(ListBox_hudong.Items[ListBox_hudong.SelectedIndex].ToString());
                MessageBox.Show("选择的项已拷贝至粘贴板");
            }
        }

        private void ListView_xinwen_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.C)//用户是否按下了Ctrl键
            {
                MyData data = new MyData();
                data = (MyData)ListView_xinwen.SelectedItem;

                Clipboard.SetText( data.time + Environment.NewLine + data.title + Environment.NewLine + data.url );
                MessageBox.Show("选择的项已拷贝至粘贴板");
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
