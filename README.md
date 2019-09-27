# AStar
A* med lite snake 
Om man kommenterar ut 
MoveTail(); 
och  
if (isScore)
                    {
                        tail.Insert(0, CreateTailNode(oldPosNode.x, oldPosNode.y)); //Adds a node to your tail
                    }
i MoveAstar() så ser man bara spelaren röra på sig.

Om man kommenterar in     
//if (isAStar)
            //{
            //    do
            //    {
            //        CreateWall();
            //        count++;
            //    } while (count < walls);
            //}
Så får man in lite obstacles i spelet (se till att sätta antalet till mer än 0 i editorn)
