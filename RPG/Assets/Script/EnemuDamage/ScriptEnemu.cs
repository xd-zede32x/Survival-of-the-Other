using UnityEngine;
using UnityEngine.UI;

public class ScriptEnemu : MonoBehaviour
{
    private int _enemuHealth = 100;
    [SerializeField] Animator _animatorEnemu;
    [SerializeField] Slider _healthBar;

    private void Update()
    {
        _healthBar.value = _enemuHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        _enemuHealth -= damageAmount;

        if (_enemuHealth <= 0)
        {
            _animatorEnemu.SetTrigger("death");
            GetComponent<Collider>().enabled = false;
            _healthBar.gameObject.SetActive(false);
            Debug.Log("Вы победили врага");
        }

        else
        {
            _animatorEnemu.SetTrigger("damage");
        }
    }
}
