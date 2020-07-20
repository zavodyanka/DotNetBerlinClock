using System.Text;

namespace BerlinClock.Classes
{
    public class TimeConverter : ITimeConverter
    {
        const int SecondsInterval = 2;
        const int HourLampsNumberInRow = 4;
        const int MinuteLampsNumberInTopRow = 11;
        const int MinuteLampsNumberInBottomRow = 4;
        const int QuarterInMinutes = 3;
        const int LampUnits = 5;

        const string Yellow = "Y";
        const string Red = "R";
        const string Grey = "O";
        
        private int _hours;
        private int _minutes;
        private int _seconds;

        public string ConvertTime(string time)
        {
            var timeParts = time.Split(':');
            _hours = int.Parse(timeParts[0]);
            _minutes = int.Parse(timeParts[1]);
            _seconds = int.Parse(timeParts[2]);
            
            return GetBerlinClockTime();
        }

        private string GetBerlinClockTime()
        {
            var sb = new StringBuilder();
            sb.AppendLine(GetSeconds())
                .AppendLine(GetHours())
                .Append(GetMinutes());

            return sb.ToString();
        }

        private string GetSeconds()
        {
            return _seconds % SecondsInterval == 0 ? Yellow : Grey;
        }

        private string GetHours()
        {
            var topLamps = _hours / LampUnits;
            var bottomLamps = _hours % LampUnits;

            var sb = new StringBuilder();
            sb.AppendLine(GetLampRow(HourLampsNumberInRow, topLamps, Red))
                .Append(GetLampRow(HourLampsNumberInRow, bottomLamps, Red));

            return sb.ToString();
        }

        private string GetMinutes()
        {
            var topLamps = _minutes / LampUnits;
            var bottomLamps = _minutes % LampUnits;

            var sb = new StringBuilder();
            sb.AppendLine(GetMinuteLampRow(topLamps))
                .Append(GetLampRow(MinuteLampsNumberInBottomRow, bottomLamps, Yellow));

            return sb.ToString();
        }

        private string GetLampRow(int totalNumberLamps, int numberLampsOn, string lampSymbol)
        {
            var sb = new StringBuilder();
            for (var i = 1; i <= totalNumberLamps; i++)
                sb.Append(i <= numberLampsOn ? lampSymbol : Grey);

            return sb.ToString();
        }

        private string GetMinuteLampRow(int numberLampsOn)
        {
            var sb = new StringBuilder();
            for (var i = 1; i <= MinuteLampsNumberInTopRow; i++)
            {
                sb.Append(
                    i <= numberLampsOn 
                    ? GetMinuteLampColor(i)
                    : Grey
                );
            }

            return sb.ToString();
        }

        private string GetMinuteLampColor(int index)
        {
            return index % QuarterInMinutes == 0 ? Red : Yellow;
        }
    }
}
