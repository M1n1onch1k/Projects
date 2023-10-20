using AppLibrary.Helpers;
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

namespace AppLibrary.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageReaders.xaml
    /// </summary>
    public partial class PageReaders : Page
    {
        EducationalEntities context = EducationalEntities.GetContext();
        List<Reader> readers = EducationalEntities.GetContext().Reader.ToList();
        public PageReaders()
        {
            InitializeComponent();
            dbReader.ItemsSource = readers;
        }

        private void btnAdd1_Click(object sender, RoutedEventArgs e)
        {
            AppHelper.mainFrame.Navigate(new PageEditReader(new Reader()));
        }

        private void btnChangeRep_Click(object sender, RoutedEventArgs e)
        {
            AppHelper.mainFrame.Navigate(new PageEditReader((sender as Button).DataContext as Reader));
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            AppHelper.mainFrame.GoBack();
        }

        private void BtnDelete1_Click(object sender, RoutedEventArgs e)
        {
            var selectedRed = dbReader.SelectedItems.Cast<Reader>().ToList();

            if ((MessageBox.Show($"Удалить информацию о {selectedRed.Count} читателе?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question)
                == MessageBoxResult.Yes))
            {
                try
                {
                    context.Library.RemoveRange(selectedRed);
                    context.SaveChanges();
                    readers = context.Reader.ToList();
                    dbReader.ItemsSource = readers;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


    }
}
