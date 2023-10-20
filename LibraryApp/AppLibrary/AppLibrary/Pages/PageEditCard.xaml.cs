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
    /// Логика взаимодействия для PageEditCard.xaml
    /// </summary>
    public partial class PageEditCard : Page
    {
        EducationalEntities context = EducationalEntities.GetContext();
        Library card;
        public PageEditCard(Library cards)
        {
            InitializeComponent();

            card = cards;
            InitializeComponent();
            DataContext = card;

            CmbSur.ItemsSource = context.Reader.ToList();
            CmbSur.SelectedValuePath = "ID_readers";
            CmbSur.DisplayMemberPath = "Surname";

            CmbName.ItemsSource = context.Reader.ToList();
            CmbName.SelectedValuePath = "ID_readers";
            CmbName.DisplayMemberPath = "Name";



        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (card.ID_card == 0 && context.Library.FirstOrDefault(x => x.Book_title == card.Book_title) == null) context.Library.Add(card);
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
