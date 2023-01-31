using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClientType
{
    Carnivorous = 0,
    Vegan = 1,
    LowCarb = 3,
    DairyIntolerant = 2,
    Random = 4
}

public class Client
{

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
    public List<ClientType> clientsLevel1, clientsLevel2, clientsLevel3, clientsLevel4, clientsLevel5;
    public List<ClientType> clientsLevel6, clientsLevel7, clientsLevel8, clientsLevel9, clientsLevel10;

    [HideInInspector] public ClientType currentClient;
    [HideInInspector] public int currentClientIndex;
    public int[] targetLevelScore;
    [HideInInspector] public int currentLevel = 1;
    public static ClientMaster instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InitLevel();
    }

    public List<ClientType> GetLevelClients(int level)
    {
        if (level == 1) return clientsLevel1;
        else if (level == 2) return clientsLevel2;
        else if (level == 3) return clientsLevel3;
        else if (level == 4) return clientsLevel4;
        else if (level == 5) return clientsLevel5;
        else if (level == 6) return clientsLevel6;
        else if (level == 7) return clientsLevel7;
        else if (level == 8) return clientsLevel8;
        else if (level == 9) return clientsLevel9;
        else if (level == 10) return clientsLevel10;
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

    public void InitLevel()
    {
        Debug.Log($"NextLevel-{currentLevel}");
        levelClients = new List<ClientType>();
        levelClients = GetLevelClients(currentLevel);
        currentClient = levelClients[0];
        currentClientIndex = 0;
        if (GetLevelClients(currentLevel).Contains(ClientType.Random))
        {
            for (int i = 0; i < GetLevelClients(currentLevel).Count; i++)
            {
                if (GetLevelClients(currentLevel)[i] == ClientType.Random)
                {
                    GetLevelClients(currentLevel)[i] = ((ClientType)Random.Range(0, 3));
                }
            }
        }
        RoundController.instance.StartRoundLevel(currentLevel);
        DeckMaster.instance.AddShuffleUses();
        DeckMaster.instance.UpdateShuffleRemain();
    }

    public void NextClient()
    {
        currentClientIndex++;

        if (currentClientIndex < levelClients.Count)
        {
            currentClient = levelClients[currentClientIndex];
        }
        else
        {
            if (ScoreController.instance.Score >= GetLevelTargetScore())
            {
                LevelEndScreen.instance.Init(true);
            }
            else
            {
                LevelEndScreen.instance.Init(false); // Chamar aqui o game over
            }
            DeckMaster.instance.BuildInitalGameDeck();
            currentClientIndex = 0;
        }
    }

    public int GetLevelTargetScore()
    {
        if (currentLevel-1 < 9)
        {
            return targetLevelScore[currentLevel - 1];
        }
        else if(currentLevel -1 >= 9)
        {
            return targetLevelScore[9];
        }
        else return 30;
    }


    public int CheckIfThisFoodTypeIsUnpleasant(FoodType food)
    {
        if (currentClient == ClientType.Carnivorous)
        {
            if (food == FoodType.Vegetable) return 1;
            else return 0;
        }
        else if (currentClient == ClientType.Vegan)
        {
            if (food == FoodType.Meat) return 1;
            else return 0;
        }
        else if (currentClient == ClientType.LowCarb)
        {
            if (food == FoodType.Carb) return 1;
            else return 0;
        }
        else if (currentClient == ClientType.DairyIntolerant)
        {
            if (food == FoodType.Dairy) return 1;
            else return 0;
        }
        else
            return 0;
    }

}
