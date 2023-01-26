using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkBurgerTypesUsed : MonoBehaviour
{
    public static MarkBurgerTypesUsed instance;
    [SerializeField] private List<SpriteRenderer> _marks;
    private List<bool> _marked = new List<bool>() { false, false, false, false};

    void Awake()
    {
        instance = this;
    }
   
    public void MarkType(BurguerType type)
    {
        _marks[(int)type].gameObject.SetActive(true);
        _marked[(int)type] = true;
    }

    public void ClearAllMarks()
    {
        for(int i = 0; i < _marks.Count; i++)
        {
            _marks[i].gameObject.SetActive(false);
            _marked[i] = false;
        }
    }

    public bool CheckComplete()
    {
        for (int i = 0; i < _marked.Count; ++i)
        {
            if (_marked[i] == false)
            {
                return false;
            }
        }
        return true;
    }
}
