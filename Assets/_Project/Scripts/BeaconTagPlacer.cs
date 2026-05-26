using UnityEngine;
using UnityEngine.InputSystem;

public class BeaconTagPlacer : MonoBehaviour
{
    [Header("Beacon")]
    [SerializeField] private GameObject beaconTagPrefab;

    [Header("Settings")]
    [SerializeField] private int maxBeaconTags = 12;

    private int currentBeaconTags;

    void Start()
    {
        currentBeaconTags = maxBeaconTags;
    }

    void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            PlaceBeaconTag();
        }
    }

    void PlaceBeaconTag()
    {
        if (currentBeaconTags <= 0) return;

        Instantiate(
            beaconTagPrefab,
            transform.position + transform.forward * 0.8f,
            Quaternion.identity
        );

        currentBeaconTags--;
    }
}