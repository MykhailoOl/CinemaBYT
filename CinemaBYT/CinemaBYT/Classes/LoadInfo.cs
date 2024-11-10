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
        public List<OwnsLoyaltyCard> loyaltyOwners = new List<OwnsLoyaltyCard>();
        public List<Seat> seats = new List<Seat>();
        public List<Movie> movies = new List<Movie>();
        public List<Support> supportStaff = new List<Support>();
        public List<Manager> managers = new List<Manager>();
        public List<Person> people = new List<Person>();
        public List<Hall> halls = new List<Hall>();
        public List<Ticket> tickets = new List<Ticket>();
        public List<Session> sessions = new List<Session>();
        public List<Cinema> cinemas = new List<Cinema>();
        public List<History> histories = new List<History>();
        public List<Comment> comments = new List<Comment>();
        public List<Payment> payments = new List<Payment>();
        public void LoadFromXml(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode OwnsLoyaltyCardNode = doc.SelectSingleNode("/CinemaData/OwnsLoyaltyCard");

            if (OwnsLoyaltyCardNode != null)
            {  

                foreach (XmlNode loyaltyCardNode in OwnsLoyaltyCardNode.ChildNodes)
                {
                    if (loyaltyCardNode != null)
                    {



                        //String name,String email, DateTime birthDate,String pesel,

                        try
                        {
                            DateTime startDate = DateTime.ParseExact(loyaltyCardNode["startDate"].InnerText, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            DateTime expireDate = DateTime.ParseExact(loyaltyCardNode["expireDate"].InnerText, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            decimal discount = decimal.Parse(loyaltyCardNode["discount"].InnerText);
                            string name = loyaltyCardNode["name"].InnerText;
                            DateTime birthDate = DateTime.ParseExact(loyaltyCardNode["birthDate"].InnerText, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            string email = loyaltyCardNode["email"].InnerText;
                            string pesel = loyaltyCardNode["pesel"].InnerText;
                            loyaltyOwners.Add(new OwnsLoyaltyCard(name, email, birthDate, pesel, startDate, expireDate, discount));
                        }

                        catch (FormatException ex)
                        {
                            // Handle parsing errors, e.g., log an error or throw an exception
                            Console.WriteLine("Error parsing loyalty card node: " + loyaltyCardNode.OuterXml);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Unexpected error parsing seat node: {loyaltyCardNode.OuterXml}. Exception: {ex.Message}");
                        }
                    }
                }
            }

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

        
            people.AddRange(managers);
            people.AddRange(supportStaff);

  
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
                            Person person = people[people.FindIndex(p => p.PESEL.Equals(commentNode["Person"].InnerText))];

                            comments.Add(new Comment(commentText, date, movie, person));
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
                            Person person = people[people.FindIndex(p => p.PESEL == ticketNode["person"].InnerText)];

                            tickets.Add(new Ticket(seatNumber, price, purchaseDate, type, session, seat, person));
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

        public void SerializeToXml(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("CinemaData");
            doc.AppendChild(root);

            // OwnsLoyaltyCard
            XmlElement loyaltyCardElement = doc.CreateElement("OwnsLoyaltyCard");

            foreach (var loyaltyCard in loyaltyOwners)
            {
                XmlElement cardElement = doc.CreateElement("LoyaltyCard");        
                XmlElement startDate = doc.CreateElement("startDate");
                startDate.InnerText = loyaltyCard.StartDate.ToString("yyyy-MM-dd");
                cardElement.AppendChild(startDate);
                XmlElement expireDate = doc.CreateElement("expireDate");
                expireDate.InnerText = loyaltyCard.ExpireDate.ToString("yyyy-MM-dd");
                cardElement.AppendChild(expireDate);            
                XmlElement discount = doc.CreateElement("discount");
                discount.InnerText = loyaltyCard.Discount.ToString();
                cardElement.AppendChild(discount);           
                XmlElement name = doc.CreateElement("name");
                name.InnerText = loyaltyCard.Name;
                cardElement.AppendChild(name);
                XmlElement birthDate = doc.CreateElement("birthDate");
                birthDate.InnerText = loyaltyCard.BirthDate.ToString("yyyy-MM-dd");
                cardElement.AppendChild(birthDate);     
                XmlElement email = doc.CreateElement("email");
                email.InnerText = loyaltyCard.Email;
                cardElement.AppendChild(email);   
                XmlElement pesel = doc.CreateElement("pesel");
                pesel.InnerText = loyaltyCard.PESEL;
                cardElement.AppendChild(pesel);
                loyaltyCardElement.AppendChild(cardElement);
            }

  
            root.AppendChild(loyaltyCardElement);


            // Seats
            XmlElement seatsElements = doc.CreateElement("Seats");
            foreach (var seat in seats)
            {
                XmlElement seatElement = doc.CreateElement("Seat");

                XmlElement seatNo = doc.CreateElement("seatNo");
                seatNo.InnerText = seat.SeatNo.ToString();
                seatElement.AppendChild(seatNo);

                XmlElement isVIP = doc.CreateElement("isVIP");
                isVIP.InnerText = seat.IsVIP.ToString();
                seatElement.AppendChild(isVIP);

                XmlElement isAvailable = doc.CreateElement("isAvailable");
                isAvailable.InnerText = seat.IsAvailable.ToString();
                seatElement.AppendChild(isAvailable);

                seatsElements.AppendChild(seatElement);
            }
            root.AppendChild(seatsElements);

            // Movies
            XmlElement moviesElement = doc.CreateElement("Movies");
            foreach (var movie in movies)
            {
                XmlElement movieElement = doc.CreateElement("Movie");

                XmlElement name = doc.CreateElement("name");
                name.InnerText = movie.Name;
                movieElement.AppendChild(name);

                XmlElement releaseDate = doc.CreateElement("releaseDate");
                releaseDate.InnerText = movie.ReleaseDate.ToString("yyyy-MM-dd");
                movieElement.AppendChild(releaseDate);

                XmlElement ageRating = doc.CreateElement("ageRating");
                ageRating.InnerText = movie.AgeRating.ToString();
                movieElement.AppendChild(ageRating);

                XmlElement genresElement = doc.CreateElement("listOfGenres");
                foreach (var genre in movie.ListOfGenres)
                {
                    XmlElement genreElement = doc.CreateElement("genre");
                    genreElement.InnerText = genre;
                    genresElement.AppendChild(genreElement);
                }
                movieElement.AppendChild(genresElement);

                moviesElement.AppendChild(movieElement);
            }
            root.AppendChild(moviesElement);

            // Support Staff
            XmlElement supportStaffElement = doc.CreateElement("SupportStaff");
            foreach (var staff in supportStaff)
            {
                XmlElement staffElement = doc.CreateElement("Staff");

                XmlElement name = doc.CreateElement("name");
                name.InnerText = staff.Name;
                staffElement.AppendChild(name);

                XmlElement level = doc.CreateElement("level");
                level.InnerText = staff.Level;
                staffElement.AppendChild(level);

                XmlElement hireDate = doc.CreateElement("hireDate");
                hireDate.InnerText = staff.HireDate.ToString("yyyy-MM-dd");
                staffElement.AppendChild(hireDate);

                XmlElement salary = doc.CreateElement("salary");
                salary.InnerText = staff.Salary.ToString();
                staffElement.AppendChild(salary);

                XmlElement birthDate = doc.CreateElement("birthDate");
                birthDate.InnerText = staff.BirthDate.ToString("yyyy-MM-dd");
                staffElement.AppendChild(birthDate);

                XmlElement email = doc.CreateElement("email");
                email.InnerText = staff.Email;
                staffElement.AppendChild(email);

                XmlElement pesel = doc.CreateElement("pesel");
                pesel.InnerText = staff.PESEL;
                staffElement.AppendChild(pesel);

                supportStaffElement.AppendChild(staffElement);
            }
            root.AppendChild(supportStaffElement);

            // Management
            XmlElement managersElement = doc.CreateElement("Management");
            foreach (var manager in managers)
            {
                XmlElement managerElement = doc.CreateElement("Manager");

                XmlElement name = doc.CreateElement("name");
                name.InnerText = manager.Name;
                managerElement.AppendChild(name);

                XmlElement position = doc.CreateElement("position");
                position.InnerText = manager.Position;
                managerElement.AppendChild(position);

                XmlElement hireDate = doc.CreateElement("hireDate");
                hireDate.InnerText = manager.HireDate.ToString("yyyy-MM-dd");
                managerElement.AppendChild(hireDate);

                XmlElement salary = doc.CreateElement("salary");
                salary.InnerText = manager.Salary.ToString();
                managerElement.AppendChild(salary);

                XmlElement birthDate = doc.CreateElement("birthDate");
                birthDate.InnerText = manager.BirthDate.ToString("yyyy-MM-dd");
                managerElement.AppendChild(birthDate);

                XmlElement email = doc.CreateElement("email");
                email.InnerText = manager.Email;
                managerElement.AppendChild(email);

                XmlElement pesel = doc.CreateElement("pesel");
                pesel.InnerText = manager.PESEL;
                managerElement.AppendChild(pesel);

                managersElement.AppendChild(managerElement);
            }
            root.AppendChild(managersElement);

            // Halls
            XmlElement hallsElement = doc.CreateElement("Halls");
            foreach (var hall in halls)
            {
                XmlElement hallElement = doc.CreateElement("Hall");

                XmlElement hallNumber = doc.CreateElement("hallNumber");
                hallNumber.InnerText = hall.HallNumber.ToString();
                hallElement.AppendChild(hallNumber);

                XmlElement numberOfSeats = doc.CreateElement("numberOfSeats");
                numberOfSeats.InnerText = hall.NumberOfSeats.ToString();
                hallElement.AppendChild(numberOfSeats);

                XmlElement seatsElement = doc.CreateElement("seats");
                foreach (var seat in hall.Seats)
                {
                    XmlElement seatNo = doc.CreateElement("seatNo");
                    seatNo.InnerText = seat.SeatNo.ToString();
                    seatsElement.AppendChild(seatNo);
                }
                hallElement.AppendChild(seatsElement);

                hallsElement.AppendChild(hallElement);
            }
            root.AppendChild(hallsElement);
            XmlElement sessionsElement = doc.CreateElement("Sessions");
            foreach (var session in sessions)
            {
                XmlElement sessionElement = doc.CreateElement("Session");

                XmlElement duration = doc.CreateElement("duration");
                duration.InnerText = session.Duration.ToString();
                sessionElement.AppendChild(duration);

                XmlElement timeStart = doc.CreateElement("timeStart");
                timeStart.InnerText = session.TimeStart.ToString("yyyy-MM-dd HH:mm:ss");
                sessionElement.AppendChild(timeStart);

                XmlElement income = doc.CreateElement("income");
                income.InnerText = session.Income.ToString();
                sessionElement.AppendChild(income);

                XmlElement movieId = doc.CreateElement("movieId");
                movieId.InnerText = movies.IndexOf(session.Movie).ToString();
                sessionElement.AppendChild(movieId);

                XmlElement hallId = doc.CreateElement("hallId");
                hallId.InnerText = halls.IndexOf(session.Hall).ToString();
                sessionElement.AppendChild(hallId);

                sessionsElement.AppendChild(sessionElement);
            }
            root.AppendChild(sessionsElement);

            // Cinemas
            XmlElement cinemasElement = doc.CreateElement("Cinemas");
            foreach (var cinema in cinemas)
            {
                XmlElement cinemaElement = doc.CreateElement("Cinema");

                XmlElement name = doc.CreateElement("name");
                name.InnerText = cinema.Name;
                cinemaElement.AppendChild(name);

                XmlElement city = doc.CreateElement("city");
                city.InnerText = cinema.City;
                cinemaElement.AppendChild(city);

                XmlElement country = doc.CreateElement("country");
                country.InnerText = cinema.Country;
                cinemaElement.AppendChild(country);

                XmlElement contactPhone = doc.CreateElement("contactPhone");
                contactPhone.InnerText = cinema.ContactPhone;
                cinemaElement.AppendChild(contactPhone);

                XmlElement openingHours = doc.CreateElement("openingHours");
                openingHours.InnerText = cinema.OpeningHours;
                cinemaElement.AppendChild(openingHours);

                cinemasElement.AppendChild(cinemaElement);
            }
            root.AppendChild(cinemasElement);

            // Histories
            XmlElement historiesElement = doc.CreateElement("Histories");
            foreach (var history in histories)
            {
                XmlElement historyElement = doc.CreateElement("History");

                XmlElement person = doc.CreateElement("Person");
                person.InnerText = history.Person.PESEL;
                historyElement.AppendChild(person);

                XmlElement sessionsNode = doc.CreateElement("Sessions");
                foreach (var session in history.ListOfSessions)
                {
                    XmlElement sessionId = doc.CreateElement("Session");
                    sessionId.InnerText = sessions.IndexOf(session).ToString();
                    sessionsNode.AppendChild(sessionId);
                }
                historyElement.AppendChild(sessionsNode);

                historiesElement.AppendChild(historyElement);
            }
            root.AppendChild(historiesElement);

            // Comments
            XmlElement commentsElement = doc.CreateElement("Comments");
            foreach (var comment in comments)
            {
                XmlElement commentElement = doc.CreateElement("Comment");

                XmlElement commentText = doc.CreateElement("commentText");
                commentText.InnerText = comment.CommentText;
                commentElement.AppendChild(commentText);

                XmlElement date = doc.CreateElement("date");
                date.InnerText = comment.Date.ToString("yyyy-MM-dd");
                commentElement.AppendChild(date);

                XmlElement movie = doc.CreateElement("movie");
                movie.InnerText = movies.IndexOf(comment.Movie).ToString();
                commentElement.AppendChild(movie);

                XmlElement person = doc.CreateElement("Person");
                person.InnerText = comment.Person.PESEL;
                commentElement.AppendChild(person);

                commentsElement.AppendChild(commentElement);
            }
            root.AppendChild(commentsElement);

            // Payments
            XmlElement paymentsElement = doc.CreateElement("Payments");
            foreach (var payment in payments)
            {
                XmlElement paymentElement = doc.CreateElement("Payment");

                XmlElement type = doc.CreateElement("type");
                type.InnerText = payment.Type.ToString();
                paymentElement.AppendChild(type);

                XmlElement paymentDate = doc.CreateElement("paymentDate");
                paymentDate.InnerText = payment.PaymentDate.ToString("yyyy-MM-dd");
                paymentElement.AppendChild(paymentDate);

                XmlElement maxTicketPerPayment = doc.CreateElement("maxTicketPerPayment");
                maxTicketPerPayment.InnerText = payment.MaxTicketPerPayment.ToString();
                paymentElement.AppendChild(maxTicketPerPayment);

                paymentsElement.AppendChild(paymentElement);
            }
            root.AppendChild(paymentsElement);

            // Tickets
            XmlElement ticketsElement = doc.CreateElement("Tickets");
            foreach (var ticket in tickets)
            {
                XmlElement ticketElement = doc.CreateElement("Ticket");

                XmlElement seatNumber = doc.CreateElement("seatNumber");
                seatNumber.InnerText = ticket.SeatNumber.ToString();
                ticketElement.AppendChild(seatNumber);

                XmlElement price = doc.CreateElement("price");
                price.InnerText = ticket.Price.ToString();
                ticketElement.AppendChild(price);

                XmlElement purchaseDate = doc.CreateElement("purchaseDate");
                purchaseDate.InnerText = ticket.PurchaseDate.ToString("yyyy-MM-dd");
                ticketElement.AppendChild(purchaseDate);

                XmlElement type = doc.CreateElement("type");
                type.InnerText = ticket.Type.ToString();
                ticketElement.AppendChild(type);

                XmlElement sessionId = doc.CreateElement("session");
                sessionId.InnerText = sessions.IndexOf(ticket.Session).ToString();
                ticketElement.AppendChild(sessionId);

                XmlElement seatId = doc.CreateElement("seat");
                seatId.InnerText = seats.IndexOf(ticket.Seat).ToString();
                ticketElement.AppendChild(seatId);

                XmlElement person = doc.CreateElement("Person");
                person.InnerText = ticket.Person.PESEL;
                ticketElement.AppendChild(person);

                ticketsElement.AppendChild(ticketElement);
            }
            root.AppendChild(ticketsElement);

            // Save the XML document
            doc.Save(filePath);
        }

    }
}
