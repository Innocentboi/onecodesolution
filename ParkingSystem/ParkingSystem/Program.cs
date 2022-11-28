using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingSystem
{
    class Program
    {
        private IDictionary<int, Vehicle> Vehicles = new Dictionary<int, Vehicle>();
        static void Main(string[] args)
        {
            Program program = new Program();
            program.ParkingSystem();
        }
        public void ParkingSystem()
        {
            while(true)
            {
                Console.WriteLine("Welcome to Fidelis Parking Lot!");
                Console.WriteLine("Please input your command below!\n");
                string input = null;
                input = Console.ReadLine();
                string[] inputs = input.Split(" ");
                string valMsg = Validations(inputs);
                if (valMsg != "")
                {
                    Console.WriteLine(valMsg);
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
                switch (inputs[0])
                {
                    case "create_parking_lot":
                        int currLot = Vehicles.Count;
                        for (int a = currLot + 1; a <= currLot + int.Parse(inputs[1]); a++) Vehicles.Add(a,null);
                        Console.WriteLine("Created a parking lot with " + inputs[1] +  " slots");
                        break;
                    case "park":
                        DateTime entryTime = DateTime.Now;
                        int slotNumber = 0;
                        for(int a=1;a<=Vehicles.Count;a++)
                        {
                            if (Vehicles[a] == null)
                            {
                                slotNumber = a;
                                break;
                            }
                        }
                        if(slotNumber==0)
                        {
                            Console.WriteLine("Sorry, parking lot is full");
                            break;
                        }
                        Vehicle vehicle = new Vehicle(inputs[1],inputs[3],inputs[2], entryTime, 1);
                        Vehicles[slotNumber] = vehicle;
                        Console.WriteLine("Allocated slot number: "+slotNumber);
                        break;
                    case "leave":
                        int leaveTime = (int) Math.Ceiling((DateTime.Now - Vehicles[int.Parse(inputs[1])].EntryTime).TotalHours);
                        Vehicles[int.Parse(inputs[1])] = null;
                        Console.WriteLine("Slot number " + inputs[1] + " is free");
                        break;
                    case "status":
                        Console.WriteLine("Slot\t\t" + "No.\t\t" + "Type\t\t" + "Registration No Colour");
                        for(int a=1;a<=Vehicles.Count;a++)
                        {
                            if (Vehicles[a] == null) continue;
                            Console.WriteLine(a + "\t\t" + Vehicles[a].No + "\t" + Vehicles[a].Type + "\t\t" + Vehicles[a].Colour);
                        }
                        break;
                    case "type_of_vehicles":
                        Console.WriteLine(Vehicles.Values.Where(x=> x!=null && x.Type==inputs[1]).Count());
                        break;
                    case "registration_numbers_for_vehicles_with_ood_plate":
                        Console.WriteLine(string.Join(", ", Vehicles.Values.Where(x => x != null && int.Parse(x.No.Split('-')[1]) % 2 != 0).Select(x => x.No).ToArray()));
                        break;
                    case "registration_numbers_for_vehicles_with_even_plate":
                        Console.WriteLine(string.Join(", ", Vehicles.Values.Where(x => x != null && int.Parse(x.No.Split('-')[1]) % 2 == 0).Select(x => x.No).ToArray()));
                        break;
                    case "registration_numbers_for_vehicles_with_colour":
                        Console.WriteLine(string.Join(", ", Vehicles.Values.Where(x => x != null && x.Colour==inputs[1]).Select(x => x.No).ToArray()));
                        break;
                    case "slot_numbers_for_vehicles_with_colour":
                        Console.WriteLine(string.Join(", ", Vehicles.Where(x => x.Value != null && x.Value.Colour == inputs[1]).Select(x => x.Key).ToArray()));
                        break;
                    case "slot_number_for_registration_number":
                        string slotNum = Vehicles.Where(x => x.Value != null && x.Value.No == inputs[1]).Select(x => x.Key).FirstOrDefault().ToString();
                        if (slotNum == "0") slotNum = "Not found";
                        Console.WriteLine(slotNum);
                        break;
                    case "exit":
                        return;
                    default:
                        break;
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }

        }
        public string Validations(string[] inputs)
        {
            switch (inputs[0])
            {
                case "create_parking_lot":
                    if (inputs.Length != 2) return "Invalid input format!";
                    break;
                case "park":
                    if (inputs.Length != 4) return "Invalid input format!";
                    if (inputs[3] != "Mobil" && inputs[3] != "Motor") return "Only Mobil and Motor are allowed!";
                    break;
                case "leave":
                    if (inputs.Length != 2) return "Invalid input format!";
                    if (!int.TryParse(inputs[1],out int result)) return "Invalid input format for slot number!";
                    break;
                case "status":
                    if (inputs.Length != 1) return "Invalid input format!";
                    break;
                case "type_of_vehicles":
                    if (inputs.Length != 2) return "Invalid input format!";
                    break;
                case "registration_numbers_for_vehicles_with_ood_plate":
                    if (inputs.Length != 1) return "Invalid input format!";
                    break;
                case "registration_numbers_for_vehicles_with_event_plate":
                    if (inputs.Length != 1) return "Invalid input format!";
                    return "Do you mean \"even\"?";
                case "registration_numbers_for_vehicles_with_even_plate":
                    if (inputs.Length != 1) return "Invalid input format!";
                    break;
                case "registration_numbers_for_vehicles_with_colour":
                    if (inputs.Length != 2) return "Invalid input format!";
                    break;
                case "slot_numbers_for_vehicles_with_colour":
                    if (inputs.Length != 2) return "Invalid input format!";
                    break;
                case "slot_number_for_registration_number":
                    if (inputs.Length != 2) return "Invalid input format!";
                    break;
                case "exit":
                    if (inputs.Length != 1) return "Invalid input format!";
                    break;
                default:
                    Console.WriteLine("\"" + inputs[0] + "\" is not recognized as a command.");
                    break;
            }
            return "";
        }
    }
}
