using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MovilNotas.Models;
using MovilNotas.Services;
using System.Linq;

namespace MovilNotas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearAportePage : ContentPage
    {
        private readonly ApiService _apiService;
        private readonly CursoFlat _cursoSeleccionado;

        public CrearAportePage(CursoFlat curso)
        {
            InitializeComponent();
            _apiService = new ApiService();
            _cursoSeleccionado = curso;

            lblMateria.Text = curso.Materia;
            lblJornada.Text = curso.Jornada;
            lblNivel.Text = curso.Nivel;
            lblParalelo.Text = curso.Paralelo;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            categoriaPicker.ItemsSource = await _apiService.ObtenerCategoriasAsync();
            bimestrePicker.ItemsSource = await _apiService.ObtenerBimestresAsync();
        }

        private async void OnCrearAporteClicked(object sender, EventArgs e)
        {
            if (categoriaPicker.SelectedItem == null ||
                bimestrePicker.SelectedItem == null ||
                string.IsNullOrEmpty(tituloEntry.Text))
            {
                await DisplayAlert("Aviso", "Seleccione una categoría, un bimestre e ingrese un título.", "OK");
                return;
            }

            var aporte = new AporteSrweel
            {
                id_categoria = ((CategoriaSrweel)categoriaPicker.SelectedItem).id,
                id_bimestre = ((BimestreSrweel)bimestrePicker.SelectedItem).id,
                titulo = tituloEntry.Text,
                descripcion = descripcionEditor.Text ?? "",
                id_materia = _cursoSeleccionado.IdMateria,
                id_jornada = _cursoSeleccionado.IdJornada,
                id_nivel = _cursoSeleccionado.IdNivel,
                id_paralelo = _cursoSeleccionado.IdParalelo
            };

            var (exito, mensaje) = await _apiService.CrearAporteAsync(aporte);

            if (exito)
            {
                await DisplayAlert("Éxito", mensaje, "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", mensaje, "OK"); // Muestra el error detallado
            }
        }


    }
}
