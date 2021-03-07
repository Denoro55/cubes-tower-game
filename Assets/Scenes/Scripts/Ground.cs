using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private bool _isCollided = false;
    public GameObject restartButton;

    private void OnCollisionEnter(Collision other) {
        if (!_isCollided && other.gameObject.tag == "Cube") {
            for (int i = other.transform.childCount - 1; i >= 0; i--) {
                Transform child = other.transform.GetChild(i);
                child.gameObject.AddComponent<Rigidbody>();
                child.gameObject.GetComponent<Rigidbody>().AddExplosionForce(70f, Vector3.up, 5f);
                child.SetParent(null);    
            }
            
            Destroy(other.gameObject);
            _isCollided = true;
            Camera.main.gameObject.transform.position -= new Vector3(0, 0, -3f);

            restartButton.SetActive(true);
        }
    }
}
