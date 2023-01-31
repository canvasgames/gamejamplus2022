using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.U2D.Animation;
using TotemEntities.DNA;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Mask))]
[RequireComponent(typeof(ScrollRect))]
public class ScrollSnapRect : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
int tbd = 0;
    #region === Vars ===
    [Tooltip("Set starting page index - starting from 0")]
    public int startingPage = 0;
    [Tooltip("Threshold time for fast swipe in seconds")]
    public float fastSwipeThresholdTime = 0.3f;
    [Tooltip("Threshold time for fast swipe in (unscaled) pixels")]
    public int fastSwipeThresholdDistance = 100;
    [Tooltip("How fast will page lerp to target position")]
    public float decelerationRate = 10f;
    [Tooltip("Button to go to the previous page (optional)")]
    public GameObject prevButton;
    [Tooltip("Button to go to the next page (optional)")]
    public GameObject nextButton;
    [Tooltip("Sprite for unselected page (optional)")]
    public Sprite unselectedPage;
    [Tooltip("Sprite for selected page (optional)")]
    public Sprite selectedPage;
    [Tooltip("Container with page images (optional)")]
    public Transform pageSelectionIcons;

    // fast swipes should be fast and short. If too long, then it is not fast swipe
    private int _fastSwipeThresholdMaxLimit;

    private ScrollRect _scrollRectComponent;
    private RectTransform _scrollRectRect;
    private RectTransform _container;

    private bool _horizontal;

    // number of pages in container
    private int _pageCount;
    public int _currentPage;

    // whether lerping is in progress and target lerp position
    private bool _lerp;
    private Vector2 _lerpTo;

    // target position of every page
    private List<Vector2> _pagePositions = new List<Vector2>();

    // in draggging, when dragging started and where it started
    private bool _dragging;
    private float _timeStamp;
    private Vector2 _startPosition;

    // for showing small page icons
    private bool _showPageSelection;
    private int _previousPageSelectionIndex;
    // container with Image components - one Image for each page
    private List<Image> _pageSelectionImages;
    public Text charPrice;

    public GameObject TotemCharacterPrefab;
    public GameObject TotemFollowerPrefab;
    int totalTotemChars = 0;
    public List<int> totemSkinsIndex;

    private bool totemCharactersActivated = false;
    #endregion
    //------------------------------------------------------------------------
    void Start()
    {
        //totemSkinsIndex = new List<Skin>();

        _scrollRectComponent = GetComponent<ScrollRect>();
        _scrollRectRect = GetComponent<RectTransform>();
        _container = _scrollRectComponent.content;

 
        totemCharactersActivated = true;
        _pageCount = TotemMaster.instance.userAvatarCount;
        totalTotemChars = TotemMaster.instance.userAvatarCount;

        // is it horizontal or vertical scrollrect
        if (_scrollRectComponent.horizontal && !_scrollRectComponent.vertical)
        {
            _horizontal = true;
        }
        else if (!_scrollRectComponent.horizontal && _scrollRectComponent.vertical)
        {
            _horizontal = false;
        }
        else
        {
            Debug.LogWarning("Confusing setting of horizontal/vertical direction. Default set to horizontal.");
            _horizontal = true;
        }

        _lerp = false;

        // init
        SetPagePositions();
        SetPage(startingPage);
        InitPageSelection();
        SetPageSelection(startingPage);

        // prev and next buttons
        if (nextButton)
            nextButton.GetComponent<Button>().onClick.AddListener(() => { NextScreen(); });

        if (prevButton)
            prevButton.GetComponent<Button>().onClick.AddListener(() => { PreviousScreen(); });
    }

    //------------------------------------------------------------------------
    void Update()
    {
        // if moving to target position
        if (_lerp)
        {
            // prevent overshooting with values greater than 1
            float decelerate = Mathf.Min(decelerationRate * Time.deltaTime, 1f);
            _container.anchoredPosition = Vector2.Lerp(_container.anchoredPosition, _lerpTo, decelerate);
            // time to stop lerping?
            if (Vector2.SqrMagnitude(_container.anchoredPosition - _lerpTo) < 0.25f)
            {
                // snap to target and stop lerping
                _container.anchoredPosition = _lerpTo;
                _lerp = false;
                // clear also any scrollrect move that may interfere with our lerping
                _scrollRectComponent.velocity = Vector2.zero;
            }

            // switches selection icon exactly to correct page
            if (_showPageSelection)
            {
                SetPageSelection(GetNearestPage());
            }
        }
    }

    int width = 0;
    int height = 0;
    int offsetX = 0;
    int offsetY = 0;
    int containerWidth = 0;
    int containerHeight = 0;
    float yPosTotemFollower = -0.4f;
    float xPosTotemFollower = 1.176f;
    int extraCharCount;
    //------------------------------------------------------------------------
    private void SetPagePositions()
    {
        width = 0;
        height = 0;
        offsetX = 0;
        offsetY = 0;
        containerWidth = 0;
        containerHeight = 0;
        extraCharCount = 0;
        if (_horizontal)
        {
            // screen width in pixels of scrollrect window
            width = (int)_scrollRectRect.rect.width;
            // center position of all pages
            offsetX = width / 2;
            // total width
            containerWidth = width * _pageCount;
            // limit fast swipe length - beyond this length it is fast swipe no more
            _fastSwipeThresholdMaxLimit = width;
        }
        else
        {
            height = (int)_scrollRectRect.rect.height;
            offsetY = height / 2;
            containerHeight = height * _pageCount;
            _fastSwipeThresholdMaxLimit = height;
        }

        // set width of container
        Vector2 newSize = new Vector2(containerWidth, containerHeight);
        _container.sizeDelta = newSize;
        Vector2 newPosition = new Vector2(containerWidth / 2, containerHeight / 2);
        _container.anchoredPosition = newPosition;

        // delete any previous settings
        _pagePositions.Clear();


        // #if USE_TOTEM
        for (int i = 0; i < totalTotemChars; i++)
        {
            GameObject currentCharacter = Instantiate(TotemCharacterPrefab, _container);
            TotemDNADefaultAvatar curAvatar = TotemMaster.instance.avatars[i];
           // currentCharacter.GetComponent<DemonAvatar>().DefinePartsTotem(curAvatar.sex_bio, curAvatar.body_strength, curAvatar.body_type, curAvatar.human_eyeball_color, curAvatar.human_eye_color_dark, curAvatar.human_eye_color_light, curAvatar.human_skin_color, curAvatar.human_skin_color, curAvatar.primary_color, curAvatar.secondary_color, curAvatar.hair_styles);
            if (i == 0)
                currentCharacter.SetActive(true);

            RectTransform child = currentCharacter.GetComponent<RectTransform>();

            Vector2 childPosition;
            if (_horizontal)
                childPosition = new Vector2(i * width - containerWidth / 2 + offsetX, 0f);
            else
                childPosition = new Vector2(0f, -(i * height - containerHeight / 2 + offsetY));

            child.anchoredPosition = childPosition;
            _pagePositions.Add(-childPosition);
            //SetTotemCharacterSkin(currentCharacter, i + extraCharCount, i);

            }
           // #endif
         /*
        else
        {
            for (int i = 0; i < _pageCount; i++)
            {
                RectTransform child = _container.GetChild(i).GetComponent<RectTransform>();

                Vector2 childPosition;
                if (_horizontal)
                    childPosition = new Vector2(i * width - containerWidth / 2 + offsetX, 0f);
                else
                    childPosition = new Vector2(0f, -(i * height - containerHeight / 2 + offsetY));

                child.anchoredPosition = childPosition;
                _pagePositions.Add(-childPosition);
            }
        }     */
    }

    //---------Inicio de simplicação tas chamadas acima
    //private void HasTotemCharacters(int i, MusicStyle style)
    //{
    //    GameObject currentCharacter2 = Instantiate(TotemCharacterPrefab, _container);
    //    extraCharCount++;
    //    SetTotemCharacterSkin(currentCharacter2, i + extraCharCount, i);
    //    List<int> followersIds = new List<int>();
    //    //Anviland
    //    if (1 == 1 || TotemMaster.s.CheckIfMedievalCharacterOneIsUnlocked(i))
    //    {
    //        EachCharacter(followersIds, currentCharacter2, 1, style);
    //    }
    //    if (1 == 1 || TotemMaster.s.CheckIfMedievalCharacterTwoIsUnlocked(i))
    //    {
    //        EachCharacter(followersIds, currentCharacter2, 2, style);
    //    }
    //    totemSkinsIndex.Add(GD.s.NewSkinTotemChar(i, i + extraCharCount, MusicStyle.Medieval, true, followersIds.Count + 1, followersIds.ToArray()));

    //    //Dreadstone
    //    if (1 == 1 || TotemMaster.s.CheckIfDungeonCharacterOneIsUnlocked(i))
    //    {
    //        EachCharacter(followersIds, currentCharacter2, 1, style);
    //    }
    //    if (1 == 1 || TotemMaster.s.CheckIfDungeonCharacterTwoIsUnlocked(i))
    //    {
    //        EachCharacter(followersIds, currentCharacter2, 2, style);
    //    }
    //    totemSkinsIndex.Add(GD.s.NewSkinTotemChar(i, i + extraCharCount, MusicStyle.Dungeon, true, followersIds.Count + 1, followersIds.ToArray()));
    //    // tree tales
    //    if (1 == 1 || TotemMaster.s.CheckIfHappyFarmCharacterOneIsUnlocked(i))
    //    {
    //        EachCharacter(followersIds, currentCharacter2, 1, style);
    //    }
    //    if (1 == 1 || TotemMaster.s.CheckIfHappyFarmCharacterTwoIsUnlocked(i))
    //    {
    //        EachCharacter(followersIds, currentCharacter2, 2, style);
    //    }
    //    totemSkinsIndex.Add(GD.s.NewSkinTotemChar(i, i + extraCharCount, MusicStyle.HappyFarm, true, followersIds.Count + 1, followersIds.ToArray()));
    //}

    //private void EachCharacter(List<int> followersIds, GameObject currentCharacter2, int followerNumber, MusicStyle style)
    //{
    //    followersIds.Add(followerNumber);
    //    GameObject follower = Instantiate(TotemFollowerPrefab, transform.position, transform.rotation, currentCharacter2.transform);
    //    if (followerNumber == 1)
    //    {
    //        follower.transform.localPosition = new Vector3(-xPosTotemFollower, yPosTotemFollower, 0);
    //    }
    //    else
    //        follower.transform.localPosition = new Vector3(xPosTotemFollower, yPosTotemFollower, 0);
    //    follower.GetComponent<SpriteLibrary>().spriteLibraryAsset = Resources.Load("Sprites/Animations/" + style.ToString() + "/" + style.ToString() + followerNumber) as SpriteLibraryAsset;
    //}
    //------------------------------

    void SetTotemCharacterSkin(GameObject currentCharacter, int i, int totemCharIndex)
    {
#if USE_TOTEM
        currentCharacter.transform.GetChild(0).GetComponent<SpriteRenderer>().color = GD.s.totemPartDataList[totemCharIndex].totemBodyColor;
        currentCharacter.transform.GetChild(1).GetComponent<SpriteLibrary>().spriteLibraryAsset = GD.s.totemPartDataList[totemCharIndex].totemOutfitSkinSpriteLibrary;
        currentCharacter.transform.GetChild(2).GetComponent<SpriteRenderer>().color = GD.s.totemPartDataList[totemCharIndex].totemEyesColor;
        currentCharacter.transform.GetChild(3).GetComponent<SpriteLibrary>().spriteLibraryAsset = GD.s.totemPartDataList[totemCharIndex].totemHeadSkinSpriteLibrary;
        currentCharacter.transform.GetChild(3).GetComponent<SpriteRenderer>().color = GD.s.totemPartDataList[totemCharIndex].totemHeadColor;

        RectTransform child = currentCharacter.GetComponent<RectTransform>();

        Vector2 childPosition;
        if (_horizontal)
            childPosition = new Vector2(i * width - containerWidth / 2 + offsetX, 0f);
        else
            childPosition = new Vector2(0f, -(i * height - containerHeight / 2 + offsetY));

        child.anchoredPosition = childPosition;
        _pagePositions.Add(-childPosition);
#endif
    }

    //------------------------------------------------------------------------
    private void SetPage(int aPageIndex)
    {
        aPageIndex = Mathf.Clamp(aPageIndex, 0, _pageCount - 1);
        _container.anchoredPosition = _pagePositions[aPageIndex];
        _currentPage = aPageIndex;
    }

    //------------------------------------------------------------------------
    private void LerpToPage(int aPageIndex)
    {
        aPageIndex = Mathf.Clamp(aPageIndex, 0, _pageCount - 1);
        _lerpTo = _pagePositions[aPageIndex];
        _lerp = true;
        _currentPage = aPageIndex;
    }

    //------------------------------------------------------------------------
    private void InitPageSelection()
    {
        // page selection - only if defined sprites for selection icons
        _showPageSelection = unselectedPage != null && selectedPage != null;
        if (_showPageSelection)
        {
            // also container with selection images must be defined and must have exatly the same amount of items as pages container
            if (pageSelectionIcons == null || pageSelectionIcons.childCount != _pageCount)
            {
                Debug.LogWarning("Different count of pages and selection icons - will not show page selection");
                _showPageSelection = false;
            }
            else
            {
                _previousPageSelectionIndex = -1;
                _pageSelectionImages = new List<Image>();

                // cache all Image components into list
                for (int i = 0; i < pageSelectionIcons.childCount; i++)
                {
                    Image image = pageSelectionIcons.GetChild(i).GetComponent<Image>();
                    if (image == null)
                    {
                        Debug.LogWarning("Page selection icon at position " + i + " is missing Image component");
                    }
                    _pageSelectionImages.Add(image);
                }
            }
        }
    }

    //------------------------------------------------------------------------
    private void SetPageSelection(int aPageIndex)
    {
        // nothing to change
        if (_previousPageSelectionIndex == aPageIndex)
        {
            return;
        }

        // unselect old
        if (_previousPageSelectionIndex >= 0)
        {
            _pageSelectionImages[_previousPageSelectionIndex].sprite = unselectedPage;
            _pageSelectionImages[_previousPageSelectionIndex].SetNativeSize();
        }

        // select new
        _pageSelectionImages[aPageIndex].sprite = selectedPage;
        _pageSelectionImages[aPageIndex].SetNativeSize();

        _previousPageSelectionIndex = aPageIndex;
    }


