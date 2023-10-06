using UnityEngine;

public class RotateObject : BaseBehaviour
{
    private float staticRotationX = 190f;
    private float staticRotationZ = 2.5f;
    private float minInterval = 2f;
    private float maxInterval = 5f;
    private float minRotation = -30f;
    private float maxRotation = 30f;
    private string triggerRoomNum = "705";
    private string nextTriggerRoomNum = "706";

    private float targetRotation;
    private float currentRotation;
    private float rotationSpeed;
    private float timeElapsed;

    private Rigidbody rb;

    private void Start()
    {
        currentRotation = transform.rotation.eulerAngles.y;
        targetRotation = currentRotation;
        rotationSpeed = Random.Range(minInterval, maxInterval);

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rb.isKinematic && (mainUIScript.CurrentRoom == triggerRoomNum || mainUIScript.CurrentRoom == nextTriggerRoomNum))
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= rotationSpeed)
            {
                currentRotation = targetRotation;
                targetRotation = Mathf.Clamp((currentRotation + Random.Range(minRotation, maxRotation)), currentRotation, currentRotation - 50f);
                rotationSpeed = Random.Range(minInterval, maxInterval);
                timeElapsed = 0f;
            }

            float rotation = Mathf.Lerp(currentRotation, targetRotation, timeElapsed / rotationSpeed);
            transform.rotation = Quaternion.Euler(staticRotationX, rotation, staticRotationZ);
        }
    }
}
