﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Natsurainko.FluentCore.Extension.Windows.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natsurainko.FluentLauncher.Converters;

public class JavaInfoConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var file = (string)value;

        if (string.IsNullOrEmpty(file)) 
            return null;
        var info = JavaHelper.GetJavaRuntimeInfo(file);

        return $"{info.Name} ({info.Architecture}, {info.Version})";
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}