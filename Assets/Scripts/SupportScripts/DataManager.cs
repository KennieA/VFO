using UnityEngine;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using JsonFx.Json;
using ExerciseCollections;
using System;

public class DataManager : MonoBehaviour
{

    private static bool DEBUG = false;

    //Classes used in Json Data Serialization and Deserialization
    #region Json Data container classes

    public class JsonRequestArgs
    {
        public string method;
        public string @params;
        public int id;
    }


    //public class JsonExercisePart
    //{
    //    public int Id;
    //    public string Name;
    //    public bool Completed;
    //    public double Time;

    //    public JsonExercisePart()
    //    {
    //    }

    //    public JsonExercisePart(int id, string name, bool completed, double time)
    //    {
    //        Id = id;
    //        Name = name;
    //        Completed = completed;
    //        Time = time;
    //    }

    //    public override string ToString()
    //    {
    //        return Id + " " + Name + " " + Completed + " " + Time + "; ";
    //    }
    //}

    public class JsonBaseExercise
    {
        public int Id;
        public double Score;
        public string Name;

        public JsonBaseExercise()
        {
        }

        public JsonBaseExercise(int id, string name, double score)
        {
            Id = id;
            Score = score;
            Name = name;
        }

        public override string ToString()
        {
            return "( "+Id + " " + Score + " " + Name+" )";
        }
    }

    public class JsonCategoryCollection
    {
        public int UserId;
        public JsonCategory[] Categories;

        public override string ToString()
        {
            string result = UserId+" < ";
            foreach (JsonCategory c in Categories)
            {
                result += c.ToString() + " ";
            }
            result += ">";
            return result;
        }
    }

    public class JsonCategory : JsonBaseExercise
    {
        public JsonExercise[] Exercises;

        public JsonCategory()
        {
        }

        public JsonCategory(int id, string name, double score)
            : base(id, name, score)
        {
        }

        public override string ToString()
        {
            string result = base.ToString() + " " + "{ ";
            foreach (JsonExercise e in Exercises)
            {
                result += e.ToString() + " ";
            }
            result += "}";
            return result;
        }
    }

    public class JsonExercise : JsonBaseExercise
    {
        public int SceneFunction;
        public bool Attempted;
        //public JsonExercisePart[] Parts;


        public JsonExercise()
        {
        }

        public JsonExercise(int id, string name, double score, int sceneFunction, bool attempted)
            : base(id, name, score)
        {
            SceneFunction = sceneFunction;
            Attempted = attempted;
        }

        public override string ToString()
        {
            string result = base.ToString() + "Function " + SceneFunction + "[ ";
            //foreach (JsonExercisePart p in Parts)
            //{
            //    result +=p.ToString() + " ";
            //}
            result += "]";
            return result;
        }
    }

    public class JsonCredentials
    {
        public string Username = "";
        public string Password = "";

        public JsonCredentials(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public override string ToString()
        {
            return "Username: "+Username+", Password: "+Password+"";
        }
    }

    #endregion

    public static void WrongUserClicked(Message message, bool value)
    {
        if(Global.Instance.ProgramLanguage == "sv-SE")
            Application.OpenURL("http://vfo.welfaresverige.se");
        else
            Application.OpenURL("https://vfo.welfaredenmark.com");

        Application.Quit();
    }

    public static IEnumerator ValidateCredentials(JsonCredentials credentials)
    {
        string url = "https://vfo.welfaredenmark.com/Service/Authorize/"; //Production environment service
        url = "http://localhost:59477/Service/Authorize/"; //LOCAL SERVICE - Comment for release version

        if (Global.Instance.ProgramLanguage == "sv-SE")
        {
            //url = "http://vfo.welfaresverige.se/Service/Authorize/"; //OutComment if release version
        }
            
        Debug.Log("Validating Credentials -> "+credentials);

        string serialized = JsonWriter.Serialize(credentials);

        Debug.Log("Serialized:\n" + serialized);
        Encoding encoding = Encoding.UTF8;
        byte[] bytes = encoding.GetBytes(serialized);

        WWW www = new WWW(url, bytes);

        yield return www;
        // check for errors

        if (www.error == null)
        {
            Debug.Log("Result: " + www.text);
            Global.Instance.UserId = int.Parse(www.text);
            if (Global.Instance.UserId == -1)
            {
                Util.MessageBox(new Rect(0, 0, 400, 200), Text.Instance.GetString("data_manager_wrong_username_password"), Message.Type.Error, true, true);
            }
            else if (Global.Instance.UserId == -1000)
            {
                Util.OkMessageBox(new Rect(0, 0, 400, 200), Text.Instance.GetString("data_manager_admin_loging_error"), true, Message.Type.Info, WrongUserClicked);
            }
            else if (Global.Instance.UserId == -9000)
            {
                Util.MessageBox(new Rect(0, 0, 400, 200), Text.Instance.GetString("data_manager_general_error"), Message.Type.Error, true, true);
            }
            else
            {
                Debug.Log("Upload Complete.");
                Application.LoadLevel("Loading");
            }
        }
        else
        {
            Debug.Log(www.error);
            Util.MessageBox(new Rect(0, 0, 400, 200), Text.Instance.GetString("data_manager_connect_error"), Message.Type.Error, true, true);
        }

        GameObject go = GameObject.Find("infomessage");
        if (go)
            GameObject.Destroy(go);
    }

