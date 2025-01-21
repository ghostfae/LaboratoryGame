namespace LaboratoryGame;

internal class RoomAction 
{
   public string Description;

   private Action<RoomAction, GameContext> _action;

   public RoomAction(string description, Action<RoomAction, GameContext> action)
   {
      Description = description;
      _action = action;
   }

   public void PerformAction(GameContext context) 
   {
      _action.Invoke(this, context);
   }
}