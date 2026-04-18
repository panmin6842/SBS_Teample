using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class StageDetector : MonoBehaviour
{
    PortalManager portalManager;
    StageManager stageManager;

    private void Awake()
    {
    }

    void Start()
    {
        portalManager = GetComponentInParent<PortalManager>();
        stageManager = StageManager.instance;

        StartCoroutine(StageChangeCoroutine());
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(StageChangeCoroutine());
            var confinder = portalManager.CinemachineCamera.GetComponent<CinemachineConfiner3D>();
            confinder.BoundingVolume = gameObject.GetComponent<Collider>();

            stageManager.CurStagePos = new Vector2Int((int)(transform.localPosition.x / stageManager.spacing), (int)(transform.localPosition.y / stageManager.spacing));
        }
    }

    IEnumerator StageChangeCoroutine()
    {
        portalManager.PortalEffectImage.gameObject.SetActive(true);
        //yield return new WaitForSeconds(0.5f);
        Color c = Color.black;
        //Time.timeScale = 0;
        for (float i = 1; i > 0; i -= Time.unscaledDeltaTime)
        {
            c.a = i;
            portalManager.PortalEffectImage.color = c;
            yield return null;
        }
        //Time.timeScale = 1;
        portalManager.PortalEffectImage.gameObject.SetActive(false);
    }
}
