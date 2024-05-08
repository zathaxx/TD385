using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private bool UITower;
    private Vector3 original_position;
    private bool dragging;
    private UIController uIController;
    public int towerCost;
    private Tilemap tiles;
    public bool canPlace;
    void Start()
    {
        if (transform.parent == GameObject.Find("UI").transform) {
            UITower = true;
            canPlace = true;
        } else {
            UITower = false;
        }
        original_position = transform.position;
        dragging = false;
        uIController = GameObject.Find("UI").GetComponent<UIController>();
        tiles = GameObject.Find("Tilemap").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging) {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        }
    }

    void OnMouseDown() {
        if (UITower) {
            original_position = transform.position;
            dragging = true;
        }
    }

    void OnMouseUp() {
        if (UITower) {
            dragging = false;
            Vector3Int cell = tiles.WorldToCell(transform.position);
            Vector3 world_cell = tiles.CellToWorld(cell);
            TileBase tile = tiles.GetTile(cell);
            if (tile != null && canPlace && uIController.coins - towerCost >= 0) {
                GameObject e = Instantiate(Resources.Load("Prefabs/" + gameObject.name + "Variant") as GameObject);
                e.transform.localScale = transform.localScale;
                e.transform.position = new Vector3(world_cell.x +4f, world_cell.y + 4f, e.transform.position.z);
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
            transform.position = original_position;
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Tower" || other.tag == "Enemy") {
            canPlace = false;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Tower"|| other.tag == "Enemy") {
            canPlace = true;
        }
    }

    public bool IsUITower() {
        return UITower;
    }
}
