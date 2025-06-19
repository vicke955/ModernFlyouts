using System.Diagnostics;
using System.Windows.Controls;
using ModernFlyouts.Utilities;

namespace ModernFlyouts.Navigation
{
    public partial class ExitPage : Page
    {
        public ExitPage()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CommonCommands.ExitAppCommand.Execute(null);
        }
    }
} 