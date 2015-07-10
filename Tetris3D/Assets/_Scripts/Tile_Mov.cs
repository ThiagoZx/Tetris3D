﻿using UnityEngine;
using System.Collections;

public class Tile_Mov : MonoBehaviour {

    public int[, ,] board;
    
    public void eraseInGrid(int[, ,] grid, int facing, GameObject group) {
        foreach (Transform child in group.transform) {
            int actualColumn = new int();
            if (facing == 0 || facing == 2) {
                actualColumn = Mathf.FloorToInt(child.transform.position.x);
            } else if (facing == 1 || facing == 3) {
                actualColumn = Mathf.FloorToInt(child.transform.position.z);
            }
            int actualRow = (-1) * (Mathf.RoundToInt(child.transform.position.y + 0.5f));
            grid[facing, actualRow, actualColumn] = 0;
        }
    }

    public void appearInGrid (int[, ,] grid, int facing, GameObject group) {
        foreach (Transform child in group.transform) {
            int actualColumn = new int();
            if (facing == 0 || facing == 2) {
                actualColumn = Mathf.FloorToInt(child.transform.position.x);
            } else if (facing == 1 || facing == 3) {
                actualColumn = Mathf.FloorToInt(child.transform.position.z);
            }
            int actualRow = (-1) * (Mathf.RoundToInt(child.transform.position.y + 0.5f));
            grid[facing, actualRow, actualColumn] = 1;
        }
    }

    public bool canRotate(int[, ,] grid, int facing, string direction, GameObject group) {
        //Erase the group position in grid so it won't hit itself
        eraseInGrid(grid, facing, group);

        //Rotate the group itself
        if (facing == 0 || facing == 2) {

			if (direction == "left") {
				group.transform.Rotate(0, 0, 90);
			} else {
				group.transform.Rotate(0, 0, -90);
			}

			foreach (Transform child in group.transform) {
				child.transform.rotation = Quaternion.Euler(0, 0, group.transform.eulerAngles.z * (-1));
			}

        } else {

			if (direction == "left") {
				group.transform.Rotate(90, 0, 0);
			} else {
				group.transform.Rotate(-90, 0, 0);
			}
			
			foreach (Transform child in group.transform) {
				child.transform.rotation = Quaternion.Euler(group.transform.eulerAngles.x * (-1),0,0);
			}
		}
        

        //Loop that goes through the position in desired rotation checking if it's empty 
        foreach (Transform child in group.transform) {
            int actualColumn = new int();
            if (facing == 0 || facing == 2) {
                actualColumn = Mathf.FloorToInt(child.transform.position.x);
            } else if (facing == 1 || facing == 3) {
                actualColumn = Mathf.FloorToInt(child.transform.position.z);
            }
            int actualRow = (-1) * (Mathf.RoundToInt(child.transform.position.y + 0.5f));
            if (grid[facing, actualRow, actualColumn] == 1) {
                
                //If it hits something, rotate back to the original position, draws it back in the grid and returns false (Not allowing the rotation)
				if (facing == 0 || facing == 2) {

					if (direction == "left") {
						group.transform.Rotate(0, 0, -90);
					} else {
						group.transform.Rotate(0, 0, 90);
					}

					foreach (Transform childz in group.transform) {
						childz.transform.rotation = Quaternion.Euler(0, 0, group.transform.eulerAngles.z * (-1));
					}
					
				} else {

					if (direction == "left") {
						group.transform.Rotate(-90, 0, 0);
					} else {
						group.transform.Rotate(90, 0, 0);
					}

					foreach (Transform childz in group.transform) {
						childz.transform.rotation = Quaternion.Euler(group.transform.eulerAngles.x * (-1),0,0);
					}

				}

                appearInGrid(grid, facing, group);
                return false;

            }
        }

        //If it goes through the entire loop without hiting anything, rotates back to the original position, draws it back in the grid and returns true (Allowing rotation)
		if (facing == 0 || facing == 2) {

			if (direction == "left") {
				group.transform.Rotate(0, 0, -90);
			} else {
				group.transform.Rotate(0, 0, 90);
			}

			foreach (Transform child in group.transform) {
				child.transform.rotation = Quaternion.Euler(0, 0, group.transform.eulerAngles.z * (-1));
			}
			

			
		} else {

			if (direction == "left") {
				group.transform.Rotate(-90, 0, 0);
			} else {
				group.transform.Rotate(90, 0, 0);
			}

			foreach (Transform child in group.transform) {
				child.transform.rotation = Quaternion.Euler(group.transform.eulerAngles.x * (-1),0,0);
			}

		}
		
		appearInGrid(grid, facing, group);
		return true;
	}
	
	public bool canMoveVertical(int[, ,] grid, int facing, GameObject group) {
        eraseInGrid(grid, facing, group);
        foreach (Transform child in group.transform) {
            int actualColumn = new int();
            if (facing == 0 || facing == 2) {
                actualColumn = Mathf.FloorToInt(child.transform.position.x);
            } else if (facing == 1 || facing == 3) {
                actualColumn = Mathf.FloorToInt(child.transform.position.z);
            }
            int actualRow = (-1) * (Mathf.RoundToInt(child.transform.position.y + 0.5f));
            int desiredRow = actualRow + 1;
            if (grid[facing, desiredRow, actualColumn] == 1) {
                appearInGrid(grid, facing, group);
                return false;
            }
        }

        appearInGrid(grid, facing, group);
        return true;
    }

