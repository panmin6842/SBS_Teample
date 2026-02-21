using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private PlayerProfile playerProfile;

    private Vector2 movement;
    private RaycastHit hit;
    private float rayDistance = 2;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerProfile = GetComponent<PlayerProfile>();
    }

    // Update is called once per frame
    void Update()
    {
        float xOffset = movement.x * playerProfile.moveSpeed * Time.deltaTime;
        float yOffset = movement.y * playerProfile.moveSpeed * Time.deltaTime;
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
            if (hit.transform.CompareTag("Wall"))
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
