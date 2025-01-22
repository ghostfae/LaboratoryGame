

namespace LaboratoryGame
{
   internal class EndScreen : IRoom
   {
      private bool _hasEnded;
      private readonly List<RoomAction> _actionList;

      public EndScreen()
      {
         _actionList =
            [
               new RoomAction("Leave for good.", OnExit),
         ];
      }

      private void OnExit(RoomAction action, GameContext context)
      {
         Environment.Exit(0);
      }

      public void Describe()
      {
         if (!_hasEnded)
         {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("After what seems to be an age, you reach the bottom.");
            Console.WriteLine("You find yourself in a sewer.");
            Console.WriteLine("Why is this connected to the laboratory?");
            Console.WriteLine("What was that laboratory for?");
            Console.WriteLine("How did you find yourself here?");
            Console.WriteLine();
            Console.WriteLine("All of these questions flood your mind.");
            Console.WriteLine("But you don't focus on them anymore.");
            Console.WriteLine();
            Console.WriteLine("Somehow, you know you're out of there.");
            Console.WriteLine("There is still a sewer to traverse, but you will get out.");
            Console.WriteLine("The laboratory will not claim you.");
            Console.WriteLine("You will get out.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("There is only one thing to do.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("  Leave for good");
            _hasEnded = true;
         }
      }

      public IList<RoomAction> GetOptions()
      {
         return _actionList;
      }
   }
}