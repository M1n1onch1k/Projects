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
    /// Логика взаимодействия для LibraryCard.xaml
    /// </summary>
    public partial class LibraryCard : Page
    {
        EducationalEntities context = EducationalEntities.GetContext();
        List<Library> cards = EducationalEntities.GetContext().Library.ToList();
        List<Library> current = new List<Library>();
        public LibraryCard()
        {
            InitializeComponent();

            current = cards;
            dgCard.ItemsSource = current;

            cboxConf.ItemsSource = context.Library.ToList();
            cboxConf.SelectedValue = "ConfId";
            cboxConf.DisplayMemberPath = "ConfName";

        }
        /// <summary>
        /// Обновление фильтров
        /// </summary>
        private void UpdateFilters()
        {
            current = cards.Where(x => x.Reader.Surname.ToLower().Contains(tboxSearch.Text.ToLower())).ToList();
            if (cboxConf.SelectedIndex != -1) current = current.Where(x => x.ID_card == cboxConf.SelectedIndex + 1).ToList();
            dgCard.ItemsSource = current;
        }

        private void cboxConf_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFilters();
        }

        private void tboxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateFilters();
        }

        private void cboxOrg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFilters();
        }
        /// <summary>
        /// Переход на страницу добавляения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppHelper.mainFrame.Navigate(new AddReaders(new Reader()));
        }
        /// <summary>
        /// Удаление доклада
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <summary>
        /// Переход на страницу изменения доклада
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangeRep_Click(object sender, RoutedEventArgs e)
        {
            AppHelper.mainFrame.Navigate(new AddEditReportsPage((sender as Button).DataContext as Library));
        }
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var selectedRep = dgCard.SelectedItems.Cast<Library>().ToList();

            if ((MessageBox.Show($"Удалить информацию о {selectedRep.Count} читателе?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question)
                == MessageBoxResult.Yes))
            {
                try
                {
                    context.Library.RemoveRange(selectedRep);
                    context.SaveChanges();
                    cards = context.Library.ToList();
                    dgCard.ItemsSource = cards;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        /// <summary>
        /// Обновление данных в дата гриде
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRef_Click(object sender, RoutedEventArgs e)
        {
            cards = context.Library.ToList();
            dgCard.ItemsSource = cards;
        }
        /// <summary>
        /// Переход на страницу изменения доклада
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangeRep_Click(object sender, RoutedEventArgs e)
        {
            AppHelper.mainFrame.Navigate(new AddReaders((sender as Button).DataContext as Reader));
        }
        /// <summary>
        /// Очистка фильтров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearFilters_Click(object sender, RoutedEventArgs e)
        {
            cboxConf.SelectedIndex = -1;
            tboxSearch.Text = "";
            current = cards;
            dgCard.ItemsSource = current;
        }

        private void btnToSci_Click(object sender, RoutedEventArgs e)
        {
            AppHelper.mainFrame.Navigate(new Readers());
        }

    }
}

