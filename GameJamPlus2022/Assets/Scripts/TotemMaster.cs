using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if !USE_TOTEM
using TotemEntities;
using TotemEntities.DNA;
using TotemServices.DNA;
#endif

public class TotemMaster : MonoBehaviour
{

#if !USE_TOTEM
    private TotemCore totemCore;
    public static TotemMaster s;
    public string _gameId = "TotemDemo";
    public string[] hairStyle;
    public string[] hairColor;
    public string[] bodyColor;
    public string[] outfitStyle;
    public string[] eyeColor;
    public int skinId=0;

    [HideInInspector]
    public bool[] legacyDreadStoneBaronessDefeated;
    [HideInInspector]
    public bool[] legacyDreadStoneLichDefeated;
    [HideInInspector]
    public bool[] legacyTreeTalesReachTwoThousand;
    [HideInInspector]
    public bool[] legacyAnvilandDeliveredGiantRing;
    [HideInInspector]
    public bool[] legacyAnvilandHundredCoinsInBag;

    private List<TotemDNADefaultAvatar> avatars;
    private List<TotemDNADefaultItem> items;

    private bool watingLogin;
    
    void Awake()
    {
        s = this;
        totemCore = new TotemCore(_gameId);
        hairStyle = new string[10];
        hairColor = new string[10];
        bodyColor = new string[10];
        outfitStyle = new string[10];
        eyeColor = new string[10];
    }

    // Update is called once per frame

    public void OnLoginButtonClick()
    {
        StartCoroutine(WaitLogin(10));
        watingLogin = true;

        totemCore.AuthenticateCurrentUser(OnUserLoggedIn);
    }

    public string charData = "";
    private void OnUserLoggedIn(TotemUser user)
    {
        watingLogin = false;

        totemCore.GetUserAvatars<TotemDNADefaultAvatar>(user, TotemDNAFilter.DefaultAvatarFilter, (avatars) =>
        {
            if(avatars.Count > 0){
                //globals.s.IS_TOTEM_LOGGED_IN = true;

                this.avatars = avatars;

                for (int index = 0; index < avatars.Count; index++){
                    Debug.Log("Avatar:" + avatars[index].ToString()); 

                    TotemDNADefaultAvatar curAvatar = avatars[index];
                    //charData = curAvatar.human_hair_color.ToString();
                    //  DefineTotemSpritesAndColors(curAvatar.human_eye_color, curAvatar.human_hair_color, curAvatar.human_skin_color, curAvatar.hair_styles, index);

                    //GD.s.NewSkinFromTotem("Totem Custom", ConvertMusicStyleFromTotem(curAvatar.music_style), curAvatar.human_skin_color, curAvatar.human_eye_color, curAvatar.human_hair_color, curAvatar.hair_styles, GD.s.RandomMusicStyle().ToString());
                }


                totemCore.GetUserItems<TotemDNADefaultItem>(user, TotemDNAFilter.DefaultItemFilter, (items) => {
                    this.items = items;

                });
            }else{
                //Não tem nenhum avatar
                //store_controller.s.loadingScreen.SetActive(false);
            }
        });
        
    }
    private void DefineTotemSpritesAndColors(string eye_color, string hair_color, string body_color, string hair_style, int index)
    {
        eyeColor[index] = eye_color;
        hairColor[index] = hair_color;
        bodyColor[index] = body_color;
        hairStyle[index] = hair_style;
    }


    IEnumerator WaitLogin(float time)
    {
        yield return new WaitForSeconds(time);

        //Muito tempo sem concluir login
       //if(watingLogin)
            //store_controller.s.loadingScreen.SetActive(false);
    }

    public void SaveLegacyEvent(string legacyData){
        //if(globals.s.IS_TOTEM_LOGGED_IN)
            //totemCore.AddLegacyRecord(avatars[globals.s.ACTUAL_TOTEM_SKIN_ID], legacyData, (record) =>
               // { Debug.Log("Legacy Event Saved: " + legacyData); });
    }


   /* MusicStyle ConvertMusicStyleFromTotem(string style)
    {
        MusicStyleTotem parsed_enum = (MusicStyleTotem)System.Enum.Parse(typeof(MusicStyleTotem), style);
        int toReturn = (int)parsed_enum;
        if (toReturn > 8)
        {
            toReturn -= 8;
        }

        return (MusicStyle) toReturn;
    }
   */


#endif
}