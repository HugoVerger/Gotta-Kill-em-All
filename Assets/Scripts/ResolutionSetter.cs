using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionSetter : MonoBehaviour {

    void Start() {
        Screen.SetResolution(768, 768, true, 0);
    }
}