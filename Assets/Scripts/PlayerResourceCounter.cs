using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResourceCounter : MonoBehaviour
{
    [SerializeField] private Text text;
    private int Resource; // Assuming 'wood' is an integer value.

    // Start is called before the first frame update
    void Start()
    {
        UpdateCounterUI();
    }

    public void UpdateCounterUI()
    {
        text.text = Resource.ToString(); // Changed 'value' to 'text' and converted wood value to string.
    }

    public void ChangeCounter(int value)
    {
        Resource = value;
        UpdateCounterUI();
    }
}