using UnityEngine;
public class SetQuestion : MonoBehaviour
{
    int jsonTextIndex;
    public void setIndex(int index){
        jsonTextIndex = index;
    }

    public int ReturnIndex(){
        return jsonTextIndex;
    }

}
