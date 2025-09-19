using System;
using System.Windows;
using BioDesk.App.Controls;

namespace BioDesk.App.Views
{
    public partial class AssinaturaWindow : Window
    {
        public event EventHandler<SignatureEventArgs>? AssinaturaCompleted;
        public event EventHandler? AssinaturaCancelled;

        public AssinaturaWindow()
        {
            InitializeComponent();
            Title = $"Assinatura Digital - BioDesk PRO";
        }

        private void OnSignatureCompleted(object sender, SignatureEventArgs e)
        {
            AssinaturaCompleted?.Invoke(this, e);
        }

        private void OnSignatureCancelled(object sender, EventArgs e)
        {
            AssinaturaCancelled?.Invoke(this, e);
            Close();
        }

        private void FecharJanela_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            // Cleanup if needed
            AssinaturaCanvas.ClearSignature();
            base.OnClosed(e);
        }
    }
}