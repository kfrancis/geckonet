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
    using System.ComponentModel;
using Newtonsoft.Json.Converters;
    
    #endregion

    #region Custom Widgets
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

    public enum DataItemType
    {
        None = 0,   // (no corner icon)
        Alert,      // (yellow corner icon)
        Info        // (grey corner icon)
    }
    #endregion

    #region Mapping
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
        [Range(1, 10)]
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
#endregion

    #region Highchart

    [DataContract(Namespace = ""), XmlRoot("root", Namespace = "")]
    public class GeckoHighchart
    {
        [DataMember(Name = "chart", IsRequired = false), XmlElement("chart"), JsonProperty("chart")]
        public HighchartChart Chart { get; set; }
        public bool ShouldSerializeChart() { return Chart != null; }

        [DataMember(Name = "colors", IsRequired = false), XmlArray("colors"), JsonProperty("colors")]
        public List<string> Colors { get; set; }
        public bool ShouldSerializeColors() { return (Colors != null && Colors.Count > 0); }

        [DataMember(Name = "credits", IsRequired = false), XmlElement("credits"), JsonProperty("credits")]
        public HighchartCredits Credits { get; set; }
        public bool ShouldSerializeCredits() { return Credits != null; }

        [DataMember(Name = "title", IsRequired = false), XmlElement("title"), JsonProperty("title")]
        public HighchartTitle Title { get; set; }
        public bool ShouldSerializeTitle() { return Title != null; }

        [DataMember(Name = "tooltip", IsRequired = false), XmlElement("tooltip"), JsonProperty("tooltip")]
        public HighchartTooltip Tooltip { get; set; }
        public bool ShouldSerializeTooltip() { return Tooltip != null; }

        [DataMember(Name = "legend", IsRequired = false), XmlElement("legend"), JsonProperty("legend")]
        public HighchartLegend Legend { get; set; }
        public bool ShouldSerializeLegend() { return Legend != null; }

        [DataMember(Name = "plotOptions", IsRequired = false), XmlElement("plotOptions"), JsonProperty("plotOptions")]
        public HighchartPlotOptions PlotOptions { get; set; }
        public bool ShouldSerializePlotOptions() { return PlotOptions != null; }

        [DataMember(Name = "series", IsRequired = false), XmlElement("series"), JsonProperty("series")]
        public List<HighchartSeries> Series { get; set; }
        public bool ShouldSerializeSeries() { return Series != null; }
    }

    [DataContract(Name = "series", Namespace = ""), XmlType("series", Namespace = ""), JsonObject("series")]
    public class HighchartSeries
    {
        [DataMember(Name = "type", IsRequired = false), XmlElement("type"), JsonProperty("type")]
        public string Type { get; set; }
        public bool ShouldSerializeType() { return !string.IsNullOrWhiteSpace(Type); }

        [DataMember(Name = "name", IsRequired = false), XmlElement("name"), JsonProperty("name")]
        public string Name { get; set; }
        public bool ShouldSerializeName() { return !string.IsNullOrWhiteSpace(Name); }

        [DataMember(Name = "data", IsRequired = false), XmlElement("data"), JsonProperty("data")]
        public List<Dictionary<string, int>> Data { get; set; }
        public bool ShouldSerializeData() { return (Data != null) && (Data.Count > 0); }
    }

    [DataContract(Name = "plotOptions", Namespace = ""), XmlType("plotOptions", Namespace = "")]
    public class HighchartPlotOptions
    {
        public HighchartPieOptions PieOptions { get; set; }
    }

    [DataContract(Name = "pie", Namespace = ""), XmlType("pie", Namespace = "")]
    public class HighchartPieOptions
    {
        [DataMember(Name = "animation", IsRequired = false), XmlElement("animation"), JsonProperty("animation")]
        public bool? Animation { get; set; }
        public bool ShouldSerializeAnimation() { return Animation.HasValue; }

        [DataMember(Name = "allowPointSelect", IsRequired = false), XmlElement("allowPointSelect"), JsonProperty("allowPointSelect")]
        public bool? AllowPointSelect { get; set; }
        public bool ShouldSerializeAllowPointSelect() { return AllowPointSelect.HasValue; }

        [DataMember(Name = "cursor", IsRequired = false), XmlElement("cursor"), JsonProperty("cursor")]
        public string Cursor { get; set; }
        public bool ShouldSerializeCursor() { return !string.IsNullOrWhiteSpace(Cursor); }

        [DataMember(Name = "dataLabels", IsRequired = false), XmlElement("dataLabels"), JsonProperty("dataLabels")]
        public HighchartDataLabelOptions DataLabelOptions { get; set; }
        public bool ShouldSerializeDataLabelOptions() { return DataLabelOptions != null; }

        [DataMember(Name = "showInLegend", IsRequired = false), XmlElement("showInLegend"), JsonProperty("showInLegend")]
        public bool? ShowInLegend { get; set; }
        public bool ShouldSerializeShowInLegend() { return ShowInLegend.HasValue; }

        [DataMember(Name = "size", IsRequired = false), XmlElement("size"), JsonProperty("size")]
        public string Size { get; set; }
        public bool ShouldSerializeSize() { return !string.IsNullOrWhiteSpace(Size); }
    }

    [DataContract(Name = "dataLabels", Namespace = ""), XmlType("dataLabels", Namespace = "")]
    public class HighchartDataLabelOptions
    {
        [DataMember(Name = "enabled", IsRequired = false), XmlElement("enabled"), JsonProperty("enabled")]
        public bool? Enabled { get; set; }
        public bool ShouldSerializeEnabled() { return Enabled.HasValue; }
    }

    [DataContract(Name = "legend", Namespace = ""), XmlType("legend", Namespace = "")]
    public class HighchartLegend
    {
        [DataMember(Name = "borderColor", IsRequired = false), XmlElement("borderColor"), JsonProperty("borderColor")]
        public string BorderColor { get; set; }
        public bool ShouldSerializeBorderColor() { return !string.IsNullOrWhiteSpace(BorderColor); }

        [DataMember(Name = "itemWidth", IsRequired = false), XmlElement("itemWidth"), JsonProperty("itemWidth")]
        public int? ItemWidth { get; set; }
        public bool ShouldSerializeItemWidth() { return ItemWidth.HasValue; }

        [DataMember(Name = "margin", IsRequired = false), XmlElement("margin"), JsonProperty("margin")]
        public int? Margin { get; set; }
        public bool ShouldSerializeMargin() { return Margin.HasValue; }

        [DataMember(Name = "width", IsRequired = false), XmlElement("width"), JsonProperty("width")]
        public int? Width { get; set; }
        public bool ShouldSerializeWidth() { return Width.HasValue; }
    }

    [DataContract(Name = "tooltip", Namespace = ""), XmlType("tooltip", Namespace = "")]
    public class HighchartTooltip
    {
        [DataMember(Name = "formatter", IsRequired = false), XmlElement("formatter"), JsonProperty("formatter")]
        public string Formatter { get; set; }
        public bool ShouldSerializeFormatter() { return !string.IsNullOrWhiteSpace(Formatter); }
    }

    [DataContract(Name = "title", Namespace = ""), XmlType("title", Namespace = "")]
    public class HighchartTitle
    {
        [DataMember(Name = "text", IsRequired = false), XmlElement("text"), JsonProperty("text", Required = Required.AllowNull)]
        public string Text { get; set; }
        public bool ShouldSerializeText() { return !string.IsNullOrWhiteSpace(Text); }
    }

    [DataContract(Name = "credits", Namespace = ""), XmlType("credits", Namespace = "")]
    public class HighchartCredits
    {
        [DataMember(Name = "enabled", IsRequired = false), XmlElement("enabled"), JsonProperty("enabled")]
        public bool? Enabled { get; set; }
        public bool ShouldSerializeEnabled() { return Enabled.HasValue; }
    }

    [DataContract(Name = "chart", Namespace = ""), XmlType("chart", Namespace = "")]
    public class HighchartChart
    {
        [DataMember(Name = "renderTo", IsRequired = false), XmlElement("renderTo"), JsonProperty("renderTo"), DefaultValue("container")]
        public string RenderTo { get; set; }
        public bool ShouldSerializeRenderTo() { return !string.IsNullOrWhiteSpace(RenderTo); }

        [DataMember(Name = "plotBackgroundColor", IsRequired = false), XmlElement("plotBackgroundColor"), JsonProperty("plotBackgroundColor")]
        public string PlotBackgroundColor { get; set; }
        public bool ShouldSerializePlotBackgroundColor() { return !string.IsNullOrWhiteSpace(PlotBackgroundColor); }

        [DataMember(Name = "backgroundColor", IsRequired = false), XmlElement("backgroundColor"), JsonProperty("backgroundColor")]
        public string BackgroundColor { get; set; }
        public bool ShouldSerializeBackgroundColor() { return !string.IsNullOrWhiteSpace(BackgroundColor); }

        [DataMember(Name = "borderColor", IsRequired = false), XmlElement("borderColor"), JsonProperty("borderColor")]
        public string BorderColor { get; set; }
        public bool ShouldSerializeBorderColor() { return !string.IsNullOrWhiteSpace(BorderColor); }

        [DataMember(Name = "lineColor", IsRequired = false), XmlElement("lineColor"), JsonProperty("lineColor")]
        public string LineColor { get; set; }
        public bool ShouldSerializeLineColor() { return !string.IsNullOrWhiteSpace(LineColor); }

        [DataMember(Name = "plotBorderColor", IsRequired = false), XmlElement("plotBorderColor"), JsonProperty("plotBorderColor")]
        public string PlotBorderColor { get; set; }
        public bool ShouldSerializePlotBorderColor() { return !string.IsNullOrWhiteSpace(PlotBorderColor); }

        [DataMember(Name = "plotBorderWidth", IsRequired = false), XmlElement("plotBorderWidth"), JsonProperty("plotBorderWidth")]
        public int? PlotBorderWidth { get; set; }
        public bool ShouldSerializePlotBorderWidth() { return PlotBorderWidth.HasValue; }

        [DataMember(Name = "height", IsRequired = false), XmlElement("height"), JsonProperty("height")]
        public int? Height { get; set; }
        public bool ShouldSerializeHeight() { return Height.HasValue; }

        [DataMember(Name = "plotShadow", IsRequired = false), XmlElement("plotShadow"), JsonProperty("plotShadow")]
        public bool? PlotShadow { get; set; }
        public bool ShouldSerializeSeries() { return PlotShadow.HasValue; }
    }

