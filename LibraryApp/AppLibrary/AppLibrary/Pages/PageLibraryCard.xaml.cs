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
    /// Логика взаимодействия для PageLibraryCard.xaml
    /// </summary>
    public partial class PageLibraryCard : Page
    {

        EducationalEntities context = EducationalEntities.GetContext();
        List<Library> cards = EducationalEntities.GetContext().Library.ToList();
        List<Library> current = new List<Library>();
        public PageLibraryCard()
        {
            InitializeComponent();

            current = cards;
            dbCard.ItemsSource = current;

            CmbSur.ItemsSource = context.Reader.ToList();
            CmbSur.SelectedValue = "ID_reader";
            CmbSur.DisplayMemberPath = "Surname";

            CmbBook.ItemsSource = context.Library.Select(x => x.Book_title).Distinct().ToList();

        }

        private void UpdateFilters()
        {
            current = cards.Where(x => x.Reader.Surname.ToLower().Contains(CmbSur.Text.ToLower())).ToList();
            if (CmbSur.SelectedIndex != -1) current = current.Where(x => x.ID_readers == CmbSur.SelectedIndex + 1).ToList();
            if (CmbBook.SelectedIndex != -1) current = current.Where(x => x.Book_title == CmbBook.SelectedValue.ToString()).ToList();
            dbCard.ItemsSource = current;
        }

        private void BtnRead_Click(object sender, RoutedEventArgs e)
        {
            AppHelper.mainFrame.Navigate(new PageReaders());
        }

        private void btnClearFilters_Click(object sender, RoutedEventArgs e)
        {
            CmbSur.SelectedIndex = -1;
            CmbBook.SelectedIndex = -1;
            TxtSearchName.Text = "";
            current = cards;
            dbCard.ItemsSource = current;
        }

        private void tboxSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (TxtSearchName.Text != "") dbCard.ItemsSource = context.Reader.Where(x => x.Surname.ToLower().Contains(TxtSearchName.Text.ToLower())).ToList();
            else dbCard.ItemsSource = context.Reader.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppHelper.mainFrame.Navigate(new PageEditCard(new Library()));
        }

        private void btnReaders_Click(object sender, RoutedEventArgs e)
        {
            AppHelper.mainFrame.Navigate(new PageReaders());
        }

        private void btnChangeRep_Click(object sender, RoutedEventArgs e)
        {
            AppHelper.mainFrame.Navigate(new PageEditCard((sender as Button).DataContext as Library));
        }

        private void btnRef_Click(object sender, RoutedEventArgs e)
        {
            cards = context.Library.ToList();
            dbCard.ItemsSource = cards;
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var selectedRep = dbCard.SelectedItems.Cast<Library>().ToList();

            if ((MessageBox.Show($"Удалить информацию о {selectedRep.Count} карте читателя?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question)
                == MessageBoxResult.Yes))
            {
                try
                {
                    context.Library.RemoveRange(selectedRep);
                    context.SaveChanges();
                    cards = context.Library.ToList();
                    dbCard.ItemsSource = cards;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnGoListView_Click(object sender, RoutedEventArgs e)
        {
            AppHelper.mainFrame.Navigate(new PageListView());
        }
    }
}
