using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Binding_AndroidTreeView_Sample.Extensions
{
    public static class ObjectTypeHelper
    {
        public static T Cast<T>(this Java.Lang.Object obj) where T : class
        {
            var type = obj.GetType();
            var propertyInfo = obj.GetType().GetProperty("Obj");
            return propertyInfo == null ? null : propertyInfo.GetValue(obj, null) as T;
        }
    }
}