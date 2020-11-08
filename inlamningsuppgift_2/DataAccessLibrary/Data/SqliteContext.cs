using DataAccessLibrary.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DataAccessLibrary.Data
{
    public static class SqliteContext
    {
        #region Configuration and Properties

        private static string _dbpath { get; set; }

        public static async void UseSQLite(string databaseName = "sqlite4.db")
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync(databaseName, CreationCollisionOption.OpenIfExists);
            _dbpath = $"Filename={Path.Combine(ApplicationData.Current.LocalFolder.Path, databaseName)}";

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();
                var query = "CREATE TABLE IF NOT EXISTS Customers (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, FirstName TEXT NOT NULL, LastName TEXT NOT NULL, Created DATETIME NOT NULL); CREATE TABLE IF NOT EXISTS Issues (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, CustomerId INTEGER NOT NULL, Title TEXT NOT NULL, Description TEXT NOT NULL, Status TEXT NOT NULL, Created DATETIME NOT NULL, FOREIGN KEY (CustomerId) REFERENCES Customers(Id)); CREATE TABLE IF NOT EXISTS Comments (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, IssueId INTEGER NOT NULL, Description TEXT NOT NULL, Created DATETIME NOT NULL, FOREIGN KEY (IssueId) REFERENCES Issues(Id));";
                var cmd = new SqliteCommand(query, db);

                await cmd.ExecuteNonQueryAsync();
                db.Close();
            }
        }

        #endregion

        //*******************************************************************************************************************************
        #region Create Methods 

        public static async Task<long> CreateCustomerAsync(Customer customer)
        {
            long id = 0;

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();

                var query = "INSERT INTO Customers VALUES(null,@FirstName,@LastName,@Created);";
                var cmd = new SqliteCommand(query, db);

                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@Created", DateTime.Now);
                await cmd.ExecuteNonQueryAsync();

                cmd.CommandText = "SELECT last_insert_rowid()";
                id = (long)await cmd.ExecuteScalarAsync();


                db.Close();
            }

            return id;
        }

        public static async Task<long> CreateIssueAsync(Issue issue)
        {
            long id = 0;

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();

                var query = "INSERT INTO Issues VALUES(null,@CustomerId,@Title,@Description,@Status,@Created);";
                var cmd = new SqliteCommand(query, db);

                cmd.Parameters.AddWithValue("@CustomerId", issue.CustomerId);
                cmd.Parameters.AddWithValue("@Title", issue.Title);
                cmd.Parameters.AddWithValue("@Description", issue.Description);
                cmd.Parameters.AddWithValue("@Status", "new");
                cmd.Parameters.AddWithValue("@Created", DateTime.Now);
                await cmd.ExecuteNonQueryAsync();

                cmd.CommandText = "SELECT last_insert_rowid()";
                id = (long)await cmd.ExecuteScalarAsync();

                db.Close();
            }

            return id;
        }



        public static async Task<long> CreateCommentAsync(Comment comment)
        {
            long id = 0;

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();

                var query = "INSERT INTO Comments VALUES(null,@IssueId,@Description,@Created);";
                var cmd = new SqliteCommand(query, db);

                cmd.Parameters.AddWithValue("@IssueId", comment.IssueId);
                cmd.Parameters.AddWithValue("@Description", comment.Description);
                cmd.Parameters.AddWithValue("@Created", DateTime.Now);
                await cmd.ExecuteNonQueryAsync();

                cmd.CommandText = "SELECT last_insert_rowid()";
                id = (long)await cmd.ExecuteScalarAsync();

                db.Close();
            }
            return id;
        }



        #endregion

        //*******************************************************************************************************************************
        #region Get Methods

        public static async Task<IEnumerable<Customer>> GetCustomers()
        {
            var customers = new List<Customer>();

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();

                var query = "SELECT * FROM Customers";
                var cmd = new SqliteCommand(query, db);

                var result = await cmd.ExecuteReaderAsync();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        customers.Add(new Customer(result.GetInt64(0), result.GetString(1), result.GetString(2), result.GetDateTime(3)));
                    }
                }

                db.Close();
            }

            return customers;
        }

        public static async Task<Customer> GetCustomer()
        {
            var customers = new List<Customer>();
            var customer = new Customer();

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();

                var query = "SELECT * FROM Customers";
                var cmd = new SqliteCommand(query, db);

                var result = await cmd.ExecuteReaderAsync();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        new Customer(result.GetInt64(0), result.GetString(1), result.GetString(2), result.GetDateTime(3));
                    }
                }

                db.Close();
            }

            return customer;
        }

        public static async Task<IEnumerable<string>> GetCustomerNames()
        {
            var customernames = new List<string>();

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();

                var query = "SELECT Name FROM Customers";
                var cmd = new SqliteCommand(query, db);

                var result = await cmd.ExecuteReaderAsync();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        customernames.Add(result.GetString(0));
                    }
                }

                db.Close();
            }

            return customernames;
        }

        public static async Task<Customer> GetCustomerById(long id)
        {
            var customer = new Customer();

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();

                var query = "SELECT * FROM Customers WHERE Id = @Id";
                var cmd = new SqliteCommand(query, db);

                cmd.Parameters.AddWithValue("@Id", id);
                var result = await cmd.ExecuteReaderAsync();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        customer = new Customer(result.GetInt64(0), result.GetString(1), result.GetString(2), result.GetDateTime(3));
                    }
                }

                db.Close();
            }

            return customer;
        }

        public static async Task<Customer> GetCustomerByName(string firstName)
        {
            var customer = new Customer();

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();

                var query = "SELECT * FROM Customers WHERE FirstName = @FirstName";
                var cmd = new SqliteCommand(query, db);

                cmd.Parameters.AddWithValue("@FirstName", firstName);
                var result = await cmd.ExecuteReaderAsync();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        customer = new Customer(result.GetInt64(0), result.GetString(1), result.GetString(2), result.GetDateTime(3));
                    }
                }

                db.Close();
            }

            return customer;
        }

        public static async Task<long> GetCustomerIdByName(string name)
        {
            long customerid = 0;

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();

                var query = "SELECT Id FROM Customers WHERE Name = @Name";
                var cmd = new SqliteCommand(query, db);

                cmd.Parameters.AddWithValue("@Name", name);
                customerid = (long)await cmd.ExecuteScalarAsync();

                db.Close();
            }

            return customerid;
        }

        public static async Task<ICollection<Comment>> GetCommentsByIssueId(long issueid)
        {
            var comments = new List<Comment>();

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();

                var query = "SELECT * FROM Comments WHERE IssueId = @IssueId";
                var cmd = new SqliteCommand(query, db);

                cmd.Parameters.AddWithValue("@IssueId", issueid);
                var result = await cmd.ExecuteReaderAsync();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        comments.Add(new Comment(result.GetInt64(0), result.GetInt64(1), result.GetString(2), result.GetDateTime(3)));
                    }
                }

                db.Close();
            }

            return comments;
        }

        public static async Task<IEnumerable<Issue>> GetIssues()
        {
            var issues = new List<Issue>();

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();

                var query = "SELECT * FROM Issues";
                var cmd = new SqliteCommand(query, db);

                var result = await cmd.ExecuteReaderAsync();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        var issue = new Issue(result.GetInt64(0), result.GetInt64(1), result.GetString(2), result.GetString(3), result.GetString(4), result.GetDateTime(5));
                        issue.Customer = await GetCustomerById(result.GetInt64(1));
                        issue.Comments = await GetCommentsByIssueId(result.GetInt64(1));

                        issues.Add(issue);
                    }
                }

                db.Close();
            }

            return issues;
        }


        public static async Task UpdateIssueById(long issueId, string status, string title, string description)
        {
            var Issue = new Issue();

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();

                var query = "UPDATE Issues SET Status = @Status, Title = @Title, Description = @Description WHERE Id = @Id";

                
                var cmd = new SqliteCommand(query, db);

                cmd.Parameters.AddWithValue("@Id", issueId);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Description", description);
                await cmd.ExecuteNonQueryAsync();
                

                db.Close();
            }          
        }


        public static async Task UpdateIssueStatusById(long issueId, string status)
        {
            var Issue = new Issue();

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();

                var query = "UPDATE Issues SET Status = @Status WHERE Id = @Id";


                var cmd = new SqliteCommand(query, db);

                cmd.Parameters.AddWithValue("@Id", issueId);
                cmd.Parameters.AddWithValue("@Status", status);
                await cmd.ExecuteNonQueryAsync();
                db.Close();
            }
        }
        #endregion














    }
}
