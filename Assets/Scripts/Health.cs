using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    public Manager manager;
    [Range(0, 1)]
    public float value;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != tag && manager.isAlive[other.gameObject.tag])
        {
            manager.TakeDamage(value, tag);
        }
    }
}
