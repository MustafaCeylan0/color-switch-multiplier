using UnityEngine;

public class LineObstacle : MonoBehaviour
{
    /*[SerializeField] private GameObject[] lineIndividualPrefabs;
    private int lineToGetIndex = 0;
    private float midPointX;
    LinkedList<GameObject> lines = new LinkedList<GameObject>();


    void Start()
    {
        Transform[] childsTransform = gameObject.GetComponentsInChildren<Transform>();
        foreach (var tr in childsTransform)
        {
            lines.AddLast(tr.gameObject);
        }

        ArrayList arrayList = new ArrayList();



        StartCoroutine(move());
    }


    IEnumerator move()
    {
        while (true)
        {
            gameObject.transform.position = gameObject.transform.position + new Vector3(0.1f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void destroyLastCreateFirst(float scaleX)
    {
        //the last unit in the right is already destroyed
        //create the next instance in the left

        float posX = lines.ElementAt(0).transform.position.x - 1.5f;
        lines.AddFirst(Instantiate(lineIndividualPrefabs[lineToGetIndex],
            new Vector3(posX, transform.position.y, 0), quaternion.identity));
        lineToGetIndex++;
        if (lineToGetIndex == lineIndividualPrefabs.Length)
        {
            lineToGetIndex = 0;
        }
    }*/
}