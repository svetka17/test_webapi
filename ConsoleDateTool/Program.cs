using System;
using System.Configuration;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Data.SqlClient;

namespace ConsoleDateTool
{

    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static readonly string connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;"
            + "Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;App=EntityFramework";

        private class DateInterval
        {
            public DateTime dateFrom { get; set; }
            public DateTime dateTo { get; set; }

            public DateInterval(DateTime dateFrom, DateTime dateTo)
            {
                this.dateFrom = dateFrom;
                this.dateTo = dateTo;
            }
        }

        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 3)
                {
                    throw new FormatException($"Invalid arguments. Command format: [insert/select] [yyyy-MM-dd] [yyyy-MM-dd]");
                }

                string format = "yyyy-MM-dd";

                if (!DateTime.TryParseExact(args[1], format, new CultureInfo("en-US"), DateTimeStyles.None, out DateTime dateFrom))
                {
                    throw new FormatException($"{args[1]} is not in the correct format.");
                }

                if (!DateTime.TryParseExact(args[2], format, new CultureInfo("en-US"), DateTimeStyles.None, out DateTime dateTo))
                {
                    throw new FormatException($"{args[2]} is not in the correct format.");
                }

                if (dateTo < dateFrom)
                {
                    throw new FormatException($"The first date must be less than the second");
                }

                switch (args[0])
                {
                    case "insert":
                        Insert(dateFrom, dateTo).Wait();
                        break;
                    case "select":
                        Select(dateFrom, dateTo).Wait();
                        break;
                    default:
                        Console.WriteLine("Invalid command");
                        AddLog("Invalid command");
                        break;
                }
            }
            catch (Exception e) {
                Console.WriteLine($"Error: {e.Message}\n  {e.InnerException?.Message}");
                AddLog($"Error: {e.Message}\n  {e.InnerException?.Message}");
            }
        }

        static async Task Insert(DateTime dateFrom, DateTime dateTo)
        {  
            DateInterval dateInterval = new DateInterval(dateFrom, dateTo);
            string host = ConfigurationManager.AppSettings["host"];
            HttpContent content = JsonContent.Create(dateInterval);
            HttpResponseMessage response = await client.PostAsync($"http://{host}/api/dateintervals/insert", content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
            AddLog(responseBody);
        }

        static async Task Select(DateTime dateFrom, DateTime dateTo)
        {
            //try
            //{
                string host = ConfigurationManager.AppSettings["host"];
                HttpResponseMessage response = await client.GetAsync($"http://{host}/api/dateintervals/select?ticksFrom={dateFrom.Ticks}&ticksTo={dateTo.Ticks}");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                AddLog(responseBody);
            //}
            //catch (HttpRequestException e)
            //{
            //    Console.WriteLine("\nException Caught!");
            //    Console.WriteLine("Message :{0} ", e.Message);
            //}
        }

        static void AddLog(string message)
        {
            //string sqlExpression = String.Format("INSERT INTO AppLogs (DateLog, Message) VALUES ({0}, '{1}')", DateTime.Now, message);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlCommand command = new SqlCommand("INSERT INTO AppLogs (DateLog, Message) VALUES (@DT, @message)", connection);
                command.Parameters.AddWithValue("@DT", DateTime.Now);
                command.Parameters.AddWithValue("@message", message);
                int number = command.ExecuteNonQuery();
                //Console.WriteLine("Добавлено объектов: {0}", number);
            }
        }
    }
}
