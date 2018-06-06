using OutageManagementSystem.Client.Enums;
using OutageManagementSystem.Common;
using OutageManagementSystem.Common.Enums;
using OutageManagementSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace OutageManagementSystem.Client
{
    /// <summary>
    /// Interaction logic for OutageWindow.xaml
    /// </summary>
    public partial class OutageWindow : Window
    {
        private IOutageManagementSystem oms;
        private WindowMode mode;
        private int id;
        private Outage Outage { get; set; } = new Outage();

        public OutageWindow(IOutageManagementSystem oms, WindowMode mode, int id)
        {
            this.oms = oms;
            this.mode = mode;
            this.id = id;
            InitializeComponent();
            InitializeWindow();
            DataContext = Outage;
        }

        #region HelperMethods

        private void InitializeWindow()
        {
            switch (mode)
            {
                case WindowMode.Details:
                    InitializeDetailsWindow();
                    break;
                case WindowMode.Update:
                    InitializeUpdateWindow();
                    break;
                case WindowMode.Create:
                default:
                    InitializeCreateWindow();
                    break;
            }
        }

        private void InitializeCreateWindow()
        {
            InitializeFields();
        }

        private void InitializeUpdateWindow()
        {
            InitializeFields();
            GetOutage();
        }

        private void InitializeDetailsWindow()
        {
            InitializeFields();
            DisableEditing();
            GetOutage();
        }

        private void InitializeFields()
        {
            XCreationDate.SelectedDateFormat = DatePickerFormat.Short;
            XCreationDate.DisplayDateEnd = DateTime.Now;
            XActionExecutionDate.DisplayDateEnd = DateTime.Now;
            XActionExecutionDate.SelectedDate = DateTime.Now;
            XVoltageLevel.ItemsSource = new List<VoltageLevel> { VoltageLevel.Low, VoltageLevel.Medium, VoltageLevel.High };
            XState.ItemsSource = new List<OutageState> { OutageState.New, OutageState.OnHold, OutageState.InTesting, OutageState.InProgress, OutageState.Closed };
        }

        private void DisableEditing()
        {
            XDescription.IsReadOnly = true;
            XCreationDate.IsHitTestVisible = false;
            XVoltageLevel.IsHitTestVisible = false;
            XState.IsHitTestVisible = false;
            XElementName.IsReadOnly = true;
            XElementLongitude.IsReadOnly = true;
            XElementLatitude.IsReadOnly = true;
            LActionDescription.Visibility = Visibility.Collapsed;
            XActionDescription.Visibility = Visibility.Collapsed;
            LActionExecutionDate.Visibility = Visibility.Collapsed;
            XActionExecutionDate.Visibility = Visibility.Collapsed;
            XAddAction.Visibility = Visibility.Collapsed;
            XDeleteAction.Visibility = Visibility.Collapsed;
            XCancel.Visibility = Visibility.Collapsed;
        }

        public bool OpenConfirmation()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to save the changes?", "Confirmation", MessageBoxButton.OKCancel);
            if (result != MessageBoxResult.OK)
            {
                return false;
            }
            return true;
        }

        #endregion HelperMethods

        #region OutageActions

        private void UpdateOutage()
        {
            try
            {
                Response<bool> response = oms.Update(Outage);
                if (response.Status == ResponseStatus.OK)
                {
                    Close();
                }
                else
                {
                    MessageBox.Show($"Error: {response.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Server error: {ex.Message}", "Server error", MessageBoxButton.OK, MessageBoxImage.Error);
                oms = Connection.GenerateChannel();
            }
        }

        private void CreateOutage()
        {
            try
            {
                Response<int> response = oms.Insert(Outage);
                if (response.Status == ResponseStatus.OK)
                {
                    Close();
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

        private void GetOutage()
        {
            try
            {
                Response<Outage> response = oms.Get(id);
                if (response.Status == ResponseStatus.OK)
                {
                    Outage = response.Data;
                    Outage.Actions = new List<ExecutedAction>(response.Data.Actions);
                }
                else
                {
                    MessageBox.Show($"Error: {response.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Server error: {ex.Message}", "Server error", MessageBoxButton.OK, MessageBoxImage.Error);
                oms = Connection.GenerateChannel();
                Close();
            }
        }

        #endregion OutageActions

        #region ButtonEventHandlers

        private void XAddAction_Click(object sender, RoutedEventArgs e)
        {
            string description = XActionDescription.Text;
            DateTime executionDate = XActionExecutionDate.SelectedDate ?? DateTime.Now;
            Outage.Actions.Add(new ExecutedAction() { Description = description, ExecutionDate = executionDate });
            XActionDescription.Text = string.Empty;
            XActionExecutionDate.SelectedDate = DateTime.Now;
            XActions.Items.Refresh();
        }

        private void XDeleteAction_Click(object sender, RoutedEventArgs e)
        {
            Outage.Actions.Remove((ExecutedAction)XActions.SelectedItem);
            XActions.Items.Refresh();
        }

        private void XOK_Click(object sender, RoutedEventArgs e)
        {
            switch (mode)
            {
                case WindowMode.Details:
                    Close();
                    break;
                case WindowMode.Update:
                    if (OpenConfirmation())
                        UpdateOutage();
                    break;
                case WindowMode.Create:
                default:
                    if (OpenConfirmation())
                        CreateOutage();
                    break;
            }
        }

        private void XCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion ButtonEventHandlers
    }
}
