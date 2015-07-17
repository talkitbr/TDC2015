using TDC2015.BackgroundTask;
using System;
using System.Linq;
using Windows.ApplicationModel.Background;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TDC2015
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var taskRegistered = Windows.ApplicationModel.Background.BackgroundTaskRegistration.AllTasks.Select(task => task.Value.Name == nameof(TileUpdaterTask)).Count() > 0;
            if (!taskRegistered)
            {
                BackgroundAccessStatus allowed = await BackgroundExecutionManager.RequestAccessAsync();

                if (allowed == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity || allowed == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity)
                {
                    var task = new BackgroundTaskBuilder
                    {
                        Name = nameof(TileUpdaterTask),
                        CancelOnConditionLoss = false,
                        TaskEntryPoint = typeof(TileUpdaterTask).ToString(),
                    };
                    task.SetTrigger(new SystemTrigger(SystemTriggerType.InternetAvailable, false));
                    task.AddCondition(new SystemCondition(SystemConditionType.UserPresent));
                    task.Register();
                }
            }
        }

        private void SetTileButton_Click(object sender, RoutedEventArgs e)
        {
            this.StatusLabel.Text = "Setting tile...";            

            TileManager.CreateTile();

            this.StatusLabel.Text = "Tile has been set!";
        }

        private void RegisterTaskButton_Click(object sender, RoutedEventArgs e)
        {
            this.StatusLabel.Text = "Registering task...";

            this.RegisterTask();            
        }

        private async void RegisterTask()
        {
            var taskRegistered = Windows.ApplicationModel.Background.BackgroundTaskRegistration.AllTasks.Select(task => task.Value.Name == nameof(TileUpdaterTask)).Count() > 0;
            if (taskRegistered)
            {
                this.StatusLabel.Text = "Task already registered!";
            }
            else
            {
                BackgroundAccessStatus allowed = await BackgroundExecutionManager.RequestAccessAsync();

                if (allowed == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity || allowed == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity)
                {
                    var task = new BackgroundTaskBuilder
                    {
                        Name = nameof(TileUpdaterTask),
                        CancelOnConditionLoss = false,
                        TaskEntryPoint = typeof(TileUpdaterTask).ToString(),
                    };

                    task.SetTrigger(new TimeTrigger(15, false));
                    task.AddCondition(new SystemCondition(SystemConditionType.UserPresent));
                    task.Register();                    

                    this.StatusLabel.Text = "Task has been registered!";
                }
                else
                {
                    this.StatusLabel.Text = "Not allowd to register task!";
                }                
            }
        }
    }
}
