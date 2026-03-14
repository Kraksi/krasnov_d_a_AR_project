using UnityEngine;

public class FPCoinCounter : MonoBehaviour
{
    private int _coinCount = 0;

    public void AddCoin()
    {
        _coinCount++;
    }

    public int GetCount() => _coinCount;
}