using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class MPBullet : Projectile
{
    float pastTime = 0f;
    int estimateTimePast = 0;
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        pastTime += Time.deltaTime;
        if (pastTime >= 10f)
        {
            Destroy(gameObject);
        }
        if(pastTime >= estimateTimePast + 1f)
        {
            estimateTimePast++;
            this.transform.LookAt(player.transform.position);
        }
        this.transform.Translate(Vector3.forward * Time.deltaTime * 2f);
    }
}
