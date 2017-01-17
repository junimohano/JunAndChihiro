using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JunAndChihiro.ViewModels;
using Xamarin.Forms;

namespace JunAndChihiro.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            BindingContext = new LoginPageViewModel(this);

            //Device.OnPlatform(Android: () =>
            //{
            //    this.BackgroundImage = "background1.jpg";
            //});
        }
    }
}
