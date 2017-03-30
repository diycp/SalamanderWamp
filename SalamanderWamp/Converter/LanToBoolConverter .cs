﻿using SalamanderWamp.UI;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SalamanderWamp.Converter
{
    class LanToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CodePanelWin.ProgramLan lan = (CodePanelWin.ProgramLan)value;
            return lan == (CodePanelWin.ProgramLan)int.Parse(parameter.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isChecked = (bool)value;
            if (!isChecked)
            {
                return null;
            }
            return (CodePanelWin.ProgramLan)int.Parse(parameter.ToString());
        }
    }
}
