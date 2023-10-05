using UnityEngine;
public class DamageScript : MonoBehaviour
{
    [SerializeField] private int _damageAmount = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemu")
        {
            other.GetComponent<ScriptEnemu>().TakeDamage(_damageAmount);
            Debug.Log("-20");
        }
    }
} 