using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScript : MonoBehaviour
{
    public int IntValue;
    public float FloatValue;
    public string StringValue;
    public double DoubleValue;
    public bool BoolValue;
    public Color Color;

    public SampleScript SampleComponent;
    private void Start()
    {
        Debug.Log($"IntValue: {IntValue}");
        Debug.Log($"FloatValue: {FloatValue}");
        Debug.Log($"StringValue: {StringValue}");
        Debug.Log($"DoubleValue: {DoubleValue}");
        Debug.Log($"BoolValue: {BoolValue}");

        
    }

    private void Update()
    {
        Debug.Log($"@IntValue: {IntValue}");
        Debug.Log($"@FloatValue: {FloatValue}");
        Debug.Log($"@StringValue: {StringValue}");
        Debug.Log($"@DoubleValue: {DoubleValue}");
        Debug.Log($"@BoolValue: {BoolValue}");
    }
}
