namespace LaboratoryGame;

internal class RoomAction(string description, Action<RoomAction, GameContext> action)
{
   public string Description = description;

   public void PerformAction(GameContext context) 
   {
      action.Invoke(this, context);
   }
}