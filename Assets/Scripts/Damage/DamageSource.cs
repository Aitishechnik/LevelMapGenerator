using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField]
    private int _damageValue = 1;

    [SerializeField]
    private List<string> tags = new List<string>();
    private void OnTriggerStay(Collider other)
    {
        foreach (var tag in tags)
        {
            if (other.CompareTag(tag))
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
