using UnityEngine;

public class TrainController : MonoBehaviour
{
    public float PushForce = 500f;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.forward * PushForce);
            Debug.Log("Сила применена к вагончику!");
        }
    }
}