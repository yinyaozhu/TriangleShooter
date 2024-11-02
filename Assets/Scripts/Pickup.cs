using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestoryAfter5Sec());
    }

    IEnumerator DestoryAfter5Sec() {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public virtual void Onpicked() { 
        Destroy(gameObject);
    }
}
