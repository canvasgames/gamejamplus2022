using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkBurgerTypesUsed : MonoBehaviour
{
    public static MarkBurgerTypesUsed instance;
    [SerializeField] public List<SpriteRenderer> _marks;

    void Awake()
    {
        instance = this;
    }
   
    public void MarkType(BurguerType type)
    {
        _marks[(int)type].gameObject.SetActive(true);
    }

    public void ClearAllMarks()
    {
        foreach(SpriteRenderer mark in _marks)
        {
            mark.gameObject.SetActive(false);
        }
    }
}
