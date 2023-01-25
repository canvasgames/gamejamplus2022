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

    [SerializeField] NamedImage[] hairSprites;
    DemonAvatarBody myBody;
    DemonAvatarBody[] femaleBodies;
    DemonAvatarBody[] maleBodies;

    bodyTypes myBodyType;
   

    void Start()
    {
        
        /*Dictionary<string, Sprite> bodies = new Dictionary<string, Sprite>();
        foreach (NamedImage item in femaleBodyParts)
        {
            bodies.Add(item.name, item.image);
        }*/
    }

    public void DefineParts(bool sex, bool body_strenght, bool body_type, string eye_color, string hair_color, string skin_color, string primary_color, string secondary_color, string hair_style)
    {
        switch (sex) {
            case true: // female
                if (body_strenght == true && body_type == true) // muscular and thin
                {
                    myBody = femaleBodies[(int)bodyTypes.thinMuscular];
                }
                else if (body_strenght == true && body_type == false) // muscular and fat
                {
                    myBody = femaleBodies[(int)bodyTypes.thinWimp];
                }
                else if (body_strenght == false && body_type == true) // wimp and thin
                {
                    myBody = femaleBodies[(int)bodyTypes.fatWimp];
                }
                else if (body_strenght == true && body_type == false) // wimp and thin
                {
                    myBody = femaleBodies[(int)bodyTypes.fatMuscular];
                }
                break;

            case false: // male
                if (body_strenght == true && body_type == true) // muscular and thin
                {
                    myBody = maleBodies[(int)bodyTypes.thinMuscular];
                }
                else if (body_strenght == true && body_type == false) // muscular and fat
                {
                    myBody = maleBodies[(int)bodyTypes.thinWimp];
                }
                else if (body_strenght == false && body_type == true) // wimp and thin
                {
                    myBody = maleBodies[(int)bodyTypes.fatWimp];
                }
                else if (body_strenght == true && body_type == false) // wimp and thin
                {
                    myBody = maleBodies[(int)bodyTypes.fatMuscular];
                }
                break;
        }

        myBody.bodyClothes1.color = HexToColor(primary_color);
        myBody.bodyClothes2.color = HexToColor(secondary_color);
        myBody.hair.color = HexToColor(hair_color);
        myBody.bodySkin.color = HexToColor(skin_color);
        myBody.arm1.color = HexToColor(skin_color);
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
