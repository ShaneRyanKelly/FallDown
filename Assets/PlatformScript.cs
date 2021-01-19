using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public float upperBounds = 10.0f;
    public float lowerBounds = -6.0f;
    public float platformSpeed = .01f;
    public int numOpen = 2;
    public bool enablePickup = false;
    public bool spawnPickup = false;
    public GameObject section;
    public GameObject pickup;
    private int[] positions = {0, 1, 2, 3, 4, 5, 6, 7, 8};
    private float[] positionLocations = new float[9];

    private CompositeCollider2D platCollider;
    Vector3 startingPosition;

    public int itemPercent;

    private int numSegments;

    bool ArrayContains(int[] array, int current)
    {
        foreach (int position in array)
        {
            if (position == current)
            {
                return true;
            }
        }
        return false;
    }
    ArrayList CloneArray(int[] SeedArray)
    {
        ArrayList Clone = new ArrayList();
        foreach (int num in SeedArray)
        {
            Clone.Add(num);
        }
        return Clone;
    }
    int[] GetOpenings(int numOpenings)
    {
        int[] randomPositions = GetRandomPositions();
        int[] openLocations = new int[numOpenings];
        for (int i = 0; i < numOpenings; i++)
        {
            openLocations[i] = randomPositions[i];
        }
        return openLocations;
    }

    int[] GetRandomPositions()
    {
        int[] randomPositions = new int[positions.Length];
        int[] positionsClone = positions;
        int randomIndex;
        for (int i = 0; i < positions.Length; i++)
        {
            randomIndex = Random.Range(0, positionsClone.Length);
            randomPositions[i] = positionsClone[randomIndex];
            RemovePosition(positionsClone, randomIndex);
        }
        return randomPositions;
    }
    public void IncreaseSpeed(float speedFactor)
    {
        platformSpeed = platformSpeed + speedFactor;
    }
    int[] RemovePosition(int[] oldArray, int index)
    {
        int newLength = oldArray.Length - 1;
        int[] newArray = new int[newLength];
        int intNewIndex = 0;
        for (int i = 0; i < oldArray.Length; i++)
        {
            if (i == index)
                continue;
            else
                newArray[intNewIndex] = oldArray[i];
            intNewIndex++;
        }
        return newArray;
    }
    void SetPositionLocations()
    {
        float currentLocation = -7.9f;
        for (int i = 0; i < positionLocations.Length; i++)
        {
            positionLocations[i] = currentLocation;
            currentLocation += 2.0f;
        }
    }
    private Vector2[] GetVertices(BoxCollider2D currentCollider)
    {
        Vector2[] vertices = new Vector2[4];

        vertices[0] = new Vector2(-currentCollider.bounds.extents.x, currentCollider.bounds.extents.y);
        vertices[1] = new Vector2(currentCollider.bounds.extents.x, currentCollider.bounds.extents.y);
        vertices[2] = new Vector2(currentCollider.bounds.extents.x, -currentCollider.bounds.extents.y);
        vertices[3] = new Vector2(-currentCollider.bounds.extents.x, -currentCollider.bounds.extents.y);

        return vertices;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetPositionLocations();
        platCollider = gameObject.GetComponent<CompositeCollider2D>();
        int[] openPositions = new int[Random.Range(1, numOpen + 1)];
        if (enablePickup)
        {
            int pickupToss = Random.Range(0,9);
            if (pickupToss > itemPercent)
            {
                spawnPickup = true;
            }
        }
        openPositions = GetOpenings(openPositions.Length);
        for (int i = 0; i < positions.Length; i++)
        {
            bool openSection = ArrayContains(openPositions, i);
            if (spawnPickup && openSection)
            {
                if (Random.Range(0, 9) > itemPercent)
                {
                    startingPosition = new Vector3(positionLocations[i], lowerBounds, 1.0f);
                    var currentPickup = Instantiate(pickup, startingPosition, Quaternion.identity);
                    currentPickup.transform.parent = gameObject.transform;
                }
            }
            if (!openSection)
            {
                startingPosition = new Vector3(positionLocations[i], lowerBounds, 1.0f);
                var currentSection = Instantiate(section, startingPosition, Quaternion.identity);
                currentSection.transform.parent = gameObject.transform;
                /**if (firstSection){
                    polyPath[0] = GetVertices(currentSection.GetComponent<BoxCollider2D>())[0];
                    polyPath[4] = GetVertices(currentSection.GetComponent<BoxCollider2D>())[4];
                    firstSection = false;
                    platCollider.pathCount++;
                }
                if (!firstSection && i < positions.Length && ArrayContains(openPositions, i+1)){
                    polyPath[1] = GetVertices(currentSection.GetComponent<BoxCollider2D>())[1];
                    polyPath[3] = GetVertices(currentSection.GetComponent<BoxCollider2D>())[3];
                    firstSection = true;
                    platCollider.SetPath(pathIndex, polyPath);
                    pathIndex++;

                }**/
                //Destroy(currentSection.GetComponent<BoxCollider2D>());
            }
            
        }
        //platformCollider = gameObject.AddComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(0, platformSpeed, 0);
        if (transform.position.y > upperBounds)
        {
            Destroy(gameObject);
        }
    }
}
