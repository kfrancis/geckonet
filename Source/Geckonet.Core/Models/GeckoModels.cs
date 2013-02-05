#region License, Terms and Conditions
//
// GeckoModels.cs
//
// Authors: Kori Francis <twitter.com/djbyter>
// Copyright (C) 2013 Kori Francis. All rights reserved.
// 
//  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW:
//
//  Permission is hereby granted, free of charge, to any person obtaining a
//  copy of this software and associated documentation files (the "Software"),
//  to deal in the Software without restriction, including without limitation
//  the rights to use, copy, modify, merge, publish, distribute, sublicense,
//  and/or sell copies of the Software, and to permit persons to whom the
//  Software is furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
//  DEALINGS IN THE SOFTWARE.
//
#endregion

namespace Geckonet.Core.Models
{
    #region Imports
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using Newtonsoft.Json;
using System.Collections.Generic;
    using System.Net;
    
    #endregion

    /// <summary>
    /// Response consists of 3 values each with an associated description. The first value will be coloured red, 
    /// the second will be amber value and the third, green. If the value = 0 or is blank, the corresponding 
    /// indicator won’t be displayed. Description is a max 40 characters but you may need to see what fits best
    /// in the available space on the widget.
    /// See <a href="http://docs.geckoboard.com/custom-widgets/rag.html">documentation</a>.
    /// </summary>
    [DataContract(Name = "root", Namespace = ""), XmlRoot("root", Namespace = "")]
    public class GeckoItems
    {
        [XmlElement("item"), JsonProperty("item")]
        [MaxLength(3)]
        public DataItem[] DataItems { get; set; }
    }

    /// <summary>
    /// Response consists of two values. First value is main to be displayed, the second value is used to work out the % change as a secondary stat.
    /// See <a href="http://docs.geckoboard.com/custom-widgets/number.html">documentation</a>.
    /// </summary>
    [DataContract(Name = "root", Namespace = ""), XmlRoot("root", Namespace = "")]
    public class NumberAndSecondaryStat
    {
        /// <summary>
        /// Optional. Setting this to 'true' will show the numerical difference not the percent difference.
        /// </summary>
        [DataMember(Name = "absolute", IsRequired = false), XmlElement("absolute"), JsonProperty("absolute")]
        public bool? Absolute { get; set; }
        public bool ShouldSerializeAbsolute() { return Absolute.HasValue; }

        [DataMember(Name = "type", IsRequired = false), XmlElement("type"), JsonProperty("type")]
        public string Type { get; set; }
        public bool ShouldSerializeType() { return !string.IsNullOrEmpty(Type); }

        [XmlElement("item"), JsonProperty("item")]
        public DataItem[] DataItems { get; set; }
    }

    [DataContract(Name = "item", Namespace = ""), XmlType("item", Namespace = "")]
    public class DataItem
    {
        /// <summary>
        /// The label of this data point
        /// </summary>
        [Required, DataMember(Name = "text", IsRequired = true), XmlElement("text"), JsonProperty("text", Required = Required.AllowNull)]
        public string Text { get; set; }
        public bool ShouldSerializeText() { return !string.IsNullOrEmpty(Text); }

        /// <summary>
        /// The value of this data point
        /// </summary>
        [DataMember(Name = "value", IsRequired = false), XmlElement("value"), JsonProperty("value")]
        [StringLength(40)]
        public decimal? Value { get; set; }
        public bool ShouldSerializeValue() { return Value.HasValue; }

        [DataMember(Name = "type", IsRequired = false), XmlElement("type"), JsonProperty("type")]
        public DataItemType? Type { get; set; }
        public bool ShouldSerializeType() { return Type.HasValue; }

        [DataMember(Name = "prefix", IsRequired = false), XmlElement("prefix"), JsonProperty("prefix")]
        public string Prefix { get; set; }
        public bool ShouldSerializePrefix() { return !string.IsNullOrEmpty(Prefix); }
    }

    [DataContract(Namespace = ""), XmlRoot("root", Namespace = "")]
    public class GeckoMapPoints
    {
        [Required, DataMember(Name = "points", IsRequired = true), XmlElement("points"), JsonProperty("points")]
        public List<MapPoint> Points { get; set; }
    }

    [DataContract(Name = "point", Namespace = ""), XmlType("point", Namespace = "")]
    public class MapPoint 
    {
        [DataMember(Name = "city", IsRequired = false), XmlElement("city"), JsonProperty("city")]
        public MapCity City { get; set; }
        public bool ShouldSerializeCity() { return City != null; }

        [DataMember(Name = "size", IsRequired = false), XmlElement("size"), JsonProperty("size")]
        public int? Size { get; set; }
        public bool ShouldSerializeSize() { return Size.HasValue; }

        [DataMember(Name = "color", IsRequired = false), XmlElement("color"), JsonProperty("color")]
        public string Color { get; set; }
        public bool ShouldSerializeColor() { return !string.IsNullOrEmpty(Color); }

        [DataMember(Name = "cssclass", IsRequired = false), XmlElement("cssclass"), JsonProperty("cssclass")]
        public string CssClass { get; set; }
        public bool ShouldSerializeCssClass() { return !string.IsNullOrEmpty(CssClass); }

        [DataMember(Name = "latitude", IsRequired = false), XmlElement("latitude"), JsonProperty("latitude")]
        public string Latitude { get; set; }
        public bool ShouldSerializeLatitude() { return !string.IsNullOrWhiteSpace(Latitude); }

        [DataMember(Name = "longitude", IsRequired = false), XmlElement("longitude"), JsonProperty("longitude")]
        public string Longitude { get; set; }
        public bool ShouldSerializeLongitude() { return !string.IsNullOrWhiteSpace(Longitude); }

        [DataMember(Name = "host", IsRequired = false), XmlElement("host"), JsonProperty("host")]
        public string Host { get; set; }
        public bool ShouldSerializeHost() { return !string.IsNullOrEmpty(Host); }

        [DataMember(Name = "ip", IsRequired = false), XmlElement("ip"), JsonProperty("ip")]
        public string IP { get; set; }
        public bool ShouldSerializeIP() { return !string.IsNullOrWhiteSpace(IP); }
    }

    [DataContract(Name = "city", Namespace = ""), XmlType("city", Namespace = "")]
    public class MapCity
    {
        [DataMember(Name = "city_name", IsRequired = false), XmlElement("city_name"), JsonProperty("city_name")]
        public string CityName { get; set; }
        public bool ShouldSerializeCityName() { return !string.IsNullOrEmpty(CityName); }

        [DataMember(Name = "country_code", IsRequired = false), XmlElement("country_code"), JsonProperty("country_code")]
        [StringLength(2)]
        public string CountryCode { get; set; }
        public bool ShouldSerializeCountryCode() { return !string.IsNullOrEmpty(CountryCode); }

        [DataMember(Name = "region_code", IsRequired = false), XmlElement("region_code"), JsonProperty("region_code")]
        [StringLength(2)]
        public string RegionCode { get ;set; }
        public bool ShouldSerializeRegionCode() { return !string.IsNullOrEmpty(RegionCode); }
    }

    public enum DataItemType
    {
        None = 0,   // (no corner icon)
        Alert,      // (yellow corner icon)
        Info        // (grey corner icon)
    }
}
