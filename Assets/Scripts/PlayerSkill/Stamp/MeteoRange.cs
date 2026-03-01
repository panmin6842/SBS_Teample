using UnityEngine;
using UnityEngine.InputSystem;

public class MeteoRange : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector3 mouseClickPoint;
    private Vector3 mouseWorldPos;

    private Ray ray;
    private RaycastHit hit;
    private float rayDistance = 100;

    private bool attack;
    private bool click;

    [SerializeField] private GameObject meteo;

    [SerializeField] private InputActionAsset uiInputAction;
    [SerializeField] private InputActionMap uiActionMap;

    private PlayerAttack playerAttack;
    private GameObject player;

    private void Awake()
    {
        uiActionMap = uiInputAction.FindActionMap("Player");
    }

    private void OnEnable()
    {
        uiActionMap.Enable();
        uiActionMap.FindAction("Attack").performed += OnAttack;
    }

    private void Start()
    {
        player = GameObject.Find("Player");
        playerAttack = player.GetComponent<PlayerAttack>();
        playerAttack.uiClicking = true;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (attack && !click)
        {
            if (gameObject.GetComponent<SpriteRenderer>().enabled)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            click = true;

            Invoke("DestroyObject", 2);
        }
    }

    private void DestroyObject()
    {
        Instantiate(meteo, new Vector3(mouseClickPoint.x, 10, mouseClickPoint.z),
                meteo.transform.rotation);
        playerAttack.uiClicking = false;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!click)
        {
            transform.position = player.transform.position;

            mousePos = Mouse.current.position.ReadValue();
            ray = Camera.main.ScreenPointToRay(mousePos);

            //Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

            if (Physics.Raycast(ray, out hit, rayDistance, LayerMask.GetMask("MeteoArea")))
            {
                mouseClickPoint = hit.point;
                attack = true;
            }
            else
            {
                attack = false;
            }
        }
    }
}
