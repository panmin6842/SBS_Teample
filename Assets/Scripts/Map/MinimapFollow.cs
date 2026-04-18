using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    void Start()
    {
        
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
    }
}
