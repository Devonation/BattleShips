using System.Threading.Channels;

internal class Program
{
    private static CustomArray<Spaceship> fleet = new(10);
    private static Map<int> map = new(5, 5);

    public static void Main(string[] args)
    {
        MainMenu();   
    }

    public static void MainMenu()
    {
        Console.Clear();
        Console.WriteLine("""
            1. Ship Menu
            2. View Map
            """);
        switch (Console.ReadLine())
        {
            case "1": ShipScreen(); break;
            case "2": MapScreen(); break;
                default: Console.WriteLine("Invalid Input. Try again..."); break;
        }
    }

    public static void ShipScreen()
    {
        Console.Clear();
        Console.WriteLine("""
            1. Create Ship
            2. Delete Ship
            3. Find Ship
            4. Back
            """);

        switch (Console.ReadLine() )
        {
            case "1": CreateShipScreen(); break;
            case "2": DeleteShipScreen(); break;
            case "3": FindShipScreen(); break;
            case "4": MainMenu(); break;
            default: Console.WriteLine("Invalid input. Try again..."); break;
        }
    }

    public static void CreateShipScreen()
    {
        Console.Clear();
        Console.WriteLine("""
            1. Name
            2. Model
            3. Crew Capacity
            4. Max Speed
            5. Status
            6. Launch Date
            7. Mission Type
            Type 'back' to return to previous screen
            
            """);
        
        Console.WriteLine("Name:");
        string name = Console.ReadLine();
        if (name == "back") ShipScreen();

        Console.WriteLine("Model:");
        string model = Console.ReadLine(); 
        if (model == "back") ShipScreen();

        Console.WriteLine("Crew Capacity:");
        string crewCap = Console.ReadLine();
        if (crewCap == "back") ShipScreen();

        Console.WriteLine("Max Speed:");
        string maxSpeed = Console.ReadLine();
        if (maxSpeed == "back") ShipScreen();

        Console.WriteLine("Status:");
        string status = Console.ReadLine();
        if (status == "back") ShipScreen();

        Console.WriteLine("Launch Date:");
        string launch = Console.ReadLine();
        if (launch == "back") ShipScreen();

        Console.WriteLine("Mission Type:");
        string type = Console.ReadLine();
        if (type == "back") ShipScreen();


        fleet.Add(new Spaceship(name, model, int.Parse(crewCap), double.Parse(maxSpeed), status, launch, type));
        ShipScreen();
    }

    public static void DeleteShipScreen()
    {
        Console.Clear();
    }

    public static void FindShipScreen()
    {
        Console.Clear();
    }

    public static void MapScreen()
    {
        Console.Clear();
        Console.WriteLine("""
            1. Place Ship
            2. Remove Ship
            3. Back
            """);
        Console.WriteLine(map.Display());
        Console.WriteLine("Input: ");

        switch (Console.ReadLine())
        {
            case "1": PlaceShipScreen(); break;
            case "2": PlaceShipScreen(); break;
            case "3": MapScreen(); break;
            default: Console.WriteLine("Invalid input. Try Again..."); break;
        }
    }

    public static void PlaceShipScreen()
    {
        Console.Clear();
        Console.WriteLine(map.Display());
        Console.WriteLine("\nRow: ");
        int row = Console.Read();
    }
}

class Spaceship
{
    public Spaceship(string name, string model, int crewCapacity, double maxSpeed, string status, string launchDate, string missionType)
    {
        Name = name;
        Model = model;
        CrewCapacity = crewCapacity;
        MaxSpeed = maxSpeed;
        Status = status;
        LaunchDate = launchDate;
        MissionType = missionType;
    }

    public string Name { get; set; }
    public string Model { get; set; }
    public int CrewCapacity { get; set; }
    public double MaxSpeed { get; set; }
    public string Status { get; set; }
    public string LaunchDate { get; set; }
    public string MissionType { get; set; }

    public override string ToString()
    {
        return $"{Name}, {Model}, {CrewCapacity}, {MaxSpeed}, {Status}, {LaunchDate}, {MissionType}";
    }
}

class Map<T>
{
    private T[,] mapArray;

    public Map(int rows, int columns)
    {
        mapArray = new T[rows, columns];
    }

    public void Place(int row, int column, T item)
    {
        mapArray[row, column] = item;
    }
    public void RemoveAt(int row, int column)
    {
        mapArray[row, column] = default(T);
    }
    public string Display()
    {
        string output = "\n";
        for (int i = 0; i < mapArray.GetLength(0); i++)
        {
            for (int j = 0; j < mapArray.GetLength(1); j++)
            {
                if (!mapArray[i, j].Equals(default(T)))
                { 
                    output += " X ";
                }
                else
                {
                    output += " - ";
                }
            }
            output += "\n";
        }
        return output;
    }
}

class CustomArray<T>
{
    private T[] innerArray;

    public CustomArray(int size)
    {
        innerArray = new T[size];
    }

    //Add element to array
    public void Add(T item)
    {
        bool isFull = true;
        for (int i = 0; i < innerArray.Length; i++)
        {
            if (innerArray[i] == null)
            {
                innerArray[i] = item;
                isFull = false;
                break;
            }
        }
        if (isFull)
        {
            AddResize(item);
        }
    }

    //Resize array if too many elements are added
    public void AddResize(T item)
    {
        T[] largerArray = new T[innerArray.Length + 1];
        largerArray = innerArray;
        largerArray[^1] = item;
        innerArray = largerArray;
    }

    //Removes first element that is found
    public void RemoveAt(int index)
    {
        for (int i = 0; i < innerArray.Length;i++)
        {
            if (innerArray[i].Equals(innerArray[index]))
            {
                T[] smallerArray = new T[innerArray.Length - 1];
                int count = 0;

                for (int j = 0; j < innerArray.Length; j++)
                {
                    smallerArray[j] = innerArray[count];

                    if (i != j)
                    {
                        count++;
                    }
                }
                break;
            }
        }
    }

    //Finds the index of the first element meeting the conditions
    public int FindIndex(T item)
    {
        for (int i = 0; i < innerArray.Length; i++)
        {
            if (innerArray[i].Equals(item)) return i;
        }
        return -1;
    }

    //Displays elements of array
    public string ToString()
    {
        string output = "";
        foreach (T item in innerArray)
        {
            output += item.ToString() + "\n";
        }
        return output;
    }
}