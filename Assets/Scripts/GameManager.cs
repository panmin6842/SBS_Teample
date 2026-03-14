using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //어디서든 접근 가능

    [Header("PlayerData")]
    public string nickName;

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

    }

    // Update is called once per frame
    void Update()
    {

    }
}
