using System;
using System.Collections.Generic;
using System.Linq;

namespace Meeting
{
    public class MeetingManager
    {
        private List<Meet> _meets;
        private int _currentIndex;

        public MeetingManager(List<Meet> meets)
        {
            _meets = meets;
        }

        public void AddMeeting(Meet meet)
        {
            if(meet.information == "Error")
            {
                Console.WriteLine("Error: You enter invalid data!!!");
                return;
            }
            if(!FreeTimeCheck(meet))
            {
                Console.WriteLine("This time seems you will busy at other meeting. Sorry.");
                return;
            }

            _meets.Add(meet);
        }

        public void DeleteCurrentMeet()
        {
            _meets.RemoveAt(_currentIndex);
            if (_currentIndex > _meets.Count)
                _currentIndex = _meets.Count;
        }

        public Meet GetCurrentMeet()
        {
            if(_meets.Count == 0)
            {
                var error = new Meet();
                error.information = "Error";
                return error;
            }
            return _meets.ElementAt(_currentIndex);
        }

        public Meet NextMeet()
        {
            _currentIndex = _currentIndex < _meets.Count - 1 ? _currentIndex + 1 : 0;
            Console.WriteLine($"Current index: {_currentIndex}");
            return GetCurrentMeet();
        }

        public void ChangeMeet(Meet meet)
        {
            if (meet.information == "Error")
            {
                Console.WriteLine("Error: You enter invalid data!!!");
                return;
            }
            if (_meets.Count == 0)
                _meets.Add(new Meet());
            var currentMeet = _meets.ElementAt(_currentIndex);
            currentMeet.information = meet.information;
            currentMeet.beginTime = meet.beginTime;
            currentMeet.endTime = meet.endTime;
            currentMeet.remindTime = meet.remindTime;
        }

        public List<Meet> GetDayMeets(DateTime date)
        {
            var result = new List<Meet>();
            Console.WriteLine($"Finding meets at: {date.Date}");
            foreach (var meet in _meets)
            {
                Console.WriteLine($"Checking: {meet.information} : {meet.beginTime.Date}");
                if (meet.beginTime.Date == date.Date)
                    result.Add(meet);
            }

            return result;
        }

        public int GetMeetingCount()
        {
            return _meets.Count;
        }

        private bool FreeTimeCheck(Meet meet)
        {
            foreach (var mt in _meets)
            {
                if (mt.endTime < meet.beginTime || mt.beginTime > meet.endTime)
                    continue;
                return false;
            }
                return true;
        }
    }
}
