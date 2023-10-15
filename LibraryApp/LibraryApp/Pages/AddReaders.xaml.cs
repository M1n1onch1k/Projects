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
    /// Логика взаимодействия для AddReaders.xaml
    /// </summary>
    public partial class AddReaders : Page
    {
        EducationalEntities context = EducationalEntities.GetContext();
        Reader card;
        public AddReaders(Reader reports)
        {
            card = reports;
            InitializeComponent();
            DataContext = card;

            cboxAuthor.ItemsSource = context.Reader.ToList();
            cboxAuthor.SelectedValuePath = "ID_readers";
            cboxAuthor.DisplayMemberPath = "Surname";

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (card.ID_readers == 0 && context.Reader.FirstOrDefault(x => x.Surname == card.Surname) == null) context.Reader.Add(card);
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

