namespace WebApi.Utilities.Handler
{
    public class WorkingDaysHandler
    {
        public static int CalculateWorkingDays(DateTime startDate, DateTime endDate)
        {
            int workingDays = 0; //definisi working days
            DateTime currentDate = startDate; //mendefinisikan currentdate

            //perulangan jika current date <= end date
            while (currentDate <= endDate)
            {
                //pengkondisian bahwa currentdate bukan hari sabtu atau minggu
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    workingDays++; //menambahkan workingdays
                }

                currentDate = currentDate.AddDays(1); //menambah 1 hari pada currentdate 
            }

            return workingDays; //mengembalikkan nilai working days
        }
    }
}
