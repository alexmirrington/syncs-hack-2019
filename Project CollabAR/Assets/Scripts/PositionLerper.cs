using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionLerper : MonoBehaviour {

    private Vector3 newPosition;
    public float speed = 0.5f;

    public void UpdatePosition(Vector3 pos) {
        newPosition = pos;
    }

    void Start() {
        newPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update() {
        this.transform.position = Vector3.Lerp(this.transform.position, newPosition, speed);
    }
}
