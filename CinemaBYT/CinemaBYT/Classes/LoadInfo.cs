using System;
using System.Globalization;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace CinemaBYT.Classes
{
    public class LoadInfo
    {
        public string CinemaName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string MovieName { get; set; }
        public string ReleaseDate { get; set; }
        public string AgeRating { get; set; }

        public void LoadFromXml(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            List<OwnsLoyaltyCard> loyaltyOwners = new List<OwnsLoyaltyCard>();

            XmlNode OwnsLoyaltyCardNode = doc.SelectSingleNode("/CinemaData/OwnsLoyaltyCard");

            if (OwnsLoyaltyCardNode != null)
            {  

                foreach (XmlNode loyaltyCardNode in OwnsLoyaltyCardNode.ChildNodes)
                {
                    if (loyaltyCardNode != null)
                    {
                        DateTime startDate, expireDate;
                        decimal discount;

                        if (DateTime.TryParseExact(loyaltyCardNode["startDate"].InnerText, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate) &&
                            DateTime.TryParseExact(loyaltyCardNode["expireDate"].InnerText, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out expireDate) &&
                            decimal.TryParse(loyaltyCardNode["discount"].InnerText, out discount))
                        {
                            loyaltyOwners.Add(new OwnsLoyaltyCard(startDate, expireDate, discount));
                        }
                        else
                        {
                            // Handle parsing errors, e.g., log an error or throw an exception
                            Console.WriteLine("Error parsing loyalty card node: " + loyaltyCardNode.OuterXml);
                        }
                    }
                }
            }
            List<Seat> seats = new List<Seat>();

            XmlNode seatsNode = doc.SelectSingleNode("/CinemaData/Seats");

            if (seatsNode != null)
            {
                foreach (XmlNode seatNode in seatsNode.ChildNodes)
                {
                    if (seatNode != null)
                    {
                        try
                        {
                            int seatNo = int.Parse(seatNode["seatNo"].InnerText);
                            bool isVIP = bool.Parse(seatNode["isVIP"].InnerText);
                            bool isAvailable = bool.Parse(seatNode["isAvailable"].InnerText);

                            seats.Add(new Seat(seatNo, isVIP, isAvailable));
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Error parsing seat node: {seatNode.OuterXml}. Exception: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Unexpected error parsing seat node: {seatNode.OuterXml}. Exception: {ex.Message}");
                        }
                    }
                }
            }

            List<Movie> movies = new List<Movie>();

            XmlNode moviesNode = doc.SelectSingleNode("/CinemaData/Movies");

            if (moviesNode != null)
            {
                foreach (XmlNode movieNode in moviesNode.ChildNodes)
                {
                    if (movieNode != null)
                    {
                        try
                        {
                            string name = movieNode["name"].InnerText;
                            DateTime releaseDate = DateTime.ParseExact(movieNode["releaseDate"].InnerText, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            int ageRating = int.Parse(movieNode["ageRating"].InnerText);

                            List<string> genres = new List<string>();
                            foreach (XmlNode genreNode in movieNode.SelectNodes("listOfGenres/genre"))
                            {
                                genres.Add(genreNode.InnerText);
                            }

                            movies.Add(new Movie(name, releaseDate, ageRating, genres));
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Error parsing movie node: {movieNode.OuterXml}. Exception: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Unexpected error parsing movie node: {movieNode.OuterXml}. Exception: {ex.Message}");
                        }
                    }
                }
            }

            List<Support> supportStaff = new List<Support>();

            XmlNode supportStaffNode = doc.SelectSingleNode("/CinemaData/SupportStaff");

            if (supportStaffNode != null)
            {
                foreach (XmlNode staffNode in supportStaffNode.ChildNodes)
                {
                    if (staffNode != null)
                    {
                        try
                        {
                            string name = staffNode["name"].InnerText;
                            string level = staffNode["level"].InnerText;
                            DateTime hireDate = DateTime.ParseExact(staffNode["hireDate"].InnerText, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            int salary = int.Parse(staffNode["salary"].InnerText);
                            DateTime birthDate = DateTime.ParseExact(staffNode["birthDate"].InnerText, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            string email = staffNode["email"].InnerText;
                            string pesel = staffNode["pesel"].InnerText;


                            supportStaff.Add(new Support(hireDate, salary, name, email, birthDate, pesel, level));
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Error parsing staff node: {staffNode.OuterXml}. Exception: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Unexpected error parsing staff node: {staffNode.OuterXml}. Exception: {ex.Message}");
                        }
                    }
                }
            }

            List<Manager> managers = new List<Manager>();

            XmlNode managersNode = doc.SelectSingleNode("/CinemaData/Management");

            if (managersNode != null)
            {
                foreach (XmlNode managerNode in managersNode.ChildNodes)
                {
                    if (managerNode != null)
                    {
                        try
                        {
                            string name = managerNode["name"].InnerText;
                            string position = managerNode["position"].InnerText;
                            DateTime hireDate = DateTime.ParseExact(managerNode["hireDate"].InnerText, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            int salary = int.Parse(managerNode["salary"].InnerText);
                            DateTime birthDate = DateTime.ParseExact(managerNode["birthDate"].InnerText, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            string email = managerNode["email"].InnerText;
                            string pesel = managerNode["pesel"].InnerText;

                            managers.Add(new Manager(hireDate, salary, position, name, email, birthDate, pesel));
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Error parsing manager node: {managerNode.OuterXml}. Exception: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Unexpected error parsing manager node: {managerNode.OuterXml}. Exception: {ex.Message}");
                        }
                    }
                }
            }

            List<Person> people = new List<Person>();
            people.AddRange(managers);
            people.AddRange(supportStaff);

            List<Hall> halls = new List<Hall>();

            XmlNode hallsNode = doc.SelectSingleNode("/CinemaData/Halls");

            if (hallsNode != null)
            {
                foreach (XmlNode hallNode in hallsNode.ChildNodes)
                {
                    if (hallNode != null)
                    {
                        try
                        {
                            int hallNumber = int.Parse(hallNode["hallNumber"].InnerText);
                            int numberOfSeats = int.Parse(hallNode["numberOfSeats"].InnerText);

                            List<Seat> Hallseats = new List<Seat>();
                            foreach (XmlNode seatNode in hallNode.SelectNodes("seats/seatNo"))
                            {
                                //seats.Count();
                                Hallseats.Add(seats[seats.FindIndex(s => s.SeatNo.Equals(int.Parse(seatNode.InnerText)))]);
                            }

                            halls.Add(new Hall(hallNumber, numberOfSeats, seats));
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Error parsing hall node: {hallNode.OuterXml}. Exception: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Unexpected error parsing hall node: {hallNode.OuterXml}. Exception: {ex.Message}");
                        }
                    }
                }
            }

            List<Session> sessions = new List<Session>();

            XmlNode sessionsNode = doc.SelectSingleNode("/CinemaData/Sessions");

            if (sessionsNode != null)
            {
                foreach (XmlNode sessionNode in sessionsNode.ChildNodes)
                {
                    if (sessionNode != null)
                    {
                        try
                        {
                            TimeSpan duration = TimeSpan.Parse(sessionNode["duration"].InnerText);
                            DateTime timeStart = DateTime.ParseExact(sessionNode["timeStart"].InnerText, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                            decimal income = decimal.Parse(sessionNode["income"].InnerText);

                            // Assuming Movie, Hall, and Ticket objects are already deserialized
                            Movie movie = movies[int.Parse(sessionNode["movieId"].InnerText)]; // Deserialize movie information
                            Hall hall = halls[int.Parse(sessionNode["hallId"].InnerText)]; ; // Deserialize hall information
                            List<Ticket> sessiontickets = new List<Ticket>(); // No tickets purchased yet)

                            sessions.Add(new Session(duration, timeStart, income, movie, hall, sessiontickets));
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Error parsing session node: {sessionNode.OuterXml}. Exception: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Unexpected error parsing session node: {sessionNode.OuterXml}. Exception: {ex.Message}");
                        }
                    }
                }
            }

            List<Cinema> cinemas = new List<Cinema>();

            XmlNode cinemasNode = doc.SelectSingleNode("/CinemaData/Cinemas");

            if (cinemasNode != null)
            {
                foreach (XmlNode cinemaNode in cinemasNode.ChildNodes)
                {
                    if (cinemaNode != null)
                    {
                        try
                        {
                            string name = cinemaNode["name"].InnerText;
                            string city = cinemaNode["city"].InnerText;
                            string country = cinemaNode["country"].InnerText;
                            string contactPhone = cinemaNode["contactPhone"].InnerText;
                            string openingHours = cinemaNode["openingHours"].InnerText;
                            //we may add list of halls, if you want, but we don't have to

                            cinemas.Add(new Cinema(name, city, country, contactPhone, openingHours));
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Error parsing cinema node: {cinemaNode.OuterXml}. Exception: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Unexpected error parsing cinema node: {cinemaNode.OuterXml}. Exception: {ex.Message}");
                        }
                    }
                }
            }

            List<History> histories = new List<History>();

            XmlNode historiesNode = doc.SelectSingleNode("/CinemaData/Histories");

            if (historiesNode != null)
            {
                foreach (XmlNode historyNode in historiesNode.ChildNodes)
                {
                    if (historyNode != null)
                    {
                        try
                        {
                            List<Session> Historysessions = new List<Session>();
                            foreach (XmlNode hsessionNode in historyNode.SelectNodes("Sessions/Session"))
                            {
                                // ... Deserialize Session object as shown in previous responses ...
                                Historysessions.Add(sessions[int.Parse(hsessionNode.InnerText)]);
                            }

                            // Assuming Person object is deserialized separately or from another part of the XML
                            people.Count();
                            Person person = people[people.FindIndex(p => p.PESEL.Equals(historyNode["Person"].InnerText))]; // Deserialize person information

                            histories.Add(new History(sessions, person));
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Error parsing history node: {historyNode.OuterXml}. Exception: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Unexpected error parsing history node: {historyNode.OuterXml}. Exception: {ex.Message}");
                        }
                    }
                }
            }

            List<Comment> comments = new List<Comment>();

            XmlNode commentsNode = doc.SelectSingleNode("/CinemaData/Comments");

            if (commentsNode != null)
            {
                foreach (XmlNode commentNode in commentsNode.ChildNodes)
                {
                    if (commentNode != null)
                    {
                        try
                        {
                            string commentText = commentNode["commentText"].InnerText;
                            DateTime date = DateTime.ParseExact(commentNode["date"].InnerText, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                            // Assuming Movie object is deserialized separately or from another part of the XML
                            Movie movie = movies[int.Parse(commentNode["movie"].InnerText)]; // Deserialize movie information
                            
                            // could be used, if we had Person in constructor
                            //Person person = people[people.FindIndex(p => p.PESEL.Equals(historyNode["Person"]))];

                            comments.Add(new Comment(commentText, date, movie));
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Error parsing comment node: {commentNode.OuterXml}. Exception: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Unexpected error parsing comment node: {commentNode.OuterXml}. Exception: {ex.Message}");
                        }
                    }
                }
            }

            List<Payment> payments = new List<Payment>();

            XmlNode paymentsNode = doc.SelectSingleNode("/CinemaData/Payments");

            if (paymentsNode != null)
            {
                foreach (XmlNode paymentNode in paymentsNode.ChildNodes)
                {
                    if (paymentNode != null)
                    {
                        try
                        {
                            PaymentType type = (PaymentType)Enum.Parse(typeof(PaymentType), paymentNode["type"].InnerText);
                            DateTime paymentDate = DateTime.ParseExact(paymentNode["paymentDate"].InnerText, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            int maxTicketPerPayment = int.Parse(paymentNode["maxTicketPerPayment"].InnerText);

                            payments.Add(new Payment(type, paymentDate, maxTicketPerPayment));
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Error parsing payment node: {paymentNode.OuterXml}. Exception: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Unexpected error parsing payment node: {paymentNode.OuterXml}. Exception: {ex.Message}");
                        }
                    }
                }
            }

          
            List<Ticket> tickets = new List<Ticket>();

            XmlNode ticketsNode = doc.SelectSingleNode("/CinemaData/Tickets");

            if (ticketsNode != null)
            {
                foreach (XmlNode ticketNode in ticketsNode.ChildNodes)
                {
                    if (ticketNode != null)
                    {
                        try
                        {
                            int seatNumber = int.Parse(ticketNode["seatNumber"].InnerText);
                            decimal price = decimal.Parse(ticketNode["price"].InnerText);
                            DateTime purchaseDate = DateTime.ParseExact(ticketNode["purchaseDate"].InnerText, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            TicketType type = (TicketType)Enum.Parse(typeof(TicketType), ticketNode["type"].InnerText);

                            // Assuming Session and Seat objects are deserialized separately or from another part of the XML
                            Session session = sessions[int.Parse(ticketNode["session"].InnerText)]; // Deserialize session information
                            Seat seat = seats[seats.FindIndex(s => s.SeatNo.Equals(int.Parse(ticketNode["seat"].InnerText)))]; // Deserialize seat information
                            //Person can be added after being added to constructor

                            tickets.Add(new Ticket(seatNumber, price, purchaseDate, type, session, seat));
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Error parsing ticket node: {ticketNode.OuterXml}. Exception: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Unexpected error parsing ticket node: {ticketNode.OuterXml}. Exception: {ex.Message}");
                        }
                    }
                }
            }
        }

        public void LoadFromJson(string path)
        {
            string jsonString = File.ReadAllText(path);
            OwnsLoyaltyCard o = JsonSerializer.Deserialize<OwnsLoyaltyCard>(jsonString)!;

            Console.WriteLine($"Date start: {o.StartDate}");
            Console.WriteLine($"Expire: {o.ExpireDate}");
            Console.WriteLine($"Discount: {o.Discount}");
        }
    }
}
