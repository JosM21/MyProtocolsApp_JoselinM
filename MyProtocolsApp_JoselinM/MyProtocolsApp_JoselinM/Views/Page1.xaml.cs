using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyProtocolsApp_JoselinM.ViewModels;
using Acr.UserDialogs;

namespace MyProtocolsApp_JoselinM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class Page1 : ContentPage
    {
        UserViewModel viewModel;

        public Page1()
        {
            InitializeComponent();

            this.BindingContext = viewModel = new UserViewModel();
        }

        private void SwShowPassword_Toggled(object sender, ToggledEventArgs e)
        {
            if (SwShowPassword.IsToggled)
            {

                TxtPassword.IsPassword = false;

            }
            else
            {
                TxtPassword.IsPassword = true;
            }

        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {

            if (TxtUserName.Text != null && !string.IsNullOrEmpty(TxtUserName.Text.Trim()) &&
                TxtPassword.Text != null && !string.IsNullOrEmpty(TxtPassword.Text.Trim())) 
            {
                try
                {

                    UserDialogs.Instance.ShowLoading("Checking User Access...");
                    await Task.Delay(2000);

                    string username = TxtUserName.Text.Trim();
                    string password = TxtPassword.Text.Trim();

                    bool R = await viewModel.UserAccessValidation(username, password);



                    if (R)
                    {


                        GlobalObjects.MyLocalUser = await viewModel.GetUserDataAsync(TxtUserName.Text.Trim());

                        await Navigation.PushAsync(new StartPage());
                        return;

                    }
                    else
                    {
                        await DisplayAlert("User Access Denied", "User or Password are incorrect", "OK");
                        return;
                    }

                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    UserDialogs.Instance.HideLoading();

                }



            }
            else
            {
                await DisplayAlert("Data required", "Username and Password are required...", "OK");
                return;
            }




        }

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {


            await Navigation.PushAsync(new UserSingUpPage());


        }
    }
}