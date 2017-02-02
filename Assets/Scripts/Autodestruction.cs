using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestruction : MonoBehaviour {
    [SerializeField]
    private float m_TimeOut = 1.0f;
    [SerializeField]
    private bool m_DetachChildren = false;

    private void Awake() {
        Invoke("DestroyNow", m_TimeOut);
    }

    private void DestroyNow() {
        if (m_DetachChildren) {
            transform.DetachChildren();
        }
        DestroyObject(gameObject);
    }
}
