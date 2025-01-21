namespace LaboratoryGame;

public enum GameCondition 
{
   Playing,
   Win,
   Loss
}
internal class GameContext(IRoom startingRoom)
{
   public GameCondition Condition { get; set; } = GameCondition.Playing;
   public IRoom CurrentRoom { get; set; } = startingRoom;
}

internal class Program
{
   static void Main(string[] args)
   {
      var context = new GameContext(new EntranceHall());

      while (true)
      {
         context.CurrentRoom.Describe();
         var selectedAction = SelectOptions(context.CurrentRoom);
         Console.WriteLine();
         selectedAction.PerformAction(context);
      }
   }

   private static RoomAction SelectOptions(IRoom room)
   {
      var actionList = room.GetOptions();

      while (true)
      {
         Console.WriteLine("Type the number for the following options:");

         for (int i = 0; i < actionList.Count; i++)
         {
            Console.WriteLine($"{i + 1}. {actionList[i].Description}");
         }


         var userInput = Console.ReadLine();

         if (int.TryParse(userInput, out var result))
         {
            if (result >= 1 && result <= actionList.Count)
            {
               return actionList[result - 1];
            }
         }
      }
   }
}
