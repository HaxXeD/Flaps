using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SavePlane (PlaneSo[] planes){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/plane.data";
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        PlaneData data = new PlaneData(planes);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveCollectable (PowerUpSO[] powerUps){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/collectable.data";
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        CollectableData data = new CollectableData(powerUps);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveBackground (BackgroundSO[] backgrounds){

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/background.data";
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        BackgroundData data = new BackgroundData(backgrounds);
        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static PlaneData LoadPlane(){
        string path = Application.persistentDataPath + "/plane.data";
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        if(File.Exists(path)&&stream.Length>0){
            BinaryFormatter formatter = new BinaryFormatter();
            PlaneData data = formatter.Deserialize(stream) as PlaneData;
            stream.Close();
            return data;
        }
        else{
            Debug.Log("Save file was not found in " + path);
            // BinaryFormatter formatter = new BinaryFormatter();
            // PlaneData data = new PlaneData(planes);
            // formatter.Serialize(stream, data);
            // stream.Close();
            // return data;
            return null;
        }
    }

    public static CollectableData LoadPowers(){
        string path = Application.persistentDataPath + "/collectable.data";
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        if(File.Exists(path)&&stream.Length>0){
            BinaryFormatter formatter = new BinaryFormatter();
            CollectableData data = formatter.Deserialize(stream) as CollectableData;
            stream.Close();
            return data;
        }
        else{
            Debug.Log("Save file was not found in " + path);
            return null;
        }
    }

    public static BackgroundData LoadBackground(BackgroundSO[] backgrounds){
        string path = Application.persistentDataPath + "/background.data";
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        if(File.Exists(path)&&stream.Length>0){
            BinaryFormatter formatter = new BinaryFormatter();
            BackgroundData data = formatter.Deserialize(stream) as BackgroundData;
            stream.Close();
            return data;
        }
        else{
            Debug.Log("Save file was not found in " + path);
            return null;
        }
    }
}
