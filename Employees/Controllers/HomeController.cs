using Employees.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Employees.Controllers
{
    public class HomeController : Controller
    {
        // Azione iniziale, visualizzo tutti i dipendenti
        public ActionResult Index()
        {
            // Istanza apertura connessione al database
            SqlConnection dbConn = new SqlConnection();

            // Dichiaro la lista dei dipendenti
            List<Employee> employeeList = new List<Employee>();

            try
            {
                // Mi connetto al database
                dbConn.ConnectionString = ConfigurationManager.ConnectionStrings["dbConn"].ToString();
                dbConn.Open();

                // Preparazione della query
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Employees";

                cmd.Connection = dbConn;

                // Lettura della query
                SqlDataReader reader = cmd.ExecuteReader();

                // Solo se la query restituisce dei records allora ciclo...
                if (reader.HasRows)
                {
                    while (reader.Read()) // Finché riesco a leggere...
                    {
                        Employee emp = new Employee
                        {
                            ID = Convert.ToInt32(reader["id"]),
                            FirstName = reader["first_name"].ToString(),
                            LastName = reader["last_name"].ToString(),
                            Age = Convert.ToInt32(reader["age"]),
                            Email = reader["email"].ToString(),
                            Gender = reader["gender"].ToString(),
                            JobTitle = reader["job_title"].ToString(),
                            Salary = Convert.ToDouble(reader["salary"]),
                            HireDate = Convert.ToDateTime(reader["hire_data"]),
                            Department = reader["department"].ToString()
                        };
                        employeeList.Add(emp);
                    }
                }
            } 
            catch(Exception) 
            {
                // Qui gestisco gli errori
                dbConn.Close();
            }

            // Chiudo la connessione al database
            dbConn.Close();

            // Restituisco la lista dei dipendenti
            return View(employeeList);
        }

        // Per aggiungere un dipendente
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee emp) 
        { 
            SqlConnection dbConn = new SqlConnection();

            try
            {
                dbConn.ConnectionString = ConfigurationManager.ConnectionStrings["dbConn"].ToString();
                dbConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@first_name", emp.FirstName);
                cmd.Parameters.AddWithValue("@last_name", emp.LastName);
                cmd.Parameters.AddWithValue("@age", emp.Age);
                cmd.Parameters.AddWithValue("@email", emp.Email);
                cmd.Parameters.AddWithValue("@gender", emp.Gender);
                cmd.Parameters.AddWithValue("@job_title", emp.JobTitle);
                cmd.Parameters.AddWithValue("@salary", emp.Salary);
                cmd.Parameters.AddWithValue("@hire_data", emp.HireDate);
                cmd.Parameters.AddWithValue("@department", emp.Department);

                cmd.CommandText = "INSERT INTO Employees VALUES (@first_name, @last_name, @age, @email, @gender, @job_title, @salary, @hire_data, @department)";
                cmd.Connection = dbConn;

                // Eseguo la query
                cmd.ExecuteNonQuery();
            }
            catch(Exception) 
            { 
                // Qui gestisco gli errori
                dbConn.Close();
            }

            // Chiudo la connessione al database
            dbConn.Close();

            // Reindirizzo alla index
            return RedirectToAction("Index");
        }

        // Per aggiornare i dati di un dipendente
        public ActionResult Edit(int ID)
        {
            // Istanza apertura connessione al database
            SqlConnection dbConn = new SqlConnection();

            // Dichiarazione dipendente
            Employee emp = new Employee();

            try
            {
                dbConn.ConnectionString = ConfigurationManager.ConnectionStrings["dbConn"].ToString();
                dbConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@ID", ID);

                cmd.CommandText = "SELECT * FROM Employees WHERE id = @ID";
                cmd.Connection = dbConn;

                // Eseguo la query
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while(reader.Read()) 
                    {
                        emp.ID = Convert.ToInt32(reader["id"]);
                        emp.FirstName = reader["first_name"].ToString();
                        emp.LastName = reader["last_name"].ToString();
                        emp.Age = Convert.ToInt32(reader["age"]);
                        emp.Email = reader["email"].ToString();
                        emp.Gender = reader["gender"].ToString();
                        emp.JobTitle = reader["job_title"].ToString();
                        emp.Salary = Convert.ToDouble(reader["salary"]);
                        emp.HireDate = Convert.ToDateTime(reader["hire_data"]);
                        emp.Department = reader["department"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                // Qui gestisco gli errori
                dbConn.Close();
            }

            // Chiudo la connessione al database
            dbConn.Close();

            // Restituisco i dati del dipendente
            return View(emp);
        }

        [HttpPost]
        public ActionResult Edit(Employee emp) 
        {
            // Istanza apertura connessione al database
            SqlConnection dbConn = new SqlConnection();

            try
            {
                dbConn.ConnectionString = ConfigurationManager.ConnectionStrings["dbConn"].ToString();
                dbConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@id", emp.ID);
                cmd.Parameters.AddWithValue("@first_name", emp.FirstName);
                cmd.Parameters.AddWithValue("@last_name", emp.LastName);
                cmd.Parameters.AddWithValue("@age", emp.Age);
                cmd.Parameters.AddWithValue("@email", emp.Email);
                cmd.Parameters.AddWithValue("@gender", emp.Gender);
                cmd.Parameters.AddWithValue("@job_title", emp.JobTitle);
                cmd.Parameters.AddWithValue("@salary", emp.Salary);
                cmd.Parameters.AddWithValue("@hire_data", emp.HireDate);
                cmd.Parameters.AddWithValue("@department", emp.Department);

                cmd.CommandText = "UPDATE Employees SET first_name = @first_name, last_name = @last_name, age = @age, email = @email, gender = @gender, job_title = @job_title, salary = @salary, hire_data = @hire_data, department = @department WHERE id = @id";
                cmd.Connection = dbConn;

                // Eseguo la query
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                // In caso di errore chiudo la connessione al database
                dbConn.Close();
            }

            // Chiudo la connessione al database
            dbConn.Close();

            // Reindirizzo alla pagina iniziale
            return RedirectToAction("Index");
        }

        // Per eliminare un dipendente
        //public ActionResult Delete(int ID) 
        //{
        //    // Istanza apertura connessione al database
        //    SqlConnection dbConn = new SqlConnection();

        //    try
        //    {
        //        dbConn.ConnectionString = ConfigurationManager.ConnectionStrings["dbConn"].ToString();
        //        dbConn.Open();

        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Parameters.AddWithValue("@ID", ID);

        //        // Preparazione della query per eliminare il dipendente
        //        cmd.CommandText = "DELETE FROM Employees WHERE id = @ID";
        //        cmd.Connection = dbConn;

        //        // Eseguo la query
        //        cmd.ExecuteNonQuery();

        //    }
        //    catch (Exception)
        //    {
        //        // Qui gestisco gli errori
        //        dbConn.Close();
        //    }

        //    // Chiudo la connessione al database
        //    dbConn.Close();

        //    // Reindirizzo alla Index
        //    return RedirectToAction("Index");
        //}
        [HttpGet]
        public ActionResult Delete(int ID) 
        {
            // Istanza apertura connessione al database
            SqlConnection dbConn = new SqlConnection();

            Employee emp = new Employee();

            try
            {
                dbConn.ConnectionString = ConfigurationManager.ConnectionStrings["dbConn"].ToString();
                dbConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@ID", ID);

                // Preparazione della query per eliminare il dipendente
                cmd.CommandText = "SELECT first_name, last_name FROM Employees WHERE id = @ID";
                cmd.Connection = dbConn;

                // Eseguo la query
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows) 
                { 
                    while (reader.Read()) 
                    {
                        emp.ID = ID;
                        emp.FirstName = reader["first_name"].ToString();
                        emp.LastName = reader["last_name"].ToString();
                    }
                }

            }
            catch (Exception)
            {
                // Qui gestisco gli errori
                dbConn.Close();
            }

            // Chiudo la connessione al database
            dbConn.Close();

            // Restituisco le info del dipendente da eliminare
            return View(emp);
        }

        [HttpPost]
        [ActionName("Delete")] // Forzatura della rotta perché entrambi i metodi hanno la stessa firma
        public ActionResult ConfirmDelete(int ID) 
        {
            // Istanza apertura connessione al database
            SqlConnection dbConn = new SqlConnection();

            try
            {
                dbConn.ConnectionString = ConfigurationManager.ConnectionStrings["dbConn"].ToString();
                dbConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@ID", ID);

                // Preparazione della query per eliminare il dipendente
                cmd.CommandText = "DELETE FROM Employees WHERE id = @ID";
                cmd.Connection = dbConn;

                // Eseguo la query
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                // Qui gestisco gli errori
                dbConn.Close();
            }

            // Chiudo la connessione al database
            dbConn.Close();

            // Reindirizzo alla Index
            return RedirectToAction("Index");
        }
    }
}