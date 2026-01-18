using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject attackPos;
    float rotateSpeed = 100f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AttackArrow();
    }

    void AttackArrow()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
            Vector3 targetDir = hit.point - attackPos.transform.position; //방향 벡터
            float angle = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg; //수평면 상에서 몇 도 방향인지 계산
            Quaternion targetRotation = Quaternion.Euler(90f, 0f, -angle);
            attackPos.transform.localRotation = Quaternion.Slerp(
                attackPos.transform.localRotation,
                targetRotation,
                rotateSpeed * Time.deltaTime);
        }
    }
}
