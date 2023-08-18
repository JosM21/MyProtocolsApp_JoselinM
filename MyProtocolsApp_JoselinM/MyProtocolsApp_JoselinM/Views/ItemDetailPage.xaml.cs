using MyProtocolsApp_JoselinM.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyProtocolsApp_JoselinM.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}