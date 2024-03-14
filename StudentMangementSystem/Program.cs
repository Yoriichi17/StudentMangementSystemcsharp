using System;
using System.Data.SqlClient;
using System.Xml.Linq;

class StudentManagement
{   
    
    static void Main(String[] args)
    {
        SqlConnection sqlConnection;
        string connectionString = @"Data Source=LAPTOP-3MQSP2M3\SQLEXPRESS;Initial Catalog=Student;Integrated Security=True";
        try
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            Console.WriteLine("CONNECTION ESATBLISHED");
            bool op = true;
            while (op)
            {
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Enter 1 to insert new record");
                Console.WriteLine("Enter 2 to display all the record");
                Console.WriteLine("Enter 3 to update existing record");
                Console.WriteLine("Enter 4 to delete existing record");
                Console.WriteLine("--------------------------------------------------");
                int choose = int.Parse(Console.ReadLine());

                switch (choose)
                {
                    case 1:
                        Console.WriteLine("Enter USN : ");
                        string usn = Console.ReadLine();
                        Console.WriteLine("Enter First Name : ");
                        string fname = Console.ReadLine();
                        Console.WriteLine("Enter Middle Name : ");
                        string mname = Console.ReadLine();
                        Console.WriteLine("Enter Last Name : ");
                        string lname = Console.ReadLine();
                        Console.WriteLine("Enter DOB : ");
                        string dob = Console.ReadLine();
                        Console.WriteLine("Enter Gender : ");
                        string gen = Console.ReadLine();
                        Console.WriteLine("Enter Address : ");
                        string addr = Console.ReadLine();
                        Console.WriteLine("Enter Phone Number : ");
                        string phone = Console.ReadLine();

                        string insertQuery = "INSERT INTO INFO(USN,StudentFName,StudentMName,StudentLName,DOB,Gender,StudentAddress,PhoneNumber) VALUES('" + usn + "','" + fname + "','" + mname + "','" + lname + "','" + dob + "','" + gen + "','" + addr + "','" + phone + "')";
                        SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection);
                        insertCommand.ExecuteNonQuery();
                        Console.WriteLine("Data Successfully inserted !");
                        break;
                    case 2:
                        string displayQuery = "SELECT * FROM INFO";
                        SqlCommand displayCommand = new SqlCommand(displayQuery, sqlConnection);
                        SqlDataReader dataReader = displayCommand.ExecuteReader();

                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Console.WriteLine("--------------------------------------------------");
                                Console.WriteLine("USN: " + dataReader.GetValue(0).ToString());
                                Console.WriteLine("FIRST-NAME: " + dataReader.GetValue(1).ToString());
                                Console.WriteLine("MIDDLE-NAME: " + dataReader.GetValue(2).ToString());
                                Console.WriteLine("LAST-NAME: " + dataReader.GetValue(3).ToString());
                                Console.WriteLine("DOB: " + dataReader.GetValue(4).ToString());
                                Console.WriteLine("GENDER: " + dataReader.GetValue(5).ToString());
                                Console.WriteLine("ADDRESS: " + dataReader.GetValue(6).ToString());
                                Console.WriteLine("PHONE NUMBER: " + dataReader.GetValue(7).ToString());
                                Console.WriteLine("--------------------------------------------------");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Database is empty.");
                        }

                        dataReader.Close();

                        break;
                    case 3:
                        Console.WriteLine("Enter the USN of the student for update: ");
                        string Uusn = Console.ReadLine();

                        // Check if the provided USN exists
                        string checkQuery = $"SELECT COUNT(*) FROM INFO WHERE USN = '{Uusn}'";
                        SqlCommand checkCommand = new SqlCommand(checkQuery, sqlConnection);
                        int count = (int)checkCommand.ExecuteScalar();

                        if (count > 0)
                        {
                            Console.WriteLine("Enter 1 to change fname");
                            Console.WriteLine("Enter 2 to change mname");
                            Console.WriteLine("Enter 3 to change lname");
                            Console.WriteLine("Enter 4 to change dob");
                            Console.WriteLine("Enter 5 to change gender");
                            Console.WriteLine("Enter 6 to change address");
                            Console.WriteLine("Enter 7 to change phone-number");
                            Console.WriteLine();
                            Console.Write("Enter the number here: ");
                            Console.WriteLine();
                            int ch = int.Parse(Console.ReadLine());
                            string result = "";
                            string output = "";
                            switch (ch)
                            {
                                case 1:
                                    Console.WriteLine("Enter new First Name: ");
                                    result = Console.ReadLine();
                                    output = "StudentFName";
                                    break;
                                case 2:
                                    Console.WriteLine("Enter new Middle Name: ");
                                    result = Console.ReadLine();
                                    output = "StudentMName";
                                    break;
                                case 3:
                                    Console.WriteLine("Enter new Last name: ");
                                    result = Console.ReadLine();
                                    output = "StudentLName";
                                    break;
                                case 4:
                                    Console.WriteLine("Enter new Dob: ");
                                    result = Console.ReadLine();
                                    output = "DOB";
                                    break;
                                case 5:
                                    Console.WriteLine("Enter new gender: ");
                                    result = Console.ReadLine();
                                    output = "Gender";
                                    break;
                                case 6:
                                    Console.WriteLine("Enter new address: ");
                                    result = Console.ReadLine();
                                    output = "StudentAddress";
                                    break;
                                case 7:
                                    Console.WriteLine("Enter new phone number: ");
                                    result = Console.ReadLine();
                                    output = "PhoneNumber";
                                    break;
                                default:
                                    Console.WriteLine("Enter valid option: ");
                                    break;
                            }

                            string updateQuery = $"UPDATE INFO SET {output} = @Result WHERE USN = @Uusn";
                            SqlCommand updateCommand = new SqlCommand(updateQuery, sqlConnection);
                            updateCommand.Parameters.AddWithValue("@Result", result);
                            updateCommand.Parameters.AddWithValue("@Uusn", Uusn);
                            updateCommand.ExecuteNonQuery();
                            Console.WriteLine("DATA UPDATED SUCCESSFULLY!");
                        }
                        else
                        {
                            Console.WriteLine("USN does not exist.");
                        }

                        break;
                    case 4:
                        Console.WriteLine("Enter the usn of the student for deletion : ");
                        string Dusn = Console.ReadLine();
                        string deleteQuery = "DELETE FROM INFO WHERE USN = '" + Dusn + "' ";
                        SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlConnection);
                        deleteCommand.ExecuteNonQuery();
                        Console.WriteLine("DATA DELETED SUCCESSFULLY!");
                        break;

                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }

                Console.WriteLine("Enter 1 to continue or 0 to terminate ");
                int option = int.Parse(Console.ReadLine());
                op = (option == 1);
            }
           

     

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
