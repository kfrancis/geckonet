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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    #endregion

    #region Custom Widgets
    /// <summary>
    /// https://developer-custom.geckoboard.com/#monitoring
    /// </summary>
    [DataContract(Name = "root", Namespace = ""), XmlRoot("root", Namespace = "")]
    public class GeckoMonitoring
    {
        /// <summary>
        /// A string parameter indicating whether the service is up or down. 
        /// If the value is Up (or any case-insensitive combination thereof), 
        /// the text will be coloured green, and an upward-pointing arrow will 
        /// be displayed. If the value is Down, the text will be red, and a 
        /// downward-pointing arrow will be displayed. If the value is Unreported, 
        /// the text will be white, and no arrow will be displayed.
        /// </summary>
        [DataMember(Name = "status", IsRequired = true), XmlElement("status"), JsonProperty("status", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public MonitoringStatus? Status { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeType() { return Status.HasValue; }
#pragma warning restore 1591

        /// <summary>
        /// (optional) A string indicating when the last downtime happened. 
        /// If status is Down, then this value will be ignored, and displayed 
        /// as NOW. Omitting this value will display a hyphen in its place.
        /// </summary>
        [DataMember(Name = "downtime", IsRequired = false), XmlElement("downtime"), JsonProperty("downtime")]
        public string Downtime { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeDowntime() { return !string.IsNullOrEmpty(Downtime); }
#pragma warning restore 1591

        /// <summary>
        /// (optional) A string indicating the current response time for the 
        /// service. Omitting this value will display a hyphen in its place.
        /// </summary>
        [DataMember(Name = "responseTime", IsRequired = false), XmlElement("responseTime"), JsonProperty("responseTime")]
        public string ResponseTime { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeResponseTime() { return !string.IsNullOrEmpty(ResponseTime); }
#pragma warning restore 1591
    }

    /// <summary>
    /// Is the item being monitored up or down?
    /// </summary>
    public enum MonitoringStatus
    {
        /// <summary>
        /// The item/service is "up"
        /// </summary>
        Up,
        /// <summary>
        /// The item/service is "down"
        /// </summary>
        Down
    }

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
        /// <summary>
        /// The items
        /// </summary>
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
#pragma warning disable 1591
        public bool ShouldSerializeAbsolute() { return Absolute.HasValue; }
#pragma warning restore 1591

        /// <summary>
        /// Set this parameter's value to "reverse" to flip the colours of the up/down 
        /// arrow on the comparison metric so that up is red and down is green.
        /// </summary>
        [DataMember(Name = "type", IsRequired = false), XmlElement("type"), JsonProperty("type")]
        public string Type { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeType() { return !string.IsNullOrEmpty(Type); }
#pragma warning restore 1591

        /// <summary>
        /// The data items
        /// </summary>
        [XmlElement("item"), JsonProperty("item")]
        public DataItem[] DataItems { get; set; }
    }

    /// <summary>
    /// A data item
    /// </summary>
    [DataContract(Name = "item", Namespace = ""), XmlType("item", Namespace = "")]
    public class DataItem
    {
        /// <summary>
        /// The label of this data point
        /// </summary>
        [Required, DataMember(Name = "text", IsRequired = true), XmlElement("text"), JsonProperty("text", Required = Required.AllowNull)]
        public string Text { get; set; }

        /// <summary>
        /// The value of this data point
        /// </summary>
        [DataMember(Name = "value", IsRequired = false), XmlElement("value"), JsonProperty("value")]
        [StringLength(40)]
        public decimal? Value { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeValue() { return Value.HasValue; }
#pragma warning restore 1591

        /// <summary>
        /// Type
        /// </summary>
        [DataMember(Name = "type", IsRequired = false), XmlElement("type"), JsonProperty("type")]
        public DataItemType? Type { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeType() { return Type.HasValue; }
#pragma warning restore 1591

        /// <summary>
        /// If the number you wish to display has units then you can set a prefix value.
        /// This can be a currency symbol such as $, £ or €. The visualisation will 
        /// automatically convert a prefix of % to a suffix.
        /// </summary>
        [DataMember(Name = "prefix", IsRequired = false), XmlElement("prefix"), JsonProperty("prefix")]
        public string Prefix { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializePrefix() { return !string.IsNullOrEmpty(Prefix); }
#pragma warning restore 1591
        
        /// <summary>
        /// Label
        /// </summary>
        [DataMember(Name = "label", IsRequired = false), XmlElement("label"), JsonProperty("label")]
        public string Label { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeLabel() { return !string.IsNullOrEmpty(Label); }
#pragma warning restore 1591
    }

    /// <summary>
    /// Data item type
    /// </summary>
    public enum DataItemType
    {
        /// <summary>
        /// No corner icon
        /// </summary>
        None = 0,
        /// <summary>
        /// Yellow corner icon
        /// </summary>
        Alert,
        /// <summary>
        /// Grey corner icon
        /// </summary>
        Info
    }
    #endregion

    #region Mapping
    /// <summary>
    /// The set of points of the map
    /// </summary>
    [DataContract(Namespace = ""), XmlRoot("root", Namespace = "")]
    public class GeckoMapPoints
    {
        /// <summary>
        /// The list of points
        /// </summary>
        [Required, DataMember(Name = "points", IsRequired = true), XmlElement("points"), JsonProperty("points")]
        public List<MapPoint> Points { get; set; }
    }

    /// <summary>
    /// A single map point
    /// </summary>
    [DataContract(Name = "point", Namespace = ""), XmlType("point", Namespace = "")]
    public class MapPoint
    {
        /// <summary>
        /// City
        /// </summary>
        [DataMember(Name = "city", IsRequired = false), XmlElement("city"), JsonProperty("city")]
        public MapCity City { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeCity() { return City != null; }
#pragma warning restore 1591

        /// <summary>
        /// Size
        /// </summary>
        [DataMember(Name = "size", IsRequired = false), XmlElement("size"), JsonProperty("size")]
        [Range(1, 10)]
        public int? Size { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeSize() { return Size.HasValue; }
#pragma warning restore 1591

        /// <summary>
        /// Color, hex code
        /// </summary>
        [DataMember(Name = "color", IsRequired = false), XmlElement("color"), JsonProperty("color")]
        public string Color { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeColor() { return !string.IsNullOrEmpty(Color); }
#pragma warning restore 1591

        /// <summary>
        /// Css class specified for custom styling
        /// </summary>
        [DataMember(Name = "cssclass", IsRequired = false), XmlElement("cssclass"), JsonProperty("cssclass")]
        public string CssClass { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeCssClass() { return !string.IsNullOrEmpty(CssClass); }
#pragma warning restore 1591

        /// <summary>
        /// Latitude
        /// </summary>
        [DataMember(Name = "latitude", IsRequired = false), XmlElement("latitude"), JsonProperty("latitude")]
        public string Latitude { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeLatitude() { return !string.IsNullOrWhiteSpace(Latitude); }
#pragma warning restore 1591

        /// <summary>
        /// Longitude
        /// </summary>
        [DataMember(Name = "longitude", IsRequired = false), XmlElement("longitude"), JsonProperty("longitude")]
        public string Longitude { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeLongitude() { return !string.IsNullOrWhiteSpace(Longitude); }
#pragma warning restore 1591

        /// <summary>
        /// Host
        /// </summary>
        [DataMember(Name = "host", IsRequired = false), XmlElement("host"), JsonProperty("host")]
        public string Host { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeHost() { return !string.IsNullOrEmpty(Host); }
#pragma warning restore 1591

        /// <summary>
        /// IP Address
        /// </summary>
        [DataMember(Name = "ip", IsRequired = false), XmlElement("ip"), JsonProperty("ip")]
        public string IP { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeIP() { return !string.IsNullOrWhiteSpace(IP); }
#pragma warning restore 1591
    }

    /// <summary>
    /// Map city
    /// </summary>
    [DataContract(Name = "city", Namespace = ""), XmlType("city", Namespace = "")]
    public class MapCity
    {
        /// <summary>
        /// The name of the city
        /// </summary>
        [DataMember(Name = "city_name", IsRequired = false), XmlElement("city_name"), JsonProperty("city_name")]
        public string CityName { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeCityName() { return !string.IsNullOrEmpty(CityName); }
#pragma warning restore 1591

        /// <summary>
        /// ISO 3166-2 Code
        /// </summary>
        [DataMember(Name = "country_code", IsRequired = false), XmlElement("country_code"), JsonProperty("country_code")]
        [StringLength(2)]
        public string CountryCode { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeCountryCode() { return !string.IsNullOrEmpty(CountryCode); }
#pragma warning restore 1591

        /// <summary>
        /// Region code
        /// </summary>
        [DataMember(Name = "region_code", IsRequired = false), XmlElement("region_code"), JsonProperty("region_code")]
        [StringLength(2)]
        public string RegionCode { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeRegionCode() { return !string.IsNullOrEmpty(RegionCode); }
#pragma warning restore 1591
    }
    #endregion

    #region Highchart

    /// <summary>
    /// Highchart
    /// </summary>
    [DataContract(Namespace = ""), XmlRoot("root", Namespace = "")]
    public class GeckoHighchart
    {
        /// <summary>
        /// Chart
        /// </summary>
        [DataMember(Name = "chart", IsRequired = false), XmlElement("chart"), JsonProperty("chart")]
        public HighchartChart Chart { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeChart() { return Chart != null; }
#pragma warning restore 1591

        /// <summary>
        /// Colors
        /// </summary>
        [DataMember(Name = "colors", IsRequired = false), XmlArray("colors"), JsonProperty("colors")]
        public List<string> Colors { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeColors() { return (Colors != null && Colors.Count > 0); }
#pragma warning restore 1591

        /// <summary>
        /// Credits
        /// </summary>
        [DataMember(Name = "credits", IsRequired = false), XmlElement("credits"), JsonProperty("credits")]
        public HighchartCredits Credits { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeCredits() { return Credits != null; }
#pragma warning restore 1591

        /// <summary>
        /// Title
        /// </summary>
        [DataMember(Name = "title", IsRequired = false), XmlElement("title"), JsonProperty("title")]
        public HighchartTitle Title { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeTitle() { return Title != null; }
#pragma warning restore 1591

        /// <summary>
        /// Tool tip
        /// </summary>
        [DataMember(Name = "tooltip", IsRequired = false), XmlElement("tooltip"), JsonProperty("tooltip")]
        public HighchartTooltip Tooltip { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeTooltip() { return Tooltip != null; }
#pragma warning restore 1591

        /// <summary>
        /// Legend
        /// </summary>
        [DataMember(Name = "legend", IsRequired = false), XmlElement("legend"), JsonProperty("legend")]
        public HighchartLegend Legend { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeLegend() { return Legend != null; }
#pragma warning restore 1591

        /// <summary>
        /// Plot options of the chart
        /// </summary>
        [DataMember(Name = "plotOptions", IsRequired = false), XmlElement("plotOptions"), JsonProperty("plotOptions")]
        public HighchartPlotOptions PlotOptions { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializePlotOptions() { return PlotOptions != null; }
#pragma warning restore 1591

        /// <summary>
        /// Series
        /// </summary>
        [DataMember(Name = "series", IsRequired = false), XmlElement("series"), JsonProperty("series")]
        public List<HighchartSeries> Series { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeSeries() { return Series != null; }
#pragma warning restore 1591
    }

    /// <summary>
    /// Highchart Series
    /// </summary>
    [DataContract(Name = "series", Namespace = ""), XmlType("series", Namespace = ""), JsonObject("series")]
    public class HighchartSeries
    {
        /// <summary>
        /// Series type
        /// </summary>
        [DataMember(Name = "type", IsRequired = false), XmlElement("type"), JsonProperty("type")]
        public string Type { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeType() { return !string.IsNullOrWhiteSpace(Type); }
#pragma warning restore 1591

        /// <summary>
        /// Series name
        /// </summary>
        [DataMember(Name = "name", IsRequired = false), XmlElement("name"), JsonProperty("name")]
        public string Name { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeName() { return !string.IsNullOrWhiteSpace(Name); }
#pragma warning restore 1591

        /// <summary>
        /// Series data
        /// </summary>
        [DataMember(Name = "data", IsRequired = false), XmlElement("data"), JsonProperty("data")]
        public List<Dictionary<string, int>> Data { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeData() { return (Data != null) && (Data.Count > 0); }
#pragma warning restore 1591
    }

    /// <summary>
    /// Chart plot options
    /// </summary>
    [DataContract(Name = "plotOptions", Namespace = ""), XmlType("plotOptions", Namespace = "")]
    public class HighchartPlotOptions
    {
        /// <summary>
        /// Pie options
        /// </summary>
        public HighchartPieOptions PieOptions { get; set; }
    }

    /// <summary>
    /// Pie options
    /// </summary>
    [DataContract(Name = "pie", Namespace = ""), XmlType("pie", Namespace = "")]
    public class HighchartPieOptions
    {
        /// <summary>
        /// Animation
        /// </summary>
        [DataMember(Name = "animation", IsRequired = false), XmlElement("animation"), JsonProperty("animation")]
        public bool? Animation { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeAnimation() { return Animation.HasValue; }
#pragma warning restore 1591

        /// <summary>
        /// Allow selection?
        /// </summary>
        [DataMember(Name = "allowPointSelect", IsRequired = false), XmlElement("allowPointSelect"), JsonProperty("allowPointSelect")]
        public bool? AllowPointSelect { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeAllowPointSelect() { return AllowPointSelect.HasValue; }
#pragma warning restore 1591

        /// <summary>
        /// Cursor visible?
        /// </summary>
        [DataMember(Name = "cursor", IsRequired = false), XmlElement("cursor"), JsonProperty("cursor")]
        public string Cursor { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeCursor() { return !string.IsNullOrWhiteSpace(Cursor); }
#pragma warning restore 1591

        /// <summary>
        /// Label options
        /// </summary>
        [DataMember(Name = "dataLabels", IsRequired = false), XmlElement("dataLabels"), JsonProperty("dataLabels")]
        public HighchartDataLabelOptions DataLabelOptions { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeDataLabelOptions() { return DataLabelOptions != null; }
#pragma warning restore 1591

        /// <summary>
        /// Show this in legend?
        /// </summary>
        [DataMember(Name = "showInLegend", IsRequired = false), XmlElement("showInLegend"), JsonProperty("showInLegend")]
        public bool? ShowInLegend { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeShowInLegend() { return ShowInLegend.HasValue; }
#pragma warning restore 1591

        /// <summary>
        /// Size
        /// </summary>
        [DataMember(Name = "size", IsRequired = false), XmlElement("size"), JsonProperty("size")]
        public string Size { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeSize() { return !string.IsNullOrWhiteSpace(Size); }
#pragma warning restore 1591
    }

    /// <summary>
    /// Label options
    /// </summary>
    [DataContract(Name = "dataLabels", Namespace = ""), XmlType("dataLabels", Namespace = "")]
    public class HighchartDataLabelOptions
    {
        /// <summary>
        /// Enabled
        /// </summary>
        [DataMember(Name = "enabled", IsRequired = false), XmlElement("enabled"), JsonProperty("enabled")]
        public bool? Enabled { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeEnabled() { return Enabled.HasValue; }
#pragma warning restore 1591
    }

    /// <summary>
    /// Legend optons
    /// </summary>
    [DataContract(Name = "legend", Namespace = ""), XmlType("legend", Namespace = "")]
    public class HighchartLegend
    {
        /// <summary>
        /// Border color
        /// </summary>
        [DataMember(Name = "borderColor", IsRequired = false), XmlElement("borderColor"), JsonProperty("borderColor")]
        public string BorderColor { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeBorderColor() { return !string.IsNullOrWhiteSpace(BorderColor); }
#pragma warning restore 1591

        /// <summary>
        /// Item width
        /// </summary>
        [DataMember(Name = "itemWidth", IsRequired = false), XmlElement("itemWidth"), JsonProperty("itemWidth")]
        public int? ItemWidth { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeItemWidth() { return ItemWidth.HasValue; }
#pragma warning restore 1591

        /// <summary>
        /// Margin
        /// </summary>
        [DataMember(Name = "margin", IsRequired = false), XmlElement("margin"), JsonProperty("margin")]
        public int? Margin { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeMargin() { return Margin.HasValue; }
#pragma warning restore 1591

        /// <summary>
        /// Width
        /// </summary>
        [DataMember(Name = "width", IsRequired = false), XmlElement("width"), JsonProperty("width")]
        public int? Width { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeWidth() { return Width.HasValue; }
#pragma warning restore 1591
    }

    /// <summary>
    /// Tooltip
    /// </summary>
    [DataContract(Name = "tooltip", Namespace = ""), XmlType("tooltip", Namespace = "")]
    public class HighchartTooltip
    {
        /// <summary>
        /// Formatter
        /// </summary>
        [DataMember(Name = "formatter", IsRequired = false), XmlElement("formatter"), JsonProperty("formatter")]
        public string Formatter { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeFormatter() { return !string.IsNullOrWhiteSpace(Formatter); }
#pragma warning restore 1591
    }

    /// <summary>
    /// Title
    /// </summary>
    [DataContract(Name = "title", Namespace = ""), XmlType("title", Namespace = "")]
    public class HighchartTitle
    {
        /// <summary>
        /// Text
        /// </summary>
        [DataMember(Name = "text", IsRequired = false), XmlElement("text"), JsonProperty("text", Required = Required.AllowNull)]
        public string Text { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeText() { return !string.IsNullOrWhiteSpace(Text); }
#pragma warning restore 1591
    }

    /// <summary>
    /// Credits
    /// </summary>
    [DataContract(Name = "credits", Namespace = ""), XmlType("credits", Namespace = "")]
    public class HighchartCredits
    {
        /// <summary>
        /// Enabled
        /// </summary>
        [DataMember(Name = "enabled", IsRequired = false), XmlElement("enabled"), JsonProperty("enabled")]
        public bool? Enabled { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeEnabled() { return Enabled.HasValue; }
#pragma warning restore 1591
    }

    /// <summary>
    /// Chart
    /// </summary>
    [DataContract(Name = "chart", Namespace = ""), XmlType("chart", Namespace = "")]
    public class HighchartChart
    {
        /// <summary>
        /// Render to
        /// </summary>
        [DataMember(Name = "renderTo", IsRequired = false), XmlElement("renderTo"), JsonProperty("renderTo"), DefaultValue("container")]
        public string RenderTo { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeRenderTo() { return !string.IsNullOrWhiteSpace(RenderTo); }
#pragma warning restore 1591

        /// <summary>
        /// Plot background color
        /// </summary>
        [DataMember(Name = "plotBackgroundColor", IsRequired = false), XmlElement("plotBackgroundColor"), JsonProperty("plotBackgroundColor")]
        public string PlotBackgroundColor { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializePlotBackgroundColor() { return !string.IsNullOrWhiteSpace(PlotBackgroundColor); }
#pragma warning restore 1591

        /// <summary>
        /// Background color
        /// </summary>
        [DataMember(Name = "backgroundColor", IsRequired = false), XmlElement("backgroundColor"), JsonProperty("backgroundColor")]
        public string BackgroundColor { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeBackgroundColor() { return !string.IsNullOrWhiteSpace(BackgroundColor); }
#pragma warning restore 1591

        /// <summary>
        /// Border color
        /// </summary>
        [DataMember(Name = "borderColor", IsRequired = false), XmlElement("borderColor"), JsonProperty("borderColor")]
        public string BorderColor { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeBorderColor() { return !string.IsNullOrWhiteSpace(BorderColor); }
#pragma warning restore 1591

        /// <summary>
        /// Line color
        /// </summary>
        [DataMember(Name = "lineColor", IsRequired = false), XmlElement("lineColor"), JsonProperty("lineColor")]
        public string LineColor { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeLineColor() { return !string.IsNullOrWhiteSpace(LineColor); }
#pragma warning restore 1591

        /// <summary>
        /// Border color
        /// </summary>
        [DataMember(Name = "plotBorderColor", IsRequired = false), XmlElement("plotBorderColor"), JsonProperty("plotBorderColor")]
        public string PlotBorderColor { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializePlotBorderColor() { return !string.IsNullOrWhiteSpace(PlotBorderColor); }
#pragma warning restore 1591

        /// <summary>
        /// Border width
        /// </summary>
        [DataMember(Name = "plotBorderWidth", IsRequired = false), XmlElement("plotBorderWidth"), JsonProperty("plotBorderWidth")]
        public int? PlotBorderWidth { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializePlotBorderWidth() { return PlotBorderWidth.HasValue; }
#pragma warning restore 1591

        /// <summary>
        /// Height
        /// </summary>
        [DataMember(Name = "height", IsRequired = false), XmlElement("height"), JsonProperty("height")]
        public int? Height { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeHeight() { return Height.HasValue; }
#pragma warning restore 1591

        /// <summary>
        /// Should a shadow be used?
        /// </summary>
        [DataMember(Name = "plotShadow", IsRequired = false), XmlElement("plotShadow"), JsonProperty("plotShadow")]
        public bool? PlotShadow { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeSeries() { return PlotShadow.HasValue; }
#pragma warning restore 1591
    }

    #endregion

    #region Pie Chart
    #endregion

    #region Line Chart
    /// <summary>
    /// Line chart
    /// </summary>
    [DataContract(Namespace = ""), XmlRoot("root", Namespace = "")]
    public class GeckoLineChart
    {
        /// <summary>
        /// Chart items
        /// </summary>
        [DataMember(Name = "item", IsRequired = false), XmlElement("item"), JsonProperty("item")]
        public List<string> Items { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeItems() { return (Items != null) && (Items.Count > 0); }
#pragma warning restore 1591

        /// <summary>
        /// Settings
        /// </summary>
        [DataMember(Name = "settings", IsRequired = false), XmlElement("settings"), JsonProperty("settings")]
        public GeckoLineChartSettings Settings { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeGeckoLineChartSettings() { return (Settings != null); }
#pragma warning restore 1591
    }

    /// <summary>
    /// Line chart settings
    /// </summary>
    [DataContract(Name = "settings", Namespace = ""), XmlType("settings", Namespace = "")]
    public class GeckoLineChartSettings
    {
        /// <summary>
        /// X-axis labels
        /// </summary>
        [DataMember(Name = "axisx", IsRequired = false), XmlElement("axisx"), JsonProperty("axisx")]
        public List<string> XAxisLabels { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeXAxisLabels() { return (XAxisLabels != null) && (XAxisLabels.Count > 0); }
#pragma warning restore 1591

        /// <summary>
        /// Y-axis labels
        /// </summary>
        [DataMember(Name = "axisy", IsRequired = false), XmlElement("axisy"), JsonProperty("axisy")]
        public List<string> YAxisLabels { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeYAxisLabels() { return (YAxisLabels != null) && (YAxisLabels.Count > 0); }
#pragma warning restore 1591

        /// <summary>
        /// Color
        /// </summary>
        [DataMember(Name = "colour", IsRequired = false), XmlElement("colour"), JsonProperty("colour")]
        public string Colour { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeColour() { return !string.IsNullOrWhiteSpace(Colour); }
#pragma warning restore 1591
    }
    #endregion

    #region Bar Chart
    /// <summary>
    /// Chart x-axis
    /// </summary>
    [DataContract(Name = "x_axis", Namespace = ""), XmlType("x_axis", Namespace = "")]
    public class GeckoBarChartXAxis
    {
        /// <summary>
        /// Labels
        /// </summary>
        [DataMember(Name = "labels", IsRequired = true), XmlElement("labels"), JsonProperty("labels")]
        public List<string> Labels { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeLabels() { return (Labels != null) && (Labels.Count > 0); }
#pragma warning restore 1591
    }

    /// <summary>
    /// y-axis
    /// </summary>
    [DataContract(Name = "y_axis", Namespace = ""), XmlType("y_axis", Namespace = "")]
    public class GeckoBarChartYAxis
    {
        /// <summary>
        /// Format
        /// </summary>
        [DataMember(Name = "format", IsRequired = false), XmlElement("format"), JsonProperty("format")]
        public string Format { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeFormat() { return !string.IsNullOrWhiteSpace(Format); }
#pragma warning restore 1591

        /// <summary>
        /// Unit
        /// </summary>
        [DataMember(Name = "unit", IsRequired = false), XmlElement("unit"), JsonProperty("unit")]
        public string Unit { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeUnit() { return !string.IsNullOrWhiteSpace(Unit); }
#pragma warning restore 1591
    }

    /// <summary>
    /// Bar chart series
    /// </summary>
    [DataContract(Name = "series", Namespace = ""), XmlType("series", Namespace = "")]
    public class GeckoBarChartSeries
    {
        /// <summary>
        /// Data
        /// </summary>
        [DataMember(Name = "data", IsRequired = true), XmlElement("data"), JsonProperty("data")]
        public List<int> Data { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeData() { return (Data != null) && (Data.Count > 0); }
#pragma warning restore 1591

        /// <summary>
        /// Name
        /// </summary>
        [DataMember(Name = "name", IsRequired = false), XmlElement("name"), JsonProperty("name")]
        public string Name { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeName() { return !string.IsNullOrWhiteSpace(Name); }
#pragma warning restore 1591
    }

    /// <summary>
    /// Custom Widget
    /// </summary>
    [DataContract(Namespace = ""), XmlRoot("root", Namespace = "")]
    public class GeckoBarChart
    {
        /// <summary>
        /// X-axis
        /// </summary>
        [DataMember(Name = "x_axis", IsRequired = false), XmlElement("x_axis"), JsonProperty("x_axis")]
        public GeckoBarChartXAxis XAxis { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeXAxis() { return (XAxis != null); }
#pragma warning restore 1591

        /// <summary>
        /// Y-axis
        /// </summary>
        [DataMember(Name = "y_axis", IsRequired = false), XmlElement("y_axis"), JsonProperty("y_axis")]
        public GeckoBarChartYAxis YAxis { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeYAxis() { return (YAxis != null); }
#pragma warning restore 1591

        /// <summary>
        /// Series
        /// </summary>
        [DataMember(Name = "series", IsRequired = true), XmlElement("series"), JsonProperty("series")]
        public List<GeckoBarChartSeries> Series { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeSeries() { return (Series != null) && (Series.Count > 0); }
#pragma warning restore 1591
    }
    #endregion

    #region GeckoMeter
    /// <summary>
    /// Geckometer
    /// </summary>
    [DataContract(Namespace = ""), XmlRoot("root", Namespace = "")]
    public class GeckoMeterChart
    {
        /// <summary>
        /// Item
        /// </summary>
        [DataMember(Name = "item", IsRequired = false), XmlElement("item"), JsonProperty("item")]
        public decimal? Item { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeItem() { return Item.HasValue; }
#pragma warning restore 1591
        
        /// <summary>
        /// Min
        /// </summary>
        [DataMember(Name = "min", IsRequired = false), XmlElement("min"), JsonProperty("min")]
        public DataItem Min { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeMin() { return Min != null; }
#pragma warning restore 1591

        /// <summary>
        /// Max
        /// </summary>
        [DataMember(Name = "max", IsRequired = false), XmlElement("max"), JsonProperty("max")]
        public DataItem Max { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeMax() { return Max != null; }
#pragma warning restore 1591
        
        /// <summary>
        /// Format
        /// </summary>
        [DataMember(Name = "format", IsRequired = false), XmlElement("format"), JsonProperty("format")]
        public string Format { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeFormat() { return !string.IsNullOrEmpty(Format); }
#pragma warning restore 1591
    }
    #endregion

    #region Bullet
    /// <summary>
    /// Bullet Chart
    /// </summary>
    [DataContract(Namespace = ""), XmlRoot("root", Namespace = "")]
    public class GeckoBulletChart
    {
        /// <summary>
        /// Orientation
        /// </summary>
        [DataMember(Name = "orientation", IsRequired = false), XmlElement("orientation"), JsonProperty("orientation")]
        public string Orientation { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeOrientation() { return !string.IsNullOrWhiteSpace(Orientation); }
#pragma warning restore 1591

        /// <summary>
        /// Item
        /// </summary>
        [DataMember(Name = "item", IsRequired = false), XmlElement("item"), JsonProperty("item")]
        public GeckoBulletItem Item { get; set; }
    }

    /// <summary>
    /// Bullet Item
    /// </summary>
    [DataContract(Name = "item", Namespace = ""), XmlType("item", Namespace = "")]
    public class GeckoBulletItem
    {
        /// <summary>
        /// Label
        /// </summary>
        [DataMember(Name = "label", IsRequired = false), XmlElement("label"), JsonProperty("label")]
        public string Label { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeLabel() { return !string.IsNullOrWhiteSpace(Label); }
#pragma warning restore 1591

        /// <summary>
        /// Sub label
        /// </summary>
        [DataMember(Name = "sublabel", IsRequired = false), XmlElement("sublabel"), JsonProperty("sublabel")]
        public string SubLabel { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeSubLabel() { return !string.IsNullOrWhiteSpace(SubLabel); }
#pragma warning restore 1591

        /// <summary>
        /// Axis
        /// </summary>
        [DataMember(Name = "axis", IsRequired = false), XmlElement("axis"), JsonProperty("axis")]
        public GeckoBulletAxis Axis { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeAxis() { return Axis != null; }
#pragma warning restore 1591

        /// <summary>
        /// Range
        /// </summary>
        [DataMember(Name = "range", IsRequired = false), XmlElement("range"), JsonProperty("range")]
        public List<GeckoBulletRangeItem> Range { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeRange() { return Range != null; }
#pragma warning restore 1591

        /// <summary>
        /// Measure
        /// </summary>
        [DataMember(Name = "measure", IsRequired = false), XmlElement("measure"), JsonProperty("measure")]
        public GeckoBulletMeasure Measure { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeMeasure() { return Measure != null; }
#pragma warning restore 1591

        /// <summary>
        /// Comparative
        /// </summary>
        [DataMember(Name = "comparative", IsRequired = false), XmlElement("comparative"), JsonProperty("comparative")]
        public GeckoBulletPointString Comparative { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeComparative() { return Comparative != null; }
#pragma warning restore 1591
    }

    /// <summary>
    /// Bullet Axis
    /// </summary>
    [DataContract(Name = "point", Namespace = ""), XmlType("point", Namespace = "")]
    public class GeckoBulletAxis
    {
        /// <summary>
        /// Points
        /// </summary>
        [DataMember(Name = "point", IsRequired = false), XmlElement("point"), JsonProperty("point")]
        public List<string> Points { get; set; }
    }

    /// <summary>
    /// Bullet Measure
    /// </summary>
    [DataContract(Name = "measure", Namespace = ""), XmlType("measure", Namespace = "")]
    public class GeckoBulletMeasure
    {
        /// <summary>
        /// Current
        /// </summary>
        [DataMember(Name = "current", IsRequired = false), XmlElement("current"), JsonProperty("current")]
        public GeckoBulletRangeItemString Current { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeCurrent() { return Current != null; }
#pragma warning restore 1591

        /// <summary>
        /// Projected
        /// </summary>
        [DataMember(Name = "projected", IsRequired = false), XmlElement("projected"), JsonProperty("projected")]
        public GeckoBulletRangeItemString Projected { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeProjected() { return Projected != null; }
#pragma warning restore 1591
    }

    /// <summary>
    /// Bullet Range
    /// </summary>
    [Obsolete("Range should now be List<GeckoBulletRangeItem>, please change your code")]
    [DataContract(Name = "range", Namespace = ""), XmlType("range", Namespace = "")]
    public class GeckoBulletRange
    {
        /// <summary>
        /// Red
        /// </summary>
        [DataMember(Name = "red", IsRequired = false), XmlElement("red"), JsonProperty("red")]
        public GeckoBulletRangeItem Red { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeRed() { return Red != null; }
#pragma warning restore 1591

        /// <summary>
        /// Amber
        /// </summary>
        [DataMember(Name = "amber", IsRequired = false), XmlElement("amber"), JsonProperty("amber")]
        public GeckoBulletRangeItem Amber { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeAmber() { return Amber != null; }
#pragma warning restore 1591

        /// <summary>
        /// Green
        /// </summary>
        [DataMember(Name = "green", IsRequired = false), XmlElement("green"), JsonProperty("green")]
        public GeckoBulletRangeItem Green { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeGreen() { return Green != null; }
#pragma warning restore 1591
    }

    /// <summary>
    /// Bullet Range Item
    /// </summary>
    public class GeckoBulletRangeItem
    {
        /// <summary>
        /// Color
        /// </summary>
        [DataMember(Name = "color", IsRequired = false), XmlElement("color"), JsonProperty("color")]
        public string Color { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeColor() { return !string.IsNullOrWhiteSpace(Color); }
#pragma warning restore 1591

        /// <summary>
        /// Start
        /// </summary>
        [DataMember(Name = "start", IsRequired = false), XmlElement("start"), JsonProperty("start")]
        public int? Start { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeStart() { return Start.HasValue; }
#pragma warning restore 1591

        /// <summary>
        /// End
        /// </summary>
        [DataMember(Name = "end", IsRequired = false), XmlElement("end"), JsonProperty("end")]
        public int? End { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeEnd() { return End.HasValue; }
#pragma warning restore 1591
    }

    /// <summary>
    /// Range Item
    /// </summary>
    public class GeckoBulletRangeItemString
    {
        /// <summary>
        /// Color
        /// </summary>
        [DataMember(Name = "color", IsRequired = false), XmlElement("color"), JsonProperty("color")]
        public string Color { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeColor() { return !string.IsNullOrWhiteSpace(Color); }
#pragma warning restore 1591

        /// <summary>
        /// Start
        /// </summary>
        [DataMember(Name = "start", IsRequired = false), XmlElement("start"), JsonProperty("start")]
        public string Start { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeStart() { return !string.IsNullOrWhiteSpace(Start); }
#pragma warning restore 1591

        /// <summary>
        /// End
        /// </summary>
        [DataMember(Name = "end", IsRequired = false), XmlElement("end"), JsonProperty("end")]
        public string End { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeEnd() { return !string.IsNullOrWhiteSpace(End); }
#pragma warning restore 1591
    }

    /// <summary>
    /// Bullet Point
    /// </summary>
    [DataContract(Name = "point", Namespace = ""), XmlType("point", Namespace = "")]
    public class GeckoBulletPoint
    {
        /// <summary>
        /// Point
        /// </summary>
        [DataMember(Name = "point", IsRequired = false), XmlElement("point"), JsonProperty("point")]
        public int? Point { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializePoint() { return Point.HasValue; }
#pragma warning restore 1591
    }

    /// <summary>
    /// Bullet Point String
    /// </summary>
    [DataContract(Name = "point", Namespace = ""), XmlType("point", Namespace = "")]
    public class GeckoBulletPointString
    {
        /// <summary>
        /// Point
        /// </summary>
        [DataMember(Name = "point", IsRequired = false), XmlElement("point"), JsonProperty("point")]
        public string Point { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializePoint() { return !string.IsNullOrWhiteSpace(Point); }
#pragma warning restore 1591
    }
    #endregion

    #region Funnel
    /// <summary>
    /// Funnel Chart
    /// </summary>
    [DataContract(Namespace = ""), XmlRoot("root", Namespace = "")]
    public class GeckoFunnelChart
    {
        /// <summary>
        /// Type
        /// </summary>
        [DataMember(Name = "type", IsRequired = false), XmlElement("type"), JsonProperty("type")]
        public string Type { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeType() { return !string.IsNullOrWhiteSpace(Type); }
#pragma warning restore 1591

        /// <summary>
        /// Percentage
        /// </summary>
        [DataMember(Name = "percentage", IsRequired = false), XmlElement("percentage"), JsonProperty("percentage")]
        public string PercentageMode { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializePercentageMode() { return !string.IsNullOrWhiteSpace(PercentageMode); }
#pragma warning restore 1591

        /// <summary>
        /// Items
        /// </summary>
        [DataMember(Name = "item", IsRequired = false), XmlElement("item"), JsonProperty("item")]
        public List<DataItem> Items { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeItems() { return (Items != null) && (Items.Count > 0); }
#pragma warning restore 1591
    }
    #endregion

    #region Leaderboard
    /// <summary>
    /// Leaderboard item
    /// </summary>
    [DataContract(Name = "item", Namespace = ""), XmlType("item", Namespace = "")]
    public class GeckoLeaderboardItem
    {
        /// <summary>
        /// Label
        /// </summary>
        [DataMember(Name = "label", IsRequired = true), XmlElement("label"), JsonProperty("label")]
        public string Label { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeLabel() { return !string.IsNullOrEmpty(Label); }
#pragma warning restore 1591

        /// <summary>
        /// Value
        /// </summary>
        [DataMember(Name = "value", IsRequired = true), XmlElement("value"), JsonProperty("value")]
        public int Value { get; set; }

        /// <summary>
        /// Previous Rank
        /// </summary>
        [DataMember(Name = "previous_rank", IsRequired = true), XmlElement("previous_rank"), JsonProperty("previous_rank")]
        public int PreviousRank { get; set; }
    }

    /// <summary>
    /// Custom Widget
    /// </summary>
    [DataContract(Namespace = ""), XmlRoot("root", Namespace = "")]
    public class GeckoLeaderboard
    {
        /// <summary>
        /// Items
        /// </summary>
        [DataMember(Name = "items", IsRequired = true), XmlElement("items"), JsonProperty("items")]
        public List<GeckoLeaderboardItem> Items { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeItems() { return (Items != null) && (Items.Count > 0); }
#pragma warning restore 1591
    }
    #endregion

    #region List
    /// <summary>
    /// Gecko list
    /// </summary>
    [DataContract(Namespace = ""), XmlRoot("root", Namespace = "")]
    public class GeckoList : List<DataListItem>
    {
    }

    /// <summary>
    /// A list item datum
    /// </summary>
    [DataContract(Namespace = ""), XmlType(Namespace = "")]
    public class DataListItem
    {
        /// <summary>
        /// The label of this data point
        /// </summary>
        [Required, DataMember(Name = "title", IsRequired = true), XmlElement("title"), JsonProperty("title", Required = Required.AllowNull)]
        public DataListItemTitle Title { get; set; }

        /// <summary>
        /// Label
        /// </summary>
        [DataMember(Name = "label", IsRequired = false), XmlElement("label"), JsonProperty("label")]
        public DataListItemLabel Label { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeLabel() { return Label != null; }
#pragma warning restore 1591

        /// <summary>
        /// Description
        /// </summary>
        [DataMember(Name = "description", IsRequired = false), XmlElement("description"), JsonProperty("description")]
        public string Description { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeDescription() { return !string.IsNullOrEmpty(Description); }
#pragma warning restore 1591
    }

    /// <summary>
    /// List item label data
    /// </summary>
    public class DataListItemLabel
    {
        /// <summary>
        /// The text of this label
        /// </summary>
        [Required, DataMember(Name = "name", IsRequired = true), XmlElement("name"), JsonProperty("name", Required = Required.AllowNull)]
        public string Name { get; set; }

        /// <summary>
        /// Color, in hex
        /// </summary>
        [DataMember(Name = "color", IsRequired = false), XmlElement("color"), JsonProperty("color")]
        public string Color { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeColor() { return !string.IsNullOrEmpty(Color); }
#pragma warning restore 1591
    }

    /// <summary>
    /// Item title
    /// </summary>
    [DataContract(Name="title", Namespace = ""), XmlType("title", Namespace = "")]
    public class DataListItemTitle
    {
        /// <summary>
        /// The text of this label
        /// </summary>
        [Required, DataMember(Name = "text", IsRequired = true), XmlElement("text"), JsonProperty("text", Required = Required.AllowNull)]
        public string Text { get; set; }
    }
    #endregion

    #region Push
    /// <summary>
    /// The push widget payload
    /// </summary>
    /// <typeparam name="T">The type of widget data to push</typeparam>
    public class PushPayload<T>
    {
        /// <summary>
        /// The API key of the Geckoboard account
        /// </summary>
        [JsonProperty("api_key", Required = Required.Always)]
        public string ApiKey { get; set; }
#pragma warning disable 1591
        public bool ShouldSerializeApiKey() { return !string.IsNullOrEmpty(ApiKey); }
#pragma warning restore 1591

        /// <summary>
        /// The payload data
        /// </summary>
        [JsonProperty("data", Required = Required.Always)]
        public T Data { get; set; }
    }
    #endregion
}
