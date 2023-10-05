using UnityEngine;
public class LookAlPlayer : MonoBehaviour
{
    [SerializeField] private Transform _camera;

    private void LateUpdate()
    {
        transform.LookAt(_camera);
    }
}