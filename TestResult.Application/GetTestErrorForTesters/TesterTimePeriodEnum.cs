namespace TestResult.Application.GetTestErrorForTesters;

public enum TesterTimePeriodEnum
{
    Last_Full_Year,
    This_Year,
    Last_Full_Month,
    This_Month,
    Last_Full_Week,
    This_Week,
    Yesterday,
    Today
}

public static class TesterTimePeriodEnumMapper
{
    public static (DateTime, DateTime, int) MapEnumToDateAndIntervalInMinutes(TesterTimePeriodEnum timePeriodEnum)
    {
        DateTime currentDate = DateTime.Now;

        int numberOfMinutesInADay = 1440;
        int numberOfMinutesInAnHour = 60;
        DateTime firstDayOfLastYear = new DateTime(currentDate.Year - 1, 1, 1).Date;
        DateTime firstDayOfThisYear = new DateTime(currentDate.Year, 1, 1).Date;

        DateTime firstDayOfLastMonth = new DateTime(currentDate.Year, currentDate.Month - 1, 1).Date;
        DateTime firstDayOfThisMonth = new DateTime(currentDate.Year, currentDate.Month, 1).Date;

        DateTime firstDayOfLastWeek = GetFirstDayOfLastWeek();
        DateTime firstDayOfThisWeek = GetFirstDayThisLastWeek();

        DateTime yesterday = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day - 1).Date;
        DateTime startOfToday = currentDate.Date;


        DateTime now = DateTime.Now;

        switch (timePeriodEnum)
        {
            case TesterTimePeriodEnum.Last_Full_Year:

                return (firstDayOfLastYear, firstDayOfThisYear, numberOfMinutesInADay);

            case TesterTimePeriodEnum.This_Year:

                return (firstDayOfThisYear, now, numberOfMinutesInADay);

            case TesterTimePeriodEnum.Last_Full_Month:

                return (firstDayOfLastMonth, firstDayOfThisMonth, numberOfMinutesInADay);

            case TesterTimePeriodEnum.This_Month:

                return (firstDayOfThisMonth, now, numberOfMinutesInADay);

            case TesterTimePeriodEnum.Last_Full_Week:

                return (firstDayOfLastWeek, firstDayOfThisWeek, numberOfMinutesInADay);

            case TesterTimePeriodEnum.This_Week:

                return (firstDayOfThisWeek, now, numberOfMinutesInADay);

            case TesterTimePeriodEnum.Yesterday:

                return (yesterday, startOfToday, numberOfMinutesInAnHour);

            case TesterTimePeriodEnum.Today:

                return (startOfToday, now, numberOfMinutesInAnHour);
            default:
                throw new ArgumentOutOfRangeException(nameof(timePeriodEnum), timePeriodEnum, null);
        }
    }

    public static DateTime GetFirstDayOfLastWeek()
    {
        var now = DateTime.Now;
        var lastWeek = now.AddDays(-7);
        
        while (lastWeek.DayOfWeek != DayOfWeek.Monday)
        {
            lastWeek = lastWeek.AddDays(-1);
        }

        return lastWeek.Date;
    }

    public static DateTime GetFirstDayThisLastWeek()
    {
        var now = DateTime.Now;
        while (now.DayOfWeek != DayOfWeek.Monday)
        {
            now = now.AddDays(-1);
        }

        return now.Date;
    }
}