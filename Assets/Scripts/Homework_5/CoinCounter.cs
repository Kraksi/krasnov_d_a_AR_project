using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    private int _coinCount = 0;

    public void AddCoin()
    {
        _coinCount++;
        Debug.Log($"Монетка собрана! Всего монет: {_coinCount}");
    }

    public int GetCount() => _coinCount;
}