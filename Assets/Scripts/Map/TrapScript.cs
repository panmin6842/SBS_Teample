using System.Collections;
using UnityEngine;

enum TrapType
{
    Spike
}

public class TrapScript : MonoBehaviour
{
    GameObject player;
    [SerializeField] TrapType trapType;

    bool isDamage = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (trapType)
            {
                case TrapType.Spike:
                    SpikeDamage();
                    break;
            }
        }
    }

    IEnumerator SpikeDamage()
    {
        isDamage = true;
        player.GetComponent<PlayerProfile>().GetDamage(10);
        yield return new WaitForSeconds(0.5f);
        isDamage = false;
    }
}
