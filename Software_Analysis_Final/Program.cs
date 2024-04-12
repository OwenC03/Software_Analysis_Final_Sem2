using MySqlConnector;
namespace Software_Analysis_Final
{
    class Program
    {
        static MySqlConnection connection;
        static void Main(string[] args)
        {

            Console.WriteLine("Hello, Welcome to Village Rentals please make your selection below.");

            // Initialize MySQL connection
            InitializeDatabase();

            // Show menu
            ShowMenu();
        }

        static void InitializeDatabase()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                UserID = "root",
                Password = "DarkWolf",
                Database = "village rentals" 
            };

            connection = new MySqlConnection(builder.ConnectionString);
        }

        static void ShowMenu()
        {
            Console.WriteLine("1: Add or Delete equipment.");
            Console.WriteLine("2: Add new client");
            Console.WriteLine("3: Display all equipment");
            Console.WriteLine("4: Display all clients");
            Console.WriteLine("5: Process Rental");
            Console.WriteLine("6: Exit");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    EquipmentMenu();
                    break;
                case "2":
                    AddNewClient();
                    break;
                case "3":
                    DisplayAllEquipment();
                    break;
                case "4":
                    DisplayAllClients();
                    break;
                case "5":
                    ProcessRental();
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    ShowMenu();
                    break;
            }
        }

        static void EquipmentMenu()
        {
            Console.WriteLine("1: Add equipment");
            Console.WriteLine("2: Delete equipment");
            Console.WriteLine("3: Back to main menu");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    AddEquipment();
                    break;
                case "2":
                    DeleteEquipment();
                    break;
                case "3":
                    ShowMenu();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    EquipmentMenu();
                    break;
            }
        }

        static void AddEquipment()
        {
            Console.WriteLine("Adding new equipment...");
            Console.WriteLine("Please Input the category id:");
            var categoryId = Console.ReadLine();

            Console.WriteLine("Please Input the equipment's name:");
            var rental_name = Console.ReadLine();

            Console.WriteLine("Please Input the equipment's description:");
            var rental_description = Console.ReadLine();

            Console.WriteLine("Please enter the Daily rate:");
            var dailyRate = Console.ReadLine();

            connection.Open();
            string sql = "INSERT INTO RentalEquipment (category_id, rental_name, rental_description, daily_rate) VALUES (@CategoryId, @RentalName, @RentalDescription, @DailyRate)";
            using (var cmd = new MySqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                cmd.Parameters.AddWithValue("@RentalName", rental_name);
                cmd.Parameters.AddWithValue("@RentalDescription", rental_description);
                cmd.Parameters.AddWithValue("@DailyRate", dailyRate);
                cmd.ExecuteNonQuery();
            }
            connection.Close();

            Console.WriteLine("Equipment added successfully.Thank you");
            ShowMenu();
        }

        static void DeleteEquipment()
        {
            Console.WriteLine("Deleting Old Equipment....");
            Console.WriteLine("Please input Equipment Id");
            var equipment_id = Console.ReadLine();

            connection.Open();
            string sql = "DELETE FROM RentalEquipment WHERE equipment_id = @EquipmentId";
            using (var cmd = new MySqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@EquipmentId", equipment_id);
                cmd.ExecuteNonQuery ();
            }
            connection.Close();
            Console.WriteLine("Equipment deleted successfully. Thank you");
            ShowMenu();
        }

        static void AddNewClient()
        {
            Console.WriteLine("Adding New Client....");
            Console.WriteLine("Please Input Client last name:");
            var last_name = Console.ReadLine();

            Console.WriteLine("Please Input Client first name:");
            var first_name = Console.ReadLine();

            Console.WriteLine("Please Input Clients Phone number:");
            var phone = Console.ReadLine();

            Console.WriteLine("Please Input Clients Email:");
            var email = Console.ReadLine();

            connection.Open();
            string sql = "INSERT INTO CustomerInformation (last_name, first_name, contact_phone, email) VALUES (@LastName, @FirstName, @ContactNumber, @Email)";
            using (var cmd = new MySqlCommand (sql, connection))
            {
                cmd.Parameters.AddWithValue("@LastName", last_name);
                cmd.Parameters.AddWithValue("@FirstName", first_name);
                cmd.Parameters.AddWithValue("@ContactNumber", phone);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.ExecuteNonQuery ();
            }
            connection.Close();
            Console.WriteLine("New Client added successfully. Thank you");
            ShowMenu();
        }

        static void DisplayAllEquipment()
        {
            Console.WriteLine("Displaying All Equipment.....");
            Console.WriteLine("How would you like them displayed?");
            Console.WriteLine("1: By Name");
            Console.WriteLine("2: By Category");
            Console.WriteLine("3: By price");

            var option = Console.ReadLine();
            switch(option)
            {
                case "1":
                    ByName();
                    break;
                case "2":
                    ByCategory();
                    break;
                case "3":
                    ByPrice();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    DisplayAllEquipment();
                    break;
            }
        }

        static void ByName()
        {
            connection.Open();
            string sql = "SELECT rental_name, rental_description FROM RentalEquipment ORDER BY rental_name;";
            using (MySqlCommand command = new MySqlCommand (sql, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Name: {reader["rental_name"]}, Description: {reader["rental_description"]}");
                    }
                }
            }
            connection.Close();
            Console.WriteLine("Here's all the equipment by name. Have a nice day");
            ShowMenu ();
        }

        static void ByCategory()
        {
            connection.Open();
            string sql = "SELECT category_id, rental_description FROM RentalEquipment ORDER BY category_id;";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Category ID: {reader["category_id"]}, Description: {reader["rental_description"]}");
                    }
                }
            }
            
            connection.Close();
            Console.WriteLine("Here's all the equipment by Category. have a nice day");
            ShowMenu();
        }

        static void ByPrice()
        {
            connection.Open();
            string sql = "SELECT rental_name, rental_description, daily_rate FROM RentalEquipment ORDER BY daily_rate;";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Name: {reader["rental_name"]}, Description: {reader["rental_description"]}, Daily Rate: {reader["daily_rate"]}");
                    }
                }
            }
            
            connection.Close();
            Console.WriteLine("Heres all the equipment by price. have a nice day");
            ShowMenu();
        }

        static void DisplayAllClients()
        {
            Console.WriteLine("Displaying Client info....");
            connection.Open();
            string sql = "SELECT first_name, last_name FROM CustomerInformation ORDER BY first_name";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                using ( MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"First Name: {reader["first_name"]}, Last Name: {reader["last_name"]}");
                    }
                }
            }
            connection.Close();
            Console.WriteLine("Here is all the clients. have a nice day");
            ShowMenu();
        }

        static void ProcessRental()
        {
            Console.WriteLine("Processing Rental....");

            
            Console.WriteLine("Please input Customer ID:");
            int customerId = int.Parse(Console.ReadLine());

            Console.WriteLine("Please input Equipment ID:");
            int equipmentId = int.Parse(Console.ReadLine());

            Console.WriteLine("Please input Rental Date (YYYY-MM-DD):");
            DateTime rentalDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Please input Return Date (YYYY-MM-DD):");
            DateTime returnDate = DateTime.Parse(Console.ReadLine());

            
            decimal totalCost = CalculateRentalCost(equipmentId, rentalDate, returnDate);

            
            AddRentalRecord(customerId, equipmentId, rentalDate, returnDate, totalCost);

            
            UpdateEquipmentAvailability(equipmentId, false);

            Console.WriteLine("Rental processed successfully. Thank you");
            ShowMenu();
        }

        static decimal CalculateRentalCost(int equipmentId, DateTime rentalDate, DateTime returnDate)
        {
            try
            {
                connection.Open();
                string sql = "SELECT daily_rate FROM RentalEquipment WHERE equipment_id = @EquipmentId";
                using (var cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@EquipmentId", equipmentId);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        decimal dailyRate = Convert.ToDecimal(result);

                        TimeSpan rentalPeriod = returnDate - rentalDate;
                        int rentalDays = (int)Math.Ceiling(rentalPeriod.TotalDays);

                        return rentalDays * dailyRate;
                    }
                    else
                    {
                        Console.WriteLine("Equipment with ID {0} not found.", equipmentId);
                        return 0;
                    }
                }
            }
            finally
            {
                if(connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        static void AddRentalRecord(int customerId, int equipmentId, DateTime rentalDate, DateTime returnDate, decimal totalCost)
        {
            connection.Open();
            string sql = "INSERT INTO RentalInformation (customer_id, equipment_id, rental_date, return_date, cost) VALUES (@CustomerId, @EquipmentId, @RentalDate, @ReturnDate, @TotalCost)";
            using (var cmd = new MySqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                cmd.Parameters.AddWithValue("@EquipmentId", equipmentId);
                cmd.Parameters.AddWithValue("@RentalDate", rentalDate);
                cmd.Parameters.AddWithValue("@ReturnDate", returnDate);
                cmd.Parameters.AddWithValue("@TotalCost", totalCost);
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }

        static void UpdateEquipmentAvailability(int equipmentId, bool isAvailable)
        {
            connection.Open();
            string sql = "UPDATE RentalEquipment SET is_available = @IsAvailable WHERE equipment_id = @EquipmentId";
            using (var cmd = new MySqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@IsAvailable", isAvailable);
                cmd.Parameters.AddWithValue("@EquipmentId", equipmentId);
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }

    }

}
