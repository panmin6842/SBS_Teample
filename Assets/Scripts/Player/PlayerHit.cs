using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerHit : PlayerState
{
    [Header("Text")]
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI actCountText;

    private void Update()
    {
        hpText.text = "hp : " + curHp;
        actCountText.text = "actCount : " + actCount;

        if (curHp <= 0)
        {
            curHp = 0;
            SceneManager.LoadScene("MainScene");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyTestAttack"))
        {
            curHp -= 2;
        }
    }

    public void OnTestKey(InputAction.CallbackContext context)
    {
        maxHp = 0;
    }
}
