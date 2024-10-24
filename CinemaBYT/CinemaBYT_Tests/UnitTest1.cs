using CinemaBYT.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace CinemaBYT_Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CorrectLoyaltyDicountTest() {
            Assert.That(Payment.LoyaltyDiscount(100, true), Is.EqualTo(90));
        }

        [Test]
        public void PersonInEmployee()  //CorrectClassInClassExtent
        {
            Manager manager = new Manager(new DateTime(2020, 10, 10), 4500, "Top manager", "Alan", "tt@rr.yy", 
                new DateTime(2000, 8, 26), "09876543210");
            //manager.BirthDate = new DateTime(2000, 8, 26);
            Assert.That(manager.GetType(), Is.EqualTo(typeof(Person)));
        }

        //+ check reading from file with initial info
        [Test]
        public void LoadInitInfo()
        {
            List<Cinema> initCinemas = Cinema.LoadFromXml("text.xml");
            Assert.That(initCinemas.Count, Is.EqualTo(1));
        }

        //check incapsulation with Hall attribute Cinema
        [Test]
        public void CheckEncapsulation() {
            Hall h = new Hall(12, 30, new List<Seat>(30));
            h.SetCinema(new Cinema("fff", "New York", "USA", "0987654321", "all week 10-22"));
            Assert.That(h.GetCinema().Name, Is.EqualTo("fff"));
        }

        //check exceptions
        //Cinema exception
        [Test]
        public void UnavailableHallsTest()
        {
            Cinema c = new Cinema("fff", "New York", "USA", "0987654321", "all week 10-22");
            Assert.Throws<CinemaException>(() => c.GetAvailableHalls());
        }

        //HallException
        [Test]
        public void WrongNumberOfSeats()
        {
            Assert.Throws<ValidationException>(() => new Hall(12, 10, new List<Seat>(10)));
        }
        [Test]
        public void NoSeats()
        {
            Assert.Throws<HallException>(() => new Hall(12, 0, new List<Seat>(0)));
        }

        //movie exception
        [Test]
        public void InappropriateAge()
        {
            Movie m = new Movie("Film", new DateTime(2018, 9, 20), 18, new List<string>());
            Assert.Throws<MovieException>(() => m.IsSuitableForAge(-5));
        }

        //payment exception
        [Test]
        public void InappropriatePrice()
        {
            Assert.Throws<MovieException>(() => Payment.LoyaltyDiscount(-100, true));
        }

        //seat exception
        [Test]
        public void BookUnavailableSeat()
        {
            Seat s = new Seat(1, false, true);
            s.ReserveSeat();
            Assert.Throws<SeatReservationException>(() => s.ReserveSeat());
        }

        //session exception
        [Test]
        public void NoTicketsDeclared()
        {
            Movie m = new Movie("Film", new DateTime(2018, 9, 20), 18, new List<string>());
            Hall h = new Hall(12, 30, new List<Seat>(30));
            Session s = new Session(new TimeSpan(2, 30, 0), new DateTime(2022, 2, 24), 200000, m, h, new List<Ticket>());
            Assert.Throws<SessionException>(() => s.CheckAvailability());
        }


        //[Test]
        //public void EmptyGenresTest() 
        //{
        //    Assert.Throws<Exception>(() => new Movie("Film", new DateTime(2018, 9, 20), 18, new List<string>()));
        //}

        //[Test]
        //public void FutureDateTest()
        //{
        //    List<string> list = new List<string>();
        //    list.Add("Romcom");
        //    Assert.Throws<Exception>(() => new Movie("Film", new DateTime(2028, 9, 20), 18, list));
        //}

        //session

        //[Test]
        //public void FutureTimeStartTest()
        //{
        //    List<string> list = new List<string>();
        //    list.Add("Romcom");
        //    Movie m1 = new Movie("Film", new DateTime(2018, 9, 20), 18, new List<string>());
        //    Assert.Throws<Exception>(() => new Session(new TimeSpan(2, 30, 0), new DateTime(2028, 9, 20), 120000, m1, 
        //        new Hall();
        //}

    }
}