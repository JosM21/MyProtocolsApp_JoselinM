using MyProtocolsApp_JoselinM.Models;
using MyProtocolsApp_JoselinM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyProtocolsApp_JoselinM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserChangePasswordPage : ContentPage
    {
        UserViewModel viewModel;
        public UserChangePasswordPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new UserViewModel();

        }

        private bool ValidateFields()
        {
            bool R = false;
            if (TxtPassword_1.Text != null && !string.IsNullOrEmpty(TxtPassword_1.Text.Trim()) &&
                TxtPassword_2.Text != null && !string.IsNullOrEmpty(TxtPassword_2.Text.Trim()) &&
                TxtPassword_3.Text != null && !string.IsNullOrEmpty(TxtPassword_3.Text.Trim()))
            {
                R = true;
            }
            else
            {
                if (TxtPassword_1.Text == null || string.IsNullOrEmpty(TxtPassword_1.Text.Trim()))
                {
                    DisplayAlert("Validation Failed", "Old password is required", "OK");
                    TxtPassword_1.Focus();
                    return false;
                }

                if (TxtPassword_2.Text == null || string.IsNullOrEmpty(TxtPassword_2.Text.Trim()))
                {
                    DisplayAlert("Validation Failed", "New password is required", "OK");
                    TxtPassword_2.Focus();
                    return false;
                }

                if (TxtPassword_3.Text == null || string.IsNullOrEmpty(TxtPassword_3.Text.Trim()))
                {
                    DisplayAlert("Validation Failed", "Confirm the password is required", "OK");
                    TxtPassword_3.Focus();
                    return false;
                }


            }
            return R;

        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {

            if (ValidateFields() && ValidatePasswords())
            {

                try
                {
                    GlobalObjects.MyLocalUser.Contrasennia = TxtPassword_3.Text.Trim();

                    var answer = await DisplayAlert("???", "Are you sure to continue updating user info?", "Yes", "No");

                    if (answer)
                    {
                        bool R = await viewModel.UpdateUser(GlobalObjects.MyLocalUser);

                        if (R)
                        {
                            await DisplayAlert(":)", "Password Updated!!!", "OK");
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert(":(", "Something went wrong...", "OK");
                            await Navigation.PopAsync();
                        }

                    }

                }
                catch (Exception)
                {

                    throw;
                }



            }
        }

        private bool ValidatePasswords()
        {
            {
                bool R = false;

                if (TxtPassword_1.Text == GlobalObjects.MyLocalUser.Contrasennia &&
                    TxtPassword_2.Text == TxtPassword_3.Text)
                {
                    R = true;
                }

                if (TxtPassword_1.Text != GlobalObjects.MyLocalUser.Contrasennia)
                {
                    DisplayAlert("Validation Failed!", "Old password is wrong, plaese try again", "OK");
                    TxtPassword_1.Focus();
                    return false;
                }

                if (TxtPassword_2.Text != TxtPassword_3.Text)
                {
                    DisplayAlert("Validation Failed!", "Password have to be the same", "OK");
                    TxtPassword_2.Focus();
                    return false;
                }


                if (TxtPassword_2.Text == null || string.IsNullOrEmpty(TxtPassword_2.Text.Trim()))
                {
                    DisplayAlert("Validation Failed!", "New password is required", "OK");
                    TxtPassword_2.Focus();
                    return false;
                }


                return R;
            }



        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new UserManagmentPage());


        }

        private void SwShowPassword_Toggled(object sender, ToggledEventArgs e)
        {
            if (SwShowPassword.IsToggled)
            {

                TxtPassword_1.IsPassword = false;
                TxtPassword_2.IsPassword = false;
                TxtPassword_3.IsPassword = false;

            }
            else
            {
                TxtPassword_1.IsPassword = true;
                TxtPassword_2.IsPassword = true;
                TxtPassword_2.IsPassword = true;
            }
        }
    }
}
