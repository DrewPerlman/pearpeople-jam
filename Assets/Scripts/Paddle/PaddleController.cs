using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 10.0f)] private float movementSpeed = 1.0f;
    [SerializeField] private bool invertDirection = false;
    [SerializeField] [Range(0.0f, 1.0f)] private float allowedTravelAngle = 0.5f;
    [SerializeField] [Range(-180.0f, 180.0f)] private float initialAngle = 0.0f;

    private float radius = 1.0f;
    private float currentAngle = 0.0f;
    private float inputDirection = 0.0f;

    private Vector2 initialAxis = Vector2.zero;

    private void OnValidate()
    {
        Start();
    }

    private void Start()
    {
        radius = GetRadiusFromRoot();
        initialAxis = GetDirectionFromSignedAngle(initialAngle * Mathf.Deg2Rad, Vector2.left);
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
        Vector3 min = (Vector3)GetDirectionFromSignedAngle(allowedTravelAngle * Mathf.PI, initialAxis) * radius + root;
        Vector3 max = (Vector3)GetDirectionFromSignedAngle(-allowedTravelAngle * Mathf.PI, initialAxis) * radius + root;

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
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) {
            inputDirection = 0.0f;
        }
        if (Input.GetKey(KeyCode.A)) {
            inputDirection = -1.0f;
        }
        else if (Input.GetKey(KeyCode.D)) {
            inputDirection = 1.0f;
        }
        else {
            inputDirection = 0.0f;
        }
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
        float deltaAngle = movementSpeed * inputDirection * Time.deltaTime * inverted;
        float nextAngle = currentAngle + deltaAngle;

        currentAngle = Mathf.Clamp(nextAngle, allowedTravelAngle * Mathf.PI * -1.0f, allowedTravelAngle * Mathf.PI);

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
