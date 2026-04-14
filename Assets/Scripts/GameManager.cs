using System.Collections.Generic;
using UnityEditor.Animations;
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
    public float criticalPoint;
    public int profileIndex;
    public float e_hp;
    public float a_hp;
    public float e_atk;
    public float a_atk;
    public float e_def;
    public float a_def;
    public int a_mp;
    public float a_skillCoolTime;
    public float e_critical;
    public float a_critical;
    public AnimatorController curAnimation;
    public int gold;
    public int playerLevel;
    public int skillPoint;
    public List<int> canBuyCount = new List<int>();

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
