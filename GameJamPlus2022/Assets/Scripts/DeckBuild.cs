using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class DeckBuild : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DrawCard()
    {
        GameDeck.s._deck.Remove(null);//null = carta pra sair do deck do jogo? Fazer um random do que tem no deck
        //Player.deck.Add(card); Adiciona essa carta pra mão do jogador?
    }

    private void PlayCard(Card card)
    {
        //Player.deck.Remove(card);
        //Score(card); //Contabiliza a pontuação da carta, sendo positiva ou negativa depenendo do cliente
    }

    
}
