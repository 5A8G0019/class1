using Microsoft.Win32;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using CsvHelper;

namespace WpfApp2
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Drink> drinks = new List<Drink>();
        public MainWindow()
        {
            InitializeComponent();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV檔案|*.csv|全部檔案|*,*|文字檔|*.txt";
            openFileDialog.DefaultExt = "*.csv";
            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;
                StreamReader sr = new StreamReader(path, Encoding.Default);
                CsvReader csv = new CsvReader(sr, CultureInfo.InvariantCulture);
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    Drink d = new Drink()
                    {
                        Name = csv.GetField("Name"),
                        Size = csv.GetField("Size"),
                        Price = csv.GetField<int>("Price")
                    };
                    drinks.Add(d);
                }
                foreach (Drink d in drinks)
                {
                    TextBlock1.Text += $"{d.Name}{d.Size}{d.Price}\n";
                }

            }
        }

        private void PrintButton1_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "文字檔|*.txt";
            saveFileDialog.DefaultExt = "*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                string path = saveFileDialog.Filter;
                if (File.Exists(path))
                {
                    File.WriteAllText(path, TextBlock1.Text);
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.Write(TextBlock1.Text);
                    }
                }
            }
        }
    }
}