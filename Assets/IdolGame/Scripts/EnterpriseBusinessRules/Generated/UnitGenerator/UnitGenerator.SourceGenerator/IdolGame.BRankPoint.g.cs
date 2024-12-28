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
    [JsonConverter(typeof(BRankPointJsonConverter))]
    [System.ComponentModel.TypeConverter(typeof(BRankPointTypeConverter))]
    readonly partial struct BRankPoint 
        : IEquatable<BRankPoint>
        , IFormattable
#if NET8_0_OR_GREATER
        , IEqualityOperators<BRankPoint, BRankPoint, bool>
#endif
    {
        readonly float value;

        public float AsPrimitive() => value;

        public BRankPoint(float value)
        {
            this.value = value;
        }
        
        public static implicit operator float(BRankPoint value)
        {
            return value.value;
        }

        public static implicit operator BRankPoint(float value)
        {
            return new BRankPoint(value);
        }

        public bool Equals(BRankPoint other)
        {
            return value.Equals(other.value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var t = obj.GetType();
            if (t == typeof(BRankPoint))
            {
                return Equals((BRankPoint)obj);
            }
            if (t == typeof(float))
            {
                return value.Equals((float)obj);
            }

            return value.Equals(obj);
        }
        
        public static bool operator ==(BRankPoint x, BRankPoint y)
        {
            return x.value.Equals(y.value);
        }

        public static bool operator !=(BRankPoint x, BRankPoint y)
        {
            return !x.value.Equals(y.value);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString() => value.ToString();

        public string ToString(string? format, IFormatProvider? formatProvider) => value.ToString(format, formatProvider);

        // UnitGenerateOptions.JsonConverter
        
        private class BRankPointJsonConverter : JsonConverter<BRankPoint>
        {
            public override void Write(Utf8JsonWriter writer, BRankPoint value, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(float)) as JsonConverter<float>;
                if (converter != null)
                {
                    converter.Write(writer, value.value, options);
                }
                else
                {
                    throw new JsonException($"{typeof(float)} converter does not found.");
                }
            }

            public override BRankPoint Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(float)) as JsonConverter<float>;
                if (converter != null)
                {
                    return new BRankPoint(converter.Read(ref reader, typeToConvert, options));
                }
                else
                {
                    throw new JsonException($"{typeof(float)} converter does not found.");
                }
            }

        }

        // Default
        
        private class BRankPointTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(BRankPoint);
            private static readonly Type ValueType = typeof(float);

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
                    if (t == typeof(BRankPoint))
                    {
                        return (BRankPoint)value;
                    }
                    if (t == typeof(float))
                    {
                        return new BRankPoint((float)value);
                    }
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (value is BRankPoint wrappedValue)
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