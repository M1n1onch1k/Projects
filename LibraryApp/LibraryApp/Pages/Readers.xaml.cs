using LibraryApp.Helpers;
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

namespace LibraryApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для Readers.xaml
    /// </summary>
    public partial class Readers : Page
    {
        EducationalEntities context = EducationalEntities.GetContext();
        List<Reader> readers = EducationalEntities.GetContext().Reader.ToList();
        public Readers()
        {
            InitializeComponent();
            dgScientists.ItemsSource = readers;
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var selectedSci = dgScientists.SelectedItems.Cast<Reader>().ToList();

            if ((MessageBox.Show($"Удалить информацию о {selectedSci.Count} читателях?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question)
                == MessageBoxResult.Yes))
            {
                try
                {
                    context.Reader.RemoveRange(selectedSci);
                    context.SaveChanges();
                    dgScientists.ItemsSource = context.Reader.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnRef_Click(object sender, RoutedEventArgs e)
        {
            dgScientists.ItemsSource = context.Reader.ToList();
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            AppHelper.mainFrame.GoBack();
        }

        private void tboxSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (tboxSearch.Text != "") dgScientists.ItemsSource = context.Reader.Where(x => x.Surname.ToLower().Contains(tboxSearch.Text.ToLower())).ToList();
            else dgScientists.ItemsSource = context.Reader.ToList();
        }
    }
}

