using UnityEngine;
using UnityEngine.UI;

public class ScriptEnemu : MonoBehaviour
{
    [SerializeField] private GameObject _vfx;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private GameObject _textWin;
    [SerializeField] private Animator _animatorEnemu;
    [SerializeField] private Animator _textAnimation;
    [SerializeField] private Transform _enemuTransform;

    private int _enemuHealth = 100;

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
            _textWin.SetActive(true);
            _textAnimation.Play("text");
            _healthBar.gameObject.SetActive(false);
            GameObject _effect = Instantiate(_vfx, _enemuTransform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject, 2);
            Destroy(_effect, 10);
        }
    }

}