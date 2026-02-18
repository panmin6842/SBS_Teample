using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackPos;
    [Header("SkillObject")]
    [SerializeField] GameObject swordAttackObj;
    [SerializeField] GameObject bowAttackObj;
    [SerializeField] GameObject stampAttackObj;
    float rotateSpeed = 100f;

    Vector3 targetDir;
    float angle;

    float attackTime;
    float attackDelay = 1;
    bool attack = false;
    public bool uiClicking = false;

    [SerializeField] int skillCount = 1;
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

            if (attackTime > attackDelay)
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

    public GameObject AttackPos
    {
        get { return attackPos; }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!attack && !uiClicking)
        {
            switch (skillCount)
            {
                case 1:
                    {
                        Instantiate(swordAttackObj, transform.position, attackPos.transform.rotation);
                        attack = true;
                    }
                    break;
                case 2:
                    {
                        Instantiate(bowAttackObj, transform.position, attackPos.transform.rotation);
                        attack = true;
                    }
                    break;
                case 3:
                    {
                        Instantiate(stampAttackObj, transform.position, attackPos.transform.rotation);
                        attack = true;
                    }
                    break;
            }
        }
    }

    public void OnSkillChange(InputAction.CallbackContext context)
    {
        //if (context.started)
        //{
        //    string pressNumber = context.control.name;

        //    switch (pressNumber)
        //    {
        //        case "1":
        //            {
        //                Debug.Log("swordskill");
        //                attackDelay = 1;
        //                attack = false;
        //                skillCount = 1;
        //            }
        //            break;
        //        case "2":
        //            {
        //                Debug.Log("bowskill");
        //                attackDelay = 0.5f;
        //                attack = false;
        //                skillCount = 2;
        //            }
        //            break;
        //        case "3":
        //            {
        //                Debug.Log("stampskill");
        //                attackDelay = 2.5f;
        //                attack = false;
        //                skillCount = 3;
        //            }
        //            break;
        //    }
        //}
    }
}
