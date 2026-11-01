using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using RMP2.ViewModel;

namespace RMP2.View
{
    internal class AppointedTimeToDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(DateTime))
                return value;
            else
                return ((DateTime)value).TimeOfDay;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            NoteVM note = MainVM.Instance.NoteCouch;
            Type receiverType = (Type)parameter;
            if (receiverType == typeof(DateTime))
                return (DateTime)value + note.AppointedTime.TimeOfDay;
            else
                return note.AppointedTime.Date + (TimeSpan)value;
        }
    }
}
