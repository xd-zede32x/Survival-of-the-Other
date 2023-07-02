using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomController : MonoBehaviour
{
    public Animator anim;
    public Rigidbody rig;
    public Transform mainCamera;
    public float jumpForce = 3.5f;
    public float walkingSpeed = 2f;
    public float runningSpeed = 6f;
    public float currentSpeed;
    private float _animationInterpolation = 1f;
    private AudioSource _stepsSourse;
    public InventoryManagerr inventoryManagerr;
    public QuickslotInventoru quickslotInventoru;
    public Indicators indicators;

    public Transform AimTarget;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _stepsSourse = GetComponent<AudioSource>();
    }
    void Run()
    {
        _animationInterpolation = Mathf.Lerp(_animationInterpolation, 1.5f, Time.deltaTime * 3);
        anim.SetFloat("x", Input.GetAxis("Horizontal") * _animationInterpolation);
        anim.SetFloat("y", Input.GetAxis("Vertical") * _animationInterpolation);

        currentSpeed = Mathf.Lerp(currentSpeed, runningSpeed, Time.deltaTime * 3);
    }
    void Walk()
    {
        _animationInterpolation = Mathf.Lerp(_animationInterpolation, 1f, Time.deltaTime * 3);
        anim.SetFloat("x", Input.GetAxis("Horizontal") * _animationInterpolation);
        anim.SetFloat("y", Input.GetAxis("Vertical") * _animationInterpolation);

        currentSpeed = Mathf.Lerp(currentSpeed, walkingSpeed, Time.deltaTime * 3);
    }

    public void ChangeLayerWeigth(float newLayerHeigth)
    {
        StartCoroutine(SmoothLayerChange(anim.GetLayerWeight(1), newLayerHeigth, 0.3f));
    }

    IEnumerator SmoothLayerChange(float oldWigth, float newWigth, float changeDuration)
    {
        float enased = 0;

        while (enased < changeDuration)
        {
            float currentWeigth = Mathf.Lerp(oldWigth, newWigth, enased / changeDuration);
            anim.SetLayerWeight(1, currentWeigth);
            enased += Time.deltaTime;
            yield return null;
        }
        anim.SetLayerWeight(1, newWigth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (quickslotInventoru.activaSlot != null)
            {
                if (quickslotInventoru.activaSlot.item != null)
                {
                    if (quickslotInventoru.activaSlot.item.itemTape == ItemTape.Instrument)
                    {
                        if (inventoryManagerr.isOpened == false)
                        {
                            anim.SetBool("Hit", true);
                        }
                    }
                }

            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            anim.SetBool("Hit", false);
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, mainCamera.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                Walk();
            }
            else
            {
                Run();
                if (!_stepsSourse.isPlaying)
                {
                    _stepsSourse.Play();
                }
            }
        }

        else
        {
            Walk();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
        }

        Ray desiredTargetRay = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        Vector3 desiredTargetPosition = desiredTargetRay.origin + desiredTargetRay.direction * 0.7f;
        AimTarget.position = desiredTargetPosition;
    }
    void FixedUpdate()
    {
        Vector3 camF = mainCamera.forward;
        Vector3 camR = mainCamera.right;
        camF.y = 0;
        camR.y = 0;
        Vector3 movingVector;

        movingVector = Vector3.ClampMagnitude(camF.normalized * Input.GetAxis("Vertical") * currentSpeed + camR.normalized * Input.GetAxis("Horizontal") * currentSpeed, currentSpeed);
        anim.SetFloat("magnitude", movingVector.magnitude / currentSpeed);
        rig.velocity = new Vector3(movingVector.x, rig.velocity.y, movingVector.z);
        rig.angularVelocity = Vector3.zero;
    }
    public void Jump()
    {
        rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 0)
        {
            indicators.isInWater = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 0)
        {
            indicators.isInWater = false;
        }
    }
}
