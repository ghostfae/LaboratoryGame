namespace LaboratoryGame;

internal class Laboratory : IRoom
{
   private bool _hasRoomBeenInvestigated = false;
   private bool _isTerminalLocked = true;
   private bool _isCabinetLocked = true;
   private bool _isDoorLocked = true;


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
         if (!_isCabinetLocked)
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
      if (_isDoorLocked)
      {
      }
   }

   private void OnViewPosters(RoomAction _, GameContext __)
   {
      Console.WriteLine("There are three posters and a calendar.");
      Console.WriteLine("One is of the periodic table.");
      Console.WriteLine();
   }
   private void OnViewCabinets(RoomAction _, GameContext __)
   {
      if (_isCabinetLocked)
      {
      }
      else
      {
      }
   }
   private void OnViewTerminal(RoomAction _, GameContext __)
   {
      if (_isTerminalLocked)
      {
         // describe
         // would you like to 
      }
      else
      {
      }
   }
}