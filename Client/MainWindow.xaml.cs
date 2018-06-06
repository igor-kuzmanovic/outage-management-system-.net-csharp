using Microsoft.Win32;
using OutageManagementSystem.Client.Enums;
using OutageManagementSystem.Common;
using OutageManagementSystem.Common.Enums;
using OutageManagementSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace OutageManagementSystem.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static IOutageManagementSystem oms = Connection.GenerateChannel();

        public MainWindow()
        {
            InitializeComponent();
            XStatus.Content = "Ready";
        }

        #region ButtonEventHandlers

        private void XSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string startDate = XStartDate.SelectedDate.HasValue ? XStartDate.SelectedDate.ToString() : string.Empty;
                string endDate = XEndDate.SelectedDate.HasValue ? XEndDate.SelectedDate.ToString() : string.Empty;
                Response<IEnumerable<Outage>> response = oms.FindByDate(startDate, endDate);
                if (response.Status == ResponseStatus.OK)
                {
                    XStatus.Content = $"Search returned {response.Data.Count()} results";
                    XOutages.ItemsSource = response.Data;
                }
                else
                {
                    MessageBox.Show($"Information: {response.Message}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    XOutages.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Server error: {ex.Message}", "Server error", MessageBoxButton.OK, MessageBoxImage.Error);
                oms = Connection.GenerateChannel();
            }
        }

        private void XCreate_Click(object sender, RoutedEventArgs e)
        {
            OutageWindow window = new OutageWindow(oms, WindowMode.Create, 0);
            window.ShowDialog();
            XSearch_Click(new object(), new RoutedEventArgs());
        }

        private void XUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (((Outage)XOutages.SelectedItem).State == OutageState.Closed)
            {
                MessageBox.Show($"Error: Outage is closed and cannot be further updated.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int id = ((Outage)XOutages.SelectedItem).Id;
            OutageWindow window = new OutageWindow(oms, WindowMode.Update, id);
            window.ShowDialog();
            XSearch_Click(new object(), new RoutedEventArgs());
        }

        private void XDetails_Click(object sender, RoutedEventArgs e)
        {
            int id = ((Outage)XOutages.SelectedItem).Id;
            OutageWindow window = new OutageWindow(oms, WindowMode.Details, id);
            window.ShowDialog();
        }

        private void XPDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Response<PDFReport> response = oms.GenerateReport(((Outage)XOutages.SelectedItem).Id);
                if (response.Status == ResponseStatus.OK)
                {
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    dialog.Title = "Generate PDF Report";
                    dialog.AddExtension = true;
                    dialog.DefaultExt = ".pdf";
                    dialog.Filter = "PDF files(*.pdf)|*.pdf";
                    dialog.FileName = response.Data.FileName;
                    if (dialog.ShowDialog() == true)
                    {
                        File.WriteAllBytes(dialog.FileName, response.Data.BinaryData);
                    }
                }
                else
                {
                    MessageBox.Show($"Error: {response.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Server error: {ex.Message}", "Server error", MessageBoxButton.OK, MessageBoxImage.Error);
                oms = Connection.GenerateChannel();
            }
        }

        #endregion ButtonEventHandlers
    }
}
