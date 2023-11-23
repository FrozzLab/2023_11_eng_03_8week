using System;
using UnityEngine;

namespace Menu
{
    [Serializable]
    public class ResolutionContainer
    {
        public int width, height;

        public ResolutionContainer(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
    }
    
    [Serializable]
    public class ScreenModeContainer
    {
        public FullScreenMode mode;

        public ScreenModeContainer(FullScreenMode mode)
        {
            this.mode = mode;
        }
    }
}