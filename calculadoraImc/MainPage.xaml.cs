using System.Globalization;

namespace calculadoraImc
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnCalcular_Clicked(object sender, EventArgs e)
        {
            string sPeso = entryPeso.Text;
            string sAltura = entryAltura.Text;

            if (string.IsNullOrEmpty(sPeso) || string.IsNullOrEmpty(sAltura))
            {
                await DisplayAlert("Error", "Faltan datos por rellenar", "OK");
                return;
            }

            sPeso = sPeso.Replace(",", ".");
            sAltura = sAltura.Replace(",", ".");

            double peso, altura;

            bool pesoValido = double.TryParse(sPeso, NumberStyles.Any, CultureInfo.InvariantCulture, out peso);
            bool alturaValida = double.TryParse(sAltura, NumberStyles.Any, CultureInfo.InvariantCulture, out altura);

            if (!pesoValido || !alturaValida)
            {
                await DisplayAlert("Error", "Introduce valores numéricos válidos", "OK");
                return;
            }

            if (peso <= 0 || altura <= 0)
            {
                await DisplayAlert("Error", "El peso y la altura tienen que ser mayores a 0", "OK");
                return;
            }

            double imc = peso / (altura * altura);

            lblResultado.Text = "Tu IMC es: " + imc.ToString("F2");

            string categoria = "";
            string imagen = "";

            if (imc < 18.5)
            {
                categoria = "DELGADO";
                imagen = "delgado.png";
            }
            else if (imc >= 18.5 && imc <= 24.9)
            {
                categoria = "NORMAL";
                imagen = "normal.png";
            }
            else if (imc >= 25 && imc <= 29.9)
            {
                categoria = "SOBREPESO";
                imagen = "sobrepeso.png";
            }
            else
            {
                categoria = "OBESIDAD";
                imagen = "obesidad.png";
            }

            lblCategoria.Text = "Categoría: " + categoria;
            imgCategoria.Source = imagen;
            imgCategoria.IsVisible = true;
        }
    }
}