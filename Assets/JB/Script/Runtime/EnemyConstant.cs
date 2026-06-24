using UnityEngine;

public static class EnemyConstant
{
    public static readonly float NEAR_BOUNDARY = 5.0f;
    /// <summary>
    /// 5.0f로 설정된 NEAR_BOUNDARY의 제곱 값입니다. 이 값은 적이 플레이어와 가까운 거리에 있는지 판단하는 데 사용됩니다.
    /// </summary>
    public static readonly float NEAR_BOUNDARY_SQUARED = NEAR_BOUNDARY * NEAR_BOUNDARY;
    public static readonly float FAR_BOUNDARY = 15.0f;
    /// <summary>
    /// 15.0f로 설정된 FAR_BOUNDARY의 제곱 값입니다. 이 값은 적이 플레이어와 먼 거리에 있는지 판단하는 데 사용됩니다.
    /// </summary>
    public static readonly float FAR_BOUNDARY_SQUARED = FAR_BOUNDARY * FAR_BOUNDARY;
    public static readonly float APPROACH_DISTANCE = 15.0f;
    /// <summary>
    /// 15.0f로 설정된 APPROACH_DISTANCE의 제곱 값입니다. 이 값은 적이 플레이어와 접근 거리에 있는지 판단하는 데 사용됩니다.
    /// </summary>
    public static readonly float APPROACH_DISTANCE_SQUARED = APPROACH_DISTANCE * APPROACH_DISTANCE;

}
