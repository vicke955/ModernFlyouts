using System.Windows.Controls;
using System.Windows;
using System.Threading.Tasks;

namespace ModernFlyouts.Navigation
{
    public partial class GeneralSettingsPage : Page
    {
        public GeneralSettingsPage()
        {
            InitializeComponent();
        }
        
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // 切换确认按钮的可见性
            if (ResetConfirmButton.Visibility == Visibility.Visible)
                ResetConfirmButton.Visibility = Visibility.Collapsed;
            else
                ResetConfirmButton.Visibility = Visibility.Visible;
        }

        private async void ResetConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            // 切换确认按钮的可见性
            await Task.Delay(500);
            ResetConfirmButton.Visibility = Visibility.Collapsed;
        }
    }
}
