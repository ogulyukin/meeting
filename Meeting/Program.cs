using System;
using System.Collections.Generic;
using System.Threading;

namespace Meeting
{
    class Program
    {
        public static void TimerMethod(object a)
        {
            var now = DateTime.Now;
            foreach (var meet in (List<Meet>)a)
            {
                if (meet.remindTime.Date == now.Date && meet.remindTime.Hour == now.Hour &&
                    meet.remindTime.Minute == now.Minute)
                    Console.WriteLine($"Meet reminder: {meet.information} begin at: {meet.beginTime}");
            }

            //Console.WriteLine("Reminder cheking done."); //was added for debug purpose
        }

        static void Main(string[] args)
        {
            var metingList = new List<Meet>();
            var metingManager = new MeetingManager(metingList);
            int userChoise = 100;
            TimerCallback callback = new TimerCallback(TimerMethod);
            Timer timer = new(callback, metingList, 0 , 2000);
            timer.Change(1000, 30000); 
            while (userChoise != 0)
            {
                UserUI.PrintMenu();
                do
                {
                    userChoise = UserUI.GetUserChoise();
                } while (userChoise < 0);
                Console.Clear();
                Console.WriteLine($"Totale meetings in base: {metingManager.GetMeetingCount()}");
                Console.WriteLine($"You choose {userChoise}");
                switch(userChoise)
                {
                    case 1:
                        UserUI.ShowMeet(metingManager.GetCurrentMeet());
                        break;
                    case2:
                        metingManager.DeleteCurrentMeet();
                        break;
                    case 3:
                        UserUI.ShowMeet(metingManager.GetCurrentMeet());
                        metingManager.ChangeMeet(UserUI.GetNewMeet());
                        break;
                    case 4:
                        metingManager.NextMeet();
                        UserUI.ShowMeet(metingManager.GetCurrentMeet());
                        break;
                    case 5:
                        metingManager.AddMeeting(UserUI.GetNewMeet());
                        break;
                    case 6:
                        UserUI.ShowMeetsList(metingManager.GetDayMeets(UserUI.GetDayFromUser()));
                        break;
                    case 7:
                        _ = FileWorks.WrightReport(metingManager.GetDayMeets(UserUI.GetDayFromUser()));
                        break;
                }
            }
        }
    }
}