#region === CHARACTER SELECTION ===
    public void SetCurrentPage(int skinId)
    {
        //		Debug.Log("lerp to page : " + (int)style);
        LerpToPage(skinId);

        //		LerpToPage(2);
        //		NextScreen();
    }
    //------------------------------------------------------------------------
    public void NextScreen()
    {
        if (_currentPage == totalTotemChars - 1)
        {
            Debug.Log("NEXT SCREEN BT PRESSED");
            //			store_controller.s.OnCharacterChanged(0);
            //store_controller.s.OnCharacterTotemChangedNew(totemSkinsIndex[0]);
            LerpToPage(0);
        }
        else
        {
            //store_controller.s.OnCharacterTotemChangedNew(totemSkinsIndex[_currentPage + 1]);
            LerpToPage(_currentPage + 1);

        }

    }

    //------------------------------------------------------------------------
    private void PreviousScreen()
    {
        if (_currentPage == 0)
        {
            LerpToPage(totalTotemChars-1);
            //store_controller.s.OnCharacterTotemChangedNew(totemSkinsIndex[totalTotemChars-1]);
        }
        else
        {
            //store_controller.s.OnCharacterTotemChangedNew(totemSkinsIndex[_currentPage - 1]);
            LerpToPage(_currentPage - 1);
        }
    }
