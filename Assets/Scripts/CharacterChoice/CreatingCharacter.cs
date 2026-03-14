using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreatingCharacter : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject nameZone;
    [SerializeField] private TMP_InputField nameInput;

    private bool characterClick = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!characterClick)
        {
            nameZone.SetActive(true);
            characterClick = true;
        }
    }

    public void NickNameCreate()
    {
        string name = nameInput.text;
        GameManager.instance.nickName = name;
        nameZone.SetActive(false);
    }
}
