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
                    case "1":
                        WorkWithUsers.ShowUsers();
                        ServantClass.PressAnyKey();
                        break;
                    case "2":
                        WorkWithUsers.GetUserById();
                        ServantClass.PressAnyKey();
                        break;
                    case "3":
                        WorkWithUsers.AddUser();
                        ServantClass.PressAnyKey();
                        break;
                    case "4":
                        WorkWithUsers.UpdateUser();
                        ServantClass.PressAnyKey();
                        break;
                    case "5":
                        WorkWithUsers.RemoveUser();
                        ServantClass.PressAnyKey();
                        break;
                    case "6":
                        WorkWithAwards.ShowAwards();
                        ServantClass.PressAnyKey();
                        break;
                    case "7":
                        WorkWithAwards.AddAward();
                        ServantClass.PressAnyKey();
                        break;
                    case "8":
                        WorkWithAwards.GiveToUser();
                        ServantClass.PressAnyKey();
                        break;
                    case "9":
                        WorkWithAwards.TakeFromUser();
                        ServantClass.PressAnyKey();
                        break;
                    case "q":
                        return;
                    default:
                        break;
                }
            }
        }
    }
}