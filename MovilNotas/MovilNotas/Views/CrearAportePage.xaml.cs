using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MovilNotas.Models;
using MovilNotas.Services;

namespace MovilNotas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearAportePage : ContentPage
    {
        private ApiService _apiService;
        private CursoFlat _cursoSeleccionado; // Curso recibido

        public CrearAportePage(CursoFlat curso)
        {
            InitializeComponent();
            _apiService = new ApiService();
            _cursoSeleccionado = curso;

            // Mostrar información en la vista
            lblMateria.Text = curso.Materia;
            lblJornada.Text = curso.Jornada;
            lblNivel.Text = curso.Nivel;
            lblParalelo.Text = curso.Paralelo;
        }

        private async void OnCrearAporteClicked(object sender, EventArgs e)
        {
            string titulo = tituloEntry.Text;
            string descripcion = descripcionEditor.Text;
            string categoriaText = categoriaEntry.Text;
            string bimestreText = bimestreEntry.Text;

            if (string.IsNullOrEmpty(titulo) || string.IsNullOrEmpty(categoriaText) || string.IsNullOrEmpty(bimestreText))
            {
                await DisplayAlert("Error", "Todos los campos son requeridos.", "OK");
                return;
            }

            int idCategoria = Convert.ToInt32(categoriaText);
            int idBimestre = Convert.ToInt32(bimestreText);

            Console.WriteLine($"📌 Enviando datos:");
            Console.WriteLine($"  ➡️ idCategoria: {idCategoria}");
            Console.WriteLine($"  ➡️ idBimestre: {idBimestre}");
            Console.WriteLine($"  ➡️ Título: {titulo}");
            Console.WriteLine($"  ➡️ Descripción: {descripcion}");
            Console.WriteLine($"  ➡️ Materia: {_cursoSeleccionado.ProfesorMateriaId}");
            Console.WriteLine($"  ➡️ Jornada: {_cursoSeleccionado.Jornada}");
            Console.WriteLine($"  ➡️ Nivel: {_cursoSeleccionado.Nivel}");
            Console.WriteLine($"  ➡️ Paralelo: {_cursoSeleccionado.Paralelo}");

            var result = await _apiService.CrearAporteAsync(
                idCategoria, idBimestre, titulo, descripcion,
                _cursoSeleccionado.ProfesorMateriaId, _cursoSeleccionado.Jornada,
                _cursoSeleccionado.Nivel, _cursoSeleccionado.Paralelo
            );

            if (result)
            {
                await DisplayAlert("Éxito", "Aporte creado exitosamente", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo crear el aporte.", "OK");
            }
        }

    }
}
