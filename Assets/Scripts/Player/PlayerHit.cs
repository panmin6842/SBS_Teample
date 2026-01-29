using TMPro;
using UnityEngine;

public class PlayerHit : PlayerState
{
    [Header("Text")]
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI actCountText;

    private void Update()
    {
        hpText.text = "hp : " + hp;
        actCountText.text = "actCount : " + actCount;
    }
    private void OnTriggerEnter(Collider other)
    {

    }
}
