using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmaSquadsDLLTest
{
    class Program
    {
        static string TOKEN = "Ob0g5pIQCcadilIzimWoko8Ht5cjFhfemBoEs8vhun1xeS76o7";

        static void Main(string[] args)
        {
            Console.WriteLine("*********************************************************");
            Console.WriteLine("***             ARMA SQUADS DLL TESTER                ***");
            Console.WriteLine("*********************************************************");
            if (TOKEN != null)
            {
                Console.WriteLine("Token Found >> " + TOKEN);
                //squadInstance = new ArmaSquads();
            }
            ////////////////////////////////////////////////////////////////////////////////
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Make a choice:");
            Console.WriteLine("\t1. Get Squad IDs");
            Console.WriteLine("\t2. Update Member");
            Console.WriteLine("\t3. Add Member");
            Console.WriteLine("\t4. Delete Member");
            string choice = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            HandleChoice(choice);
            ////////////////////////////////////////////////////////////////////////////////
            Console.ReadKey();
        }


        static void HandleChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    GetSquadIDs();
                    break;
                case "2":
                    UpdateMember();
                    break;
                case "3":
                    AddMember();
                    break;
                case "4":
                    DeleteMember();
                    break;
                default:
                    break;
            }
        }
        static async void GetSquadIDs()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("<< Getting Squad IDs...");
            List<Team> teamList = await ArmaSquads.GetSquadIDAsync(TOKEN);
            Console.ForegroundColor = ConsoleColor.Green;
            foreach(Team t in teamList)
                Console.WriteLine("   >> " + t.ID + " -- " + t.Name);
            Console.ForegroundColor = ConsoleColor.White;
        }
        static async void UpdateMember()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.Write("Select the squad ID: ");
            string squadID = Console.ReadLine();
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("Available members:");
            List<SquadMember> membersList = await ArmaSquads.GetSquadMembersAsync(TOKEN, squadID);
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (SquadMember m in membersList)
                Console.WriteLine(m.UUID + " -- " + m.Username);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("----------------------------------------------------------");
            Console.Write("Select the member UUID to update: ");
            string uuid = Console.ReadLine();
            Console.Write("Set new username: ");
            string newUsername = Console.ReadLine();
            Console.Write("Set new UUID: ");
            string newUUID = Console.ReadLine(); if (newUUID == "") newUUID = uuid;
            Console.Write("Set new name: ");
            string newName = Console.ReadLine();
            Console.Write("Set new eMail: ");
            string newMail = Console.ReadLine();
            Console.Write("Set new ICQ: ");
            string newIcq = Console.ReadLine();
            Console.Write("Set new Remark: ");
            string newRemark = Console.ReadLine();
            Console.WriteLine("<< Sending update...");
            string result = await ArmaSquads.UpdateMemberAsync(newUsername, newUUID, newName, newMail, newIcq, newRemark, squadID, TOKEN);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  >> " + result.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void AddMember()
        {
            Console.WriteLine("----------------------------------------------------------");
        }
        static void DeleteMember()
        {
            Console.WriteLine("----------------------------------------------------------");
        }

    }
}
