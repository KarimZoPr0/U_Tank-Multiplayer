using System;
using UnityEngine;

public class RespawningCoin : Coin
{
    public event Action<RespawningCoin> OnCollected;

    private Vector3 _previousPosition;
    public override int Collect()
    {
        if (!IsServer)
        {
            Show(false);
            return 0;
        }
        if (AlreadyCollected) return 0;
        AlreadyCollected = true;
        OnCollected?.Invoke(this);
        return CoinValue;
    }

    private void Update()
    {
        if (!IsClient) return;
        if (_previousPosition != transform.position )
        {
            Show(true);
        }
        _previousPosition = transform.position;

    }

    public void Reset() => AlreadyCollected = false;
}
