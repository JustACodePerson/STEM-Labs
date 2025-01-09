using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ButtonScript : MonoBehaviour
{
    public GameObject objectToChangeTo;

    void Start(){
        Button btn = this.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);

        /* DOESN'T WORK IN BUILD, UTILIZES EDITOR WHICH CAN'T BE ACCESSED IN BUILT GAME
        Texture2D tex = AssetPreview.GetAssetPreview(objectToChangeTo); //Grab Prefab Picture (Texture2D)   
        Sprite spr = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero); //Convert Picture to Sprite
        this.GetComponent<Image>().sprite = spr; //Change Button Picture to Sprite
        */
    }

    void TaskOnClick(){
        BuildSystem.current.changeObject(objectToChangeTo);
        BuildSystem.current.cloneObject();
    }

    public void Clear(){ //Button Inspector Only Accepts an Object, Then It Accesses the Object's Script's Function. Clear Button Accesses Button 1's Object to Access this Function
        GameObject gObj =  GameObject.Find("Build_1");
        for(int i = gObj.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(gObj.transform.GetChild(i).gameObject);
        }
    }

}
