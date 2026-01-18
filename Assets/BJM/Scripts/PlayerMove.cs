using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    float moveSpeed = 5;

    Vector2 movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float xOffset = movement.x * moveSpeed * Time.deltaTime;
        float yOffset = movement.y * moveSpeed * Time.deltaTime;
        transform.localPosition += new Vector3(xOffset, 0f, yOffset);
    }

    private void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }
}
