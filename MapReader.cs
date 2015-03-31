using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelveCodeB
{
    class MapReader
    {
//Will hold the coordinates for all the objects.
        //Attributes
        private string currentFloorStr; 
        private string currentRoomStr;
        private int floorNum;
        private int roomNum;

        Random rgen = new Random();



        //Maps are 5x5 grids, and mostly empty.


        private int mapx;
        private int mapy;
        private char[,] mapXY = new char[5, 5];

        //Rooms are 25x25 grids, and can be filled with a lot of stuff.
        private int roomx;
        private int roomy;
        private char[,] roomXY = new char[25, 25];


        //properties
        public int mapX
        {
            get { return mapx; }
            set { mapx = value; }
        }

        public int mapY
        {
            get { return mapy; }
            set { mapy = value; }
        }

        public int roomX
        {
            get { return roomx; }
            set { roomx = value; }
        }

        public int roomY
        {
            get { return roomy; }
            set { roomy = value; }
        }
        


        //methods

        /// <summary>
        /// Floor number must be a three digit number between 1 and the max number of floors.
        /// </summary>
        /// <param name="floorNumber"></param>
        public void readFloor(int floorNumber)
        {

            //Concatenation of the floorNumber and "floor_"
            currentFloorStr = "floor_" + floorNum;

            StreamReader floorFileInput = new StreamReader(currentFloorStr);
            

            for(int i = 0; i<=4;i++)
            {
                
                for(int j = 0; j<=4;j++)
                {
                    mapXY[i, j] = (char) floorFileInput.Read();
                }
            }

            floorFileInput.Close();
        }


        public void readRoom(int roomNumber)
        {

            currentRoomStr = "room_" + roomNumber;

            StreamReader roomFileInput = new StreamReader(currentRoomStr);


            for(int i = 0; i<=24;i++)
            {
                for (int j = 0; j <= 24; j++)
                {
                    roomXY[i, j] = (char)roomFileInput.Read();
                }
            }

            roomFileInput.Close();

        }



        
    }
}
