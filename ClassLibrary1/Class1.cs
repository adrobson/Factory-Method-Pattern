using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{

    public class MazeGame
    {
        public Maze CreateMaze()
        {
            Maze aMaze = MakeMaze();

            Room r1 = MakeRoom(1);
            Room r2 = MakeRoom(2);

            Door theDoor = MakeDoor(r1, r2);

            aMaze.AddRoom(r1);
            aMaze.AddRoom(r2);

            r1.SetSide(Direction.North, MakeWall());
            r1.SetSide(Direction.East, theDoor);
            r1.SetSide(Direction.South, MakeWall());
            r1.SetSide(Direction.West, MakeWall());

            r2.SetSide(Direction.North, MakeWall());
            r2.SetSide(Direction.East, MakeWall());
            r2.SetSide(Direction.South, MakeWall());
            r2.SetSide(Direction.West, theDoor);

            return aMaze;
        }

        internal virtual Maze MakeMaze()
        {
            return new Maze();
        }

        internal virtual Room MakeRoom(int roomNo)
        {
            return new Room(roomNo);
        }

        internal virtual Wall MakeWall()
        {
            return new Wall();
        }

        internal virtual Door MakeDoor(Room r1, Room r2)
        {
            return new Door(r1, r2);
        }
    }

    public class BombedMazeGame : MazeGame
    {
        internal override Wall MakeWall()
        {
            return new BombedWall();
        }

        internal override Room MakeRoom(int roomNo)
        {
            return new RoomWithABomb(roomNo);
        }
    }

    public class EnchantedMazeGame : MazeGame
    {
        internal override Room MakeRoom(int roomNo)
        {
            return new EnchantedRoom(roomNo, CastSpell());
        }

        internal override Door MakeDoor(Room r1, Room r2)
        {
            return new DoorNeedingSpell(r1, r2);
        }

        protected Spell CastSpell()
        {
            return new Spell();
        }
    }
    public class Maze
    {
        public Maze()
        {
            Rooms = new List<Room>();
        }
        public void AddRoom(Room r1)
        {
            Rooms.Add(r1);
        }

        private List<Room> Rooms;
    }

    public abstract class MapSite
    {
        public virtual void Enter()
        {

        }
    }

    public class Room : MapSite
    {
        public int _roomNumber { get; private set; }

        public Room(int roomNo)
        {
            _roomNumber = roomNo;
            Sides = new Dictionary<Direction, MapSite>();
        }

        public void SetSide(Direction direction, MapSite m)
        {
            Sides.Add(direction, m);
        }
        public override void Enter()
        {

        }

        protected Dictionary<Direction, MapSite> Sides;
    }

    public class EnchantedRoom : Room
    {
        public EnchantedRoom(int roomNo, Spell spell) : base(roomNo)
        {
            _spell = spell;
        }

        protected Spell _spell;
    }

    public class RoomWithABomb : Room
    {
        public RoomWithABomb(int roomNo) : base(roomNo)
        {

        }
    }

    public class Spell
    {

    }

    public class Wall : MapSite
    {
        public override void Enter()
        {

        }
    }

    public class BombedWall : Wall
    {

    }

    public class Door : MapSite
    {
        public Door(Room r1, Room r2)
        {
            _room1 = r1;
            _room2 = r2;
        }
        public override void Enter()
        {

        }

        private Room _room1;
        private Room _room2;
        private bool _isOpen;
    }

    public class DoorNeedingSpell : Door
    {
        public DoorNeedingSpell(Room r1, Room r2) : base(r1, r2)
        {
        }
    }

    public enum Direction { North, South, East, West };
}
