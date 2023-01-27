using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct NamedImage
{
    public string name;
    public Sprite image;
}

public class DemonAvatar : MonoBehaviour
{
    enum bodyTypes
    {
        thinWimp = 0,
        thinMuscular = 1,
        fatWimp = 2,
        fatMuscular = 3,
    }

   [SerializeField] Color[] skinColors, primaryColors, secondaryColors, hairColors, eyeColors;

   [SerializeField] NamedImage[] femaleHairSprites, maleHairSprites;
   [SerializeField] DemonAvatarBody myBody;
   [SerializeField] DemonAvatarBody[] femaleBodies, maleBodies;

    Dictionary<string, Sprite> femaleHairDictionary, maleHairDictionary;

    bodyTypes myBodyType;


    void Start()
    {

        femaleHairDictionary = new Dictionary<string, Sprite>();
        foreach (NamedImage item in femaleHairSprites)
        {
            femaleHairDictionary.Add(item.name, item.image);
        }
        maleHairDictionary = new Dictionary<string, Sprite>();
        foreach (NamedImage item in maleHairSprites)
        {
            maleHairDictionary.Add(item.name, item.image);
        }

        //DefineParts(true,        RandomBool(),     RandomBool(),      "#f9d4ab",        "#f9d4ab",        "#f9d4ab",               "#f9d4ab",           "#f9d4ab",     "#f9d4ab");
        CreateDemon();
        CreateDemon();
        CreateDemon();
        CreateDemon();
        CreateDemon();
        CreateDemon();
        dist = 0;
    }

    public void CreateDemon()
    {
        DefineParts(true, RandomBool(), RandomBool(), RandomEyeColors(), RandomHairColor(), RandomSkinColor(), RandomPrimaryColor(), RandomSecondaryColor(), RandomColor());
    }

    string RandomColor()
    {
        int r = UnityEngine.Random.Range(0, 9);
        if (r == 0) return "#f9d4ab";
        else if (r == 1) return "#efd2c4";
        else if (r == 2) return "#e2c6c2";
        else if (r == 3) return "#e0d0bb";
        else if (r == 4) return "#ebb77d";
        else if (r == 5) return "#dca788";
        else if (r == 6) return "#cda093";
        else if (r == 7) return "#ccab80";
        else return "";
    }

    string RandomSkinColor() {
        Debug.Log("colooor " + ColorUtility.ToHtmlStringRGB(skinColors[UnityEngine.Random.Range(0, skinColors.Length)]));
        return "#"+ColorUtility.ToHtmlStringRGB(skinColors[UnityEngine.Random.Range(0, skinColors.Length)]);
    }
    string RandomPrimaryColor()
    {
        return "#"+ColorUtility.ToHtmlStringRGB(primaryColors[UnityEngine.Random.Range(0, primaryColors.Length)]);
    }
    string RandomSecondaryColor()
    {
        return "#"+ColorUtility.ToHtmlStringRGB(secondaryColors[UnityEngine.Random.Range(0, secondaryColors.Length)]);
    }
    string RandomHairColor()
    {
        return "#"+ColorUtility.ToHtmlStringRGB(hairColors[UnityEngine.Random.Range(0, hairColors.Length)]);
    }
    string RandomEyeColors()
    {
        return "#"+ColorUtility.ToHtmlStringRGB(eyeColors[UnityEngine.Random.Range(0, eyeColors.Length)]);
    }

    bool RandomBool() { return UnityEngine.Random.Range(0, 2) == 0 ? false : true; }


