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
    [JsonConverter(typeof(AlbumRecommendationJsonConverter))]
    [System.ComponentModel.TypeConverter(typeof(AlbumRecommendationTypeConverter))]
    readonly partial struct AlbumRecommendation 
        : IEquatable<AlbumRecommendation>
#if NET8_0_OR_GREATER
        , IEqualityOperators<AlbumRecommendation, AlbumRecommendation, bool>
#endif
    {
        readonly bool value;

        public bool AsPrimitive() => value;

        public AlbumRecommendation(bool value)
        {
            this.value = value;
        }
        
        public static implicit operator bool(AlbumRecommendation value)
        {
            return value.value;
        }

        public static implicit operator AlbumRecommendation(bool value)
        {
            return new AlbumRecommendation(value);
        }

        public bool Equals(AlbumRecommendation other)
        {
            return value.Equals(other.value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var t = obj.GetType();
            if (t == typeof(AlbumRecommendation))
            {
                return Equals((AlbumRecommendation)obj);
            }
            if (t == typeof(bool))
            {
                return value.Equals((bool)obj);
            }

            return value.Equals(obj);
        }
        
        public static bool operator ==(AlbumRecommendation x, AlbumRecommendation y)
        {
            return x.value.Equals(y.value);
        }

        public static bool operator !=(AlbumRecommendation x, AlbumRecommendation y)
        {
            return !x.value.Equals(y.value);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString() => value.ToString();

       // Default

       public static bool operator true(AlbumRecommendation x)
       {
           return x.value;
       }
        
       public static bool operator false(AlbumRecommendation x)
       {
           return !x.value;
       }
        
       public static bool operator !(AlbumRecommendation x)
       {
           return !x.value;
       }

        // UnitGenerateOptions.JsonConverter
        
        private class AlbumRecommendationJsonConverter : JsonConverter<AlbumRecommendation>
        {
            public override void Write(Utf8JsonWriter writer, AlbumRecommendation value, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(bool)) as JsonConverter<bool>;
                if (converter != null)
                {
                    converter.Write(writer, value.value, options);
                }
                else
                {
                    throw new JsonException($"{typeof(bool)} converter does not found.");
                }
            }

            public override AlbumRecommendation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(bool)) as JsonConverter<bool>;
                if (converter != null)
                {
                    return new AlbumRecommendation(converter.Read(ref reader, typeToConvert, options));
                }
                else
                {
                    throw new JsonException($"{typeof(bool)} converter does not found.");
                }
            }

        }

        // Default
        
        private class AlbumRecommendationTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(AlbumRecommendation);
            private static readonly Type ValueType = typeof(bool);

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
                    if (t == typeof(AlbumRecommendation))
                    {
                        return (AlbumRecommendation)value;
                    }
                    if (t == typeof(bool))
                    {
                        return new AlbumRecommendation((bool)value);
                    }
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (value is AlbumRecommendation wrappedValue)
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
