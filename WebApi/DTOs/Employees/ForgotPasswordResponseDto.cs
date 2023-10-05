namespace WebApi.DTOs.Employees
{
    public class ForgotPasswordResponseDto
    {
        public int Otp {  get; set; }

        public static explicit operator ForgotPasswordResponseDto(int value)
        {
            return new ForgotPasswordResponseDto
            {
                Otp = value
            };
        }
    }
}
