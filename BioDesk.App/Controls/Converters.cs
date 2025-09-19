using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BioDesk.App.Controls;

/// <summary>
/// Converter para ajustar tamanho da fonte baseado no tamanho do logo
/// </summary>
public class SizeToFontSizeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double size)
        {
            // Font size é aproximadamente 50% do tamanho do container
            return size * 0.5;
        }
        return 28.0; // Default
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converter para inverter boolean para Visibility
/// </summary>
public class InverseBooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue ? Visibility.Collapsed : Visibility.Visible;
        }
        return Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}