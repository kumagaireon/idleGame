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
    [JsonConverter(typeof(SRankTextJsonConverter))]
    [System.ComponentModel.TypeConverter(typeof(SRankTextTypeConverter))]
    readonly partial struct SRankText 
        : IEquatable<SRankText>
#if NET8_0_OR_GREATER
        , IEqualityOperators<SRankText, SRankText, bool>
#endif
    {
        readonly string value;

        public string AsPrimitive() => value;

        public SRankText(string value)
        {
            this.value = value;
        }
        
        public static implicit operator string(SRankText value)
        {
            return value.value;
        }

        public static implicit operator SRankText(string value)
        {
            return new SRankText(value);
        }

        public bool Equals(SRankText other)
        {
            return value.Equals(other.value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var t = obj.GetType();
            if (t == typeof(SRankText))
            {
                return Equals((SRankText)obj);
            }
            if (t == typeof(string))
            {
                return value.Equals((string)obj);
            }

            return value.Equals(obj);
        }
        
        public static bool operator ==(SRankText x, SRankText y)
        {
            return x.value.Equals(y.value);
        }

        public static bool operator !=(SRankText x, SRankText y)
        {
            return !x.value.Equals(y.value);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString() => value == null ? "null" : value.ToString(); 

        // UnitGenerateOptions.JsonConverter
        
        private class SRankTextJsonConverter : JsonConverter<SRankText>
        {
            public override void Write(Utf8JsonWriter writer, SRankText value, JsonSerializerOptions options)
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

            public override SRankText Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(string)) as JsonConverter<string>;
                if (converter != null)
                {
                    return new SRankText(converter.Read(ref reader, typeToConvert, options));
                }
                else
                {
                    throw new JsonException($"{typeof(string)} converter does not found.");
                }
            }

        }

        // Default
        
        private class SRankTextTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(SRankText);
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
                    if (t == typeof(SRankText))
                    {
                        return (SRankText)value;
                    }
                    if (t == typeof(string))
                    {
                        return new SRankText((string)value);
                    }
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (value is SRankText wrappedValue)
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
