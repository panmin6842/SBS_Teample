using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StageDetector : MonoBehaviour
{
    PortalManager portalManager;
    private void Awake()
    {
    }

    void Start()
    {
        portalManager = PortalManager.instance;
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(StageChangeCoroutine());
        }
    }

    IEnumerator StageChangeCoroutine()
    {
        portalManager.PortalEffectImage.gameObject.SetActive(true);
        Color c = Color.black;
        for (float i = 1; i > 0; i -= Time.deltaTime)
        {
            c.a = i;
            portalManager.PortalEffectImage.color = c;
            yield return new WaitForSeconds(0f);
        }
        portalManager.PortalEffectImage.gameObject.SetActive(false);
    }
}
