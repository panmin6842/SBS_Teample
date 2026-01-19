using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject attackPos;
    [SerializeField] GameObject swordAttackObj;
    float rotateSpeed = 100f;

    Vector3 targetDir;
    float angle;

    float attackTime;
    bool attack = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AttackArrow();

        if (attack)
        {
            attackTime += Time.deltaTime;

            if (attackTime > 1)
            {
                attack = false;
                attackTime = 0;
            }
        }
    }

    void AttackArrow()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
            targetDir = hit.point - attackPos.transform.position; //방향 벡터
            angle = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg; //수평면 상에서 몇 도 방향인지 계산
            Quaternion targetRotation = Quaternion.Euler(90f, 0f, -angle);
            attackPos.transform.localRotation = Quaternion.Slerp(
                attackPos.transform.localRotation,
                targetRotation,
                rotateSpeed * Time.deltaTime);
        }
    }

    private void OnAttack(InputValue value)
    {
        if (!attack)
        {
            Instantiate(swordAttackObj, transform.position, attackPos.transform.rotation);
            attack = true;
        }
    }
}