    public bool canMoveHorizontal(int[, ,] grid, int facing, string direction, GameObject group) {
        eraseInGrid(grid, facing, group);

        foreach (Transform child in group.transform) {
            int actualColumn = new int();
            if (facing == 0 || facing == 2) {
                actualColumn = Mathf.FloorToInt(child.transform.position.x);
            } else if (facing == 1 || facing == 3) {
                actualColumn = Mathf.FloorToInt(child.transform.position.z);
            }
            int actualRow = (-1) * (Mathf.RoundToInt(child.transform.position.y + 0.5f));
            int desiredColumn = new int();
            if (direction == "left") {
                if (facing == 0 || facing == 2) {
                    desiredColumn = actualColumn - 1 + facing;
                } else if (facing == 1 || facing == 3) {
                    desiredColumn = actualColumn - 2 + facing;
                }
            } else {
                if (facing == 0 || facing == 2) {
                    desiredColumn = actualColumn + 1 - facing;
                } else if (facing == 1 || facing == 3) {
                    desiredColumn = actualColumn + 2 - facing;
                }
            }

            if (grid[facing, actualRow, desiredColumn] == 1) {
                appearInGrid(grid, facing, group);
                return false;
            }
        }

        appearInGrid(grid, facing, group);
        return true;
    }

    public void rotateObject(GameObject group, int[, ,] grid, int facing) {
        if (Input.GetKeyDown(KeyCode.Z) && canRotate(grid, facing, "left", group)) {
            
			eraseInGrid(grid, facing, group);

			if (facing == 0 || facing == 2) {
				group.transform.Rotate(0, 0, -90);
			} else {
				group.transform.Rotate(-90, 0, 0);
			}

			//child.transform.rotation = Quaternion.Euler(0, 0, group.transform.eulerAngles.z * (-1));
			//child.transform.rotation = Quaternion.Euler(group.transform.eulerAngles.x * (-1),0,0);

			
			foreach (Transform child in group.transform) {
				if (facing == 0 || facing == 2) {
					child.transform.rotation = Quaternion.Euler(0, 0, group.transform.eulerAngles.z * (-1));
				} else {
					child.transform.rotation = Quaternion.Euler(group.transform.eulerAngles.x * (-1),0,0);
				}
			}
            
            appearInGrid(grid, facing, group);

        } else if (Input.GetKeyDown(KeyCode.X) && canRotate(grid, facing, "right", group)) {
            eraseInGrid(grid, facing, group);

			if (facing == 0 || facing == 2) {
				group.transform.Rotate(0, 0, 90);
			} else {
				group.transform.Rotate(90, 0, 0);
			}

			foreach (Transform child in group.transform) {
				if (facing == 0 || facing == 2) {
					child.transform.rotation = Quaternion.Euler(0, 0, group.transform.eulerAngles.z * (-1));
				} else {
					child.transform.rotation = Quaternion.Euler(group.transform.eulerAngles.x * (-1),0,0);
				}
			}

            appearInGrid(grid, facing, group);

        }
    }

    public IEnumerator MovVertical (GameObject group, int[, ,] grid, int facing, GameObject[] parent, float time){
        yield return new WaitForSeconds(time);
        if (canMoveVertical(grid, facing, group)) {
            eraseInGrid(grid, facing, group);
            group.transform.position = new Vector3(group.transform.position.x, group.transform.position.y - 1, group.transform.position.z);
            appearInGrid(grid, facing, group);
        } else {
            group.tag = "Board";
            //group.transform.parent = parent[facing].transform;
			//GameObject a = group;
			//Transform[] allChildren = GetComponentsInChildren<a>();

            /*foreach (Transform child in a) {
				child.transform.parent = parent[facing].transform;
                child.tag = "Board";
            }*/

			/*for(int i = 0; i <= 4; i++){
				Transform a =  group.GetComponentInChildren<group.transform>();
				a.transform.parent = parent[facing].transform;
				a.tag = "Board";
			}*/
			while(group.transform.childCount > 1){
				Transform aChild = transform.FindChild( "Cube" );
				aChild.transform.parent = parent[facing].transform;
				aChild.tag = "Board";
			}

            time = 0.7f;
        }
		
		StartCoroutine (MovVertical (group, grid, facing, parent, time));
	}

    public void MovHorizontal(GameObject group, int[, ,] grid, int facing) {
        
        if (Input.GetKeyDown(KeyCode.RightArrow) && canMoveHorizontal(grid, facing, "left", group)) {
            eraseInGrid(grid, facing, group);
            if (facing == 0 || facing == 2) {
                group.transform.position = new Vector3(group.transform.position.x - 1 + facing, group.transform.position.y, group.transform.position.z);
            } else if (facing == 1 || facing == 3) {
                group.transform.position = new Vector3(group.transform.position.x, group.transform.position.y, group.transform.position.z - 2 + facing);
            }
            appearInGrid(grid, facing, group);

        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && canMoveHorizontal(grid, facing, "right", group)) {
            eraseInGrid(grid, facing, group);
            if (facing == 0 || facing == 2) {
                group.transform.position = new Vector3(group.transform.position.x + 1 - facing, group.transform.position.y, group.transform.position.z);
            } else if (facing == 1 || facing == 3) {
                group.transform.position = new Vector3(group.transform.position.x, group.transform.position.y, group.transform.position.z + 2 - facing);
            }
            appearInGrid(grid, facing, group);
        }
    }
}