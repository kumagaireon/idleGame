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
    [JsonConverter(typeof(ARankVoicePathJsonConverter))]
    [System.ComponentModel.TypeConverter(typeof(ARankVoicePathTypeConverter))]
    readonly partial struct ARankVoicePath 
        : IEquatable<ARankVoicePath>
#if NET8_0_OR_GREATER
        , IEqualityOperators<ARankVoicePath, ARankVoicePath, bool>
#endif
    {
        readonly string value;

        public string AsPrimitive() => value;

        public ARankVoicePath(string value)
        {
            this.value = value;
        }
        
        public static implicit operator string(ARankVoicePath value)
        {
            return value.value;
        }

        public static implicit operator ARankVoicePath(string value)
        {
            return new ARankVoicePath(value);
        }

        public bool Equals(ARankVoicePath other)
        {
            return value.Equals(other.value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var t = obj.GetType();
            if (t == typeof(ARankVoicePath))
            {
                return Equals((ARankVoicePath)obj);
            }
            if (t == typeof(string))
            {
                return value.Equals((string)obj);
            }

            return value.Equals(obj);
        }
        
        public static bool operator ==(ARankVoicePath x, ARankVoicePath y)
        {
            return x.value.Equals(y.value);
        }

        public static bool operator !=(ARankVoicePath x, ARankVoicePath y)
        {
            return !x.value.Equals(y.value);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString() => value == null ? "null" : value.ToString(); 

        // UnitGenerateOptions.JsonConverter
        
        private class ARankVoicePathJsonConverter : JsonConverter<ARankVoicePath>
        {
            public override void Write(Utf8JsonWriter writer, ARankVoicePath value, JsonSerializerOptions options)
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

            public override ARankVoicePath Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(string)) as JsonConverter<string>;
                if (converter != null)
                {
                    return new ARankVoicePath(converter.Read(ref reader, typeToConvert, options));
                }
                else
                {
                    throw new JsonException($"{typeof(string)} converter does not found.");
                }
            }

        }

        // Default
        
        private class ARankVoicePathTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(ARankVoicePath);
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
                    if (t == typeof(ARankVoicePath))
                    {
                        return (ARankVoicePath)value;
                    }
                    if (t == typeof(string))
                    {
                        return new ARankVoicePath((string)value);
                    }
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (value is ARankVoicePath wrappedValue)
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
