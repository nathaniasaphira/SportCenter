using System.Windows.Controls;
using System.Windows.Input;

namespace SportCenter.Views
{
    public partial class LoadingModalView : UserControl
    {
        public LoadingModalView()
        {
            InitializeComponent();
        }

        private void InputBlocker_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
        }
    }
}