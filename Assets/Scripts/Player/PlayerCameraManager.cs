using Unity.Cinemachine;
using UnityEngine;

public class PlayerCameraManager : MonoBehaviour
{
    private CinemachineCamera vcam; // 연결할 시네머신 카메라
    public LayerMask wallLayer;    // 벽으로 설정된 레이어

    [Header("Height Settings")]
    public float normalHeight = 10f; // 평소 높이
    public float raisedHeight = 15f; // 벽 근처에서 올라갈 높이
    public float smoothSpeed = 3f;   // 올라가고 내려오는 속도

    [Header("Detection Settings")]
    public float detectionRadius = 4f; // 벽 감지 범위

    private CinemachineFollow follow;

    void Start()
    {
        vcam = GameObject.FindWithTag("PlayerCamera").GetComponent<CinemachineCamera>();
        if (vcam != null)
            follow = vcam.GetComponent<CinemachineFollow>();
    }

    //카메라가 벽에 부딪히면 좀 위로 올라가게 함
    void Update()
    {
        if (follow == null) return;

        bool isNearWall = Physics.CheckSphere(transform.position, detectionRadius, wallLayer);

        float targetHeight = isNearWall ? raisedHeight : normalHeight;

        Vector3 currentOffset = follow.FollowOffset;
        currentOffset.y = Mathf.Lerp(currentOffset.y, targetHeight, Time.deltaTime * smoothSpeed);

        follow.FollowOffset = currentOffset;
    }
}
