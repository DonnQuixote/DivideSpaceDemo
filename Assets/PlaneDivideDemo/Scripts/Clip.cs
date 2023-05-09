using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clip
{
    //判断包围盒的八个点是否都在视锥空间内
   public static bool CheckBoundsIsInCamera(Bounds bound,Camera camera,float viewRatio = 1)
    {
        Vector4 worldPos = Vector4.one;
        int code = 63;
        for (int i = -1; i <= 1; i += 2)
        {
            for (int j = -1; j <= 1; j += 2)
            {
                for (int k = -1; k <= 1; k += 2)
                {
                    worldPos.x = bound.center.x + i * bound.extents.x;
                    worldPos.y = bound.center.y + i * bound.extents.y;
                    worldPos.z = bound.center.z + i * bound.extents.z;

                    code &= ComputeOutCode(camera.projectionMatrix * camera.worldToCameraMatrix * worldPos * viewRatio);
                }
            }
        }
        return code ==0 ?true : false;
    }

    private static int ComputeOutCode(Vector4 projectionPos)
    {
        int _code = 0;
        if (projectionPos.x < -projectionPos.w) _code |= 1;
        if (projectionPos.x > projectionPos.w) _code |= 2;
        if (projectionPos.x < -projectionPos.w) _code |= 4;
        if (projectionPos.x < -projectionPos.w) _code |= 8;
        if (projectionPos.x < -projectionPos.w) _code |= 16;
        if (projectionPos.x < -projectionPos.w) _code |= 32;
        if (projectionPos.x < -projectionPos.w) _code |= 64;
        if (projectionPos.x < -projectionPos.w) _code |= 128;
        return _code;
    }
}
