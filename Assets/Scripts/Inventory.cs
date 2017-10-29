using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class Inventory : MonoBehaviour, IEnumerable<InventoryObject>
{
    private List<InventoryObject> _letters;

	// Use this for initialization
	void Start () {
	    _letters = new List<InventoryObject>();
    }

    private string GetCurrentInventory()
    {
        return string.Join("|", _letters.Select(x => x.ToString()).ToArray());
    }

    private InventoryObject FindMatchingObjectInLetters(string letterToFind)
    {
        return _letters.Find(x => string.Equals(x.Character, letterToFind));
    }

    public void Add(string letter)
    {
        InventoryObject matchingObject = FindMatchingObjectInLetters(letter);
        if (matchingObject != default(InventoryObject))
        {
            matchingObject.CaptureCount += 1;
            Debug.Log(string.Format("Incremented capture count on '{0}' to {1}\nCurrent Inventory: {2}", matchingObject.Character, matchingObject.CaptureCount, GetCurrentInventory()));
        }
        else
        {
            InventoryObject newLetter = new InventoryObject
            {
                CaptureCount = 1,
                Character = letter
            };

            _letters.Add(newLetter);
            Debug.Log(string.Format("Added '{0}' to inventory\nCurrent Inventory: {1}", letter, GetCurrentInventory()));
        }

        _letters = _letters.OrderBy(x => x.Character).ToList();
    }

    public void Remove(string letter)
    {
        if (_letters.Count == 0)
        {
            return;
        }

        InventoryObject matchingObject = FindMatchingObjectInLetters(letter);
        if (matchingObject != default(InventoryObject))
        {
            if (matchingObject.CaptureCount > 1)
            {
                matchingObject.CaptureCount -= 1;
                Debug.Log(string.Format("Decremented capture count on '{0}' to {1}\nCurrent Inventory: {2}", matchingObject.Character, matchingObject.CaptureCount, GetCurrentInventory()));
            }
            else
            {
                if (_letters.Count == 1)
                {
                    Flush();
                }
                else
                {
                    _letters.Remove(matchingObject);
                }

                Debug.Log(string.Format("Removed letter '{0}' from inventory\nCurrent Inventory: {1}", letter, GetCurrentInventory()));
            }
        }
    }

    public List<InventoryObject> Dump()
    {
        return _letters;
    }

    public void Flush()
    {
        _letters = new List<InventoryObject>();
    }

    public IEnumerator<InventoryObject> GetEnumerator()
    {
        return _letters.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
