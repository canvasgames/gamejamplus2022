using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClientType {
    Carnivorous = 0,
    Vegan = 1,
    LowCarb = 3,
    DairyIntolerant = 2
}

public class Client {

    public ClientType myType;

    public Client(int turn = 0)
    {
        if (turn == 1) myType = (ClientType)Random.Range(0, 2);
        else if (turn == 2) myType = (ClientType)Random.Range(0, 3);
        else if (turn == 3) myType = (ClientType)Random.Range(0, System.Enum.GetValues(typeof(FoodId)).Length);
        else myType = (ClientType)Random.Range(0, System.Enum.GetValues(typeof(FoodId)).Length);
    }
}

public class ClientMaster : MonoBehaviour
{
    public List<ClientType> levelClients, clientsLevel1, clientsLevel2, clientsLevel3, clientsLevel4, clientsLevel5;
    public ClientType currentClient;

    List<ClientType> GetLevelClients(int level)
    {
        if (level == 1) return clientsLevel1;
        else if (level == 2) return clientsLevel2;
        else if (level == 3) return clientsLevel3;
        else if (level == 4) return clientsLevel4;
        else if (level == 5) return clientsLevel5;
        else
        {
            List<ClientType> clientsToReturn = new List<ClientType>();
            clientsToReturn.Add(new ClientType());
            clientsToReturn.Add(new ClientType());
            clientsToReturn.Add(new ClientType());
            clientsToReturn.Add(new ClientType());
            return clientsToReturn;
        }
    }


    public void InitLevel(int level)
    {
        levelClients = GetLevelClients(level);
        currentClient = levelClients[0];
    }

    public bool CheckIfThisFoodTypeIsUnpleasant(FoodType food)
    {
        if (food == FoodType.Carb) return true;

        return false;
    }
    
}
