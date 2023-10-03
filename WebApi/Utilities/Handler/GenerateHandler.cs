using WebApi.Data;

namespace WebApi.Utilities.Handler
{
    public class GenerateHandler
    {
        private readonly BookingManagementDbContext _context;

        public GenerateHandler(BookingManagementDbContext context)
        {
            _context = context;
        }

        public string GenerateNIK()
        {
            // Cek apakah data di tabel Employee
            if (_context.Employees.Any())
            {
                // Jika sudah ada data, ambil NIK terakhir
                var lastEmployee = _context.Employees.OrderByDescending(e => e.Nik).First();
                var lastNIK = lastEmployee.Nik; //menyimpan NIK terakhir

                // Parse NIK terakhir menjadi angka, tambahkan 1, dan format ulang ke dalam NIK
                var nextNIKNumber = int.Parse(lastNIK) + 1;
                var nextNIK = nextNIKNumber.ToString("D6"); // Format menjadi 6 digit

                return nextNIK; //mengembalikan data NIK yang baru
            }
            else
            {
                // Jika tidak ada data, kembalikan NIK awal (111111)
                return "111111";
            }
        }
    }
}
