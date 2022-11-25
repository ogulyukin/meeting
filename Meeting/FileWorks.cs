using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Meeting
{
    public static class FileWorks
    {
        public static async Task WrightReport(List<Meet> meets)
        {
            var task = new string[meets.Count * 5];
            int count = 0;
            foreach(var meet in meets)
            {
                task[count] = $"Information: {meet.information}";
                count++;
                task[count] = $"Begin timee: {meet.beginTime.ToString()}";
                count++;
                task[count] = $"End time: {meet.endTime.ToString()}";
                count++;
                task[count] = $"Reminder at: {meet.remindTime.ToString()}";
                count++;
                task[count] = "\n";
                count++;
            }
            try
            {
                await File.WriteAllLinesAsync($"report{(DateTime.Now).ToString().Replace(':','-')}.txt", task);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Ok");
        }
    }
}
