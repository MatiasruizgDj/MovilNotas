using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MovilNotas.Models;
using MovilNotas.Services;
using System.Threading.Tasks;

namespace MovilNotas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstudianteDetallesPage : ContentPage
    {
        private AporteDetalle _aporteSeleccionado;
        private readonly ApiService _apiService;

        public EstudianteDetallesPage(EstudianteDetalle estudiante)
        {
            InitializeComponent();
            _apiService = new ApiService();
            BindingContext = estudiante;

            // Depuración avanzada
            if (estudiante.NotasList == null || estudiante.NotasList.Count == 0)
            {
                Console.WriteLine("⚠️ La lista de NotasList está vacía.");
            }
            else
            {
                Console.WriteLine($"📌 NotasList tiene {estudiante.NotasList.Count} elementos.");
                foreach (var nota in estudiante.NotasList)
                {
                    Console.WriteLine($"➡️ Nota: {nota.Bimestre}, Categorías: {nota.CategoriasList?.Count ?? 0}");
                    foreach (var categoria in nota.CategoriasList)
                    {
                        Console.WriteLine($"   🟢 Categoría: {categoria.Categoria}, Aportes: {categoria.Aportes?.Count ?? 0}");
                        foreach (var aporte in categoria.Aportes)
                        {
                            Console.WriteLine($"      ✅ Aporte: {aporte.Titulo}, Nota: {aporte.Nota}");
                        }
                    }
                }
            }
        }


        private void OnAporteSeleccionado(object sender, EventArgs e)
        {
            if (sender is VisualElement visualElement)
            {
                var bindingContext = visualElement.BindingContext;
                Console.WriteLine($"✅ Sender es VisualElement de tipo: {visualElement.GetType().FullName}");

                if (bindingContext is AporteDetalle aporteDetalle)
                {
                    Console.WriteLine($"✅ Se seleccionó el aporte: {aporteDetalle.Titulo}");
                    _aporteSeleccionado = aporteDetalle;
                    TituloEntry.Text = aporteDetalle.Titulo;
                    NotaEntry.Text = aporteDetalle.Nota.ToString();
                    DescripcionEditor.Text = aporteDetalle.Descripcion;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        FormularioAporte.IsVisible = true;
                        NotasContainer.IsVisible = false;
                    });
                }
                else
                {
                    string bindingType = bindingContext?.GetType().FullName ?? "null";
                    Console.WriteLine($"❌ BindingContext del elemento no es un AporteDetalle. Tipo recibido: {bindingType}");
                }
            }
            else
            {
                string senderType = sender?.GetType().FullName ?? "null";
                Console.WriteLine($"❌ Sender no es un VisualElement. Tipo recibido: {senderType}");
            }
        }














        private async void OnGuardarAporte(object sender, EventArgs e)
        {
            if (_aporteSeleccionado == null) return;

            _aporteSeleccionado.Titulo = TituloEntry.Text;
            _aporteSeleccionado.Nota = NotaEntry.Text;
            _aporteSeleccionado.Descripcion = DescripcionEditor.Text;

            //bool actualizado = await _apiService.ActualizarAporteAsync(_aporteSeleccionado);

            //if (actualizado)
            //{
            //    await DisplayAlert("Éxito", "Aporte actualizado correctamente.", "OK");
            //    FormularioAporte.IsVisible = false;
            //    NotasContainer.IsVisible = true;
            //}
            //else
            //{
            //    await DisplayAlert("Error", "No se pudo actualizar el aporte.", "OK");
            //}
        }

        private async void OnEliminarAporte(object sender, EventArgs e)
        {
            if (_aporteSeleccionado == null) return;

            bool confirmar = await DisplayAlert("Confirmar", "¿Estás seguro de eliminar este aporte?", "Sí", "No");

            if (confirmar)
            {
                //bool eliminado = await _apiService.EliminarAporteAsync(_aporteSeleccionado.Id);

                //if (eliminado)
                //{
                //    await DisplayAlert("Éxito", "Aporte eliminado correctamente.", "OK");
                //    FormularioAporte.IsVisible = false;
                //    NotasContainer.IsVisible = true;
                //}
                //else
                //{
                //    await DisplayAlert("Error", "No se pudo eliminar el aporte.", "OK");
                //}
            }
        }

        private void OnCancelarEdicion(object sender, EventArgs e)
        {
            FormularioAporte.IsVisible = false;
            NotasContainer.IsVisible = true;
            _aporteSeleccionado = null;
        }
    }
}
