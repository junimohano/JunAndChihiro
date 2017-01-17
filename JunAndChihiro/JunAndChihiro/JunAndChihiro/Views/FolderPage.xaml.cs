using JunAndChihiro.ViewModels;
using Xamarin.Forms;

namespace JunAndChihiro.Views
{
    public partial class FolderPage : TabbedPage
    {
        public FolderPage()
        {
            InitializeComponent();

            BindingContext = new FolderPageViewModel(this);
            
        }

    }
}
