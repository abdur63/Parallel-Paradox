using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private FirebaseEventManager firebaseEventManager;

    public List<GameObject> blocks = new List<GameObject>();
    List<MeshRenderer> meshRenderers = new List<MeshRenderer>();


    public List<int> reds = new List<int>();
    public List<int> blues = new List<int>();

    int currentIndex;

    //public Material redMat;
    //public Material blueMat;

    bool blueBlocksDone = false;
    bool redBlocksDone = false;

    string redMat = "Mat 2 (Instance)";
    string blueMat = "Mat 3 (Instance)";

    public GameObject levelPanel;
    public static bool levelCompleted = false;

    public Animator cameraAnimator;

    public GameObject swapBtn;

    // Start is called before the first frame update
    void Start()
    {
        //firebase  event log
        //firebaseEventManager = FirebaseEventManager.Instance;
        //FirebaseEventManager.Instance.LogLevelStart(SceneManager.GetActiveScene().buildIndex);

        //Debug.Log(SceneManager.GetActiveScene().buildIndex);

        levelCompleted = false;

        for (int i = 0; i < blocks.Count; i++)
        {
            meshRenderers.Add(blocks[i].GetComponent<MeshRenderer>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!redBlocksDone || !blueBlocksDone)
        {
            redBlocksDone = checkRedBlocks();
            Debug.Log("redBlocksDone : " + redBlocksDone);
            blueBlocksDone = checkBlueBlocks();
            Debug.Log("blueBlocksDone : " + blueBlocksDone);
        }

        if (redBlocksDone && blueBlocksDone && !levelCompleted)
        {
            Debug.Log("Level completed!");

            // Play camera animation
            cameraAnimator.Play("levelFinish1");
            levelCompleted = true;
            //firebase event log
            //FirebaseEventManager.Instance.LogLevelComplete(SceneManager.GetActiveScene().buildIndex);
            if (swapBtn != null)
            {
                swapBtn.SetActive(false);
            }

            Invoke("setPanelTrue", 2);

            //StartCoroutine(PlayCameraAnimation());
        }
    }



    bool checkRedBlocks()
    {
        for (int i = 0; i < reds.Count; i++)
        {
            currentIndex = reds[i];
            if (meshRenderers[reds[i]].material.name != redMat)
            {
                return false;
            }

        }
        return true;
    }

    bool checkBlueBlocks()
    {
        //if (meshRenderers[0].material.name == blueMat && meshRenderers[3].material.name == blueMat
        //    && meshRenderers[4].material.name == blueMat)
        //{
        //    blueBlocksDone = true;
        //}
        //else
        //{
        //    blueBlocksDone = false;
        //}
        for (int i = 0; i < blues.Count; i++)
        {
            currentIndex = blues[i];

            if (meshRenderers[blues[i]].material.name != blueMat)
            {
                return false;
            }

        }
        return true;


    }

    public void goToMenu()
    {
        levelCompleted = false;
        SceneManager.LoadScene("MainMenu");

    }

    public void loadLevel(int sceneIndex)
    {
        levelCompleted = false;
        SceneManager.LoadScene(sceneIndex);

    }

    IEnumerator PlayCameraAnimation()
    {
        // Play camera animation
        //cameraAnimator.Play("level1_anim");
        cameraAnimator.Play("levelFinish1");


        // Wait for animation to finish
        //while (cameraAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 3.0f)
        /*while (cameraAnimator.GetCurrentAnimatorStateInfo(0).IsName("levelFinish1"))
        {
            yield return null;
        }*/

        yield return new WaitUntil(() => cameraAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        // Set level complete panel active
        levelCompleted = true;
        levelPanel.SetActive(true);
    }

    void setPanelTrue()
    {
        levelPanel.SetActive(true);
    }



}
