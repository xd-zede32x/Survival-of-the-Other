using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapons : MonoBehaviour
{
    public Music music;
    public GameObject camera;
    public float distanse = 15;
    GameObject currentWeapon;
    bool canPickUp;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
    }

    void PickUp()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, distanse))
        {
            if (hit.transform.tag == "Weapon")
            {
                if (canPickUp) Drop();

                currentWeapon = hit.transform.gameObject;
                currentWeapon.GetComponent<Rigidbody>().isKinematic = true;
                currentWeapon.transform.parent = transform;
                currentWeapon.transform.localPosition = Vector3.zero;
                currentWeapon.transform.localEulerAngles = new Vector3(16.7f, -44f, -2.591f);
                canPickUp = true;

            }
        }
    }

    void Drop()
    {
        currentWeapon.transform.parent = null;
        currentWeapon.GetComponent<Rigidbody>().isKinematic = false;
        canPickUp = false;
        currentWeapon = null;
    }
}