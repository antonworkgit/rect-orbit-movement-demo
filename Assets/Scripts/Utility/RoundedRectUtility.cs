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
            int pointsCount = Arcs * cornerPoints;
            Vector2[] points = new Vector2[pointsCount];

            float hx = size.x * 0.5f;
            float hy = size.y * 0.5f;

            // Base corners (no offset)
            Vector2 tl = position + new Vector2(-hx, hy);
            Vector2 tr = position + new Vector2(hx, hy);
            Vector2 br = position + new Vector2(hx, -hy);
            Vector2 bl = position + new Vector2(-hx, -hy);

            // Reverse t for lerp
            float inv = 1f / (cornerPoints - 1);

            // Clockwise from top right corner
            for (int i = 0; i < cornerPoints; i++)
            {
                float angle = Mathf.Lerp(Mathf.PI / 2f, 0f, inv * i);

                points[i] = new Vector2(
                    tr.x + cornerRadius * Mathf.Cos(angle),
                    tr.y + cornerRadius * Mathf.Sin(angle)
                );
            }

            for (int i = 0; i < cornerPoints; i++)
            {
                float angle = Mathf.Lerp(0f, -Mathf.PI / 2f, inv * i);

                points[cornerPoints + i] = new Vector2(
                    br.x + cornerRadius * Mathf.Cos(angle),
                    br.y + cornerRadius * Mathf.Sin(angle)
                );
            }

            for (int i = 0; i < cornerPoints; i++)
            {
                float angle = Mathf.Lerp(0f, -Mathf.PI / 2f, inv * i);
                points[cornerPoints * 2 + i] = new Vector2(
                    bl.x - cornerRadius * Mathf.Sin(Mathf.PI * 2 - angle),
                    bl.y - cornerRadius * Mathf.Cos(Mathf.PI * 2 - angle)
                );
            }

            for (int i = 0; i < cornerPoints; i++)
            {
                float angle = Mathf.Lerp(-3f * Mathf.PI / 2f, -Mathf.PI, inv * i);
                points[cornerPoints * 3 + i] = new Vector2(
                    tl.x - cornerRadius * Mathf.Sin(angle),
                    tl.y - cornerRadius * Mathf.Cos(angle)
                );
            }

            return points;
        }
    }
}