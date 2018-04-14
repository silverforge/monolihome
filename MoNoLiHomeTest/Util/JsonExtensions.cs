using System;
namespace MoNoLiHomeTest.Util
{
    public static class JsonExtensions
    {
        public static T GetPropertyValue<T>(this object obj, string propertyName)
        {
            return (T)obj.GetType().GetProperty(propertyName).GetValue(obj);
        }
    }
}
