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
        hpText.text = "hp : " + hp;
        actCountText.text = "actCount : " + actCount;

        if (hp <= 0)
        {
            SceneManager.LoadScene("MainScene");
        }
    }
    private void OnTriggerEnter(Collider other)
    {

    }

    public void OnTestKey(InputAction.CallbackContext context)
    {
        hp = 0;
    }
}
