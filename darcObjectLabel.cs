using UnityEngine;
using TMPro;

public class darcObjectLabel : MonoBehaviour
{
    [SerializeField] string _objectName = "";
    [SerializeField] TMP_Text _labelText = null;
    darcPanelManager panelManager = darcPanelManager.Instance;

    void Start()
    {
        if (panelManager != null)
            _labelText.font = panelManager.GetMainFont();
        _labelText.text = "";
        _objectName = gameObject.name;
        _labelText.text = _objectName;
    }

    void FixedUpdate()
    {
        if (_labelText != null)
        {
            if (_labelText.gameObject.activeSelf)
                TurnOffLabel();
        }
    }

    public void TurnOnLabel() => _labelText.gameObject.SetActive(true);
    public void TurnOffLabel() => _labelText.gameObject.SetActive(false);
}
