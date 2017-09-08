using OpenTK.Graphics;
using Processing.OpenTk.Core.Rendering;
using System;

namespace Processing.OpenTk.Core
{
    public interface ICanvas : IRenderer2d
    {
        Font Font { get; set; }        
        Color4 Fill { get; set; }
        Color4 Stroke { get; set; }
        float StrokeWeight { get; set; }
        

        ulong FrameCount { get; set; }

        event Action<Canvas> Setup;

        event Action<Canvas> Draw;

        void PushStyle();
        void PushStyle(Style style);
        Style PopStyle();
    }
}
