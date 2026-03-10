using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_2 : MonoBehaviour
{
    

    [Header("simple fields")]
    public int IntValue = 15;
    public float FloatValue = 15;
    public string StringValue = "Homework_3";
    public double DoubleValue = 45;
    public bool BoolValue = true;
    public Color Color = Color.blue;

    [SerializeField][Range(1.0f, 10.0f)] private float _privateFloatValue;

    [Header("collections")]
    public List<string> StringList = new List<string>();
    [Space(10)]
    public List<SomeData> Data;
    private Dictionary<string, int> _dictionary = new Dictionary<string, int>();

    [Space(10)]
    [Header("references")]
    public SampleScript SampleComponent;
    public GameObject PrefabToSpawn;

    private bool _spawned = false;

    private void Start()
    {
        foreach (var item in Data)
        {
            _dictionary.Add(item.Key, item.Data);
        }
    }

    private void Update()
    {
        
        if (!_spawned)
        {
            _spawned = true;

            if (PrefabToSpawn != null)
            {
                Instantiate(PrefabToSpawn, Vector3.zero, Quaternion.identity);
                Debug.Log($"Prefab '{PrefabToSpawn.name}' добавлен в сцену");
            }
            else
            {
                Debug.LogWarning("PrefabToSpawn не назначен в инспекторе");
            }

            return;
        }

        //Debug.Log($"@IntValue: {IntValue}");
        //Debug.Log($"@FloatValue: {FloatValue}");
        //Debug.Log($"@StringValue: {StringValue}");
        //Debug.Log($"@DoubleValue: {DoubleValue}");
        //Debug.Log($"@BoolValue: {BoolValue}");
    }
}

[Serializable]
public struct SomeData
{
    public string Key;
    public int Data;
}