using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    //public GameObject block;
    float maxX = 5;
    float minX = -5;
    float minY = -12;
    float maxY = 12;
    int tableWidth = 10;
    int tableHeight = 24;
    GameObject[,] table;
    GameObject[,] movingBlock;
    bool verticalNeutral = true;
    bool horizontalNeutral = true;
    bool doRotation = false;
    bool gameIsOn = false;
    bool left;
    bool right;
    int delayBetweenMoves = 30;
    int currentTicksBetweenMoves = 0;
    int point = 0; //TODO show points for player
    int lines = 0;
    public UnityEngine.UI.Text startText;
    
    
    /// <summary>
    /// Begins game
    /// </summary>
    void beginGame()
    {
        delayBetweenMoves = 30;
        startText.gameObject.SetActive(false);
        gameIsOn = true;
        table = new GameObject[tableWidth, tableHeight];
        movingBlock = new GameObject[tableWidth, tableHeight];
        newBlock(Random.Range(0, 5));

    }
    /// <summary>
    /// Checks if any block has gotten so high that it's game over
    /// </summary>
    /// <returns></returns>
    bool CheckForGameOver() {
        for (int i = 0; i < tableWidth; i++) {
            if (table[i, 20] != null) {
                return true;
            }
        }
        return false;
    }
    

    Vector3 calculatePosition(int x, int y) {
        float squarewidth = (maxX - minX) / tableWidth;
        float squareheight = (maxY - minY) / tableHeight;
        float xpart = squarewidth * (x + 0.5f) + minX;
        float ypart = squareheight * (y + 0.5f) + minY;
        return new Vector3(xpart, ypart);
    }
    GameObject showBlock(int x, int y, Color colorOfBlock) {
        Vector3 position = calculatePosition(x, y);
        GameObject block = GameObject.Instantiate(Resources.Load<GameObject>("Square"), position, new Quaternion());
        block.GetComponent<SpriteRenderer>().color = colorOfBlock ;
        return block;
    }
    /// <summary>
    /// creates new moving block
    /// </summary>
    /// <param name="whatBlock"></param>
    void newBlock(int whatBlock) {

        switch (whatBlock) {
            case 0: 
            movingBlock[5, 19] = showBlock(5, 19, Color.blue);
            movingBlock[4, 19] = showBlock(4,19, Color.blue);
            movingBlock[6, 19] = showBlock(6, 19, Color.blue);
                movingBlock[4, 18] = showBlock(4, 18, Color.blue);
                    break;
            case 1:
        movingBlock[5, 23] = showBlock(5, 23, Color.red);
        movingBlock[4, 23] = showBlock(4, 23, Color.red);
        movingBlock[6, 23] = showBlock(6, 23, Color.red);
        movingBlock[6, 22] = showBlock(6, 22, Color.red);
                break;
            case 2:
                movingBlock[5, 23] = showBlock(5, 23, Color.yellow);
                movingBlock[4, 23] = showBlock(4, 23, Color.yellow);
                movingBlock[6, 23] = showBlock(6, 23, Color.yellow);
                movingBlock[7, 23] = showBlock(7, 23, Color.yellow);
                break;
            case 3:
                movingBlock[5, 23] = showBlock(5, 23, Color.green);
                movingBlock[4, 23] = showBlock(4, 23, Color.green);
                movingBlock[6, 23] = showBlock(6, 23, Color.green);
                movingBlock[5, 22] = showBlock(5, 22, Color.green);
                break;
            case 4:
                movingBlock[5, 23] = showBlock(5, 23,Color.cyan);
                movingBlock[4, 23] = showBlock(4, 23, Color.cyan);
                movingBlock[5, 22] = showBlock(5, 22, Color.cyan);
                movingBlock[4, 22] = showBlock(4, 22, Color.cyan);
                break;
            case 5:
                movingBlock[5, 23] = showBlock(5, 23, Color.magenta);
                movingBlock[4, 23] = showBlock(4, 23, Color.magenta);
                movingBlock[5, 22] = showBlock(5, 22, Color.magenta);
                movingBlock[4, 22] = showBlock(4, 22, Color.magenta);
                movingBlock[6, 23] = showBlock(4, 22, Color.magenta);
                break;
        }
    }
    /// <summary>
    /// moves moving block side ways
    /// </summary>
    void manouver() {

        if (left)
        {
            Debug.Log("left!!!");
            bool leftBlocked = false;
            for (int X = 0; X < movingBlock.GetLength(0); X++)
            {
                for (int Y = 0; Y < movingBlock.GetLength(1); Y++)
                {
                    if (movingBlock[X, Y] != null)
                    {
                        if (X <= 0) {
                            leftBlocked = true;
                        } else {
                            if (table[X - 1, Y] != null) {
                                leftBlocked = true;
                            }
                        }

                    }
                }
            }
            if (!leftBlocked) {
                for (int X = 0; X < movingBlock.GetLength(0); X++)
                {
                    for (int Y = 0; Y < movingBlock.GetLength(1); Y++)
                    {
                        if (movingBlock[X, Y] != null)
                        {
                            movingBlock[X - 1, Y] = movingBlock[X, Y];
                            movingBlock[X, Y] = null;
                            movingBlock[X - 1, Y].transform.position = calculatePosition(X - 1, Y);

                        }
                    }
                }
            }
        }
        if (right)
        {
            Debug.Log("Right!!!");
            bool RightBlocked = false;
            for (int X = 0; X < movingBlock.GetLength(0); X++)
            {
                for (int Y = 0; Y < movingBlock.GetLength(1); Y++)
                {
                    if (movingBlock[X, Y] != null)
                    {
                        if (X >= 9)
                        {
                            RightBlocked = true;
                        }
                        else
                        {
                            if (table[X + 1, Y] != null)
                            {
                                RightBlocked = true;
                            }
                        }

                    }
                }
            }
            if (!RightBlocked)
            {
                for (int X = movingBlock.GetLength(0) - 1; X >= 0; X--)
                {
                    for (int Y = 0; Y < movingBlock.GetLength(1); Y++)
                    {
                        if (movingBlock[X, Y] != null)
                        {
                            movingBlock[X + 1, Y] = movingBlock[X, Y];
                            movingBlock[X, Y] = null;
                            movingBlock[X + 1, Y].transform.position = calculatePosition(X + 1, Y);

                        }
                    }
                }
            }
        }
        left = false; right = false;
    }
    /// <summary>
    /// moves moving block down
    /// </summary>
    void descend() {


        //MOVE ALL BLOCKS down if there is nothing directly under 
        bool somethingUnder = false;
        for (int X = 0; X < movingBlock.GetLength(0); X++) {
            for (int Y = 0; Y < movingBlock.GetLength(1); Y++)
            {
                if (movingBlock[X, Y] != null)
                {
                    if (Y > 0)
                    {
                        if (table[X, Y - 1] != null)
                        {
                            somethingUnder = true;
                        }
                    }
                    else { somethingUnder = true; }
                }
            }
        }
        if (somethingUnder)
        {
            Debug.Log("something is under");
            //add moving block to table
            addBlockToTable();
            if (CheckForGameOver()) {
                Debug.Log("gameOver");
                GameOver();
            }
            //make new block to move
            newBlock(Random.Range(0,6));
        }
        else {
            Debug.Log("nothing under");
            //Move Block down both in movingBlock[,] and gameobjects that represent them.
            for (int x = 0; x < movingBlock.GetLength(0); x++) {
                for (int y = 1; y < movingBlock.GetLength(1); y++)
                {
                    if (movingBlock[x, y] != null)
                    {
                        Debug.Log("move down");
                        movingBlock[x, y - 1] = movingBlock[x, y];
                        movingBlock[x, y] = null;
                        movingBlock[x, y - 1].transform.position = calculatePosition(x, y - 1);
                    }
                }
            }
        }
    }
    /// <summary>
    /// Returns number of full lines thet were deleted thistime
    /// </summary>
    /// <returns></returns>
    int checkAndDeleteLines() {
        int linesDeleted = 0;
        for (int Y = 0; Y < tableHeight; Y++) {
            bool hole = false;
            for (int X = 0; X < tableWidth; X++) {
                if (table[X, Y] == null) { hole = true; }
            }
            if (!hole)
            {

                //Delete Line, move everything down
                for (int i = 0; i < tableWidth; i++) {
                    GameObject.Destroy(table[i, Y]);
                }
                for (int downY = Y; downY < tableHeight - 1; downY++) {
                    for (int downX = 0; downX < tableWidth; downX++) {
                        table[downX, downY] = table[downX, downY + 1];
                        if (table[downX, downY] != null)
                        {
                            table[downX, downY].transform.position = calculatePosition(downX, downY);
                        }
                    }
                }
                Y--;

                linesDeleted++;
            }
        }
        return linesDeleted;
    }
    /// <summary>
    /// Rotates moving block if there is room for it. 
    /// </summary>
    void rotate() {
        int lengthOfBlockX = 0;
        int lengthOfBlockY = 0;
        int minx = tableWidth;
        int miny = tableHeight;
        GameObject[,] rotatedBlock = new GameObject[tableWidth, tableHeight];
        bool fits = true;
        for (int X = 0; X < movingBlock.GetLength(0); X++)
        {

            int currentYLength = 0;
            for (int Y = 0; Y < movingBlock.GetLength(1); Y++)
            {

                if (movingBlock[X, Y] != null)
                {
                    if (minx > X) { minx = X; }

                    currentYLength++;
                    if (currentYLength > lengthOfBlockY) { lengthOfBlockY = currentYLength; }
                }
            }

        }
        for (int Y = 0; Y < movingBlock.GetLength(1); Y++)
        {

            int currentXLength = 0;
            for (int X = 0; X < movingBlock.GetLength(0); X++)
            {

                if (movingBlock[X, Y] != null)
                {
                    if (miny > Y) { miny = Y; }
                    currentXLength++;
                    if (currentXLength > lengthOfBlockX) { lengthOfBlockX = currentXLength; }
                }
            }

        }
        for (int Y = miny; Y < miny + lengthOfBlockY; Y++) {
            for (int X = minx; X < minx + lengthOfBlockX; X++) {
                //Debug.Log("x"+X+" y"+Y);
                if ((miny + lengthOfBlockX - (X - minx + 1)) >= tableHeight || (miny + lengthOfBlockX - (X - minx + 1)) < 0) { fits = false; break; }
                if ((minx + (Y - miny)) >= tableWidth || (minx + (Y - miny)) < 0) { fits = false; break; }
                rotatedBlock[minx + (Y - miny), miny + lengthOfBlockX - (X - minx + 1)] = movingBlock[X, Y];
            }

        }
        if (!checkIfOverlap(rotatedBlock) && fits)
        {
            movingBlock = rotatedBlock;
            setToPlace();
        }
    }
    /// <summary>
    /// return true if there are game objects in same index as in table.
    /// </summary>
    /// <param name="checkThis"></param>
    /// <returns></returns>
    bool checkIfOverlap(GameObject[,] checkThis) {
        for (int X = 0; X < movingBlock.GetLength(0); X++)
        {
            for (int Y = 0; Y < movingBlock.GetLength(1); Y++)
            {
                if (checkThis[X, Y] != null && table[X, Y])
                {

                    return true;
                }
            }

        }

        return false;
    }
    /// <summary>
    /// fixes movingblock as part of level. Also checks and erases any completed lines
    /// </summary>
    void addBlockToTable() {
        int addedBlocks = 0;
        for (int X = 0; X < movingBlock.GetLength(0); X++)
        {
            for (int Y = 0; Y < movingBlock.GetLength(1); Y++)
            {
                if (movingBlock[X, Y] != null) {
                    table[X, Y] = movingBlock[X, Y];
                    movingBlock[X, Y] = null;
                    addedBlocks++;

                }
            }

        }
        int newlines= checkAndDeleteLines();
        lines += newlines;
        switch(newlines){
            case 0:  break;
            case 1: point += 100;break;
            case 2:point += 300;break;
            case 3:point += 500;break;
            case 4:point += 800;break;
        }
        //Debug.Log(addedBlocks + " blocks added");

    }
    void setToPlace() {
        for (int X = 0; X < movingBlock.GetLength(0); X++)
        {

            for (int Y = 0; Y < movingBlock.GetLength(1); Y++)
            {

                if (movingBlock[X, Y] != null)
                {
                    movingBlock[X, Y].transform.position = calculatePosition(X, Y);
                }
            }

        }
    }
    /// <summary>
    /// Ends game
    /// </summary>
    void GameOver(){
        for (int X = 0; X < table.GetLength(0); X++)
        {

            for (int Y = 0; Y < table.GetLength(1); Y++)
            {

                if (table[X, Y] != null)
                {
                    GameObject.Destroy(table[X, Y]);
                }
            }

        }
        gameIsOn = false;
        startText.gameObject.SetActive(true);
    }
    void FixedUpdate()
    {
        if (lines >= 10) {
            if(delayBetweenMoves > 6) {  delayBetweenMoves -= 4;lines = 0; }
        }
        
        if (!gameIsOn) {return; }
        if (doRotation) { rotate();doRotation = false; }

            manouver();
    
        if (currentTicksBetweenMoves >= delayBetweenMoves || Input.GetButton("Jump")) 
        {

                descend();

            currentTicksBetweenMoves = 0;
        }
        else { currentTicksBetweenMoves++; }

    }
    void Update()
    {
        if (!gameIsOn)
        {
            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            {
                beginGame();
            }
        }
        else
        {
            if (Input.GetAxisRaw("Horizontal") != 0 && horizontalNeutral)
            {
                if (Input.GetAxisRaw("Horizontal") > 0) { right = true;  } else { left = true; }
                horizontalNeutral = false;
            }
            else
            {
                if (Input.GetAxisRaw("Horizontal") == 0) { horizontalNeutral = true; }

                if (Input.GetAxisRaw("Vertical") != 0 && verticalNeutral) { doRotation = true; verticalNeutral = false; } else { if (Input.GetAxisRaw("Vertical") == 0) { verticalNeutral = true; } }
            }
        }
    }
}
