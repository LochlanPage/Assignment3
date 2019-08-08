using UnityEngine;

public class Camera_Control : MonoBehaviour
{
    [SerializeField]
    float speed = 5.0f;

    [SerializeField]
    float maxSpeed = 10;

    [SerializeField]
    float minSpeed = 0;

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (speed >= minSpeed && speed <= maxSpeed)
            {
                if (Input.GetKeyDown(KeyCode.E))
                    speed++;

                if (Input.GetKeyDown(KeyCode.Q))
                    speed--;
            }
            else if (speed < minSpeed)
                speed = minSpeed;
            else if (speed > maxSpeed)
                speed = maxSpeed;

            transform.position += speed * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * Time.deltaTime;
        }
    }

}
