using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Android.Net.Wifi;
using JunAndChihiro.Annotations;
using JunAndChihiro.Services;
using JunAndChihiro.Views;
using Xamarin.Forms;

namespace JunAndChihiro.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        private readonly INavigation _navigation;
        private readonly Page _page;
        private readonly IAppService _appService = new AppService();

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
                IsNotBusy = !_isBusy;
            }
        }

        private bool _isNotBusy = true;
        public bool IsNotBusy
        {
            get { return _isNotBusy; }
            set
            {
                _isNotBusy = value;
                OnPropertyChanged(nameof(IsNotBusy));
            }
        }

        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _pw;
        public string Pw
        {
            get { return _pw; }
            set
            {
                _pw = value;
                OnPropertyChanged(nameof(Pw));
            }
        }

        public ICommand LoginCommand { get; private set; }

        public LoginPageViewModel()
        {
        }
        public LoginPageViewModel(Page page)
        {
            _navigation = page.Navigation;
            _page = page;
            LoginCommand = new Command(Login);
        }

        private async void Login()
        {
            IsBusy = true;
            var result = await _appService.GetLogin(Id, Pw);
            IsBusy = false;

            if (result == true)
                await _navigation.PushModalAsync(new FolderPage());
            else if (result == false)
                await _page.DisplayAlert("Login", "Identification is wrong.", "Ok");
            else
                await _page.DisplayAlert("Login", "failed to connect a server.", "Ok");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
