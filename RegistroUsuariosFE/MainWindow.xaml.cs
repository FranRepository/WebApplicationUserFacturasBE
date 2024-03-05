// MainWindow.xaml.cs
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using RegistroUsuariosFE.Models;

namespace RegistroUsuarios
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Registrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var usuario = new Usuario
                {
                    Nombre = txtNombre.Text,
                    ApellidoPaterno = txtApellidoPaterno.Text,
                    ApellidoMaterno = txtApellidoMaterno.Text,
                    Identificacion = txtIdentificacion.Text
                };

                if (string.IsNullOrWhiteSpace(usuario.Nombre) || string.IsNullOrWhiteSpace(usuario.ApellidoPaterno) || string.IsNullOrWhiteSpace(usuario.Identificacion))
                {
                    txtMensaje.Text = "Por favor, complete todos los campos obligatorios.";
                    return;
                }

                var json = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://localhost:7119/api/DirectorioRestService", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Usuario registrado exitosamente");
                    LimpiarCampos();
                }
                else
                {
                    txtMensaje.Text = $"Error al registrar usuario: {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                txtMensaje.Text = $"Error al registrar usuario: {ex.Message}";
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = string.Empty;
            txtApellidoPaterno.Text = string.Empty;
            txtApellidoMaterno.Text = string.Empty;
            txtIdentificacion.Text = string.Empty;
            txtMensaje.Text = string.Empty;
        }
    }
}
