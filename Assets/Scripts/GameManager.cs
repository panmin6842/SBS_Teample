using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //어디서든 접근 가능

    [Header("PlayerData")]
    public string nickName;
    public int level;
    public Job job;
    public int hpPoint;
    public int atkPoint;
    public float defPoint;
    public int profileIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //오브젝트 파괴 금지
        }
        else
        {
            Destroy(gameObject); //이미 있으면 파괴
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        profileIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
