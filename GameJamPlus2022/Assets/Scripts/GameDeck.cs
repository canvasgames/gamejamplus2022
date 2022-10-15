using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDeck : MonoBehaviour
{
    public static GameDeck s = null;
    public List<Card> _deck;

    void Awake()
    {
        if (s == null)
        {
            s = this;
        }
        else if (s != this)
        {
            Destroy(gameObject);
        }
    }  
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RemoveCard()
    {
        //_deck.Remove(card); //card seria uma carta random no deck que vem quando compra
    }
}
