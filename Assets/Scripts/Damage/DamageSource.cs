using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField]
    private int _damageValue = 1;

    [SerializeField]
    private List<TagID> tags = new List<TagID>();
    private void OnTriggerStay(Collider other)
    {
        foreach (var tag in tags)
        {
            if (other.CompareTag(tag.Value))
            {
                var targetComponents = other.gameObject.GetComponents<IDamagable>();
                foreach (var targetComponent in targetComponents)
                {
                    targetComponent.ReceiveDamage(new Damage(_damageValue));
                    
                }
                return;
            }
        }
    }
}
