namespace LaboratoryGame;

internal interface IRoom 
{
   public void Describe();
   public IList<RoomAction> GetOptions();
   //public void OnProceed();
}

internal class EntranceHall : IRoom
{
   private bool _isDoorLocked = true;
   private bool _hasRoomBeenInvestigated = false;
   private bool _hasBookBeenPickedUp = false;
   private bool _canInputPasscode = false;

   private const string Passcode = "4321";
   private readonly List<RoomAction> _actionList;



   public EntranceHall()
   {
      _actionList =
      [
         new RoomAction("Look around", OnLookAround),
         new RoomAction("Attempt to open the door", OnAttemptDoor),
         new RoomAction("Attempt to escape", OnAttemptEscape)
      ];
   }

   public void Describe()
   {
      if (!_hasRoomBeenInvestigated)
      {
         Console.WriteLine("You find yourself in an entrance hall.");
         Console.WriteLine("The only light is flickering, it is hard to make much out.");
         Console.WriteLine("There is a door with a dimly illuminated keypad.");

      }
      else
      {
         Console.WriteLine("The dimly lit room is still flickering.");
         Console.WriteLine("There is a shattered lamp on the floor.");
         if (!_hasBookBeenPickedUp)
         {
            Console.WriteLine("A small, rotting end table has a notebook on it.");
         }
         else
         {
            Console.WriteLine("A small, rotting end table is falling apart.");
         }
         Console.WriteLine("The door with a keypad still stands closed.");
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
      if (!_hasRoomBeenInvestigated)
      {
         _actionList.Add(new RoomAction("Pick up notebook", OnBookPickup));
         _hasRoomBeenInvestigated = true;
      }
      else 
      {
         Console.WriteLine("Nothing has changed.");
         Console.WriteLine();
         _actionList.Remove(action);
      }
   }
   private void OnAttemptDoor(RoomAction _, GameContext __)
   {
      if (_isDoorLocked)
      {
         if (!_hasBookBeenPickedUp)
         {
            Console.WriteLine("The door is shut fast. Maybe you need a passcode?");
            Console.WriteLine();
         }
         else
         {
            Console.WriteLine("The door is shut fast. You have an idea of the passcode.");
            Console.WriteLine();
            if (!_canInputPasscode)
            {
               _canInputPasscode = true;
               _actionList.Insert(0, new RoomAction("Input a passcode", OnEnterPasscode));
            }
         }
      }

      else 
      {
         Console.WriteLine("The door opens with a clang. You may proceed onwards.");
         Console.WriteLine();

         _actionList.Clear();
         _actionList.Add(new RoomAction("Proceed forwards", OnProceed));
      }
   }

   private void OnAttemptEscape(RoomAction action, GameContext _) 
   {
      Console.WriteLine("There is nowhere to go.");
      Console.WriteLine();

      _actionList.Remove(action);
   }

   private void OnBookPickup(RoomAction action, GameContext _) 
   {
      Console.WriteLine("You have picked up the notebook.");
      Console.WriteLine($"It has the numbers '{Passcode}' on the backside of the front cover.");
      Console.WriteLine("You don't see anything else of use.");
      Console.WriteLine();

      _hasBookBeenPickedUp = true;

      _actionList.Add(new RoomAction("View notebook", OnViewNotebook));

      _actionList.Remove(action);
   }

   private void OnViewNotebook(RoomAction _, GameContext __)
   {
      Console.WriteLine($"The numbers '{Passcode}' are scribbled into the front cover.");
      Console.WriteLine("Otherwise, the notebook has diagrams and notes you can't read.");
      Console.WriteLine();
   }

   private void OnEnterPasscode(RoomAction action, GameContext _) 
   {
      while (true) 
      {
         Console.WriteLine("Please enter the 4 digit passcode.");
         Console.WriteLine("If you need more time, type the single number '9'.");
         var userInput = Console.ReadLine();
         Console.WriteLine();

         if(userInput == Passcode) 
         {
            _isDoorLocked = false;
            _actionList.Remove(action);
            Console.WriteLine("The keypad light goes green.");
            Console.WriteLine();
            break;
         }
         if (userInput == "9") 
         {
            break;
         }
         else 
         {
            Console.WriteLine("That is not correct.");
            Console.WriteLine();
         }
      }
   }

   private void OnProceed(RoomAction action, GameContext context) 
   {
      Console.WriteLine("And so you move through the door.");
      context.CurrentRoom = new Laboratory();
   
   }
}