using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float MoveSpeed = 5f;
    public float RotateSpeed = 90f;

    [Header("Color Change")]
    public Color[] Colors = new Color[]
    {
        Color.red,
        Color.green,
        Color.blue,
        Color.yellow,
        Color.cyan,
        Color.magenta
    };

    private Renderer _renderer;
    private int _colorIndex = 0;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();

        if (_renderer != null)
            _renderer.material.color = Colors[_colorIndex];
    }

    private void Update()
    {
        
        float moveInput = 0f;
        if (Input.GetKey(KeyCode.W)) moveInput = 1f;
        if (Input.GetKey(KeyCode.S)) moveInput = -1f;

        transform.position += transform.forward * moveInput * MoveSpeed * Time.deltaTime;

        
        float rotateInput = 0f;
        if (Input.GetKey(KeyCode.A)) rotateInput = -1f;
        if (Input.GetKey(KeyCode.D)) rotateInput = 1f;

        transform.Rotate(Vector3.up, rotateInput * RotateSpeed * Time.deltaTime);

        
        if (Input.GetKeyDown(KeyCode.C))
        {
            _colorIndex = (_colorIndex + 1) % Colors.Length;
            if (_renderer != null)
                _renderer.material.color = Colors[_colorIndex];
        }
    }
}