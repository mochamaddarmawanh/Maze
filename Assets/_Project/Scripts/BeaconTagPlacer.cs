using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class BeaconTagPlacer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int maxBeaconTags = 5;

    [Header("Beacon")]
    [SerializeField] private GameObject beaconTagPrefab;
    private int currentBeaconTags;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI beaconTagText;

    [Header("Player")]
    private Transform player;
    private CharacterController characterController;

    void Start()
    {
        player =
            GameObject
            .FindGameObjectWithTag("Player")
            .transform;

        characterController =
            player.GetComponent<CharacterController>();

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
        if (!characterController.isGrounded) return;

        if (currentBeaconTags <= 0) return;

        Vector3 spawnPosition =
            new Vector3(
                player.position.x, 0f,
                player.position.z
            ) + player.forward * 0.8f;

        Instantiate(
            beaconTagPrefab,
            spawnPosition,
            Quaternion.identity
        );

        currentBeaconTags--;

        UpdateBeaconTagUI();
    }

    void UpdateBeaconTagUI()
    {
        beaconTagText.text = "Beacon Tag(s): " + currentBeaconTags + "/" + maxBeaconTags;
    }
}