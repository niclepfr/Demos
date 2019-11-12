using System;
using System.Reflection;
using NLDotNet.DNN.Modules.MVCTest.Components;

using System.Web.UI.WebControls;

namespace NLDotNet.DNN.Modules.MVCTest.Components.Data.PetaPoco
{
    public static class PPUtils
    {
        /// <summary>
        /// Pour les champs Nullable
        /// Méthode transformant les valeurs null des propriétés d'un objet en valeur DBNull
        /// </summary>
        /// <param name="objSrc">Objet en entrée</param>
        /// <returns></returns>
        public static object NullableToDbNull(object objSrc)
        {
            if (objSrc == null) { return objSrc; }

            PropertyInfo[] objSrcProps = null;
            objSrcProps = ((Type)objSrc.GetType()).GetProperties();
            foreach (PropertyInfo pi in objSrcProps)
            {
                if ((pi.CanWrite) && (pi.CanRead) && (Nullable.GetUnderlyingType(pi.PropertyType) != null))
                {
                    if (pi.PropertyType.FullName.IndexOf("System.DateTime") > 0)
                    {
                        if (pi.GetValue(objSrc, null) != null)
                        {
                            var _date = Convert.ToDateTime(pi.GetValue(objSrc, null).ToString());  //DBNull.Null ne peut être convertit en System.Nullable
                            if (_date == DateTime.MinValue) { pi.SetValue(objSrc, null, null); }
                        }
                    }
                }
            }
            return objSrc;
        }
    }
}