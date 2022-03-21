using System;
using System.ComponentModel;
using System.Reflection;

namespace SimpleBookingWidget.Commons.Extension
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum enumValue, string defDesc = "")
        {
            if (enumValue == null)
                return defDesc;

            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            if (fi == null)
                return defDesc;

            object[] attrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attrs != null && attrs.Length > 0)
                return ((DescriptionAttribute)attrs[0]).Description;

            return defDesc;
        }
    }
}
