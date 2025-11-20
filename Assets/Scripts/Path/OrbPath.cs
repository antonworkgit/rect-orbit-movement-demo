using Scripts.Settings;
using Scripts.Utility;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace Scripts.Path
{
    // "Orbital" path for movement around a rect
    public sealed class OrbPath : IOrbPath
    {
        private readonly Spline _spline;
        private readonly float _perimeter;

        public float Perimeter => _perimeter;

        public OrbPath(Rect rect, PathSettings settings)
        {
            float cornerRadius = settings.CornerRadius;
            int cornerPoints = settings.CornerPoints;

            if (cornerRadius < 0) throw new System.ArgumentOutOfRangeException(nameof(cornerRadius));
            if (cornerPoints < 2) throw new System.ArgumentOutOfRangeException(nameof(cornerPoints));

            Vector2 position = rect.position;
            Vector2 size = rect.size;

            Vector2[] points = RoundedRectUtility.GetRoundRectPoints(position, size, cornerRadius, cornerPoints);
            _spline = new Spline(points.Length + 1, closed: true);

            // Extra point that will become 0t of a spline for easy evaluation
            Vector2 startPoint = position + new Vector2(0f, size.y * 0.5f + settings.CornerRadius);
            AddPoint(startPoint);

            foreach (Vector2 point in points)
            {
                AddPoint(point);
            }

            _perimeter = _spline.GetLength();
        }

        public bool EvalT(float t, out float3 position, out float3 tangent, out float3 upVector)
        {
            return _spline.Evaluate(t, out position, out tangent, out upVector);
        }

        public bool EvalP(float p, out float3 position, out float3 tangent, out float3 upVector)
        {
            float t = p / _perimeter;
            return EvalT(t, out position, out tangent, out upVector);
        }

        private void AddPoint(Vector2 point)
        {
            _spline.Add(new float3(point.x, point.y, 0f), TangentMode.AutoSmooth);
        }
    }
}