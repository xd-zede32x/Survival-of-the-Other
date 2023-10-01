using UnityEngine;
using UnityEngine.AI;
public class AnmalAi : MonoBehaviour
{
    private NavMeshAgent _navMesh;
    private Animator _animator;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float chanhePositionTime = 5f;
    [SerializeField] private float moveDistance =10f;

    private void Start()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _navMesh.speed = movementSpeed;
        _animator = GetComponent<Animator>();
        InvokeRepeating(nameof(MoveAnimal), chanhePositionTime, repeatRate: chanhePositionTime);
    }

    private void Update()
    {
      _animator.SetFloat("Speed", _navMesh.velocity.magnitude / movementSpeed / 2);
    }

    public Vector3 RandomNavSphere(float distanse)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distanse;
        randomDirection += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distanse, areaMask: -1);
        return navHit.position;
    }

    private void MoveAnimal()
    {
        _navMesh.SetDestination(target:RandomNavSphere(moveDistance));
    }
}