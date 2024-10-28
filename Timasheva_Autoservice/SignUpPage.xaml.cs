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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Timasheva_Autoservice
{
    /// <summary>
    /// Логика взаимодействия для SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        //добавим новое поле, которое будет хранить в себе экземпляр добавляемого сервиса
        private Service  _currentService = new Service();

        public SignUpPage(Service SelectedService)
        {
            InitializeComponent();
            if (SelectedService != null)
                this._currentService = SelectedService;
            
            //При инициализации установим DataContext страницы — этот созданный объект
            //чтобы на форму подгрузить выбранные наименование услуги и длительность
            DataContext = _currentService;
            
            //вытащим из БД таблицу Клиент
            var _currentClient = TimashevaAutoserviceEntities.GetContext().Client.ToList();
            //свяжем ее с комбобоксом
            ComboClient.ItemsSource = _currentClient;
        }

        private ClientService _currentClientService = new ClientService();

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (ComboClient.SelectedItem == null)
                errors.AppendLine("Укажите ФИО клиента");
            
            if (StartDate.Text == "")
                errors.AppendLine("Укажите дату услуги");

            if (TBStart.Text == "")
                errors.AppendLine("Укажите время начала услуги");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            //добавить текущие значения новой записи
            _currentClientService.ClientID = ComboClient.SelectedIndex + 1;//т.к. нумерация с 0
            _currentClientService.ServiceID = _currentService.ID;
            _currentClientService.StartTime = Convert.ToDateTime(StartDate.Text + " " + TBStart.Text);
            
            if (_currentClientService.ID == 0)
                TimashevaAutoserviceEntities.GetContext().ClientService.Add(_currentClientService);

            //сохранить изменения, если никаких ошибок не получилось при этом
            try
            {
                TimashevaAutoserviceEntities.GetContext().SaveChanges();
                MessageBox.Show("информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void CountEndTime()
        {
            string s = TBStart.Text;//"9:05"
            int len = s.Length;//4
            int startHour = 0;
            int startMin = 0;

            int pos = s.IndexOf(':');

            if (len >= 3 && pos == 1)//8:5 
            {
                startHour = Convert.ToInt32(s[0].ToString()) * 60;
                s = s.Remove(0, 2);
                startMin = Convert.ToInt32(s);
            }
            if (len >= 4 && pos == 2)//15:3//09:05
            {
                startHour = Convert.ToInt32(s[0].ToString() + s[1].ToString()) * 60;
                s = s.Remove(0, 3);
                startMin = Convert.ToInt32(s);
            }
            /*
            int sum = startHour + startMin + _currentService.DurationInSeconds;
            int EndHour = sum / 60;
            int EndMin = sum % 60;

            
            if (EndMin > 0 && EndMin < 10)
                s = EndHour.ToString() + ":0" + EndMin.ToString();
            else
                s = EndHour.ToString() + ":" + EndMin.ToString();
            if (EndMin == 0)
                s = s + "0";

            if (len < 3)
                TBEnd.Text = "";
            else
                TBEnd.Text = s;
                */
        }


        private void TBStart_TextChanged(object sender, TextChangedEventArgs e)
        {
            CountEndTime();
        }
    }
}
