﻿using System;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace RGRConverter
{
    public static class ControlsHelper
    {
        public static void InvalidateMeasure(this VisualElement control)
        {
            
            if (control == null)
                throw new ArgumentNullException(nameof(control));
            var methods = typeof(VisualElement).GetTypeInfo().DeclaredMethods;
            var method = methods.FirstOrDefault(x => x.Name == "InvalidateMeasure");
            if (method != null)
                method.Invoke(control, null);
        }
    }
}