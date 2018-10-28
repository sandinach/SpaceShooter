using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestruible
{
    void Destruir(Transform transform);
}

public class AnimacionDeDestruccion : MonoBehaviour, IDestruible
{
    public GameObject explosion;
	
	// Update is called once per frame
	public void Destruir(Transform transform)
    {
		if(explosion != null)
        {
            //Debug.Log("Destruccion...");
            Instantiate(explosion, transform.position, transform.rotation);
        }
	}
}
