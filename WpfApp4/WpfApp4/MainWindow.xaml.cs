using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WpfApp4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Budget> filtered;
        public static List<Budget> budget = new List<Budget>();
        public static List<string> TypeList = new List<string>();
        //public static List<string> types = new List<string>();
        int? note_index;
        public MainWindow()
        {
            InitializeComponent();
            //budget = DeserAndSer.Deserialize<List<Budget>>();
            //DeserAndSer.Deserialize_type();
            budget = DeserAndSer.FromJson<List<Budget>>("json.file");
            DatePicker.Text = DateTime.Today.ToString();
            //RecordType.ItemsSource = TypeList;
            List_by_date();
        }

        private void AddType_Click(object sender, RoutedEventArgs e)
        {
            RecordType.ItemsSource = TypeList;
            Window1 window1 = new Window1();
            window1.Show();
            TypeList.Add(window1.a);
            RecordType.ItemsSource = TypeList;
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            bool isInCome = false;
            int amount1 = Convert.ToInt32(AmountField.Text);
            if (amount1 > 0)
                isInCome = true;
            else
                amount1 = amount1 * (-1);
            budget.Add(new Budget { name = NameField.Text, Name = NameField.Text, RecordType = RecordType.Text, Amount = amount1, Is_income = isInCome, Date = Convert.ToDateTime(DatePicker.Text) });
            DeserAndSer.ToJson(budget,"json.file");
            UpdatePage();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            bool isInCome = false;
            int amount2 = Convert.ToInt32(AmountField.Text);
            if (amount2 > 0)
                isInCome = true;
            else
                amount2 = amount2 * (-1);
            budget[Convert.ToInt32(note_index)] = new Budget { name = NameField.Text, Name = NameField.Text, RecordType = RecordType.Text, Amount = amount2, Is_income = isInCome, Date = Convert.ToDateTime(DatePicker.Text) };
            DeserAndSer.ToJson(budget, "json.file");
            List_by_date();
            note_index = null;
            UpdatePage();
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            budget.RemoveAt(Convert.ToInt32(note_index));
            DeserAndSer.ToJson(budget, "json.file");
            UpdatePage();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePage();
        }

        private void List_by_date()
        {
            RecordType.ItemsSource = TypeList;
            filtered = budget.Where(n => n.Date.Date == Convert.ToDateTime(DatePicker.Text).Date).ToList();
            DataGrid.ItemsSource = null;
            DataGrid.ItemsSource = filtered;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RecordType.ItemsSource = TypeList;
            if (DataGrid.SelectedIndex >= 0)
            {
                for (int i = 0; i < budget.Count(); i++)
                {
                    if (filtered[DataGrid.SelectedIndex].Date == budget[i].Date)
                    {
                        if (filtered[DataGrid.SelectedIndex].Name == budget[i].Name)
                        {
                            if (filtered[DataGrid.SelectedIndex].RecordType == budget[i].RecordType)
                            {
                                if (filtered[DataGrid.SelectedIndex].Amount == budget[i].Amount)
                                {
                                    note_index = i;
                                    NameField.Text = budget[i].Name;
                                    RecordType.Text = budget[i].RecordType;
                                    if (budget[i].Is_income == false)
                                    {
                                        AmountField.Text = Convert.ToString(budget[i].Amount * (-1));
                                    }
                                    else
                                    {
                                        AmountField.Text = Convert.ToString(budget[i].Amount);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private string CountAllMoney()
        {
            string total = "Итог: 0";
            double total_sum = 0;
            for (int i = 0; i < budget.Count(); i++)
            {
                if (budget[i].Is_income == false)
                {
                    total_sum -= budget[i].Amount;
                    total = "Итог: " + Convert.ToInt16(total_sum);
                }
                else
                {
                    total_sum += budget[i].Amount;
                    total = "Итог: " + Convert.ToInt16(total_sum);
                }
            }
            return total;
        }

        public void UpdatePage()
        {
            List_by_date();
            NameField.Text = "";
            RecordType.Text = "";
            AmountField.Text = "";
            CountOfFull.Content = CountAllMoney();
        }


    }
}


