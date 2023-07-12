using Unity.Netcode;
using UnityEngine;

public class DealDamageOnContact : MonoBehaviour
{
    [SerializeField] private int damage = 5;
    private ulong _ownerClientId;
    
    public void SetOwner(ulong ownerClientId) => _ownerClientId = ownerClientId;

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.attachedRigidbody == null) return;

        if (col.attachedRigidbody.TryGetComponent(out NetworkObject networkObject))
        {
            if(_ownerClientId == networkObject.OwnerClientId) return;
        }

        if(col.attachedRigidbody.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
        }
    }
}
 