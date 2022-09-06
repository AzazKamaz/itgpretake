using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public GameObject floorPrefab;
    public GameObject wallPrefab;

    // Use https://www.dcode.fr/maze-generator
    [TextArea] public string maze;

    [ContextMenu("Destroy Maze")]
    public void DestroyMaze()
    {
        while (transform.childCount > 0)
            foreach (Transform child in transform)
                DestroyImmediate(child.gameObject);
    }

    [ContextMenu("Generate Maze")]
    public void GenerateMaze()
    {
        DestroyMaze();

        var pos1 = transform.position;
        foreach (var i in maze.Split('\n'))
        {
            var pos2 = pos1;
            pos1 += transform.forward * 2;

            foreach (var j in i)
            {
                var pos3 = pos2;
                pos2 += transform.right * 2;

                switch (j)
                {
                    case '#':
                        GameObject.Instantiate(wallPrefab, pos3 + transform.up, transform.rotation, transform);
                        break;
                    case '_':
                        GameObject.Instantiate(floorPrefab, pos3 - transform.up, transform.rotation, transform);
                        break;
                }
            }
        }
    }
}