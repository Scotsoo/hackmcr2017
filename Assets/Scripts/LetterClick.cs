using UnityEngine;

public class LetterClick : MoanymonBehaviour
{
    private Inventory _inventory;
    private string _text;
    private GameObject _textObject;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        var mesh = (TextMesh) gameObject.GetComponent(typeof(TextMesh));
        _text = mesh.text;
    }

    // Update is called once per frame
    void Update()
    {
        OnClick();
    }

    public void OnClick()
    {
        if (DetectClick())
        {
            _inventory.Add(_text);
            Destroy(gameObject);
        }
    }
}
