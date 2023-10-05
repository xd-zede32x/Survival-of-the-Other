using UnityEngine;

public class EnumeDamage : MonoBehaviour
{
    public float DamageCount = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(FindObjectOfType<PlayerManager>().Damage(DamageCount));  
    }
}