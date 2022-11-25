using System;
using System.Collections.Generic;

namespace Meeting
{
    public static class UserUI
    {
        public static void PrintMenu()
        {
            Console.WriteLine("***************************************");
            Console.WriteLine("*             Main menu               *");
            Console.WriteLine("***************************************");
            Console.WriteLine("1 - Show current meeting");
            Console.WriteLine("2 - Delete current meeting");
            Console.WriteLine("3 - Edit current meeting");
            Console.WriteLine("4 - Next meeting");
            Console.WriteLine("5 - Add new meeting");
            Console.WriteLine("6 - Show meetings at specific date");
            Console.WriteLine("7 - Fright to file meetings at specific date");
            Console.WriteLine("0 - Exit");
            Console.WriteLine();
        }

        public static int GetUserChoise()
        {
            var userInput = Console.ReadKey().KeyChar.ToString();
            Console.WriteLine($"Get from user: {userInput}");
            return ParseStringToInt(userInput);        
        }

        public static void ShowMeet(Meet meet)
        {
            if(meet.information == "Error")
            {
                Console.WriteLine("Error");
                return;
            }
            Console.WriteLine("***************************************");
            Console.WriteLine("*           Current meeting           *");
            Console.WriteLine("***************************************");
            PrintMeetDetails(meet);
        }

        public static void ShowMeetsList(List<Meet> meets)
        {
            Console.WriteLine("***************************************");
            Console.WriteLine("*           List of meetings          *");
            Console.WriteLine("***************************************");
            foreach(var meet in meets)
            {
                PrintMeetDetails(meet);
                Console.WriteLine();
            }
        }

        public static Meet GetNewMeet()
        {
            var result = new Meet();
            Console.WriteLine("***************************************");
            Console.WriteLine("*        Adding new meeting           *");
            Console.WriteLine("***************************************");
            Console.WriteLine("Wright information about the meeting:");
            result.information = Console.ReadLine();
            Console.WriteLine("Enter the begin date in format: year, month, day, hour, minute");
            var dateStr  = Console.ReadLine();
            var tempDateTime = StringToDateTime(dateStr);
            if(tempDateTime.Year == 0001)
            {
                result.information = "Error";
                return result;
            }
            if(tempDateTime < DateTime.Now)
            {
                Console.WriteLine("Begin of meeting can't be in pas!");
                result.information = "Error";
                return result;
            }
            result.beginTime = tempDateTime;

            Console.WriteLine("Enter the end date in format: year, month, day, hour, minute");
            dateStr = Console.ReadLine();
            tempDateTime = StringToDateTime(dateStr);
            if (tempDateTime.Year == 0001)
            {
                result.information = "Error";
                return result;
            }
            if(tempDateTime < result.beginTime)
            {
                Console.WriteLine("End date and time can not be less than bigin date and time!");
                result.information = "Error";
                return result;
            }

            result.endTime = tempDateTime;

            Console.WriteLine("Enter the remind date in format: year, month, day, hour, minute");
            dateStr = Console.ReadLine();
            tempDateTime = StringToDateTime(dateStr);
            if (tempDateTime.Year == 0001)
            {
                result.information = "Error";
                return result;
            }
            result.remindTime = tempDateTime;
           
            return result;
        }

        public static DateTime GetDayFromUser()
        {
            Console.WriteLine("Enter the remind date in format: year, month, day");
            var dateStr = Console.ReadLine();
            return StringToDateTime(dateStr + ",0,0");
        }

        private static DateTime StringToDateTime(string str)
        {
            var dateList = str.Split(',');
            if (dateList.Length < 5)
            {
                return new DateTime();
            }

            var dateArray = new int[5];
            int count = 0;
            foreach(var data in  dateList)
            {
                dateArray[count] = ParseStringToInt(data);
                if (dateArray[count] < 0 || !CheckDateTimeData(count, dateArray[count]))
                    return new DateTime();
                count++;
            }
            return new DateTime(dateArray[0], dateArray[1], dateArray[2], dateArray[3], dateArray[4], 0);
        }

        private static int ParseStringToInt(String str)
        {
            try
            {
                int result = Convert.ToInt32(str);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return -1;
        }

        private static bool CheckDateTimeData(int count, int data)
        {
            switch (count)
            {
                case 0:
                    return CheckYear(data);
                case 1:
                    return CheckMonth(data);
                case 2:
                    return CheckDay(data);
                case 3:
                    return CheckHour(data);
                default:
                    return CheckMinute(data);
            }
        }

        private static bool CheckYear(int year)
        {
            return (year > 2021 && year < 2030);
        }

        private static bool CheckMonth(int month)
        {
            return (month > 0 && month < 13);
        }

        private static bool CheckDay(int day)
        {
            return (day > 0 && day < 32); //Very simpe check
        }

        private static bool CheckHour(int hour)
        {
            return (hour >= 0 && hour < 25);
        }

        private static bool CheckMinute(int minute)
        {
            return (minute >= 0 && minute < 61);
        }

        private static void PrintMeetDetails(Meet meet)
        {
            Console.WriteLine($"Information: {meet.information}");
            Console.WriteLine($"Begin at: {meet.beginTime}");
            Console.WriteLine($"End at: {meet.endTime}");
            Console.WriteLine($"Reminder at: {meet.remindTime}");
        }
    }
}
