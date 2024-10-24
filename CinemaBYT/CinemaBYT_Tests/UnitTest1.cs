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

        }

        //check incapsulation with Person and Manager
        //check exceptions

        //movie

        [Test]
        public void EmptyGenresTest() 
        {
            Assert.Throws<Exception>(() => new Movie("Film", new DateTime(2018, 9, 20), 18, new List<string>()));
        }

        [Test]
        public void FutureDateTest()
        {
            List<string> list = new List<string>();
            list.Add("Romcom");
            Assert.Throws<Exception>(() => new Movie("Film", new DateTime(2028, 9, 20), 18, list));
        }

        //session

        [Test]
        public void FutureTimeStartTest()
        {
            List<string> list = new List<string>();
            list.Add("Romcom");
            Movie m1 = new Movie("Film", new DateTime(2018, 9, 20), 18, new List<string>());
            Assert.Throws<Exception>(() => new Session(new TimeSpan(2, 30, 0), new DateTime(2028, 9, 20), 120000, m1, 
                new Hall();
        }
        [Test]
        public void UnavailableSeatsTest() {

        }
        [Test]
        public void UnavailableHallTest()
        {

        }

        //Ticket

        [Test]
        public void NoSuchSeatTest()
        {

        }

        [Test]
        public void NoSuchSessionTest()
        {

        }

        //Buyer

        [Test]
        public void DiscountWorkedTest()
        {

        }

        //Payment

        [Test]
        public void NonExistingMethodTest()
        {

        }

        //Hall


        [Test]
        public void RebookedSeatTest()
        {

        }

    }
}