using UnityEngine;
using UnityEngine.UI;

public class EnemuScript : MonoBehaviour
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
            _healthBar.gameObject.SetActive(false);
            Destroy(gameObject);
            Debug.Log("Вы побед  или врага"); 
        }

        else
        {
            _animatorEnemu.SetTrigger("IsAtack");
        }
    }
} 