using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikeTrapDemo : MonoBehaviour {

    //This script goes on the SpikeTrap prefab;

    int damageAmount = 10; //Amount of damage the trap will deal to the player;
    [SerializeField] float delay;
    [SerializeField] bool stopTrap = false;

    bool isDamageOn = false; //Whether the trap is currently damaging or not;

    public Animator spikeTrapAnim; //Animator for the SpikeTrap;

    // Use this for initialization
    void Awake()
    {
        //get the Animator component from the trap;
        spikeTrapAnim = GetComponent<Animator>();
        //start opening and closing the trap for demo purposes;
        StartCoroutine(Delay(delay));
    }

    private void Update()
    {
        damageAmount = Mathf.FloorToInt(GameObject.FindWithTag("Player").GetComponent<PlayerProfile>().MaxHp * (5f / 100f));
    }

    IEnumerator Delay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(OpenCloseTrap());
    }

    IEnumerator OpenCloseTrap()
    {
        //play open animation;
        spikeTrapAnim.SetTrigger("open");
        isDamageOn = true;
        //wait 2 seconds;
        if (stopTrap)
        {
            isDamageOn = true;
            yield break;
        }
        yield return new WaitForSeconds(1);
        //play close animation;
        spikeTrapAnim.SetTrigger("close");
        isDamageOn = false;
        //wait 2 seconds;
        yield return new WaitForSeconds(2);
        //Do it again;
        StartCoroutine(OpenCloseTrap());
    }

    private void OnTriggerStay(Collider other)
    {
        //if the trap is currently damaging and the player enters the trap's trigger;
        if (isDamageOn && other.CompareTag("Player"))
        {
            //damage the player here;
            StartCoroutine(GiveDamage());
        }
    }

    IEnumerator GiveDamage()
    {
        isDamageOn = false;

        GameObject.FindWithTag("Player").GetComponent<PlayerProfile>().GetDamage(damageAmount);
        yield return new WaitForSeconds(1);

        if (stopTrap)
        {
            isDamageOn = true;
        }
    }
}