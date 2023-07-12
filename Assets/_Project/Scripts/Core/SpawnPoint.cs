using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPoint : MonoBehaviour
{
    private static List<SpawnPoint> _spawnPoints = new();

    private void OnEnable() => _spawnPoints.Add(this);
    private void OnDisable() => _spawnPoints.Remove(this);

    public static Vector3 GetRandomSpawnPos()
    {
        if (_spawnPoints.Count == 0) return Vector3.zero;
        int index = Random.Range(0, _spawnPoints.Count);
        return _spawnPoints[index].transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 1);
    }
}
