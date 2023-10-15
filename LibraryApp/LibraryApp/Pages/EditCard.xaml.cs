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
    /// Логика взаимодействия для EditCard.xaml
    /// </summary>
    public partial class EditCard : Page
    {
        EducationalEntities context = EducationalEntities.GetContext();
        Library report;
        public EditCard(Library reports)
        {
            report = reports;
            InitializeComponent();
            DataContext = report;

            cboxAuthor.ItemsSource = context.Reader.ToList();
            cboxAuthor.SelectedValuePath = "ID_card";
            cboxAuthor.DisplayMemberPath = "Book_title";

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (report.ID_card == 0 && context.Library.FirstOrDefault(x => x.Book_title == report.Book_title) == null) context.Library.Add(report);
                context.SaveChanges();
                MessageBox.Show("Данные сохранены");
                AppHelper.mainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            AppHelper.mainFrame.GoBack();
        }
    }
}
