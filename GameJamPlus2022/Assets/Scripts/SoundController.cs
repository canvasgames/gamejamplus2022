using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance = null;
    public AudioClip[] complains;
    public AudioClip[] congratulations;
    public AudioClip[] costumers;
    public AudioClip[] ingredients;
    public AudioSource randomAudio;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
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

    public void RandomComplain()
    {
        randomAudio.clip = complains[Random.Range(0, complains.Length)];
        randomAudio.Play();
    }
    public void RandomCongratulations()
    {
        randomAudio.clip = congratulations[Random.Range(0, congratulations.Length)];
        randomAudio.Play();
    }
    public void RandomCostumers()
    {
        randomAudio.clip = costumers[Random.Range(0, costumers.Length)];
        randomAudio.Play();
    }
    public void RandomIngredients()
    {
        randomAudio.clip = ingredients[Random.Range(0, ingredients.Length)];
        randomAudio.Play();
    }

}
