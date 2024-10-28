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
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        //добавим новое поле, которое будет хранить в себе экземпляр добавляемого сервиса
        private Service _currentService = new Service();

        public AddEditPage(Service SelectedService)
        {
            InitializeComponent();

            if (SelectedService != null)
                _currentService = SelectedService;

            //При инициализации установим DataContext страницы — этот созданный объект
            DataContext = _currentService;

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentService.Title))
                errors.AppendLine("Укажите название услуги");

            if (_currentService.Cost == 0)
                errors.AppendLine("Укажите стоимость услуги");

            //после 8 лабы эта проверка
            //if (Convert.ToInt32(_currentService.Duration) == 0)
            //пока проверка на строку
            if (string.IsNullOrWhiteSpace(_currentService.Duration))
                errors.AppendLine("Укажите длительность услуги");

            //if (Convert.ToInt32(_currentService.Duration) > 240)
            //    errors.AppendLine("Длительность не может быть больше 240 минут");

            //if (string.IsNullOrWhiteSpace(_currentService.Discount))
            if (Convert.ToInt32(_currentService.DiscountInt) < 0 || 
                Convert.ToInt32(_currentService.DiscountInt) > 100)
                errors.AppendLine("Укажите скидку от 0 до 100");
            


            //попытка проверки - есть ли такая услуга
            /*
            var currentServices = Timasheva_DBEntities.GetContext().Service.ToList();
            //if (_currentServise.Title == currentServices.ti)

            currentServices = currentServices.Where(p => p.Title.ToLower().Equals(TBTitle.Text.ToLower())).ToList();

            if (currentServices == null)
                MessageBox.Show("null");
            else
                MessageBox.Show(currentServices.ToString());
            */

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                   return;
            }
            //Заменить discount
            _currentService.Discount = _currentService.DiscountInt / 100.0;

            //добавить в контекст текущие значения новой услуги
            if (_currentService.ID == 0)
                TimashevaAutoserviceEntities.GetContext().Service.Add(_currentService);
            
            //сохранить изменения, если никаких ошибок не получилось при этом
            try
            {
                //MessageBox.Show(_currentService.ID.ToString());
                TimashevaAutoserviceEntities.GetContext().SaveChanges();
                MessageBox.Show("информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }
    }
}
