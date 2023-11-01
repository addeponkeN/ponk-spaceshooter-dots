
using Unity.Mathematics;

public static class MathHelper
{
    public static float LookAt(float3 from, float3 to)
    {
        var x = from.x - to.x;
        var y = from.z - to.z;
        return math.atan2(x, y) + math.PI;
    }
}