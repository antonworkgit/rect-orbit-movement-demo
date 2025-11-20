using UnityEngine;

namespace Scripts.Utility
{
    public static class RoundedRectUtility
    {
        const int Sides = 4;
        const int Arcs = 4;

        public static float RoundedRectPerimeter(float width, float height, float cornerRadius)
        {
            return 2f * (width + height) + 2f * Mathf.PI * cornerRadius;
        }

        // Corners only
        public static Vector2[] GetRoundRectPoints(Vector2 position, Vector2 size, float cornerRadius, int cornerPoints)
        {
            if (cornerPoints < 2) throw new System.ArgumentOutOfRangeException(nameof(cornerPoints));

            int pointsCount = Arcs * cornerPoints;
            Vector2[] points = new Vector2[pointsCount];

            float hx = size.x * 0.5f;
            float hy = size.y * 0.5f;

            // Base corners (no offset)
            Vector2 tl = position + new Vector2(-hx, hy);
            Vector2 tr = position + new Vector2(hx, hy);
            Vector2 br = position + new Vector2(hx, -hy);
            Vector2 bl = position + new Vector2(-hx, -hy);

            float step = 1f / (cornerPoints - 1);

            // Top-right (π/2 - 0)
            for (int i = 0; i < cornerPoints; i++)
            {
                float angle = Mathf.Lerp(Mathf.PI / 2f, 0f, step * i);

                points[i] = tr + new Vector2(
                    cornerRadius * Mathf.Cos(angle),
                    cornerRadius * Mathf.Sin(angle)
                );
            }

            // Bottom-right (0 - −π/2)
            for (int i = 0; i < cornerPoints; i++)
            {
                float angle = Mathf.Lerp(0f, -Mathf.PI / 2f, step * i);

                points[cornerPoints + i] = br + new Vector2(
                    cornerRadius * Mathf.Cos(angle),
                    cornerRadius * Mathf.Sin(angle)
                );
            }

            // Bottom-left (−π/2 - −π)
            for (int i = 0; i < cornerPoints; i++)
            {
                float angle = Mathf.Lerp(-Mathf.PI / 2f, -Mathf.PI, step * i);

                points[cornerPoints * 2 + i] = bl + new Vector2(
                    cornerRadius * Mathf.Cos(angle),
                    cornerRadius * Mathf.Sin(angle)
                );
            }

            // Top-left (π - π/2)
            for (int i = 0; i < cornerPoints; i++)
            {
                float angle = Mathf.Lerp(Mathf.PI, Mathf.PI / 2f, step * i);

                points[cornerPoints * 3 + i] = tl + new Vector2(
                    cornerRadius * Mathf.Cos(angle),
                    cornerRadius * Mathf.Sin(angle)
                );
            }

            return points;
        }
    }
}