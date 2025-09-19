using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BioDesk.App.Controls
{
    public partial class SignatureCanvas : UserControl
    {
        public event EventHandler<SignatureEventArgs>? SignatureCompleted;
        public event EventHandler? SignatureCancelled;
        public event EventHandler? SignatureCleared;

        private bool _hasSignature = false;

        public SignatureCanvas()
        {
            InitializeComponent();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            SignatureInkCanvas.StrokeCollected += OnStrokeCollected;
            SignatureInkCanvas.MouseDown += OnCanvasMouseDown;
            SignatureInkCanvas.TouchDown += OnCanvasTouchDown;
            SignatureInkCanvas.StylusDown += OnCanvasStylusDown;
        }

        private void OnStrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            UpdateSignatureStatus();
        }

        private void OnCanvasMouseDown(object sender, MouseButtonEventArgs e)
        {
            HideWatermark();
        }

        private void OnCanvasTouchDown(object? sender, TouchEventArgs e)
        {
            HideWatermark();
        }

        private void OnCanvasStylusDown(object sender, StylusDownEventArgs e)
        {
            HideWatermark();
        }

        private void HideWatermark()
        {
            if (WatermarkText.Visibility == Visibility.Visible)
            {
                WatermarkText.Visibility = Visibility.Collapsed;
            }
        }

        private void ShowWatermark()
        {
            if (!_hasSignature)
            {
                WatermarkText.Visibility = Visibility.Visible;
            }
        }

        private void UpdateSignatureStatus()
        {
            _hasSignature = SignatureInkCanvas.Strokes.Count > 0;
            
            if (_hasSignature)
            {
                StatusText.Text = "Assinatura capturada";
                StatusText.Foreground = new SolidColorBrush(Colors.Green);
                ConfirmarButton.IsEnabled = true;
                
                // Show signature info
                var strokeCount = SignatureInkCanvas.Strokes.Count;
                var bounds = SignatureInkCanvas.Strokes.GetBounds();
                SignatureInfo.Text = $"({strokeCount} traços, {bounds.Width:F0}x{bounds.Height:F0}px)";
                
                WatermarkText.Visibility = Visibility.Collapsed;
            }
            else
            {
                StatusText.Text = "Aguardando assinatura";
                StatusText.Foreground = new SolidColorBrush(Colors.Gray);
                ConfirmarButton.IsEnabled = false;
                SignatureInfo.Text = "";
                ShowWatermark();
            }
        }

        private void LimparAssinatura_Click(object sender, RoutedEventArgs e)
        {
            ClearSignature();
        }

        private void TestarAssinatura_Click(object sender, RoutedEventArgs e)
        {
            // Add a test signature for demonstration
            var testStroke = CreateTestSignature();
            SignatureInkCanvas.Strokes.Add(testStroke);
            UpdateSignatureStatus();
        }

        private void CancelarAssinatura_Click(object sender, RoutedEventArgs e)
        {
            SignatureCancelled?.Invoke(this, EventArgs.Empty);
        }

        private void ConfirmarAssinatura_Click(object sender, RoutedEventArgs e)
        {
            if (_hasSignature)
            {
                try
                {
                    var signatureData = CaptureSignature();
                    var eventArgs = new SignatureEventArgs
                    {
                        SignatureData = signatureData,
                        SignatureImage = CaptureSignatureImage(),
                        Timestamp = DateTime.Now,
                        StrokeCount = SignatureInkCanvas.Strokes.Count,
                        Bounds = SignatureInkCanvas.Strokes.GetBounds()
                    };

                    SignatureCompleted?.Invoke(this, eventArgs);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao processar assinatura: {ex.Message}", 
                                   "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void ClearSignature()
        {
            SignatureInkCanvas.Strokes.Clear();
            UpdateSignatureStatus();
            SignatureCleared?.Invoke(this, EventArgs.Empty);
        }

        public bool HasSignature => _hasSignature;

        public byte[] CaptureSignature()
        {
            if (!_hasSignature) return Array.Empty<byte>();

            using var stream = new MemoryStream();
            SignatureInkCanvas.Strokes.Save(stream);
            return stream.ToArray();
        }

        public BitmapSource CaptureSignatureImage()
        {
            if (!_hasSignature) throw new InvalidOperationException("Nenhuma assinatura para capturar");

            var bounds = SignatureInkCanvas.Strokes.GetBounds();
            var width = Math.Max((int)bounds.Width + 40, 200);
            var height = Math.Max((int)bounds.Height + 40, 100);

            // Criar um visual temporário com o canvas
            var tempCanvas = new InkCanvas();
            tempCanvas.Width = width;
            tempCanvas.Height = height;
            tempCanvas.Background = Brushes.White;
            tempCanvas.Strokes = SignatureInkCanvas.Strokes.Clone();

            tempCanvas.Measure(new Size(width, height));
            tempCanvas.Arrange(new Rect(0, 0, width, height));

            var renderTarget = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            renderTarget.Render(tempCanvas);

            return renderTarget;
        }

        public void LoadSignature(byte[] signatureData)
        {
            try
            {
                if (signatureData?.Length > 0)
                {
                    using var stream = new MemoryStream(signatureData);
                    var strokes = new StrokeCollection(stream);
                    SignatureInkCanvas.Strokes.Clear();
                    SignatureInkCanvas.Strokes.Add(strokes);
                    UpdateSignatureStatus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar assinatura: {ex.Message}", 
                               "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Stroke CreateTestSignature()
        {
            // Create a simple test signature
            var points = new StylusPointCollection
            {
                new StylusPoint(50, 100),
                new StylusPoint(80, 80),
                new StylusPoint(120, 120),
                new StylusPoint(150, 90),
                new StylusPoint(180, 110),
                new StylusPoint(200, 85)
            };

            var drawingAttributes = new DrawingAttributes
            {
                Color = Colors.Blue,
                Width = 2,
                Height = 2,
                StylusTip = StylusTip.Ellipse
            };

            return new Stroke(points, drawingAttributes);
        }
    }

    public class SignatureEventArgs : EventArgs
    {
        public byte[] SignatureData { get; set; } = Array.Empty<byte>();
        public BitmapSource? SignatureImage { get; set; }
        public DateTime Timestamp { get; set; }
        public int StrokeCount { get; set; }
        public Rect Bounds { get; set; }
    }
}