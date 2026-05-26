using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class BeaconTagPlacer : MonoBehaviour
{
    [Header("Beacon")]
    [SerializeField] private GameObject beaconTagPrefab;

    [Header("Settings")]
    [SerializeField] private int maxBeaconTags = 5;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI beaconTagText;

    private int currentBeaconTags;

    void Start()
    {
        currentBeaconTags = maxBeaconTags;

        UpdateBeaconTagUI();
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

        UpdateBeaconTagUI();
    }

    void UpdateBeaconTagUI()
    {
        beaconTagText.text = "Beacon Tags: " + currentBeaconTags + "/" + maxBeaconTags;
    }
}