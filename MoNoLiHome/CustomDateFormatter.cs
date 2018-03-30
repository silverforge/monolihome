using System;
using System.Globalization;

namespace MoNoLiHome
{
    class CustomDateFormatter : IFormatProvider
    {
        readonly IFormatProvider _basedOn;
        readonly string _shortDatePattern;

        public CustomDateFormatter(string shortDatePattern, IFormatProvider basedOn)
        {
            _shortDatePattern = shortDatePattern;
            _basedOn = basedOn;
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(DateTimeFormatInfo))
            {
                var basedOnFormatInfo = (DateTimeFormatInfo)_basedOn.GetFormat(formatType);
                var dateFormatInfo = (DateTimeFormatInfo)basedOnFormatInfo.Clone();
                dateFormatInfo.ShortDatePattern = _shortDatePattern;
                return dateFormatInfo;
            }
            return _basedOn;
        }
    }
}
