using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class WorldPlatform : MonoBehaviour
{
    private Collider2D col;
    private Tilemap tilemap;
    private InputAction m_SwitchWorld;
    public bool defaultActive;
    public Color inactiveColor;
    public Color activeColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<Collider2D>();
        col.enabled = defaultActive;
        m_SwitchWorld = InputSystem.actions.FindAction("Player/Interact");
        m_SwitchWorld.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_SwitchWorld.WasPressedThisFrame())
        {
            col.enabled = !col.enabled;
        }
    }
}
