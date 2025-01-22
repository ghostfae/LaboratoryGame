
namespace LaboratoryGame;

internal class Laboratory : IRoom
{
   private bool _hasRoomBeenInvestigated = false;
   private bool _isTerminalLocked = true;
   private bool _isCabinetLocked = true;
   private bool _hasKeycard = false;

   private const string Password = "1305";
   private readonly List<RoomAction> _actionList;
   public Laboratory()
   {
      _actionList =
         [
            new RoomAction("Look around", OnLookAround),
         ];
   }

   public void Describe()
   {
      if(!_hasRoomBeenInvestigated)
      {
         Console.WriteLine("Adjusting your eyes to a sudden brightness, you seem to be in a laboratory.");
         Console.WriteLine("White walls reflect the surgical light above.");
         Console.WriteLine();
      }
      else
      {
         if (!_isCabinetLocked && !_hasKeycard)
         {
            Console.WriteLine("One of the cabinets has a green light coming from its lock.");
         }
         Console.WriteLine("It is still blinding in here.");
         Console.WriteLine();
      }
      Console.WriteLine("What would you like to do?");
      Console.WriteLine();
   }

   public IList<RoomAction> GetOptions()
   {
      return _actionList;
   }

   private void OnLookAround(RoomAction action, GameContext _)
   {
      Console.WriteLine("There are locked glass cabinets, with test tubes and apparatus inside.");
      Console.WriteLine("There is a computer terminal with a lockscreen.");
      Console.WriteLine("Various chemistry posters line the walls.");
      Console.WriteLine("It is hidden, but there seems to be a metallic sliding door at the back.");
      Console.WriteLine();

      if (!_hasRoomBeenInvestigated)
      {

         _actionList.Insert(0, new RoomAction("Look at the posters.", OnViewPosters));
         _actionList.Insert(1, new RoomAction("Examine the cabinets.", OnViewCabinets));
         _actionList.Insert(2, new RoomAction("Examine the terminal.", OnViewTerminal));
         _actionList.Insert(3, new RoomAction("Attempt to open the door", OnAttemptDoor));
         _hasRoomBeenInvestigated = true;
      }
   }

   private void OnAttemptDoor(RoomAction _, GameContext __)
   {
      if (_hasKeycard)
      {
         Console.WriteLine("It doesn't budge.");
         Console.WriteLine("There's a keycard scanner next to it. Perhaps there's a clue.");
         Console.WriteLine();
      }
      else 
      {
         Console.WriteLine("You swipe the keycard.");
         Console.WriteLine("After a beep, the door slides open.");
         Console.WriteLine();
         _actionList.Clear();

         new RoomAction("Leave the laboratory", OnExit);
      }
   }

