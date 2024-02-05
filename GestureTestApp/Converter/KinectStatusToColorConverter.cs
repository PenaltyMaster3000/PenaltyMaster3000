using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace KinectSensorStreams.Converter
{
    /// <summary>
    /// Converter permettant de convertir un bool (état du Kinect) en une couleur (Ellipse de la page principale)
    /// </summary>
    public class KinectStatusToColorConverter : IValueConverter
    {
        /// <summary>
        /// Méthode pour convertir la valeur reçue
        /// </summary>
        /// <param name="value">Valeur à convertir (boolean)</param>
        /// <param name="targetType">Type de la valeur à retourner (BrushColor)</param>
        /// <param name="parameter">Paramètre (non utilisé ici)</param>
        /// <param name="culture">Culture (non utilisée ici)</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool status && targetType == typeof(Brush))
            {
                return status ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);
            }

            return new SolidColorBrush(Colors.Transparent);
        }

        /// <summary>
        /// Méthode pour convertir à l'inverse
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
