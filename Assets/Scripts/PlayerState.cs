using TMPro;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public float hp = 500f; //체력
    public float atk = 50f; //공격력
    public float def = 1f; //방어력
    public int actCount = 2; //행동력

    [Header("Text")]
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI actCountText;

    private void Update()
    {
        hpText.text = "hp : " + hp;
        actCountText.text = "actCount : " + actCount;
    }
}
