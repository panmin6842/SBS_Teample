using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            transform.localPosition = new Vector2(0, 0);
            transform.localScale = new Vector2(4f, 4f);
        }
        else
        {
            transform.localPosition = new Vector2(800, 400);
            transform.localScale = new Vector2(1f, 1f);
        }
    }
}
