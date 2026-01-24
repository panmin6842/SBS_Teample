using UnityEngine;

public class BowAttackManager : MonoBehaviour
{
    float power = 4.0f;

    Vector3 startPos;
    float distance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * power * Time.deltaTime;

        distance = Vector3.Distance(startPos, transform.position);

        if (distance > 10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }
}
