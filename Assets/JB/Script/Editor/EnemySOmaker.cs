using Enemy;
using UnityEditor;
using UnityEngine;

public class EnemySOmaker : EditorWindow
{
    [SerializeField] private TextAsset csvFile = null;
    [SerializeField] private EnemyInfoSO enemyInfoSO = null;
    string assetPath = $"Assets/JB/SO";


    [MenuItem("Tools/EnemySOmaker")]
    public static void ShowWindow()
    {
        GetWindow<EnemySOmaker>("EnemySOmaker");
    }

    void OnGUI()
    {
        GUILayout.Label("EnemySOmaker", EditorStyles.boldLabel);
        GUILayout.Space(10);

        SerializedObject serializedObject = new SerializedObject(this);
        SerializedProperty csv = serializedObject.FindProperty("csvFile");
        SerializedProperty enemySO = serializedObject.FindProperty("enemyInfoSO");

        EditorGUILayout.PropertyField(csv, new GUIContent("CSV File"), true);
        EditorGUILayout.PropertyField(enemySO, new GUIContent("Enemy Info SO"), true);
        serializedObject.ApplyModifiedProperties();

        if(GUILayout.Button("Make"))
        {
            if(csvFile != null && enemyInfoSO != null)
            {
                string[] data = csvFile.text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
                for (int i = 1; i < data.Length; i++) // Start from 1 to skip header
                {
                    string[] enemyData = data[i].Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

                    EnemyInfoSO newEnemy = CreateInstance<EnemyInfoSO>();

                    newEnemy.Name = enemyData[1];
                    newEnemy.MaxHp = int.Parse(enemyData[3]);
                    newEnemy.AttackPower = int.Parse(enemyData[4]);
                    newEnemy.DefensePower = float.Parse(enemyData[5]);
                    newEnemy.MoveSpeed = float.Parse(enemyData[8]);
                    newEnemy.AttackCoolTime = float.Parse(enemyData[7]);

                    AssetDatabase.CreateAsset(newEnemy, assetPath + $"/{newEnemy.Name}.asset");
                }
            }
        }
    }
}
