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
    /// Логика взаимодействия для PageEditReader.xaml
    /// </summary>
    public partial class PageEditReader : Page
    {
        EducationalEntities context = EducationalEntities.GetContext();
        Reader reader;
        public PageEditReader(Reader readers)
        {
            reader = readers;
            InitializeComponent();
            DataContext = reader;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (reader.ID_readers == 0)
                {
                    if (context.Reader.FirstOrDefault(x => x.Surname == reader.Surname) == null) context.Reader.Add(reader);
                    else MessageBox.Show("Ученый с данным ФИО уже существует");
                }
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
