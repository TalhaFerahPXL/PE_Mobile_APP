using Microsoft.Maui.ApplicationModel.Communication;
using PE_Mobile_APP.Model;
using System.Text;

namespace PE_Mobile_APP.Views;

public partial class Login : ContentPage
{
    public LoginModel Model { get; set; }
    public Login()
	{
		InitializeComponent();
        Model = new LoginModel();
        BindingContext = Model;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {

        //string Email;
        //string Wachtwoord;

        //var loginData = new
        //{
        //    Email = Model.Email,
        //    Wachtwoord = Model.Wachtwoord
        //};

        HttpClient client = new HttpClient();
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(Model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync("http://10.0.2.2:5084/api/Login/LoginUser", content);


        HttpResponseMessage responseMessage = await client.GetAsync($"http://10.0.2.2:5084/api/Login/GetUserNameByEmail?email={Model.Email}");


        //als response == 200 => succes
        if (response.IsSuccessStatusCode)
        {
            
            string naam = await responseMessage.Content.ReadAsStringAsync();

            Preferences.Set("GebruikersNaam", naam);

            await Shell.Current.GoToAsync("//Home");
        }
        else
        {
            
            foutTxt.IsVisible = true;

        }
        


        
    }
}