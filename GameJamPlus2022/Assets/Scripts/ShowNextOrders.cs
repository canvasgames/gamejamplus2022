using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowNextOrders : MonoBehaviour
{
    public static ShowNextOrders instance;
    [SerializeField] private List<GameObject> _orders;
    [SerializeField] private List<Image> _dietIcon;

    void Awake()
    {
        instance = this;
    }

    public void NextOrders()
    {
        if (RoundController.instance.infinity)
        {
            for (int i = 0; i < _orders.Count; i++)
            {
                _orders[i].gameObject.SetActive(true);
                _dietIcon[i].sprite = Resources.Load<Sprite>("Sprites/question_mark");
            }
        }
        else
        {
            List<ClientType> clientList = ClientMaster.instance.GetLevelClients(RoundController.instance.numberOfNextOrders);
            for (int i = 0; i < _orders.Count; i++)
            {
                if (i <= ClientMaster.instance.GetLevelClients(RoundController.instance.numberOfNextOrders).Count - 1)
                {
                    _orders[i].gameObject.SetActive(true);
                    _dietIcon[i].sprite = ClientOrder.instance.GetSpriteByClientType(clientList[i]);
                }
                else
                {
                    _orders[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
