using Dapper;
using Npgsql;
using System.Configuration;
using System.Data;
using System.Globalization;
using WizzyLiConsoleApp.Models;

namespace WizzyLiConsoleApp.Data
{
    internal class PostgresDataAccess
    {
        // variable to store the connection string
        private static string connectionString = LoadConnectionString();

        private static string LoadConnectionString(string id = "Default")
        {
            // Return the connection string stored in the configuration file
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        // ###### Here starts CRUD operations ( Create, Read-retrieve, Update, and Delete) ######

        // Create user
        public static void CreateNewUser(UserData user)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    user.user_password = UserAuthenticator.GetHashPassword(user);
                    string sql = "INSERT INTO user_account (username, email, user_password, first_name, last_name, date_of_birth, address, phone, is_verified, user_role) " +
                "VALUES (@username, @email, @user_password, @first_name, @last_name, @date_of_birth, @address, @phone, @is_verified, @user_role)";

                    var parameters = new DynamicParameters(user); // Use parameterized queries to prevent SQL injection attacks
                    connection.Execute(sql, parameters);
                }
                catch (Exception ex)
                {
                    throw new Exception("Ops! Something happened... Error creating user", ex);
                }
            }
        }

        // Retrieve a user by ID
        public static UserData GetUserById(int userId)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM user_account WHERE id = @id";
                    return connection.QuerySingleOrDefault<UserData>(sql, new { id = userId });
                }
                catch (Exception ex)
                {
                    throw new Exception("Ops! Something happened... Error getting user by id", ex);
                }
            }
        }

        // Retrieve all users
        public static List<UserData> GetAllUsers()
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM user_account";
                    return connection.Query<UserData>(sql).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("Ops! Something happened... Error getting a list of users", ex);
                }
            }
        }

        // Update a user

        public static void UpdateUser(UserData user)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    user.user_password = UserAuthenticator.GetHashPassword(user);
                    string sql = "UPDATE user_account SET " +
                                 "username = @username, " +
                                 "email = @email, " +
                                 "user_password = @user_password, " +
                                 "first_name = @first_name, " +
                                 "last_name = @last_name, " +
                                 "date_of_birth = @date_of_birth, " +
                                 "address = @address, " +
                                 "phone = @phone, " +
                                 "updated_at = now()" +
                                 "WHERE id = @id";

                    // Use parameterized queries to prevent SQL injection attacks
                    var parameters = new DynamicParameters(user);
                    connection.Execute(sql, parameters);
                }
                catch (NpgsqlException ex)
                {
                    throw new Exception("Ops! Something happened... Error updating user", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("Ops! Something happened...", ex);
                }
            }
        }

        // Delete a user by ID
        public static void DeleteUser(int userId)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "DELETE FROM user_account WHERE id = @id";
                    connection.Execute(sql, new { id = userId }); // Execute the SQL statement with the given user ID parameter
                }
                catch (Exception ex)
                {
                    throw new Exception("Ops! Something happened... Error deleting user", ex);
                }
            }
        }


        // Define Create a project
        public static void CreateProject(ProjectData project)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql = "INSERT INTO project (user_id, project_name, description, start_date, end_date) " +
                                     "VALUES (@user_id, @project_name, @description, @start_date, @end_date)";


                        var parameters = new DynamicParameters(project); // Use parameterized queries to prevent SQL injection attacks
                        connection.Execute(sql, parameters, transaction: transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Ops! Something happened... Error creating project", ex);
                    }
                }
            }
        }

        // Retrieve a project by ID
        public static ProjectData GetProjectById(int projectId)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM project WHERE id = @id";
                    return connection.QuerySingleOrDefault<ProjectData>(sql, new { id = projectId });
                }
                catch (Exception ex)
                {
                    throw new Exception("Ops! Something happened... Error getting project by id", ex);
                }
            }
        }

        // Update a project
        public static void UpdateProject(ProjectData project)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql = "UPDATE project SET project_name = @project_name, description = @description, " +
                                     "start_date = @start_date, end_date = @end_date, updated_at = now() " +
                                     "WHERE id = @id";

                        var parameters = new DynamicParameters(project); // Use parameterized queries to prevent SQL injection attacks
                        connection.Execute(sql, parameters, transaction: transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Ops! Something happened... Error updating project", ex);
                    }
                }
            }
        }

        // Delete a project by ID
        public static void DeleteProject(int projectId)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql = "DELETE FROM project WHERE id = @id";
                        connection.Execute(sql, new { id = projectId }, transaction: transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Ops! Something happened... Error deleting project", ex);
                    }
                }
            }
        }


        // Create a category

        public static void CreateCategory(CategoryData category)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql = "INSERT INTO category (category_name, description) " +
                                     "VALUES (@name, @description)";

                        var parameters = new DynamicParameters(category); // Use parameterized queries to prevent SQL injection attacks
                        connection.Execute(sql, parameters, transaction: transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Ops! Something happened... Error creating a category", ex);
                    }
                }
            }
        }

        // Retrieve category by ID
        public static CategoryData GetCategoryById(int categoryId)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM category WHERE id = @id";
                    return connection.QuerySingleOrDefault<CategoryData>(sql, new { id = categoryId });
                }
                catch (Exception ex)
                {
                    throw new Exception("Ops! Something happened... Error getting category by id", ex);
                }
            }
        }

        // Retrieve category by Name
        public static CategoryData GetCategoryByName(string categoryName)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM category WHERE category_name = @category_name";
                    return connection.QuerySingleOrDefault<CategoryData>(sql, new { category_name = categoryName });
                }
                catch (Exception ex)
                {
                    throw new Exception("Ops! Something happened... Error getting category by name", ex);
                }
            }
        }

        // Update a category
        public static void UpdateCategory(CategoryData category)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql = "UPDATE category SET category_name = @category_name, description = @description, " +
                                     "updated_at = now() " +
                                     "WHERE id = @id ";

                        var parameters = new DynamicParameters(category); // Use parameterized queries to prevent SQL injection attacks
                        connection.Execute(sql, parameters, transaction: transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Ops! Something happened... Error updating category", ex);
                    }
                }
            }
        }


        // Create a budget
        public static void CreateBudget(BudgetData budget)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql = "INSERT INTO budget (project_id, category_id, budget_name, amount, period_start, period_end) " +
                                     "VALUES (@project_id, @category_id, @budget_name, @amount, @period_start, @period_end) ";

                        var parameters = new DynamicParameters(budget); // Use parameterized queries to prevent SQL injection attacks
                        connection.Execute(sql, parameters, transaction: transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Ops! Something happened... Error creating budget", ex);
                    }
                }
            }
        }

        // Retrieve a budget by ID
        public static BudgetData GetBudgetById(int budgetId)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM budget WHERE id = @id";
                    return connection.QuerySingleOrDefault<BudgetData>(sql, new { id = budgetId });
                }
                catch (Exception ex)
                {
                    throw new Exception("Ops! Something happened... Error getting budget by id", ex);
                }
            }
        }

        // Update a budget
        public static void UpdateBudget(BudgetData budget)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql = "UPDATE budget SET budget_name = @budget_name, amount = @amount, " +
                                     "period_start = @period_start, period_end = @period_end, updated_at = now() " +
                                     "WHERE id = @id";

                        var parameters = new DynamicParameters(budget); // Use parameterized queries to prevent SQL injection attacks
                        connection.Execute(sql, parameters, transaction: transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Ops! Something happened... Error updating budget", ex);
                    }
                }
            }
        }

        // Delete a budget by ID
        public static void DeleteBudget(int budgetId)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql = "DELETE FROM budget WHERE id = @id";
                        connection.Execute(sql, new { id = budgetId }, transaction: transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Ops! Something happened... Error deleting budget", ex);
                    }
                }
            }
        }

        // Create a expense
        public static void CreateExpense(ExpenseData expense)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql = "INSERT INTO expense (project_id, budget_id, expense_name, description, amount, period_start, period_end) " +
                                     "VALUES (@project_id, @budget_id, @expense_name, @description, @amount, @period_start, @period_end)";

                        var parameters = new DynamicParameters(expense); // Use parameterized queries to prevent SQL injection attacks
                        connection.Execute(sql, parameters, transaction: transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Ops! Something happened... Error creating expense", ex);
                    }
                }
            }
        }

        // Retrieve a expense by ID
        public static ExpenseData GetExpenseById(int expenseId)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM expense WHERE id = @id";
                    return connection.QuerySingleOrDefault<ExpenseData>(sql, new { id = expenseId });
                }
                catch (Exception ex)
                {
                    throw new Exception("Ops! Something happened... Error getting expense by id", ex);
                }
            }
        }

        // Update a expense
        public static void UpdateExpense(ExpenseData expense)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql = "UPDATE expense SET expense_name = @expense_name, description = @description, amount = @amount, " +
                                     "period_start = @period_start, period_end = @period_end, updated_at = now() " +
                                     "WHERE id = @id";

                        var parameters = new DynamicParameters(expense); // Use parameterized queries to prevent SQL injection attacks
                        connection.Execute(sql, parameters, transaction: transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Ops! Something happened... Error updating expense", ex);
                    }
                }
            }
        }

        // Delete a expense by ID
        public static void DeleteExpense(int expenseId)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql = "DELETE FROM expense WHERE id = @id";
                        connection.Execute(sql, new { id = expenseId }, transaction: transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Ops! Something happened... Error deleting expense", ex);
                    }
                }
            }
        }


        // Create a income
        public static void CreateIncome(IncomeData income)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql = "INSERT INTO income (project_id, budget_id, income_name, description, amount, period_start, period_end) " +
                                     "VALUES (@project_id, @budget_id, @income_name, @description, @amount, @period_start, @period_end)";

                        var parameters = new DynamicParameters(income); // Use parameterized queries to prevent SQL injection attacks
                        connection.Execute(sql, parameters, transaction: transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Ops! Something happened... Error creating income", ex);
                    }
                }
            }
        }

        // Retrieve a income by ID
        public static IncomeData GetIncomeById(int incomeId)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM income WHERE id = @id";
                    return connection.QuerySingleOrDefault<IncomeData>(sql, new { id = incomeId });
                }
                catch (Exception ex)
                {
                    throw new Exception("Ops! Something happened... Error getting income by id", ex);
                }
            }
        }

        // Update a income
        public static void UpdateIncome(IncomeData income)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql = "UPDATE income SET income_name = @income_name, description = @description, amount = @amount, " +
                                     "period_start = @period_start, period_end = @period_end, updated_at = now() " +
                                     "WHERE id = @id";

                        var parameters = new DynamicParameters(income); // Use parameterized queries to prevent SQL injection attacks
                        connection.Execute(sql, parameters, transaction: transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Ops! Something happened... Error updating income", ex);
                    }
                }
            }
        }

        // Delete a income by ID
        public static void DeleteIncome(int incomeId)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql = "DELETE FROM income WHERE id = @id";
                        connection.Execute(sql, new { id = incomeId }, transaction: transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Ops! Something happened... Error deleting income", ex);
                    }
                }
            }
        }
    }
}
