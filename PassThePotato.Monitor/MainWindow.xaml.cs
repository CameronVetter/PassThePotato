using System;
using System.Collections.Generic;
using System.Windows;
using Nito.AsyncEx;
using PassThePotato.MessageContracts;
using PassThePotato.Monitor.Converters;
using PassThePotato.Monitor.Messages;
using PassThePotato.Monitor.ViewModels;

namespace PassThePotato.Monitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly PlayersViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new PlayersViewModel();

        }

        public async void AsyncStartup()
        {
            await ServiceBus.Startup();

            await ServiceBus.Bus.Publish((IRequestCurrentUsers)new RequestCurrentUsers(Application.ResourceAssembly.FullName));
            await ServiceBus.Bus.Publish((IRequestWhoHasPotato)new RequestWhoHasPotato(Application.ResourceAssembly.FullName));
        }

        public void UpdatePlayers(List<Tuple<string, string>> currentPlayers)
        {
            _viewModel.Players = TupleToPlayerConverter.Convert(currentPlayers);

            SetPlayersHasPotato();
            UpdateDisplay();
        }

        public void UpdatePotatoHolder(string id, string name)
        {
            _viewModel.Potato.HolderId = id;
            _viewModel.Potato.HolderName = name;

            SetPlayersHasPotato();
            UpdateDisplay();
        }


        public void UpdateStats(IStats stats)
        {
            CurrentUsersRequest.Content = stats.CurrentUsersRequest;
            CurrentUsersUpdates.Content = stats.CurrentUsersUpdates;
            Errors.Content = stats.Errors;
            MostSimulatneousUsers.Content = stats.MostSimulatneousUsers;
            PassesOfThePotato.Content = stats.PassesOfThePotato;
            WhoCanIPassToRequest.Content = stats.WhoCanIPassToRequest;
            WhoHasPotatoRequest.Content = stats.WhoHasPotatoRequest;
            WhoHasPotatoUpdates.Content = stats.WhoHasPotatoUpdates;
            WhoYouCanPassToUpdates.Content = stats.WhoYouCanPassToUpdates;
        }

        private void SetPlayersHasPotato()
        {
            foreach (var player in _viewModel.Players)
            {
                player.HasPotato = player.Id == _viewModel.Potato.HolderId;
            }
        }

        private void UpdateDisplay()
        {
            Players.Children.Clear();
            foreach (var player in _viewModel.Players)
            {
                Players.Children.Add(new PlayerControl(player.Id, player.Name, player.HasPotato));
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AsyncContext.Run(() => AsyncStartup());
        }
    }
}
