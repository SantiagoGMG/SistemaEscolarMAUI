using System.Diagnostics;
using System.Text;

namespace SistemaEscolarMAUI;

public partial class FormPage : ContentPage
{
    private readonly HttpClient _httpClient = new HttpClient();

    public async Task PostData()
    {
        string sexo = "";
        if (RsexoH.IsChecked)
        {
            sexo = "H";
        }
        if (RsexoM.IsChecked)
        {
            sexo = "M";
        }
        if (RsexoO.IsChecked)
        {
            sexo = "O";
        }

        string fechaUsuario = fecha.Date.ToString("yyyy-MM-dd");
        int rolUsuario = comboBox.SelectedIndex + 1;

        var persona = new
        {
            id = 0,
            nombre = EntryNombre.Text,
            apellido = EntryApellido.Text,
            sexo = sexo,
            fh_nac = fechaUsuario,
            id_rol = rolUsuario
        };
        Debug.WriteLine(persona);

        var contenido = new StringContent(System.Text.Json.JsonSerializer.Serialize(persona), Encoding.UTF8, "application/json");


        var response = await _httpClient.PostAsync("https://fi.jcaguilar.dev/v1/escuela/persona", contenido);

        if (response.IsSuccessStatusCode)
        {
            Debug.WriteLine("Usuario creado");
            await DisplayAlert("Éxito", "Persona creada con éxito", "OK");
        }
        else
        {
            Debug.WriteLine("Error en la peticion");

        }

        //var response = await _httpClient.PostStringAsync("https://fi.jcaguilar.dev/v1/escuela/persona");

    }
    public FormPage()
    {
        InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            await PostData();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex.Message}");
        }
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}