#endregion
    //------------------------------------------------------------------------
    private int GetNearestPage()
    {
        // based on distance from current position, find nearest page
        Vector2 currentPosition = _container.anchoredPosition;

        float distance = float.MaxValue;
        int nearestPage = _currentPage;

        for (int i = 0; i < _pagePositions.Count; i++)
        {
            float testDist = Vector2.SqrMagnitude(currentPosition - _pagePositions[i]);
            if (testDist < distance)
            {
                distance = testDist;
                nearestPage = i;
            }
        }

        return nearestPage;
    }

    //------------------------------------------------------------------------
    public void OnBeginDrag(PointerEventData aEventData)
    {
        // if currently lerping, then stop it as user is draging
        _lerp = false;
        // not dragging yet
        _dragging = false;
    }

    //------------------------------------------------------------------------
    public void OnEndDrag(PointerEventData aEventData)
    {
        // how much was container's content dragged
        float difference;
        if (_horizontal)
        {
            difference = _startPosition.x - _container.anchoredPosition.x;
        }
        else
        {
            difference = -(_startPosition.y - _container.anchoredPosition.y);
        }

        // test for fast swipe - swipe that moves only +/-1 item
        if (Time.unscaledTime - _timeStamp < fastSwipeThresholdTime &&
            Mathf.Abs(difference) > fastSwipeThresholdDistance &&
            Mathf.Abs(difference) < _fastSwipeThresholdMaxLimit)
        {
            if (difference > 0)
            {
                NextScreen();
            }
            else
            {
                PreviousScreen();
            }
        }
        else
        {
            // if not fast time, look to which page we got to
            LerpToPage(GetNearestPage());
        }

        _dragging = false;
    }

    //------------------------------------------------------------------------
    public void OnDrag(PointerEventData aEventData)
    {
        if (!_dragging)
        {
            // dragging started
            _dragging = true;
            // save time - unscaled so pausing with Time.scale should not affect it
            _timeStamp = Time.unscaledTime;
            // save current position of cointainer
            _startPosition = _container.anchoredPosition;
        }
        else
        {
            if (_showPageSelection)
            {
                SetPageSelection(GetNearestPage());
            }
        }
    }

    void OnDisable()
    {
        LerpToPage(0);
    }
}
