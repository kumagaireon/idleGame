﻿// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY UnitGenerator. DO NOT CHANGE IT.
// </auto-generated>
#pragma warning disable CS8669
using System;
using System.Globalization;
#if NET7_0_OR_GREATER
using System.Numerics;
#endif
using System.Text.Json;
using System.Text.Json.Serialization;
namespace IdolGame.EnterpriseBusinessRules
{
    [JsonConverter(typeof(IdolGroupImagelogoPathJsonConverter))]
    [System.ComponentModel.TypeConverter(typeof(IdolGroupImagelogoPathTypeConverter))]
    readonly partial struct IdolGroupImagelogoPath 
        : IEquatable<IdolGroupImagelogoPath>
#if NET8_0_OR_GREATER
        , IEqualityOperators<IdolGroupImagelogoPath, IdolGroupImagelogoPath, bool>
#endif
    {
        readonly string value;

        public string AsPrimitive() => value;

        public IdolGroupImagelogoPath(string value)
        {
            this.value = value;
        }
        
        public static implicit operator string(IdolGroupImagelogoPath value)
        {
            return value.value;
        }

        public static implicit operator IdolGroupImagelogoPath(string value)
        {
            return new IdolGroupImagelogoPath(value);
        }

        public bool Equals(IdolGroupImagelogoPath other)
        {
            return value.Equals(other.value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var t = obj.GetType();
            if (t == typeof(IdolGroupImagelogoPath))
            {
                return Equals((IdolGroupImagelogoPath)obj);
            }
            if (t == typeof(string))
            {
                return value.Equals((string)obj);
            }

            return value.Equals(obj);
        }
        
        public static bool operator ==(IdolGroupImagelogoPath x, IdolGroupImagelogoPath y)
        {
            return x.value.Equals(y.value);
        }

        public static bool operator !=(IdolGroupImagelogoPath x, IdolGroupImagelogoPath y)
        {
            return !x.value.Equals(y.value);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString() => value == null ? "null" : value.ToString(); 

        // UnitGenerateOptions.JsonConverter
        
        private class IdolGroupImagelogoPathJsonConverter : JsonConverter<IdolGroupImagelogoPath>
        {
            public override void Write(Utf8JsonWriter writer, IdolGroupImagelogoPath value, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(string)) as JsonConverter<string>;
                if (converter != null)
                {
                    converter.Write(writer, value.value, options);
                }
                else
                {
                    throw new JsonException($"{typeof(string)} converter does not found.");
                }
            }

            public override IdolGroupImagelogoPath Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(string)) as JsonConverter<string>;
                if (converter != null)
                {
                    return new IdolGroupImagelogoPath(converter.Read(ref reader, typeToConvert, options));
                }
                else
                {
                    throw new JsonException($"{typeof(string)} converter does not found.");
                }
            }

        }

        // Default
        
        private class IdolGroupImagelogoPathTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(IdolGroupImagelogoPath);
            private static readonly Type ValueType = typeof(string);

            public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == WrapperType || sourceType == ValueType)
                {
                    return true;
                }

                return base.CanConvertFrom(context, sourceType);
            }

            public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == WrapperType || destinationType == ValueType)
                {
                    return true;
                }

                return base.CanConvertTo(context, destinationType);
            }

            public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                if (value != null)
                {
                    var t = value.GetType();
                    if (t == typeof(IdolGroupImagelogoPath))
                    {
                        return (IdolGroupImagelogoPath)value;
                    }
                    if (t == typeof(string))
                    {
                        return new IdolGroupImagelogoPath((string)value);
                    }
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (value is IdolGroupImagelogoPath wrappedValue)
                {
                    if (destinationType == WrapperType)
                    {
                        return wrappedValue;
                    }

                    if (destinationType == ValueType)
                    {
                        return wrappedValue.AsPrimitive();
                    }
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }
}
