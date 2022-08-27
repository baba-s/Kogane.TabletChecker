using System;
using UnityEngine;

namespace Kogane
{
    [Serializable]
    public readonly struct TabletCheckerData
    {
        public bool   IsAndroid  { get; }
        public string Generation { get; }
        public int    Width      { get; }
        public int    Height     { get; }
        public float  Dpi        { get; }

        public TabletCheckerData
        (
            bool   isAndroid,
            int    width,
            int    height,
            float  dpi,
            string generation
        )
        {
            IsAndroid  = isAndroid;
            Width      = width;
            Height     = height;
            Dpi        = dpi;
            Generation = generation;
        }

        public override string ToString()
        {
            return JsonUtility.ToJson( this, true );
        }
    }
}