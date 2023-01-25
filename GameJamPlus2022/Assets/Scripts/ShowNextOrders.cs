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
        List<ClientType> clientList = ClientMaster.instance.GetLevelClients(RoundController.instance.numberOfNextOrders);
        int invertedOrderPosition;
        for (int i = 0; i < _orders.Count; i++)
        {
            invertedOrderPosition = -(i - RoundController.instance.numberOfNextOrders) - 1;
            if (i <= ClientMaster.instance.GetLevelClients(RoundController.instance.numberOfNextOrders).Count - 1)
            {
                _orders[invertedOrderPosition].gameObject.SetActive(true);
                _dietIcon[invertedOrderPosition].sprite = ClientOrder.instance.GetSpriteByClientType(clientList[i]);
            }
            else
            {
                _orders[i].gameObject.SetActive(false);
            }
        }
    }
}
