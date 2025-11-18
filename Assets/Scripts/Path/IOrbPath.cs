using Unity.Mathematics;

namespace Scripts.Path
{
    public interface IOrbPath
    {
        float Perimeter { get; }

        bool EvalT(float t, out float3 position, out float3 tangent, out float3 upVector);
        bool EvalP(float p, out float3 position, out float3 tangent, out float3 upVector);
    }
}