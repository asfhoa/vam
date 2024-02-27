using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform target;
    [SerializeField] float lerpAmount;

    Vector3 offset;
    Vector2 camSize;

    private void Start()
    {
        offset = transform.position - target.position;
        camSize.y = cam.orthographicSize * 2f;
        camSize.x = camSize.y * ((float)Screen.height / (float)Screen.width);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 destination = target.position + offset;
        destination = Background.Instance.InBoundary(destination);
        transform.position = Vector3.Lerp(transform.position, destination, lerpAmount);
    }
}
