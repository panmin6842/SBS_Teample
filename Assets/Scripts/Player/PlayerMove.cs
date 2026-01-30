using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    float moveSpeed = 5.5f;

    Vector2 movement;
    RaycastHit hit;
    float rayDistance = 2;

    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xOffset = movement.x * moveSpeed * Time.deltaTime;
        float yOffset = movement.y * moveSpeed * Time.deltaTime;
        if (!HitWall())
        {
            transform.localPosition += new Vector3(xOffset, 0f, yOffset);
        }
    }

    public bool HitWall()
    {
        Debug.DrawRay(transform.position, new Vector3(movement.x * rayDistance, 0f, movement.y * rayDistance), Color.red);

        if (Physics.Raycast(transform.position, new Vector3(movement.x, 0f, movement.y), out hit, rayDistance))
        {
            if (hit.transform.tag == "Wall")
            {
                return true;
            }
        }

        return false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
}
