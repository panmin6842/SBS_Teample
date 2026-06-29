using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StationLevelReward
{
    public int level; //데이터 스테이션 레벨
    public int requiredExp; //다음 레벨까지 필요한 경험치
    public int rewards; //보상(스킬 포인트)
}

[CreateAssetMenu(fileName = "DataStationData", menuName = "Systems/DataStation")]
public class DataStationData : ScriptableObject
{
    public List<StationLevelReward> levelRewards;
}
