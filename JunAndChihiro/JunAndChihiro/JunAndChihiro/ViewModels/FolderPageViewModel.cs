using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JunAndChihiro.Annotations;
using JunAndChihiro.Models;
using JunAndChihiro.Services;
using Xamarin.Forms;
using JunAndChihiro.Views;

namespace JunAndChihiro.ViewModels
{
    public class FolderPageViewModel : INotifyPropertyChanged
    {
        private readonly IAppService _appService = new AppService();

        private readonly INavigation _navigation;
        private readonly Page _page;

        private string _fileTypeImage = string.Empty;
        public string FileTypeImage
        {
            get { return _fileTypeImage; }
            set
            {
                _fileTypeImage = value;
                OnPropertyChanged(nameof(FileTypeImage));
            }
        }

        private string _currentFolderName = "Root";
        public string CurrentFolderName
        {
            get { return _currentFolderName; }
            set
            {
                _currentFolderName = value;
                OnPropertyChanged(nameof(CurrentFolderName));
            }
        }

        private int _fileCount = 0;
        public int FileCount
        {
            get { return _fileCount; }
            set
            {
                _fileCount = value;
                OnPropertyChanged(nameof(FileCount));
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

        public ObservableCollection<JFolder> FolderList
        {
            get { return _folderList; }
            set
            {
                _folderList = value;
                OnPropertyChanged(nameof(FolderList));
            }
        }

        public ObservableCollection<FolderPageViewModel> FolderPageViewModelList
        {
            get { return _folderPageViewModelList; }
            set
            {
                _folderPageViewModelList = value;
                OnPropertyChanged(nameof(FolderPageViewModelList));
            }
        }

        private JFolder _selectedFolder;
        public JFolder SelectedFolder
        {
            get { return _selectedFolder; }
            set
            {
                if (_selectedFolder == value)
                    return;

                _selectedFolder = value;
                OnPropertyChanged(nameof(SelectedFolder));

                FolderClick();
            }
        }

        private FolderPageViewModel _selectedFolderPageViewModel;
        public FolderPageViewModel SelectedFolderPageViewModel
        {
            get { return _selectedFolderPageViewModel; }
            set
            {
                if (_selectedFolderPageViewModel == value)
                    return;

                _selectedFolderPageViewModel = value;
                OnPropertyChanged(nameof(SelectedFolderPageViewModel));

                FileClick();
            }
        }

        private async void FileClick()
        {
            IsBusy = true;
            if (SelectedFolderPageViewModel.JFile.Date == null)
                SelectedFolderPageViewModel.JFile.Date = DateTime.Today;

            await _navigation.PushModalAsync(new FileDetailPage(SelectedFolderPageViewModel.JFile));
            IsBusy = false;
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

        public FolderPageViewModel()
        {
        }

        public FolderPageViewModel(Page page)
        {
            _navigation = page.Navigation;
            _page = page;

            RefreshFolderList();
        }

        private void FolderClick()
        {
            IsBusy = true;
            RefreshFolderList(SelectedFolder);
            RefreshFileList(SelectedFolder);
            IsBusy = false;
        }

        private async void RefreshFolderList(JFolder folder = null)
        {
            List<JFolder> result;

            if (folder == null)
                result = await _appService.GetFolder(new Guid("79eaade1-04f3-47d3-86f9-187fc9f634da"));
            else
            {
                CurrentFolderName = folder.RootFolderPath;
                result = await _appService.GetFolder(folder.FolderOid);
            }

            if (result.Count > 0)
                FolderList = new ObservableCollection<JFolder>(result);
            else
                FolderList.Clear();
        }

        private async void RefreshFileList(JFolder item)
        {
            var result = await _appService.GetFiles(item.FolderOid);

            if (result.Any())
            {
                List<FolderPageViewModel> folderViewModels = new List<FolderPageViewModel>();
                foreach (var r in result)
                {
                    r.FilePath = Constants.RestUrlUpload + r.FolderPath + "/" + r.FileName;

                    Constants.FileType fileType = Constants.GetFileType(r);
                    var fileTypeImage = string.Empty;
                    switch (fileType)
                    {
                        case Constants.FileType.Video:
                            fileTypeImage = "video.png";
                            break;
                        case Constants.FileType.Music:
                            fileTypeImage = "music.png";
                            break;
                        case Constants.FileType.Gif:
                            fileTypeImage = "gif.png";
                            break;
                        case Constants.FileType.Image:
                            fileTypeImage = "image.png";
                            break;
                    }


                    folderViewModels.Add(new FolderPageViewModel()
                    {
                        JFile = r,
                        ThumbImage = (r.Thumb != null) ? ImageSource.FromStream(() => new MemoryStream(r.Thumb)) : null,
                        FileTypeImage = fileTypeImage
                    });
                }

                FolderPageViewModelList = new ObservableCollection<FolderPageViewModel>(folderViewModels);
            }
            else
                FolderPageViewModelList.Clear();

            FileCount = FolderPageViewModelList.Count;
        }

        private ImageSource _thumbImage;
        private ObservableCollection<JFolder> _folderList = new ObservableCollection<JFolder>();
        private ObservableCollection<FolderPageViewModel> _folderPageViewModelList = new ObservableCollection<FolderPageViewModel>();


        public ImageSource ThumbImage
        {
            get { return _thumbImage; }
            set
            {
                _thumbImage = value;
                OnPropertyChanged(nameof(ThumbImage));
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
