using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ArtiFactManager : MonoBehaviour
{
    public static ArtiFactManager instance;

    private List<int> equippedArtifactIDs = new List<int>();
    private float recoveryMultiplier = 1.0f;
    private void Awake()
    {
        instance = this;
    }

    public void EquipArtifact(int itemID) //효과 등록
    {
        if (equippedArtifactIDs.Contains(itemID)) return;

        equippedArtifactIDs.Add(itemID);
        RegisterArtifactEffect(itemID, true);
    }

    public void UnequipArtifact(int itemID) //효과해제
    {
        if (equippedArtifactIDs.Remove(itemID))
        {
            RegisterArtifactEffect(itemID, false);
        }
    }

    private void RegisterArtifactEffect(int id, bool isEquip)
    {
        switch(id)
        {
            case 700:
                ApplyLanternStaminaBonus(isEquip, 5);
                break;
            case 701:
                {
                    if (isEquip) GameManager.instance.OnShelterEnter += RecoverStaminaOnShelter;
                    else GameManager.instance.OnShelterEnter -= RecoverStaminaOnShelter;
                }
                break;
            case 702:
                {
                    if (isEquip) GameManager.instance.OnRandomPortalEnter += RecoverStaminaOnShelter;
                    else GameManager.instance.OnRandomPortalEnter -= RecoverStaminaOnShelter;
                }
                break;
            case 703:
                {
                    DoubleStaminaRecovery(isEquip, 5);
                }
                break;
            case 704:
                {
                    if (isEquip) GameManager.instance.OnPortalEnter += ChanceToSkipStaminaCost;
                    else GameManager.instance.OnPortalEnter -= ChanceToSkipStaminaCost;
                }
                break;
            case 705:
                {
                    if (isEquip) GameManager.instance.OnPortalEnter += ApplyChaosStaminaCost;
                    else GameManager.instance.OnPortalEnter -= ApplyChaosStaminaCost;
                }
                break;
            case 706:
                {
                    if (isEquip) GameManager.instance.OnPortalEnter += ApplyChaosStaminaCost;
                    else GameManager.instance.OnPortalEnter -= ApplyChaosStaminaCost;
                }
                break;
            case 707:
                {
                    if (isEquip) GameManager.instance.OnActCountDeath += EmergencyEscapeOnDeath;
                    else GameManager.instance.OnActCountDeath -= EmergencyEscapeOnDeath;
                }
                break;
        }
    }

    private void ApplyLanternStaminaBonus(bool isEquip, int amount)
    {
        if (isEquip) GameManager.instance.maxActCount += amount;
        else GameManager.instance.maxActCount -= amount;
    }
    private void RecoverStaminaOnShelter()
    {
        PlayerProfile playerProfile = GameObject.FindWithTag("Player").GetComponent<PlayerProfile>();
        playerProfile.ActCountPlus(3, recoveryMultiplier);
    }
    private void DoubleStaminaRecovery(bool isEquip, int amount)
    {
        if (isEquip)
        {
            GameManager.instance.maxActCount -= amount;
            recoveryMultiplier *= 2.0f;
        }
        else
        {
            GameManager.instance.maxActCount += amount;
            recoveryMultiplier /= 2.0f;
        }
    }
    private void ChanceToSkipStaminaCost()
    {
        PlayerProfile playerProfile = GameObject.FindWithTag("Player").GetComponent<PlayerProfile>();
        int random = Random.Range(0, 100);
        if(random >= 0 && random < 10)
        {
            playerProfile.BuffActCount(1);
            playerProfile.NotUseActCount = true;
        }
        else
        {
            playerProfile.NotUseActCount = false;
        }
    }
    private void ApplyChaosStaminaCost()
    {
        PlayerProfile playerProfile = GameObject.FindWithTag("Player").GetComponent<PlayerProfile>();
        int random = Random.Range(0, 100);
        if (random >= 0 && random < 75)
        {
            
        }
        else if(random >= 75 && random < 95)
        {
            playerProfile.NotUseActCount = false;
            playerProfile.UseActCount(1);
        }
        else
        {
            if(!playerProfile.NotUseActCount)
            {
                playerProfile.BuffActCount(1);
            }
        }
    }

    private void EmergencyEscapeOnDeath()
    {
        PlayerProfile playerProfile = GameObject.FindWithTag("Player").GetComponent<PlayerProfile>();
        playerProfile.EmergencyEscape = true;
        GameManager.instance.installImpossibleStart = true;
    }
}
