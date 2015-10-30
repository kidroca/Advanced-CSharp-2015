namespace SoftUni.Homeworks.AdvancedCSharp.Multidimetional
{
    using System;

    /// <summary>
    /// This problem is from the JavaScript Basics Exam (4 September 2014). You may check 
    /// your solution here. https://judge.softuni.bg/Contests/Practice/Index/31
    /// 
    /// The year is 2185 and the SSR Normandy spaceship explores our galaxy. Unfortunately,
    /// the ship suffered severe damage in the last battle with Batarian pirates, and her
    /// navigation system is broken. Your task is to write a JavaScript program to help the
    /// Normandy safely navigate through the stars back home.
    /// 
    /// The navigation field is a 2D grid. You are given the names of 3 star systems, along
    /// with their coordinates(sx, sy) and the Normandy's initial coordinates(nx, ny).
    /// Assume that a star's coordinates are in the center of a 2x2 rectangle. The Normandy
    /// always moves in an upwards direction, 1 unit every turn. Your task is to inform the
    /// Normandy of its current location during its movement.
    /// </summary>
    class ToTheStarts
    {
        static void Main()
        {
            GameData gameData = ReadInputLines();

            for (int j = 0; j <= gameData.Turns; j++)
            {
                var currentCoordinates = new SpaceObject
                {
                    X = gameData.Spaceship.X,
                    Y = gameData.Spaceship.Y
                };

                bool inSpace = true;

                foreach (var star in gameData.Stars)
                {
                    if (star.X - 1 <= currentCoordinates.X &&
                        currentCoordinates.X <= star.X + 1 &&
                        star.Y - 1 <= currentCoordinates.Y &&
                        currentCoordinates.Y <= star.Y + 1)
                    {
                        inSpace = false;
                        Console.WriteLine(star.Name);

                        break;
                    }
                }

                if (inSpace)
                {
                    Console.WriteLine("space");
                }

                gameData.Spaceship.Y += 1;
            }
        }

        static GameData ReadInputLines()
        {
            var gameData = new GameData();

            var stars = new SpaceObject[3];

            for (int i = 0; i < 3; i++)
            {
                string[] data = Console
                    .ReadLine()
                    .Split(' ');

                stars[i] = new SpaceObject
                {
                    Name = data[0].ToLower(),
                    X = float.Parse(data[1]),
                    Y = float.Parse(data[2])
                };
            }

            gameData.Stars = stars;

            string[] normandyData = Console
                .ReadLine()
                .Split(' ');

            gameData.Spaceship = new SpaceObject()
            {
                Name = "normandy",
                X = float.Parse(normandyData[0]),
                Y = float.Parse(normandyData[1])
            };

            gameData.Turns = int.Parse(Console.ReadLine());

            return gameData;
        }
    }

    class GameData
    {
        public int Turns { get; set; }

        public SpaceObject Spaceship { get; set; }

        public SpaceObject[] Stars { get; set; }
    }

    class SpaceObject
    {
        public string Name { get; set; }

        public float X { get; set; }

        public float Y { get; set; }
    }
}



