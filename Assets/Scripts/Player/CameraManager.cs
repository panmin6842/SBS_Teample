using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;

    [Header("카메라 제한 영역")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void LateUpdate()
    {
        float targetX = player.position.x + offset.x;
        float targetY = player.position.y + offset.y;

        // 2. Mathf.Clamp를 이용해 범위를 제한
        float clampedX = Mathf.Clamp(targetX, minX, maxX);
        float clampedY = Mathf.Clamp(targetY, minY, maxY);

        // 3. 카메라 위치 적용 (Z값은 유지)
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
