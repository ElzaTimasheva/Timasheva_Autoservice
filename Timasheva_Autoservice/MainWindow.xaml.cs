using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new ServicePage());
            Manager.MainFrame = MainFrame;
            TBHeader.Text = "Услуги автосервиса";

        //    ImportPictures();
        // доделать

        }

        private void ImportPictures()
        {
            var images = Directory.GetFiles(@"D:\работа\c#\2020 303\лабы 2 сем\6\Услуги автосервиса");

            var tempservice = new Service();

            // foreach (var )
            /*
            var category = Core.DB.ACAppCategory.FirstOrDefault();
            foreach (var app in category.ACApp)
            {
                Console.WriteLine(app.title);
            }
            */
            ///////////
            /*
            try
            {
                tempservice.ImagePreview = File.ReadAllBytes(images.FirstOrDefault(p => p.Contains(tempservice.MainImagePath)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            */
            TimashevaAutoserviceEntities.GetContext().Service.Add(tempservice);
            TimashevaAutoserviceEntities.GetContext().SaveChanges();

        }



        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();

        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                BtnBack.Visibility = Visibility.Visible;
            }
            else
            {
                BtnBack.Visibility = Visibility.Hidden;
            }
        }
    }
}
