using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirPorFueraDeArea : MonoBehaviour {

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Destruido "+ other.gameObject.name);
        Destroy(other.gameObject);
    }
}
