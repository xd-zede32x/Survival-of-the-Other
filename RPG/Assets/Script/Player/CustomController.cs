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
    public AudioSource StepsSourse;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StepsSourse = GetComponent<AudioSource>();
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
    private void Update()
    {
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
                if (!StepsSourse.isPlaying)
                {
                    StepsSourse.Play();
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
}
