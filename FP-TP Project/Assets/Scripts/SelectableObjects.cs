using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectableObjects : MonoBehaviour, IPointerEnterHandler, IDeselectHandler
{
    private Button button;
    [SerializeField] private string buttonName;
    [SerializeField] private Text textBox;

    public void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Selectable>().Select();
        if (textBox)
        {
            textBox.text = buttonName;
        }
        Debug.Log("On button");
    }

    public void OnDeselect(BaseEventData eventData)
    {
        GetComponent<Selectable>().OnPointerExit(null);
        Debug.Log("Not on button");
    }

}
