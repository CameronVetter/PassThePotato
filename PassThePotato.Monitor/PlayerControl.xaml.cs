using System.Windows;
using System.Windows.Controls;

namespace PassThePotato.Monitor
{
    /// <summary>
    /// Interaction logic for Player.xaml
    /// </summary>
    public partial class PlayerControl : UserControl
    {
        public string Id { get; set; }

        public PlayerControl()
        {
            InitializeComponent();
        }

        public PlayerControl(string id, string userName, bool hasPotato) : this()
        {
            Id = id;
            Text.Text = userName;

            if (hasPotato) Potato.Visibility = Visibility.Visible;
        }
    }
}
