﻿namespace MauiAppTempoAgora.Models
{
    public class Tempo
    {
        internal string? main;

        public double? lon { get; set; }
        public double? lat { get; set; }
        public double? temp { get; set; }
        public double? temp_min { get; set; }
        public double? temp_max { get; set; }
        public string? sunrise { get; set; }
        public string ?sunset { get; set; }
        public double? speed { get; set; }
        public double? gust { get; set; }
        public int? visibility { get; set; }
        public string? description { get; internal set; }
    }
}
