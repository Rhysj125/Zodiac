using UnityEngine;

public class ScrollingCameraScript : MonoBehaviour
{
    // Settings
    [Range(0.1f, 0.9f)]
    public float ScrollSpeed = 0.5f;

    // Components
    private Transform _transform;

    void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _transform.position += new Vector3(0, ScrollSpeed * Time.deltaTime);
    }
}
