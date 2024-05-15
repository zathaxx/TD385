using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Tilemaps;
using TMPro;

public class UITowerBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 original_position;
    private bool moving;
    private UIController uIController;
    public int towerCost;
    private Tilemap tiles;
    public bool canPlace;
    GameObject shadow;
    private GameObject information;
    public string Title1;
    public string Desc1;
    public string Title2;
    public string Desc2;


    void Start()
    {
        canPlace = true;
        uIController = GameObject.Find("UI").GetComponent<UIController>();
        original_position = transform.localPosition;
        moving = false;
        tiles = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        CreateShadow();
        information = uIController.information;
    }


    void CreateShadow() {
        shadow = new GameObject();
        shadow.gameObject.name = gameObject.name + "Shadow";
        shadow.transform.parent = transform;
        shadow.AddComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
        Color c = shadow.GetComponent<SpriteRenderer>().color;
        shadow.GetComponent<SpriteRenderer>().color = new Color (c.r, c.g, c.b, 0.5f);
        shadow.transform.localScale = new Vector3(1f, 1f, 1f);
        shadow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

            Vector3Int cell = tiles.WorldToCell(transform.position);
            Vector3 world_cell = tiles.CellToWorld(cell);
            TileBase tile = tiles.GetTile(cell);

            canPlace = validPlacement(world_cell);

            if (tile != null && canPlace)
            {
                shadow.SetActive(true);
                shadow.transform.position = new Vector3(world_cell.x + 4f, world_cell.y + 4f, shadow.transform.position.z);
            } else {
                shadow.SetActive(false);
            }
        }
    }



    void OnMouseUp()
    {
        if (moving) {
            moving = false;
            Vector3Int cell = tiles.WorldToCell(transform.position);
            Vector3 world_cell = tiles.CellToWorld(cell);
            TileBase tile = tiles.GetTile(cell);
            if (tile != null && canPlace && uIController.coins - towerCost >= 0)
            {
                GameObject e = Instantiate(Resources.Load("Prefabs/" + gameObject.name + "Variant") as GameObject);
                e.transform.localScale = transform.localScale;
                e.transform.position = new Vector3(world_cell.x + 4f, world_cell.y + 4f, e.transform.position.z);
                EntityProperties tower = e.GetComponent<EntityProperties>();
                float y = 4;
                for (int i = 0; i < 5; i++)
                {
                    if (e.transform.position.y == y)
                    {
                        tower.setLane(i);
                        break;
                    }
                    else
                    {
                        y -= 8;
                    }
                }
                uIController.coins -= towerCost;
            }
            transform.localPosition = original_position;
            shadow.SetActive(false);
        } else {
            moving = true;
        }
    }

    void OnMouseOver() {
        if (transform.localPosition == original_position) {
            uIController.UpdateInfoUI(gameObject);
        } else {
            information.SetActive(false);
        }
    }

    void OnMouseExit() {
        information.SetActive(false);
    }

    private bool validPlacement(Vector3 world_cell) {
        Vector3 newPos = new Vector3(world_cell.x + 4f, world_cell.y + 4f, shadow.transform.position.z);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPos, 0.1f);
        foreach (Collider2D col in colliders) {
            if (col.tag == "Tower") {
                return false;
            }
        }
        return true;
    }


}
