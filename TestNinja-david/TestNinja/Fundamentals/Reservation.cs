namespace TestNinja.Fundamentals
{
    public class Reservation
    {
        private readonly User _madeby;

        public Reservation(User madeby)
        {
            _madeby = madeby;
        }

        public bool CanBeCancelledBy(User user)
        {
            return (user.IsAdmin || _madeby == user);
        }
        
    }

    public class User
    {
        public bool IsAdmin { get; set; }
    }
}