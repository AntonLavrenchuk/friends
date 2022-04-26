using System;
using System.Reflection;

namespace FriendsCore
{
    public static class Helper
    {
        public static void CopyProperties(object source, object destination)
        {
            if (source is null || destination is null)
            { return; }

            var typeSrc = source.GetType();
            var typeDest = destination.GetType();

            var srcProps = typeSrc.GetProperties();
            foreach (PropertyInfo srcProp in srcProps)
            {
                if (!srcProp.CanRead)
                {
                    continue;
                }
                PropertyInfo targetProperty = typeDest.GetProperty(srcProp.Name);
                if (targetProperty == null)
                {
                    continue;
                }
                if (!targetProperty.CanWrite)
                {
                    continue;
                }
                if (targetProperty.GetSetMethod(true) != null && targetProperty.GetSetMethod(true).IsPrivate)
                {
                    continue;
                }
                if ((targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) != 0)
                {
                    continue;
                }
                if (!targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType))
                {
                    continue;
                }
                if(targetProperty.Name.Equals("Id"))
                {
                    continue;
                }
                
                targetProperty.SetValue(destination, srcProp.GetValue(source, null), null);
            }
        }
    }
}
