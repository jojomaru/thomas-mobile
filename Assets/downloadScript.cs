using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class downloadScript : MonoBehaviour
{
    public GameObject mainMenu, downloadMenu;
    public TMP_InputField urlInput;
    public TMP_InputField textDesc;
    public TMP_Dropdown fileType;
    public Slider progressBar;
    string fileExt, filePath, downLog, logPath;

    public void downloadFile(){
        StartCoroutine(downloadFiles());
        mainMenu.SetActive(true);
        downloadMenu.SetActive(false);
    }

    void Start(){
        downLog = "downlog.txt";
        logPath = Application.persistentDataPath + "/" + downLog;
    }

    //downloader
    IEnumerator downloadFiles(){
        //filetype
        switch(fileType.value){
            case 0:
                filePath = "Images";
                fileExt = "png";
                break;
            case 1:
                filePath = "Videos";
                fileExt = "mp4";
                break;
            case 2:
                filePath = "PDF";
                fileExt = "pdf";
                break;
            case 3:
                filePath = "Others";
                fileExt = "";
                break;
            default:
                break;
        }

        UnityWebRequest uwr = new UnityWebRequest(urlInput.text, UnityWebRequest.kHttpVerbGET);
        string fileName = $"{textDesc.text}.{fileExt}";
        string path = Path.Combine(Application.persistentDataPath+"/"+filePath, fileName);
        uwr.downloadHandler = new DownloadHandlerFile(path);
        
        StartCoroutine(WaitForResponse(uwr));
        
        yield return uwr.SendWebRequest();
        
        //create if file doesn't exist
        /*if(!File.Exists(logPath)){
            string[] lines = {fileName, filePath};
            File.WriteAllLines(logPath, lines);
        }*/

        //add line if file exists
        string[] appendLines = {fileName, filePath, "\r\n"};
        File.AppendAllLines(logPath, appendLines);
    }

    //download progress tracker
    IEnumerator WaitForResponse(UnityWebRequest request){
        progressBar.gameObject.SetActive(true);
        while (!request.isDone){
            progressBar.value = request.downloadProgress;
            yield return null;
        }
        progressBar.gameObject.SetActive(false);
    }
}