    //sex: male = false, female = true
    //body_strenght: wimp = false, strong = true
    //body_type: thin = false, fat = true
    int dist = 1;
    public void DefineParts(bool sex, bool body_strenght, bool body_type, string eye_color, string hair_color, string skin_color, string primary_color, string secondary_color, string hair_style)
    {
        Debug.Log("CREATING A DEMON!! SEX - " + sex + " EYE COLOR " + eye_color);
        DemonAvatarBody newBody;
        myBody = new DemonAvatarBody();
        switch (sex) {
            case true: // female
                if (body_strenght == true && body_type == true) // muscular and fat
                {
                    myBody = femaleBodies[(int)bodyTypes.fatMuscular];
                }
                else if (body_strenght == true && body_type == false) // muscular and thin
                {
                    myBody = femaleBodies[(int)bodyTypes.thinMuscular];
                }
                else if (body_strenght == false && body_type == true) // wimp and fat
                {
                    myBody = femaleBodies[(int)bodyTypes.fatWimp];
                }
                else if (body_strenght == false && body_type == false) // wimp and thin
                {
                    myBody = femaleBodies[(int)bodyTypes.thinWimp];
                }

                newBody =  Instantiate(myBody, new Vector3(transform.position.x + dist, transform.position.y, transform.position.z), Quaternion.identity);
                //myBody.hair.sprite = femaleHairDictionary[hair_style];
                break;

            case false: // male
                if (body_strenght == true && body_type == true) // muscular and fat
                {
                    myBody = maleBodies[(int)bodyTypes.fatMuscular];
                }
                else if (body_strenght == true && body_type == false) // muscular and thin
                {
                    myBody = maleBodies[(int)bodyTypes.thinMuscular];
                }
                else if (body_strenght == false && body_type == true) // wimp and fat
                {
                    myBody = maleBodies[(int)bodyTypes.fatWimp];
                }
                else if (body_strenght == false && body_type == false) // wimp and thin
                {
                    myBody = maleBodies[(int)bodyTypes.thinWimp];
                }
                //myBody.hair.sprite = maleHairDictionary[hair_style];
                newBody = Instantiate(myBody, transform.position, transform.rotation);

                break;
        }

        newBody.bodyClothes1.color = HexToColor(primary_color);
        newBody.bodyClothes2.color = HexToColor(secondary_color);
        newBody.hair.color = HexToColor(hair_color);
        newBody.bodySkin.color = HexToColor(skin_color);
        newBody.arm1.color = HexToColor(skin_color);

        dist += 10;
    }

    public void DefinePartsTotem(bool sex, bool body_strenght, bool body_type, string eye_color, string hair_color, string skin_color, Color primary_color, Color secondary_color, string hair_style)
    {
        Debug.Log("CREATING A DEMON!! SEX - " + sex + " EYE COLOR " + eye_color);
        DemonAvatarBody newBody;
        myBody = new DemonAvatarBody();
        switch (sex)
        {
            case true: // female
                if (body_strenght == true && body_type == true) // muscular and fat
                {
                    myBody = femaleBodies[(int)bodyTypes.fatMuscular];
                }
                else if (body_strenght == true && body_type == false) // muscular and thin
                {
                    myBody = femaleBodies[(int)bodyTypes.thinMuscular];
                }
                else if (body_strenght == false && body_type == true) // wimp and fat
                {
                    myBody = femaleBodies[(int)bodyTypes.fatWimp];
                }
                else if (body_strenght == false && body_type == false) // wimp and thin
                {
                    myBody = femaleBodies[(int)bodyTypes.thinWimp];
                }

                newBody = Instantiate(myBody, new Vector3(transform.position.x + dist, transform.position.y, transform.position.z), Quaternion.identity);
                //myBody.hair.sprite = femaleHairDictionary[hair_style];
                break;

            case false: // male
                if (body_strenght == true && body_type == true) // muscular and fat
                {
                    myBody = maleBodies[(int)bodyTypes.fatMuscular];
                }
                else if (body_strenght == true && body_type == false) // muscular and thin
                {
                    myBody = maleBodies[(int)bodyTypes.thinMuscular];
                }
                else if (body_strenght == false && body_type == true) // wimp and fat
                {
                    myBody = maleBodies[(int)bodyTypes.fatWimp];
                }
                else if (body_strenght == false && body_type == false) // wimp and thin
                {
                    myBody = maleBodies[(int)bodyTypes.thinWimp];
                }
                //myBody.hair.sprite = maleHairDictionary[hair_style];
                newBody = Instantiate(myBody, transform.position, transform.rotation);

                break;
        }

        newBody.bodyClothes1.color = primary_color;
        newBody.bodyClothes2.color = secondary_color;
        newBody.hair.color = HexToColor(hair_color);
        newBody.bodySkin.color = HexToColor(skin_color);
        newBody.arm1.color = HexToColor(skin_color);

        dist += 10;
    }


    Color HexToColor(string hexColor)
    {
        Color color;
        ColorUtility.TryParseHtmlString(hexColor, out color);

        return color;
    }

    //Avatar:sex_bio: True | body_strength: False | human_skin_color: #ebb77d | human_hair_color: #b1b1b1 | human_eye_color: #7c8b4f |
    //hair_styles: dreadlocks | primary_color: RGBA(0.663, 0.573, 0.082, 1.000) | secondary_color: RGBA(0.251, 0.894, 0.008,

    // Update is called once per frame
}
