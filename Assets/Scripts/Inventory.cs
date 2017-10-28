using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//public class InventoryObject
//{
//    public char Letter { get; set; }
//}

public class Inventory : MonoBehaviour, IEnumerable<string>
{
    private List<string> _letters;

	// Use this for initialization
	void Start () {
	    _letters = new List<string>();
    }

    public void Add(string letter)
    {
        _letters.Add(letter);
        _letters.Sort();
        Debug.Log(string.Format("Added letter '{0}' to inventory\nCurrent Inventory: {1}", letter, string.Join(", ", _letters.ToArray())));
    }

    public void Remove(string letter)
    {
        if (_letters.Remove(letter))
        {
            _letters.Sort();
            Debug.Log(string.Format("Removed letter '{0}' from inventory\nCurrent Inventory: {1}", letter, string.Join(", ", _letters.ToArray())));
        }
    }

    public List<string> Dump()
    {
        return _letters;
    }

    public IEnumerator<string> GetEnumerator()
    {
        return _letters.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
