using Roommates.Models;
using Roommates.Repositories;
using System;
using System.Collections.Generic;
class Program
{
    //  This is the address of the database.
    //  We define it here as a constant since it will never change.
    private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";

    static void Main(string[] args)
    {
        RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);
        ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
        RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
        bool runProgram = true;
        while (runProgram)
        {
            string selection = GetMenuSelection();

            switch (selection)
            {
                case ("Show all rooms"):
                    List<Room> rooms = roomRepo.GetAll();
                    foreach (Room r in rooms)
                    {
                        Console.WriteLine($"{r.Name} has an Id of {r.Id} and a max occupancy of {r.MaxOccupancy}");
                    }
                    Console.Write("Press any key to continue");
                    Console.ReadKey();
                    break;
                case ("Search for room"):
                    Console.Write("Room Id: ");
                    int id = int.Parse(Console.ReadLine());

                    Room room = roomRepo.GetById(id);

                    Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                    Console.Write("Press any key to continue");
                    Console.ReadKey();
                    break;
                case ("Add a room"):
                    Console.WriteLine("What is the name of the room you would like to add?");
                    Console.Write(">");
                    string roomName = Console.ReadLine();
                    Console.WriteLine("What is the maximum occupancy?");
                    int occupancyMax = Int32.Parse(Console.ReadLine());
                    Room newRoom = new Room()
                    {
                        Name = roomName,
                        MaxOccupancy = occupancyMax,
                    };
                    roomRepo.Insert(newRoom);
                    Console.WriteLine("Great! A new room has been added");
                    break;
                case ("Show all roommates"):
                    List<Roommate> roommates = roommateRepo.GetAll();
                    foreach (Roommate roommate in roommates)
                    {
                        Console.WriteLine($"{roommate.FirstName} {roommate.LastName} moved into the {roommate.Room.Name} on {roommate.MovedInDate} and pays {roommate.RentPortion} percent of the rent.");
                    }
                    Console.Write("Press any key to continue");
                    Console.ReadKey();
                    break;
                case ("Show all chores"):
                    List<Chore> chores = choreRepo.GetAll();
                    foreach (Chore c in chores)
                    {
                        Console.WriteLine($"{c.Name} has an Id of {c.Id}.");
                    }
                    Console.Write("Press any key to continue.");
                    Console.ReadKey();
                    break;
                case ("Search for chore"):
                    Console.Write("Chore Id: ");
                    int choreId = Int32.Parse(Console.ReadLine());

                    Chore chore = choreRepo.GetById(choreId);

                    Console.WriteLine($"{chore.Id} - {chore.Name}.");
                    Console.Write("Press any key to continue");
                    Console.ReadKey();
                    break;
                case ("Delete a chore"):
                    Console.Write("Chore Id: ");
                    int choreToDelete = Int32.Parse(Console.ReadLine());
                    choreRepo.Delete(choreToDelete);
                    Console.Write("Chore successfully deleted.");
                    Console.Write("Press any key to continue.");
                    Console.ReadKey();
                    break;
                case ("Exit"):
                    runProgram = false;
                    break;
            }
        }

    }


    static string GetMenuSelection()
    {
        Console.Clear();

        List<string> options = new List<string>()
        {
            "Show all rooms",
            "Search for room",
            "Add a room",
            "Show all chores",
            "Search for chore",
            "Delete a chore",
            "Show all roommates",
            "Exit"
        };

        for (int i = 0; i < options.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }

        while (true)
        {
            try
            {
                Console.WriteLine();
                Console.Write("Select an option > ");

                string input = Console.ReadLine();
                int index = int.Parse(input) - 1;
                return options[index];
            }
            catch (Exception)
            {

                continue;
            }
        }

    }
}