using UnityEngine;
using UnityEngine.InputSystem;

public class SwordSkill : MonoBehaviour
{
    private int actSkill1Number = 0;
    private int actSkill2Number = 0;
    private int passiveSkillNumber = 0;

    public int ActSkill1Number()
    {
        return actSkill1Number;
    }
    public int ActSkill2Number()
    {
        return actSkill2Number;
    }
    public int PassiveSkillNumber()
    {
        return passiveSkillNumber;
    }

    public void OnSkillAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (context.action.name == "1")
            {
                ActiveSkill(actSkill1Number);
            }
        }
    }

    private void ActiveSkill(int number)
    {

    }
}
