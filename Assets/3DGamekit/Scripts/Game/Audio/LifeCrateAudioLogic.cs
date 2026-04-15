using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCrateAudioLogic : MonoBehaviour
{
    public GameObject HealthAudioFieldObject;
    public GameObject PlayerCapsule;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == PlayerCapsule.name)
        {
            HealthAudioFieldObject.SetActive(false);
        }
    }

    void Update()
    {
        
    }
}
