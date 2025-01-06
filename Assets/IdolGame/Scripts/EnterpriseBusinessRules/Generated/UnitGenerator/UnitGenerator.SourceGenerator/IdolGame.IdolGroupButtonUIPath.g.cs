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
namespace IdolGame
{
    [JsonConverter(typeof(IdolGroupButtonUIPathJsonConverter))]
    [System.ComponentModel.TypeConverter(typeof(IdolGroupButtonUIPathTypeConverter))]
    readonly partial struct IdolGroupButtonUIPath 
        : IEquatable<IdolGroupButtonUIPath>
#if NET8_0_OR_GREATER
        , IEqualityOperators<IdolGroupButtonUIPath, IdolGroupButtonUIPath, bool>
#endif
    {
        readonly string value;

        public string AsPrimitive() => value;

        public IdolGroupButtonUIPath(string value)
        {
            this.value = value;
        }
        
        public static implicit operator string(IdolGroupButtonUIPath value)
        {
            return value.value;
        }

        public static implicit operator IdolGroupButtonUIPath(string value)
        {
            return new IdolGroupButtonUIPath(value);
        }

        public bool Equals(IdolGroupButtonUIPath other)
        {
            return value.Equals(other.value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var t = obj.GetType();
            if (t == typeof(IdolGroupButtonUIPath))
            {
                return Equals((IdolGroupButtonUIPath)obj);
            }
            if (t == typeof(string))
            {
                return value.Equals((string)obj);
            }

            return value.Equals(obj);
        }
        
        public static bool operator ==(IdolGroupButtonUIPath x, IdolGroupButtonUIPath y)
        {
            return x.value.Equals(y.value);
        }

        public static bool operator !=(IdolGroupButtonUIPath x, IdolGroupButtonUIPath y)
        {
            return !x.value.Equals(y.value);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString() => value == null ? "null" : value.ToString(); 

        // UnitGenerateOptions.JsonConverter
        
        private class IdolGroupButtonUIPathJsonConverter : JsonConverter<IdolGroupButtonUIPath>
        {
            public override void Write(Utf8JsonWriter writer, IdolGroupButtonUIPath value, JsonSerializerOptions options)
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

            public override IdolGroupButtonUIPath Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(string)) as JsonConverter<string>;
                if (converter != null)
                {
                    return new IdolGroupButtonUIPath(converter.Read(ref reader, typeToConvert, options));
                }
                else
                {
                    throw new JsonException($"{typeof(string)} converter does not found.");
                }
            }

        }

        // Default
        
        private class IdolGroupButtonUIPathTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(IdolGroupButtonUIPath);
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
                    if (t == typeof(IdolGroupButtonUIPath))
                    {
                        return (IdolGroupButtonUIPath)value;
                    }
                    if (t == typeof(string))
                    {
                        return new IdolGroupButtonUIPath((string)value);
                    }
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (value is IdolGroupButtonUIPath wrappedValue)
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
