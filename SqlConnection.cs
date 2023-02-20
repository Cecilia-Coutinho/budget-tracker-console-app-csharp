using Dapper;
using Npgsql;
using System.Configuration;
using System.Data;
using System.Globalization;

namespace WizzyLiConsoleApp
{
    internal class SqlConnection
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
                connection.Open();
                user.user_password = UserAuthenticator.GetHashPassword(user);
                string sql = "INSERT INTO budget_users (username, user_email, user_password, first_name, last_name, date_of_birth, user_address, user_phone, is_verified, user_role) " +
            "VALUES (@username, @user_email, @user_password, @first_name, @last_name, @date_of_birth, @user_address, @user_phone, @is_verified, @user_role)";
                try
                {

                    connection.Execute(sql, user);
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
                connection.Open();
                string sql = "SELECT * FROM budget_users WHERE user_id = @user_id";

                try
                {
                    return connection.QuerySingleOrDefault<UserData>(sql, new { user_id = userId });
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
                connection.Open();
                string sql = "SELECT * FROM budget_users";

                try
                {
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
                connection.Open();
                user.user_password = UserAuthenticator.GetHashPassword(user);
                string sql = "UPDATE budget_users SET " +
                             "username = @username, " +
                             "user_email = @user_email, " +
                             "user_password = @user_password, " +
                             "first_name = @first_name, " +
                             "last_name = @last_name, " +
                             "date_of_birth = @date_of_birth, " +
                             "user_address = @user_address, " +
                             "user_phone = @user_phone, " +
                             "user_updated_at = now()" +
                             "WHERE user_id = @user_id";

                // Use parameterized queries to prevent SQL injection attacks
                //var userParameters = new
                //{
                //    username = user.username,
                //    user_email = user.user_email,
                //    user_password = user.user_password,
                //    first_name = user.first_name,
                //    last_name = user.last_name,
                //    date_of_birth = user.date_of_birth,
                //    user_address = user.user_address,
                //    user_phone = user.user_phone,
                //    user_role = user.user_role,
                //    user_id = user.user_id
                //};

                try
                {
                    connection.Execute(sql, user);
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
                connection.Open();
                string sql = "DELETE FROM budget_users WHERE user_id = @user_id";

                try
                {
                    // Execute the SQL statement with the given user ID parameter
                    connection.Execute(sql, new { user_id = userId });
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
                        string sql = "INSERT INTO budget_projects (user_id, project_name, project_description, project_start_date, project_end_date) " +
                                     "VALUES (@user_id, @project_name, @project_description, @project_start_date, @project_end_date)";

                        connection.Execute(sql, project, transaction: transaction);
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
                connection.Open();
                string sql = "SELECT * FROM budget_projects WHERE project_id = @project_id";

                try
                {
                    return connection.QuerySingleOrDefault<ProjectData>(sql, new { project_id = projectId });
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
                        string sql = "UPDATE budget_projects SET project_name = @project_name, project_description = @project_description, " +
                                     "project_start_date = @project_start_date, project_end_date = @project_end_date, project_updated_at = GETDATE() " +
                                     "WHERE project_id = @project_id";

                        connection.Execute(sql, project, transaction: transaction);
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
                        string sql = "DELETE FROM budget_projects WHERE project_id = @project_id";

                        connection.Execute(sql, new { project_id = projectId }, transaction: transaction);
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
                        string sql = "INSERT INTO budget_categories (category_name, category_description) " +
                                     "VALUES (@category_name, @category_description)";

                        connection.Execute(sql, category, transaction: transaction);
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
                connection.Open();
                string sql = "SELECT * FROM budget_categories WHERE category_id = @category_id";

                try
                {
                    return connection.QuerySingleOrDefault<CategoryData>(sql, new { category_id = categoryId });
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
                connection.Open();
                string sql = "SELECT * FROM budget_categories WHERE category_name = @category_name";

                try
                {
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
                        string sql = "UPDATE budget_categories SET category_name = @category_name, category_description = @category_description, " +
                                     "category_updated_at = GETDATE() " +
                                     "WHERE category_id = @category_id ";

                        connection.Execute(sql, category, transaction: transaction);
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
                        string sql = "INSERT INTO budget_budgets (project_id, category_id, budget_name, budget_amount, budget_period_start, budget_period_end) " +
                                     "VALUES (@project_id, @category_id, @budget_name, @budget_amount, @budget_period_start, @budget_period_end)";

                        connection.Execute(sql, budget, transaction: transaction);
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
                connection.Open();
                string sql = "SELECT * FROM budget_budgets WHERE budget_id = @budget_id";

                try
                {
                    return connection.QuerySingleOrDefault<BudgetData>(sql, new { budget_id = budgetId });
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
                        string sql = "UPDATE budget_budgets SET budget_name = @budget_name, budget_amount = @budget_amount, " +
                                     "budget_period_start = @budget_period_start, budget_period_end = @budget_period_end, budget_updated_at = GETDATE() " +
                                     "WHERE budget_id = @budget_id";

                        connection.Execute(sql, budget, transaction: transaction);
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
                        string sql = "DELETE FROM budget_budgets WHERE budget_id = @budget_id";

                        connection.Execute(sql, new { budget_id = budgetId }, transaction: transaction);
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
                        string sql = "INSERT INTO budget_expenses (project_id, budget_id, expense_name, expense_description, expense_amount, expense_period_start, expense_period_end) " +
                                     "VALUES (@project_id, @budget_id, @expense_name, @expense_description, @expense_amount, @expense_period_start, @expense_period_end)";

                        connection.Execute(sql, expense, transaction: transaction);
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
                connection.Open();
                string sql = "SELECT * FROM budget_expenses WHERE expense_id = @expense_id";

                try
                {
                    return connection.QuerySingleOrDefault<ExpenseData>(sql, new { expense_id = expenseId });
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
                        string sql = "UPDATE budget_expenses SET expense_name = @expense_name, expense_description = @expense_description, expense_amount = @expense_amount, " +
                                     "expense_period_start = @expense_period_start, expense_period_end = @expense_period_end, expense_updated_at = GETDATE() " +
                                     "WHERE expense_id = @expense_id";

                        connection.Execute(sql, expense, transaction: transaction);
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
                        string sql = "DELETE FROM budget_expenses WHERE expense_id = @expense_id";

                        connection.Execute(sql, new { expense_id = expenseId }, transaction: transaction);
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
                        string sql = "INSERT INTO budget_incomes (project_id, budget_id, income_name, income_description, income_amount, income_period_start, income_period_end) " +
                                     "VALUES (@project_id, @budget_id, @income_name, @income_description, @income_amount, @income_period_start, @income_period_end)";

                        connection.Execute(sql, income, transaction: transaction);
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
                connection.Open();
                string sql = "SELECT * FROM budget_incomes WHERE income_id = @income_id";

                try
                {
                    return connection.QuerySingleOrDefault<IncomeData>(sql, new { income_id = incomeId });
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
                        string sql = "UPDATE budget_incomes SET income_name = @income_name, income_description = @income_description, income_amount = @income_amount, " +
                                     "income_period_start = @income_period_start, income_period_end = @income_period_end, income_updated_at = GETDATE() " +
                                     "WHERE income_id = @income_id";

                        connection.Execute(sql, income, transaction: transaction);
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
                        string sql = "DELETE FROM budget_incomes WHERE income_id = @income_id";

                        connection.Execute(sql, new { income_id = incomeId }, transaction: transaction);
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
