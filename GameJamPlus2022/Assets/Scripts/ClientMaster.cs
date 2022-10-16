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
    List<ClientType> levelClients;
    public List<ClientType>  clientsLevel1, clientsLevel2, clientsLevel3, clientsLevel4, clientsLevel5;
    [HideInInspector] public ClientType currentClient;
    [HideInInspector] public int currentClientIndex;
    public int[] targetLevelScore;
    [HideInInspector] public int currentLevel = 1;
    public static ClientMaster instance;

    private void Awake()
    {
        instance = this;
    }


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
        levelClients = new List<ClientType>();
        levelClients = GetLevelClients(level);
        currentClient = levelClients[0];
        currentClientIndex = 0;
    }

    public void NextClient()
    {
        currentClientIndex++;

        if(currentClientIndex > levelClients.Count-1)
        {
            currentClient = levelClients[currentClientIndex];
        }
        else
        {
            if (ScoreController.instance.Score >= GetLevelTargetScore())
            {
                // victory
            }
            else
            {
                //Defeat
            }
        }
    }

    public int GetLevelTargetScore()
    {
        if (currentLevel <= 5) return targetLevelScore[currentLevel - 1];
        else return 30;
    }


    public bool CheckIfThisFoodTypeIsUnpleasant(FoodType food)
    {
        if (currentClient == ClientType.Carnivorous) { 
            if (food == FoodType.Vegetable) return true;
            else return false;
        }
        else if(currentClient == ClientType.Vegan) {
            if (food == FoodType.Meat) return true;
            else return false;
        }
        else if(currentClient == ClientType.LowCarb) {
            if (food == FoodType.Carb) return true;
            else return false;
        }
        else if (currentClient == ClientType.DairyIntolerant) {
            if (food == FoodType.Dairy) return true;
            else return false;
        }
        else
            return false;
    }
    
}
