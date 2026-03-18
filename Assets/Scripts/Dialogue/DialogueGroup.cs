using UnityEngine;

[System.Serializable]
public struct DialogueData
{
    public string name; //이름
    [TextArea(3, 5)] //입력창 넓게 만들어줌
    public string sentence; //대사
    public Sprite faceImage; //캐릭터 이미지
}

[CreateAssetMenu(fileName = "DialogueGroup", menuName = "Dialogue/Group")]
public class DialogueGroup : ScriptableObject
{
    public DialogueData[] dialogues;
}
