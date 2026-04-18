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

            stageManager.curStagePos = new Vector2Int((int)(transform.position.x / stageManager.spacing), (int)(transform.position.z / stageManager.spacing));
            stageManager.curStageType = portalManager.stageType;
            stageManager.monsterSpawnManager.isMonsterSpawn = true;
            stageManager.activePortal = false;
            stageManager.curStageMonsterCount = portalManager.MonsterCount;
        }
    }

    IEnumerator StageChangeCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);
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
