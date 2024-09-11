namespace Homework_5._1
{
    public class Program
    {
        private static DatabaseService databaseService;
        static void Main()
        {
            databaseService = new DatabaseService();
            databaseService.EnsurePopulated();

            //databaseService.EditEvent(3, 2);
            //databaseService.GetGuestsFromEvent(2);
            //databaseService.EditGuestRoleToVip(1, 1);
            //databaseService.GetEventsFromGuest(1);
            //databaseService.RemoveGuest(1, 1);
            databaseService.PrintEventsWhereGuestWasSpeaker(2);
        }
    }

}
