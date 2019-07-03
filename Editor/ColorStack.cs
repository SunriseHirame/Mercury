using System.Collections.Generic;
using UnityEngine;

namespace Hirame.Mercury.Editor
{
    public static class ColorStack
    {
        private static readonly Stack<Color> background = new Stack<Color> ();
        
        public static void PushBackgroundColor (in Color color)
        {
            background.Push (GUI.backgroundColor);
            GUI.backgroundColor = color;
        }

        public static void PopBackgroundColor ()
        {
            GUI.backgroundColor = background.Pop ();
        }
    }

}