using SciColorMaps.Portable;

namespace FirelightCore
{
    public static class Gradient
    {
        private static byte[][] RAINBOW_PALETTE_RGB = new[]
        {
            new byte[] { 255, 0, 0 },
            new byte[] { 255, 102, 0 },
            new byte[] { 255, 204, 0 },
            new byte[] { 123, 255, 0 },
            new byte[] { 39, 230, 160 },
            new byte[] { 0, 255, 255 },
            new byte[] { 0, 153, 255 },
            new byte[] { 0, 153, 255 },
        };

        private static float[] RAINBOW_PALETTE_POSITIONS = new float[] { 0, 0.16f, 0.31f, 0.47f, 0.65f, 0.89f, 0.96f, 1 };

        public static ColorMap RainbowPalette;

        public static void GeneratePalettes()
        {
            RainbowPalette = ColorMap.CreateFromColors(RAINBOW_PALETTE_RGB, RAINBOW_PALETTE_POSITIONS);
        }
    }
}
