namespace Epam.Task7.PL.ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                ServantClass.ShowMenu();
                string option = ServantClass.ReadInput();

                switch (option)
                {
                    case ServantClass.ShowUsers:
                        WorkWithUsers.ShowUsers();
                        ServantClass.PressAnyKey();
                        break;
                    case ServantClass.GetUser:
                        WorkWithUsers.GetUserById();
                        ServantClass.PressAnyKey();
                        break;
                    case ServantClass.AddUser:
                        WorkWithUsers.AddUser();
                        ServantClass.PressAnyKey();
                        break;
                    case ServantClass.UpdateUser:
                        WorkWithUsers.UpdateUser();
                        ServantClass.PressAnyKey();
                        break;
                    case ServantClass.RemoveUser:
                        WorkWithUsers.RemoveUser();
                        ServantClass.PressAnyKey();
                        break;
                    case ServantClass.ShowAwards:
                        WorkWithAwards.ShowAwards();
                        ServantClass.PressAnyKey();
                        break;
                    case ServantClass.AddAward:
                        WorkWithAwards.AddAward();
                        ServantClass.PressAnyKey();
                        break;
                    case ServantClass.GiveAward:
                        WorkWithAwards.GiveToUser();
                        ServantClass.PressAnyKey();
                        break;
                    case ServantClass.TakeAward:
                        WorkWithAwards.TakeFromUser();
                        ServantClass.PressAnyKey();
                        break;
                    case ServantClass.Quit:
                        return;
                    default:
                        break;
                }
            }
        }
    }
}