    public static IEnumerator RetrieveData()
    {
        Debug.Log("Retrieving Data");

        string url = "https://vfo.welfaredenmark.com/Service/GetExercises/" + Global.Instance.UserId + "/" + "da-DK"; //Production environment service
        url = "http://localhost:59477/Service/GetExercises/" + Global.Instance.UserId + "/" + "da-DK"; //LOCAL SERVICE - Comment for release version

        if (Global.Instance.ProgramLanguage == "sv-SE")
        {
            //url = "http://vfo.welfaresverige.se/Service/GetExercises/" + Global.Instance.UserId + "/" + "sv-SE"; //OutComment if release version
        }
            
        WWW www = new WWW(url);

        yield return www;
        
        if (www.error == null)
        {
            Debug.Log("Result:\n" + www.text);
            try
            {
                JsonCategoryCollection cc = JsonReader.Deserialize<JsonCategoryCollection>(www.text);
                Debug.Log("Deserialized:\n" + cc.ToString());
                Global.Instance.categoryCollection = JsonCategoryCollectionToExerciseCategoryCollection(cc);
                Debug.Log("Converted to ExercizeCategoryCollection:\n" + Global.Instance.categoryCollection.ToString());
                Global.Instance.LoadMain();
            }
            catch(Exception e)
            {
                Util.MessageBox(new Rect(0, 0, 400, 200), "Error: "+e.Message+"\n\nPlease try to restart the application!" , Message.Type.Error, false, true);
            }
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }   
    }

    public static IEnumerator UploadData()
    {
        string url = "https://vfo.welfaredenmark.com/Service/SaveData/"; //Production environment service
        url = "http://localhost:59477/Service/SaveData/"; //LOCAL SERVICE - Comment for release version

        if (Global.Instance.ProgramLanguage == "sv-SE")
        {
            //url = "http://vfo.welfaresverige.se/Service/SaveData/"; //OutComment if release version
        }
			


        Debug.Log("Uploading Data:\n" + Global.Instance.categoryCollection);
        JsonCategoryCollection cc = ExerciseCategoryCollectionToJsonCategoryCollection(Global.Instance.categoryCollection);

        Debug.Log("Converted To Json Container:\n" + cc.ToString());
        string serialized = JsonWriter.Serialize(cc);

        Debug.Log("Serialized:\n" + serialized);
        Encoding encoding = Encoding.UTF8;
        byte[] bytes = encoding.GetBytes(serialized);

        WWW www = new WWW(url, bytes);

        yield return www;
        // check for errors
        if (www.error == null)
        {
            Debug.Log("Result: " + www.text);
            Debug.Log("Upload Complete.");
        }
        else
        {
            Debug.Log("Upload Error: " + www.error);
        } 
    }

    static public IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Result: " + www.text);
        } else {
            Debug.Log("WWW Error: "+ www.error);
        }    
    } 

    static ExerciseCategoryCollection JsonCategoryCollectionToExerciseCategoryCollection(JsonCategoryCollection collection)
    {
        Global.Instance.UserId = collection.UserId;
        ExerciseCategoryCollection ecc = new ExerciseCategoryCollection();
        foreach (JsonCategory c in collection.Categories)
        {
            ExerciseCategory tmpCategory = new ExerciseCategory(c.Id, c.Name, c.Score);
            ecc.Add(tmpCategory);
            foreach (JsonExercise e in c.Exercises)
            {
                Exercise tmpExercise = new Exercise(e.Id, e.Name, e.Score, e.SceneFunction);
                tmpCategory.Add(tmpExercise);
                //foreach (JsonExercisePart p in e.Parts)
                //{
                //    ExercisePart tmpPart = new ExercisePart(p.Id, p.Name, p.Completed, p.Time);
                //    tmpExercise.Add(tmpPart);
                //}
            }
        }
        return ecc;
    }

    static JsonCategoryCollection ExerciseCategoryCollectionToJsonCategoryCollection(ExerciseCategoryCollection collection)
    {
        JsonCategoryCollection jcc = new JsonCategoryCollection();
        jcc.UserId = Global.Instance.UserId;
        List<JsonCategory> categoryList = new List<JsonCategory>();

        foreach (ExerciseCategory c in collection)
        {
            JsonCategory tmpCategory = new JsonCategory(c.ID, c.Name, c.Score);
            List<JsonExercise> exerciseList = new List<JsonExercise>(); 
            foreach (Exercise e in c)
            {
                JsonExercise tmpExercise = new JsonExercise(e.ID, e.Name, e.Score, e.Function, e.Attempted);
                
                // Resets attempted each time the data is set, so only current attempted exercise is sent
                e.Attempted = false;

                //List<JsonExercisePart> partList = new List<JsonExercisePart>();
                //foreach (ExercisePart p in e)
                //{
                //    JsonExercisePart tmpPart = new JsonExercisePart(p.ID, p.Name, p.Complete, p.Time);
                //    partList.Add(tmpPart);
                //}
                //tmpExercise.Parts = partList.ToArray();
                exerciseList.Add(tmpExercise);
            }
            tmpCategory.Exercises = exerciseList.ToArray();
            categoryList.Add(tmpCategory);
        }
        jcc.Categories = categoryList.ToArray();
        return jcc;
    }

    // Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
