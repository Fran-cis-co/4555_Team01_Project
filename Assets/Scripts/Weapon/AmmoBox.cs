using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmmoBox : MonoBehaviour
{
    public int ammoAmount = 200;
    public AmmoType ammoType;
    public float spinSpeed;

    public enum AmmoType
    { 
        PistolAmmo
    }

    private void Update()
    {
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f, Space.Self);
    }

}
