using System;
using System.Globalization;


namespace NLDotNet.DNN.Modules.MVCTest.Components
{
    public static class MVCTestBase
    {
        /// <summary>
        /// Convertit une string en decimal.
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="objDefaut">valeur retour par defaut si s non decimal</param>
        /// <returns></returns>
        public static bool StringToBool(string _s, bool _default = false)
        {
            bool _val;
            return bool.TryParse(_s, out _val) ? _val : _default;
        }

        /// <summary>
        /// Convertit un string en int
        /// </summary>
        /// <param name="sValeur"></param>
        /// <returns></returns>
        public static int StringToInt(string s, int _default = 0)
        {
            int _val;
            return Int32.TryParse(s, out _val) ? _val : _default;
        }

        /// <summary>
        /// Convertit une string en decimal.
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="_default">valeur retour par defaut si s non decimal</param>
        /// <returns></returns>
        public static double StringToDouble(string sValeur, double _default = double.MinValue)
        {
            double _val;
            return double.TryParse(sValeur, NumberStyles.Number, CultureInfo.CurrentCulture, out _val) ? _val : _default;
        }

        /// <summary>
        /// Convertit une string en decimal.
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="_default">valeur retour par defaut si s non decimal</param>
        /// <returns></returns>
        public static decimal StringToDecimal(string sValeur, decimal _default = 0.0M)
        {
            decimal _val;
            return decimal.TryParse(sValeur, NumberStyles.Number, CultureInfo.CurrentCulture, out _val) ? _val : _default;
        }

        /// <summary>
        /// Convertit une string en datetime
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="objDefaut">valeur retour par defaut si s non datetime - null accepté</param>
        /// <returns></returns>
        public static DateTime StringToDateTime(string objValeur, string _default = "")
        {
            return StringToDateTime(objValeur, StringToDateTime(_default, DateTime.MinValue));
        }

        /// <summary>
        /// Convertit une string en datetime
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="_default">valeur retour par defaut si s non datetime - null accepté</param>
        /// <returns></returns>
        public static DateTime StringToDateTime(string objValeur, DateTime? _default = null)
        {
            DateTime dTmp;
            if (_default == null) { _default = DateTime.MinValue; }

            return DateTime.TryParse(objValeur, CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AdjustToUniversal, out dTmp) ? dTmp : _default.Value;
        }               
    }
}
