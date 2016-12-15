using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace ConsoleApplication13 {
    class Program {
        private static int Width = 2048;
        private static int Height = 50;
        private static int SmoothPasses = 4;
        private static int WaterLevel = 20;
        static void Main(string[] args) {
            int[] levels = new int[Width];
            Random rand = new Random();
            for (int i = 0; i < Width; i++) {
                levels[i] = rand.Next(0, Height);
            }
            Bitmap b = new Bitmap(Width, Height);
            for (int h = 0; h < Height; h++) {
                for (int w = 0; w < Width; w++) {
                    b.SetPixel(w, h, Color.White);
                }
            }
            for (int n = 0; n < SmoothPasses; n++) {
                Smooth(levels, b);
            }
            Draw(b, levels);
            b.Save("image.png");
            Process.Start("image.png");
        }

        private static void Draw(Bitmap b, int[] levels) {
            for (int l = WaterLevel; l < Height; l++) {
                for (int w = 0; w < Width; w++) {
                    b.SetPixel(w,l,Color.DeepSkyBlue);
                }
            }
            for (int i = 0; i < Width; i++) {
                for (int j = levels[i]; j < Height; j++) {
                    if (j < WaterLevel) {
                        if ((j - levels[i]) < 3)
                            b.SetPixel(i, j, Color.SaddleBrown);
                        if ((j - levels[i]) > 2)
                            b.SetPixel(i, j, Color.SlateGray);
                        if ((j - levels[i]) == 0)
                            b.SetPixel(i, j, Color.Green);
                    }
                    else {
                        if ((j - levels[i]) < 3)
                            b.SetPixel(i, j, Color.SaddleBrown);
                        if ((j - levels[i]) > 2)
                            b.SetPixel(i, j, Color.SlateGray);
                    }
                }
            }
        }

        private static void Smooth(int[] levels, Bitmap b) {
            for (int i = 0; i < Width; i++) {
                if (!(i < 4)) {
                    int t = (levels[i] + levels[i - 1] + levels[i - 2]) / 3;
                    levels[i] = t;
                }
            }
        }
    }
}
