﻿using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 10.0f)] private float movementSpeed = 1.0f;
    [SerializeField] private bool invertDirection = false;
    [SerializeField] [Range(0.0f, 180.0f)] private float allowedTravelAngle = 90.0f;
    [SerializeField] [Range(-180.0f, 180.0f)] private float initialAngle = 0.0f;
    [SerializeField] [Range(0.0f, 10.0f)] private float smoothing = 5.0f;
    [SerializeField] [Range(1.0f, 100.0f)] private float acceleration = 10.0f;
    [SerializeField] private InputManager InputManager;
  

    private float radius = 1.0f;

    private float inputDirection = 0.0f;
    private float currentAngle = 0.0f;
    private float currentVelocity = 0.0f;

    private Vector2 initialAxis = Vector2.zero;

    float minAngle = 0.0f;
    float maxAngle = 0.0f;

    private void OnValidate()
    {
        Start();
    }

    private void Start()
    {
        radius = GetRadiusFromRoot();
        initialAxis = GetDirectionFromSignedAngle(initialAngle * Mathf.Deg2Rad, Vector2.left);
        minAngle = allowedTravelAngle * Mathf.Deg2Rad * -1.0f;
        maxAngle = -minAngle;
        SetTransformFromAngle();
    }

    private void Update()
    {
        SetInputDirection();
    }

    private void FixedUpdate()
    {
        UpdateCurrentAngle();
        SetTransformFromAngle();
    }

    private void OnDrawGizmos()
    {
        Vector3 root = transform.root.transform.position;
        Vector3 pos = transform.position;
        Vector3 start = (Vector3)initialAxis * radius + root;
        Vector3 min = (Vector3)GetDirectionFromSignedAngle(allowedTravelAngle * Mathf.Deg2Rad, initialAxis) * radius + root;
        Vector3 max = (Vector3)GetDirectionFromSignedAngle(-allowedTravelAngle * Mathf.Deg2Rad, initialAxis) * radius + root;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(root, pos);
        Gizmos.DrawLine(root, min);
        Gizmos.DrawLine(root, max);
        Gizmos.DrawLine(start, min);
        Gizmos.DrawLine(start, max);
    }

    private float GetRadiusFromRoot()
    {
        Vector2 root = gameObject.transform.root.position;
        Vector2 toRoot = root - (Vector2)transform.position;
        float radius = toRoot.magnitude;

        return radius;
    }

    private void SetInputDirection()
    {
        //if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) {
        //    inputDirection = 0.0f;
        //}
        //if (Input.GetKey(KeyCode.A)) {
        //    inputDirection = -1.0f;
        //}
        //else if (Input.GetKey(KeyCode.D)) {
        //    inputDirection = 1.0f;
        //}
        //else {
        //    inputDirection = 0.0f;
        //}
        inputDirection = InputManager.Direction;
    }

    private void SetTransformFromAngle()
    {
        transform.localPosition = GetDirectionFromSignedAngle(currentAngle, initialAxis) * radius;
        transform.up = transform.localPosition.normalized * -1.0f;
    }

    private void UpdateCurrentAngle()
    {
        currentAngle = Mathf.Deg2Rad * Vector2.SignedAngle(initialAxis, transform.localPosition);

        float inverted = (invertDirection) ? -1.0f : 1.0f;
        float force = inputDirection * inverted * acceleration;
        if (force == 0.0f) {
            force = -currentVelocity / Time.deltaTime;
            if (smoothing != 0.0f) {
                force /= smoothing;
            }
        }

        float velocity = currentVelocity + force * Time.deltaTime;
        currentVelocity = Mathf.Clamp(velocity, -movementSpeed, movementSpeed);

        float nextAngle = currentAngle + currentVelocity * Time.deltaTime;
        currentAngle = Mathf.Clamp(nextAngle, minAngle, maxAngle);

        transform.localPosition = GetDirectionFromSignedAngle(currentAngle, initialAxis) * radius;
    }

    private Vector2 GetDirectionFromSignedAngle(float angle, Vector2 axis)
    {
        Vector2 dir = new Vector2(
                axis.x * Mathf.Cos(angle) - axis.y * Mathf.Sin(angle),
                axis.x * Mathf.Sin(angle) + axis.y * Mathf.Cos(angle)
        );

        return dir;
    }
}
