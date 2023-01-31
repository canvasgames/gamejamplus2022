using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if !USE_TOTEM
using TotemEntities;
using TotemEntities.DNA;
using TotemServices.DNA;
#endif
public class TotemDNACustomAvatar: TotemDNADefaultAvatar {

        public string human_eyeball_color;
        public string human_eye_color_dark ;
        public string human_eye_color_light;
}

public class TotemMaster : MonoBehaviour
{

#if !USE_TOTEM
    private TotemCore totemCore;
    public static TotemMaster instance;
    public string _gameId = "TotemDemo";
    public DemonAvatar mainAvatar;
    [HideInInspector] public int userAvatarCount = 0;

    public List<TotemDNADefaultAvatar> avatars;
    private List<TotemDNADefaultItem> items;

    [SerializeField] GameObject avatarList;

    private bool watingLogin;
    
    void Awake()
    {
        instance = this;
        totemCore = new TotemCore(_gameId);
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
                userAvatarCount = avatars.Count;
                this.avatars = avatars;

                for (int index = 0; index < avatars.Count; index++)
                {
                    Debug.Log("Avatar:" + avatars[index].ToString());

                    TotemDNADefaultAvatar curAvatar = avatars[index];
                    //charData = curAvatar.human_hair_color.ToString();
                    //  DefineTotemSpritesAndColors(curAvatar.human_eye_color, curAvatar.human_hair_color, curAvatar.human_skin_color, curAvatar.hair_styles, index);

                    //FindObjectOfType<DemonAvatar>().DefinePartsTotem(curAvatar.sex_bio, curAvatar.body_strength, curAvatar.body_type, curAvatar.human_eyeball_color, curAvatar.human_eye_color_dark, curAvatar.human_eye_color_light, curAvatar.human_skin_color, curAvatar.human_skin_color, curAvatar.primary_color, curAvatar.secondary_color, curAvatar.hair_styles);
                }
                avatarList.SetActive(true);

                totemCore.GetUserItems<TotemDNADefaultItem>(user, TotemDNAFilter.DefaultItemFilter, (items) => {
                    this.items = items;

                });
            }else{
                //Não tem nenhum avatar
                //store_controller.s.loadingScreen.SetActive(false);
            }
        });
        
    }

    public void OnAvatarSelected()
    {
         avatarList.SetActive(false);
         TotemDNADefaultAvatar curAvatar = avatars[avatarList.GetComponentInChildren<ScrollSnapRect>()._currentPage];
         mainAvatar.DefinePartsTotem(curAvatar.sex_bio, curAvatar.body_strength, curAvatar.body_type, curAvatar.human_eyeball_color, curAvatar.human_eye_color_dark, curAvatar.human_eye_color_light, curAvatar.human_skin_color, curAvatar.human_skin_color, curAvatar.primary_color, curAvatar.secondary_color, curAvatar.hair_styles);
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