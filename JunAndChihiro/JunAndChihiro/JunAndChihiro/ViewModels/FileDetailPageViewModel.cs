using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JunAndChihiro.Annotations;
using JunAndChihiro.Models;
using JunAndChihiro.Services;
using JunAndChihiro.Views;
using Xamarin.Forms;

namespace JunAndChihiro.ViewModels
{
    public class FileDetailPageViewModel : INotifyPropertyChanged
    {
        private readonly INavigation _navigation;
        private readonly Page _page;
        private readonly IAppService _appService = new AppService();

        public ICommand SaveCommand { get; private set; }

        private bool _isImage = false;
        public bool IsImage
        {
            get { return _isImage; }
            set
            {
                _isImage = value;
                OnPropertyChanged(nameof(IsImage));
            }
        }

        private bool _isGif = false;
        public bool IsGif
        {
            get { return _isGif; }
            set
            {
                _isGif = value;
                OnPropertyChanged(nameof(IsGif));
            }
        }

        private bool _isVideo = false;
        public bool IsVideo
        {
            get { return _isVideo; }
            set
            {
                _isVideo = value;
                OnPropertyChanged(nameof(IsVideo));
            }
        }

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

        private JFile _jFile;
        public JFile JFile
        {
            get { return _jFile; }
            set
            {
                _jFile = value;
                OnPropertyChanged(nameof(JFile));
            }
        }

        private bool _isActive = false;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                OnPropertyChanged(nameof(IsActive));
            }
        }

        private async void SaveFile()
        {
            IsBusy = true;
            bool result = await _appService.SetFile(JFile);
            IsBusy = false;

            if (result)
                await _navigation.PopModalAsync();
            else
                await _page.DisplayAlert("Result", "Failed", "Ok");
        }

        public FileDetailPageViewModel()
        {
        }

        public FileDetailPageViewModel(Page page, JFile jFile)
        {
            _navigation = page.Navigation;
            _page = page;

            JFile = jFile;
            SaveCommand = new Command(SaveFile);

            Constants.FileType fileType = Constants.GetFileType(jFile);

            switch (fileType)
            {
                case Constants.FileType.Video:
                    IsVideo = true;
                    break;
                case Constants.FileType.Music:
                    IsVideo = true;
                    break;
                case Constants.FileType.Gif:
                    IsGif = true;
                    break;
                case Constants.FileType.Image:
                    IsImage = true;
                    break;
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
