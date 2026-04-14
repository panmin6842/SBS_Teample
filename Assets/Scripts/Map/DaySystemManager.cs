using System.Collections;
using TMPro;
using UnityEngine;

public class DaySystemManager : MonoBehaviour
{
    public static DaySystemManager instance;

    public int DayCount = 1;
    public float DungeonTime;
    public int CurMoveCount;
    public int MaxMoveCount;

    [SerializeField] TextMeshProUGUI DayCountText;
    [SerializeField] TextMeshProUGUI MoveCountText;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        //DungeonTime += Time.deltaTime;
        //DayCountText.text = "Day " + DayCount.ToString();
        //MoveCountText.text = CurMoveCount.ToString() + "/" + MaxMoveCount.ToString();
    }

    public  void DayChange()
    {
        DayCount++;
    }
}