   private void OnViewPosters(RoomAction _, GameContext __)
   {
      Console.WriteLine("There are two posters and a calendar.");
      Console.WriteLine("One poster is of the periodic table.");
      Console.WriteLine("Another has pharmaceuticals listed on it.");
      Console.WriteLine("The calendar is open on May.");
      Console.WriteLine("A date is circled: Friday the 13th.");
      Console.WriteLine("Writing on it reads, 'E's birthday'.");
      Console.WriteLine();
   }
   private void OnViewCabinets(RoomAction action, GameContext __)
   {
      if (_isCabinetLocked)
      {
         Console.WriteLine("The cabinets have electric locks on them.");
         Console.WriteLine("Most of them seem out of power, but one has a glowing red light.");
         Console.WriteLine("Maybe you can access it, somehow?");
         Console.WriteLine();
      }
      else
      {
         Console.WriteLine("There's a pocket scale, a test tube rack, a metal spoon, and a keycard.");
         Console.WriteLine("You decide the keycard is the most useful item on the shelf.");
         Console.WriteLine("You pocket it, and close the cabinet.");
         Console.WriteLine();

         _actionList.Remove(action);
      }
   }
   private void OnViewTerminal(RoomAction _, GameContext __)
   {
      if (_isTerminalLocked)
      {
         while (true)
         {
            Console.WriteLine("An old computer sits with a login page.");
            Console.WriteLine("The username is 'Eden Keyes'.");
            Console.WriteLine("Would you like to input a password?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            var input = Console.ReadLine();
            Console.WriteLine();

            if (input == "1" || input == "yes") 
            {
               OnEnterPasscode(_, __);
               break;
            }
            if (input == "2" || input == "no") 
            {
               break;
            }
            else 
            {
               Console.WriteLine("Please select a valid option.");
               Console.WriteLine();
            }
         }
      }
      else
      {
         while (true)
         {
            Console.WriteLine("Welcome to the [SCIENCE INC] Database.");
            Console.WriteLine("Please choose one of the following options:");
            Console.WriteLine("1. Email Access");
            Console.WriteLine("2. Cabinet Control");
            Console.WriteLine("3. Exit");

            var input = Console.ReadLine();
            Console.WriteLine();

            if (input == "1" || input == "email")
            {
               OnEmailAccess(_, __);
               break;
            }
            if (input == "2" || input == "cabinet") 
            {
               OnTerminalControl(_, __);
               break;
            }
            if (input == "3" || input == "exit") 
            {
               break;
            }
            else 
            {
               Console.WriteLine("Please select a valid option.");
               Console.WriteLine();
            }
         }
      }
   }

   private void OnEnterPasscode(RoomAction _, GameContext __) 
   {
      Console.WriteLine("Enter your password:");
      var input = Console.ReadLine();
      Console.WriteLine();

      if (input == Password) 
      {
         _isTerminalLocked = false;
         Console.WriteLine("Logging you in, EDEN...");
         Console.WriteLine();
         OnViewTerminal(_, __);
      }
      else 
      {
         Console.WriteLine("Incorrect password.");
      }
   }

   private void OnTerminalControl(RoomAction _, GameContext __)
   {
      while (true)
      {
         var cabinetStatus = "Locked";
         if (!_isCabinetLocked) 
         {
            cabinetStatus = "Open"; 
         }

         Console.WriteLine("Cabinet 1: ERROR");
         Console.WriteLine("Cabinet 2: ERROR");
         Console.WriteLine($"Cabinet 3: {cabinetStatus}");
         Console.WriteLine("Cabinet 4: ERROR");
         Console.WriteLine();
         Console.WriteLine("Please select a Cabinet to access.");
         Console.WriteLine("Or, type '9' to escape.");
         var input = Console.ReadLine();
         Console.WriteLine();
         if (input == "1" || input == "2" || input == "4") 
         {
            Console.WriteLine($"Cabinet {input} cannot be reached.");
         }
         if (input == "3")
         {
            while (true)
            {
               if (_isCabinetLocked)
               {
                  Console.WriteLine("Would you like to unlock this cabinet?");
                  Console.WriteLine("1. Yes");
                  Console.WriteLine("2. No");
                  input = Console.ReadLine();
                  Console.WriteLine();
                  if (input == "1")
                  {
                     Console.WriteLine("Unlocking...");
                     _isCabinetLocked = false;
                     Console.WriteLine();
                     break;
                  }
                  if (input == "2")
                  {
                     break;
                  }
                  else
                  {
                     Console.WriteLine("Please select a valid option.");
                     Console.WriteLine();
                  }
               }
               else
               {
                  Console.WriteLine("Cabinet is unlocked.");
                  Console.WriteLine();
                  break;
               }
            }
         }
         if (input == "9") 
         {
            OnViewTerminal(_, __);
            break;
         }
         else 
         {
            Console.WriteLine("Please select a valid option.");
            Console.WriteLine();
         }
      }
   }

   private void OnEmailAccess(RoomAction _, GameContext __)
   {
      Console.WriteLine("You have no new emails.");
      Console.WriteLine();
      OnViewTerminal(_, __);
   }

   private void OnExit(RoomAction _, GameContext context) 
   {
      Console.WriteLine("As you walk through the door, you wonder.");
      Console.WriteLine("All of these rooms seem disconnected.");
      Console.WriteLine("Strange.");
      Console.WriteLine();
      context.CurrentRoom = new StorageRoom();
   }
}