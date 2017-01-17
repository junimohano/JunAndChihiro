using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JunAndChihiro.Models;
using JunAndChihiro.ViewModels;
using Xamarin.Forms;

namespace JunAndChihiro.Views
{
    public partial class FileDetailPage : ContentPage
    {
        public FileDetailPage(JFile jFile)
        {
            InitializeComponent();

            BindingContext = new FileDetailPageViewModel(this, jFile);

            // here to add click ~
        }
    }
}
