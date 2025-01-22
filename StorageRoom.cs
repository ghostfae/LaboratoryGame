

namespace LaboratoryGame;

internal class StorageRoom : IRoom
{
   private bool _hasRoomBeenInvestigated = false;
   private bool _hasCrowbar;
   private bool _buttonPressed;
   private bool _hatchVisible;
   private bool _boxesGone;

   private readonly List<RoomAction> _actionList;
   public StorageRoom()
   {
      _actionList =
         [
            new RoomAction("Look around", OnLookAround),
         ];
   }

   public void Describe()
   {
      if (!_hasRoomBeenInvestigated)
      {
         Console.WriteLine("You're in a storage room.");
         Console.WriteLine("Crates upon crates are stacked all around the colourless room.");
         Console.WriteLine("Something glints in the dim light.");
         Console.WriteLine();
      }
      else 
      {
         Console.WriteLine("It really is a mess in here.");
         Console.WriteLine();
      }
   }

   public IList<RoomAction> GetOptions()
   {
      return _actionList;
   }

   private void OnLookAround(RoomAction action, GameContext _)
   {
      Console.WriteLine("Upon closer inspection, the glint seems to be a crowbar sitting on a crate.");
      Console.WriteLine("There's also a button on the wall.");
      Console.WriteLine("Maybe you can do something with the crates?");
      Console.WriteLine();

      _actionList.Remove(action);
      _actionList.Add(new RoomAction("Pick up crowbar", OnGetCrowbar));
      _actionList.Add(new RoomAction("Press button", OnPressButton));
      _actionList.Add(new RoomAction("Interact with crates", OnCrateInteraction));
      _hasRoomBeenInvestigated = true;
   }

   private void OnCrateInteraction(RoomAction action, GameContext __)
   {
      if (!_boxesGone)
      {
         Console.WriteLine("What would you like to do with the crates?");
         Console.WriteLine("1. Break them open");
         Console.WriteLine("2. Move them");
         Console.WriteLine("3. Nothing");

         var input = Console.ReadLine();
         Console.WriteLine();

         if(input == "1" || input == "break") 
         {
            OnBreakCrate(action);
         }
         if (input == "2" || input == "move") 
         {
            OnMoveCrate(action);
         }
         if (input == "3")
         {
            return;
         }
         else 
         {
            Console.WriteLine("Please select a valid option.");
            Console.WriteLine();
         }
      }
   }

   private void OnMoveCrate(RoomAction action)
   {
      if (_hasCrowbar) 
      {
         Console.WriteLine("With the crowbar as leverage, you manage to move the crates.");
         Console.WriteLine();
         if (_buttonPressed) 
         {
            _hatchVisible = true;
            _boxesGone = true;
            _actionList.Add(new RoomAction("Open the Hatch", OnHatchVisible));
         }
         _actionList.Remove(action);         
      }
      else 
      {
         Console.WriteLine("These are too heavy to move.");
         Console.WriteLine("Is there a nearby tool?");
         Console.WriteLine();
      }
   }

   private void OnBreakCrate(RoomAction action)
   {
      if (_hasCrowbar)
      {
         Console.WriteLine("With the crowbar as a weapon, you manage to break the crates.");
         Console.WriteLine();
         if (_buttonPressed)
         {
            _hatchVisible = true;
            _boxesGone = true;
            _actionList.Add(new RoomAction("Open the Hatch", OnHatchVisible));
         }
         _actionList.Remove(action);
      }
      else
      {
         Console.WriteLine("You have nothing to break these with.");
         Console.WriteLine();
      }
   }

   private void OnHatchVisible(RoomAction action, GameContext context)
   {
      Console.WriteLine("You open the hatch.");
      Console.WriteLine("There seems to be a ladder down below.");
      Console.WriteLine("Against your better judgement, you go down the ladder.");
      Console.WriteLine();
      context.CurrentRoom = new EndScreen();
   }

   private void OnPressButton(RoomAction action, GameContext _)
   {
      Console.WriteLine("You press the button.");
      Console.WriteLine();

      if (_boxesGone)
      {
         Console.WriteLine("On the newly cleared floor, you see a hatch.");
         Console.WriteLine();
         _buttonPressed = true;
         _actionList.Add(new RoomAction("Open the Hatch", OnHatchVisible));
      }

      else
      {
         Console.WriteLine("Nothing seems to happen.");
         _buttonPressed = true;
         Console.WriteLine();
         
      }
      _actionList.Remove(action);
   }

   private void OnGetCrowbar(RoomAction action, GameContext _)
   {
      Console.WriteLine("You pick up the crowbar.");
      Console.WriteLine("It is red and rusted.");
      _hasCrowbar = true;
      Console.WriteLine();
      _actionList.Remove(action);
   }
}