#endregion
    
#region Pie Chart
#endregion

    #region Line Chart
    [DataContract(Namespace = ""), XmlRoot("root", Namespace = "")]
    public class GeckoLineChart
    {
        [DataMember(Name = "item", IsRequired = false), XmlElement("item"), JsonProperty("item")]
        public List<string> Items { get; set; }
        public bool ShouldSerializeItems() { return (Items != null) && (Items.Count > 0); }

        [DataMember(Name = "settings", IsRequired = false), XmlElement("settings"), JsonProperty("settings")]
        public GeckoLineChartSettings Settings { get; set; }
        public bool ShouldSerializeGeckoLineChartSettings() { return (Settings != null); }
    }

    [DataContract(Name = "settings", Namespace = ""), XmlType("settings", Namespace = "")]
    public class GeckoLineChartSettings
    {
        [DataMember(Name = "axisx", IsRequired = false), XmlElement("axisx"), JsonProperty("axisx")]
        public List<string> XAxisLabels { get; set; }
        public bool ShouldSerializeXAxisLabels() { return (XAxisLabels != null) && (XAxisLabels.Count > 0); }

        [DataMember(Name = "axisy", IsRequired = false), XmlElement("axisy"), JsonProperty("axisy")]
        public List<string> YAxisLabels { get; set; }
        public bool ShouldSerializeYAxisLabels() { return (YAxisLabels != null) && (YAxisLabels.Count > 0); }

        [DataMember(Name = "colour", IsRequired = false), XmlElement("colour"), JsonProperty("colour")]
        public string Colour { get; set; }
        public bool ShouldSerializeColour() { return !string.IsNullOrWhiteSpace(Colour); }
    }
    
    #endregion

}
