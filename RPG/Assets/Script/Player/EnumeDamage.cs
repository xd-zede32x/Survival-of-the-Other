using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumeDamage : MonoBehaviour
{
    public int DamageCount = 10;

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(FindObjectOfType<PlayerManager>().Damage(DamageCount));
    }
}
