using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utils
{
    public class Utils
    {
        public static T TrimWhiteSpace<T>(T obj)
        {
            if (obj != null)
            {
                PropertyInfo[] properties = obj!.GetType().GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    try
                    {
                        if (property.PropertyType == typeof(string))
                        {
                            var o = property.GetValue(obj, null) ?? "";
                            string s = (string)o;
                            property.SetValue(obj, s.Trim());
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception("Error converting field " + property.Name);
                    }
                }

            }
            return obj;
        }
    }
}
