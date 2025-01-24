using TMPro;
using UnityEngine;

public class ResourcesDisplay : MonoBehaviour
{
    [SerializeField] private ResourceDepot _depot;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private void Update()
    {
        ChangeDisplay();
    }

    private void ChangeDisplay()
    {
        _textMeshProUGUI.text = "Ресурсов на базе: " + _depot.QuantityResources.ToString();
    }
}
