using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace QuaternionObjects.Serialization
{
    public class Vector3Converter : TypeConverterBase
    {
        public override Type[] ConstructorArgumentTypes { get { return new Type[] { typeof(float), typeof(float), typeof(float) }; } }
        public override string[] PropertyNames { get { return new string[] { "x", "y", "z" }; } }
        public override Type TargetType { get { return typeof(Vector3); } }
        public override object[] GetConstructorArguments(object value)
        {
            return ((Vector3)value).ToObjectArray();
        }
        public override object Parse(string text, CultureInfo culture)
        {
            return Vector3.Parse(text);
        }
        public override string GetParsableString(object value)
        {
            return ((Vector3)value).ToString();
        }
        public override object CreateInstance(ITypeDescriptorContext context, IDictionary vals)
        {
            return new Vector3((float)vals["x"], (float)vals["y"], (float)vals["z"]);
        }
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return ((ICustomTypeDescriptor)value).GetProperties();
        }
    }
}
