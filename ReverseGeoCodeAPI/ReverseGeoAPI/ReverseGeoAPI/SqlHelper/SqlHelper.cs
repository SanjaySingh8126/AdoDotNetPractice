namespace ReverseGeoAPI.SqlHelper
{
	//public class SqlHelper //: DbContext
	//{
	//	private static string _conn = string.Empty;
	//	private const int _minParametersLength = 1;

	//	//private SqlHelper()//IOptions<ConnectionStringModel> app)
	//	//{
	//	//    //_conn = app.Value.DbConn;
	//	//}

	//	//public SqlHelper(DbContextOptions<SqlHelper> options):base(options)
	//	//{
	//	//}

	//	#region private utility methods & constructors

	//	// Since this class provides only static methods, make the default constructor private to prevent
	//	// instances from being created with "new sqlDAL()"
	//	public SqlHelper() { }

	//	/// <summary>
	//	/// This method is used to attach array of SqlParameters to a SqlCommand.
	//	///
	//	/// This method will assign a value of DbNull to any parameter with a direction of
	//	/// InputOutput and a value of null.
	//	///
	//	/// This behavior will prevent default values from being used, but
	//	/// this will be the less common case than an intended pure output parameter (derived as InputOutput)
	//	/// where the user provided no input value.
	//	/// </summary>
	//	/// <param name="command">The command to which the parameters will be added</param>
	//	/// <param name="commandParameters">An array of SqlParameters to be added to command</param>
	//	private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
	//	{
	//		if (command == null) throw new ArgumentNullException("command");
	//		if (commandParameters != null)
	//		{
	//			foreach (SqlParameter p in commandParameters)
	//			{
	//				if (p != null)
	//				{
	//					// Check for derived output value with no value assigned
	//					if ((p.Direction == ParameterDirection.InputOutput ||
	//						p.Direction == ParameterDirection.Input) &&
	//						(p.Value == null))
	//					{
	//						p.Value = DBNull.Value;
	//					}
	//					command.Parameters.Add(p);
	//				}
	//			}
	//		}
	//	}

	//	/// <summary>
	//	/// This method assigns dataRow column values to an array of SqlParameters
	//	/// </summary>
	//	/// <param name="commandParameters">Array of SqlParameters to be assigned values</param>
	//	/// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values</param>
	//	private static void AssignParameterValues(SqlParameter[] commandParameters, DataRow dataRow)
	//	{
	//		if ((commandParameters == null) || (dataRow == null))
	//		{
	//			// Do nothing if we get no data
	//			return;
	//		}

	//		int? i = 0;
	//		// Set the parameters values
	//		foreach (SqlParameter commandParameter in commandParameters)
	//		{
	//			// Check the parameter name
	//			if (dataRow.Table.Columns.IndexOf(commandParameter.ParameterName.Substring(1)) != -1)
	//				commandParameter.Value = dataRow[commandParameter.ParameterName.Substring(1)];
	//			i++;
	//		}
	//	}

	//	/// <summary>
	//	/// This method assigns an array of values to an array of SqlParameters
	//	/// </summary>
	//	/// <param name="commandParameters">Array of SqlParameters to be assigned values</param>
	//	/// <param name="parameterValues">Array of objects holding the values to be assigned</param>
	//	private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
	//	{
	//		if ((commandParameters == null) || (parameterValues == null))
	//		{
	//			// Do nothing if we get no data
	//			return;
	//		}

	//		// We must have the same number of values as we pave parameters to put them in
	//		if (commandParameters.Length != parameterValues.Length)
	//		{
	//			throw new ArgumentException("Parameter count does not match Parameter Value count.");
	//		}

	//		// Iterate through the SqlParameters, assigning the values from the corresponding position in the
	//		// value array
	//		for (int i = 0, j = commandParameters.Length; i < j; i++)
	//		{
	//			// If the current array value derives from IDbDataParameter, then assign its Value property
	//			IDbDataParameter parameterValuesRef = parameterValues[i] as IDbDataParameter;
	//			if (parameterValuesRef != null)
	//			{
	//				IDbDataParameter paramInstance = (IDbDataParameter)parameterValues[i];
	//				if (paramInstance.Value == null)
	//				{
	//					commandParameters[i].Value = DBNull.Value;
	//				}
	//				else
	//				{
	//					commandParameters[i].Value = paramInstance.Value;
	//				}
	//			}
	//			else if (parameterValues[i] == null)
	//			{
	//				commandParameters[i].Value = DBNull.Value;
	//			}
	//			else
	//			{
	//				commandParameters[i].Value = parameterValues[i];
	//			}
	//		}
	//	}

	//	/// <summary>
	//	/// This method opens (if necessary) and assigns a connection, transaction, command type and parameters
	//	/// to the provided command
	//	/// </summary>
	//	/// <param name="command">The SqlCommand to be prepared</param>
	//	/// <param name="connection">A valid SqlConnection, on which to execute this command</param>
	//	/// <param name="transaction">A valid SqlTransaction, or 'null'</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <param name="commandParameters">An array of SqlParameters to be associated with the command or 'null' if no parameters are required</param>
	//	/// <param name="mustCloseConnection"><c>true</c> if the connection was opened by the method, otherwose is false.</param>
	//	private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
	//	{
	//		if (command == null) throw new ArgumentNullException("command");
	//		if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

	//		// If the provided connection is not open, we will open it
	//		if (connection.State != ConnectionState.Open)
	//		{
	//			mustCloseConnection = true;
	//			connection.Open();
	//		}
	//		else
	//		{
	//			mustCloseConnection = false;
	//		}

	//		// Associate the connection with the command
	//		command.Connection = connection;
	//		command.CommandTimeout = 360;//Added timeOut at 03-March-2022 after da.fill(ds) time out

	//		//if (System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"] == null)
	//		//    command.CommandTimeout = 2400;
	//		//else
	//		//    command.CommandTimeout = int.Parse(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"]);
	//		// Set the command text (stored procedure name or SQL statement)
	//		//command.CommandTimeout = 2400;
	//		command.CommandText = commandText;

	//		// If we were provided a transaction, assign it
	//		if (transaction != null)
	//		{
	//			if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
	//			command.Transaction = transaction;
	//		}

	//		// Set the command type
	//		command.CommandType = commandType;

	//		// Attach the command parameters if they are provided
	//		if (commandParameters != null)
	//		{
	//			AttachParameters(command, commandParameters);
	//		}
	//	}
	//	private static void PrepareCommand_Custome(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
	//	{
	//		if (command == null) throw new ArgumentNullException("command");
	//		if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

	//		// If the provided connection is not open, we will open it
	//		if (connection.State != ConnectionState.Open)
	//		{
	//			mustCloseConnection = true;
	//			connection.Open();
	//		}
	//		else
	//		{
	//			mustCloseConnection = false;
	//		}

	//		// Associate the connection with the command
	//		command.Connection = connection;
	//		command.CommandTimeout = 300;
	//		command.CommandText = commandText;

	//		// If we were provided a transaction, assign it
	//		if (transaction != null)
	//		{
	//			if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
	//			command.Transaction = transaction;
	//		}

	//		// Set the command type
	//		command.CommandType = commandType;

	//		// Attach the command parameters if they are provided
	//		if (commandParameters != null)
	//		{
	//			AttachParameters(command, commandParameters);
	//		}
	//	}

	//	#endregion private utility methods & constructors

	//	#region ExecuteNonQuery

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns no resultset and takes no parameters) against the database specified in
	//	/// the connection string
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  int? result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders");
	//	/// </remarks>
	//	/// <param name="connectionString">A valid connection string for a SqlConnection</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <returns>An int? representing the number of rows affected by the command</returns>
	//	internal static int? ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
	//	{
	//		// Pass through the call providing null for the set of SqlParameters
	//		return ExecuteNonQuery(connectionString, commandType, commandText, (SqlParameter[])null);
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string
	//	/// using the provided parameters
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  int? result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
	//	/// </remarks>
	//	/// <param name="connectionString">A valid connection string for a SqlConnection</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
	//	/// <returns>An int? representing the number of rows affected by the command</returns>
	//	internal static int? ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
	//	{
	//		if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");

	//		// Create & open a SqlConnection, and dispose of it after we are done
	//		using (SqlConnection connection = new SqlConnection(connectionString))
	//		{
	//			connection.Open();
	//			// Call the overload that takes a connection in place of the connection string
	//			return ExecuteNonQuery(connection, commandType, commandText, commandParameters);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns no resultset) against the database specified in
	//	/// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <remarks>
	//	/// This method provides no access to output parameters or the stored procedure's return value parameter.
	//	///
	//	/// e.g.:
	//	///  int? result = ExecuteNonQuery(connString, "PublishOrders", 24, 36);
	//	/// </remarks>
	//	/// <param name="connectionString">A valid connection string for a SqlConnection</param>
	//	/// <param name="spName">The name of the stored prcedure</param>
	//	/// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
	//	/// <returns>An int? representing the number of rows affected by the command</returns>
	//	internal static int? ExecuteNonQuery(string connectionString, string spName, params object[] parameterValues)
	//	{
	//		if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If we receive parameter values, we need to figure out where they go
	//		if ((parameterValues != null) && (parameterValues.Length > 0))
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(connectionString, spName, true);

	//			// Assign the provided values to these parameters based on parameter order
	//			AssignParameterValues(commandParameters, parameterValues);

	//			// Call the overload that takes an array of SqlParameters
	//			return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			// Otherwise we can just call the SP without params
	//			return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns no resultset and takes no parameters) against the provided SqlConnection.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  int? result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders");
	//	/// </remarks>
	//	/// <param name="connection">A valid SqlConnection</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <returns>An int? representing the number of rows affected by the command</returns>
	//	internal static int? ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText)
	//	{
	//		// Pass through the call providing null for the set of SqlParameters
	//		return ExecuteNonQuery(connection, commandType, commandText, (SqlParameter[])null);
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns no resultset) against the specified SqlConnection
	//	/// using the provided parameters.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  int? result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
	//	/// </remarks>
	//	/// <param name="connection">A valid SqlConnection</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
	//	/// <returns>An int? representing the number of rows affected by the command</returns>
	//	internal static int? ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
	//	{
	//		if (connection == null) throw new ArgumentNullException("connection");

	//		// Create a command and prepare it for execution
	//		SqlCommand cmd = new SqlCommand();
	//		bool mustCloseConnection = false;

	//		PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

	//		// Finally, execute the command
	//		int? retval = cmd.ExecuteNonQuery();

	//		// Detach the SqlParameters from the command object, so they can be used again
	//		cmd.Parameters.Clear();
	//		if (mustCloseConnection)
	//			connection.Close();

	//		//Added by Suraj 03-March-2022 To Close Connection
	//		if (connection.State == ConnectionState.Open)
	//		{
	//			connection.Close();
	//		}

	//		return retval;
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified SqlConnection
	//	/// using the provided parameter values.  This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <remarks>
	//	/// This method provides no access to output parameters or the stored procedure's return value parameter.
	//	///
	//	/// e.g.:
	//	///  int? result = ExecuteNonQuery(conn, "PublishOrders", 24, 36);
	//	/// </remarks>
	//	/// <param name="connection">A valid SqlConnection</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
	//	/// <returns>An int? representing the number of rows affected by the command</returns>
	//	internal static int? ExecuteNonQuery(SqlConnection connection, string spName, params object[] parameterValues)
	//	{
	//		if (connection == null) throw new ArgumentNullException("connection");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If we receive parameter values, we need to figure out where they go
	//		if ((parameterValues != null) && (parameterValues.Length > 0))
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(connection, spName);

	//			// Assign the provided values to these parameters based on parameter order
	//			AssignParameterValues(commandParameters, parameterValues);

	//			// Call the overload that takes an array of SqlParameters
	//			return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			// Otherwise we can just call the SP without params
	//			return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns no resultset and takes no parameters) against the provided SqlTransaction.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  int? result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders");
	//	/// </remarks>
	//	/// <param name="transaction">A valid SqlTransaction</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <returns>An int? representing the number of rows affected by the command</returns>
	//	internal static int? ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText)
	//	{
	//		// Pass through the call providing null for the set of SqlParameters
	//		return ExecuteNonQuery(transaction, commandType, commandText, (SqlParameter[])null);
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns no resultset) against the specified SqlTransaction
	//	/// using the provided parameters.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  int? result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
	//	/// </remarks>
	//	/// <param name="transaction">A valid SqlTransaction</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
	//	/// <returns>An int? representing the number of rows affected by the command</returns>
	//	internal static int? ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
	//	{
	//		if (transaction == null) throw new ArgumentNullException("transaction");
	//		if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

	//		// Create a command and prepare it for execution
	//		SqlCommand cmd = new SqlCommand();
	//		bool mustCloseConnection = false;
	//		PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

	//		// Finally, execute the command
	//		int? retval = cmd.ExecuteNonQuery();

	//		// Detach the SqlParameters from the command object, so they can be used again
	//		cmd.Parameters.Clear();
	//		return retval;
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified
	//	/// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <remarks>
	//	/// This method provides no access to output parameters or the stored procedure's return value parameter.
	//	///
	//	/// e.g.:
	//	///  int? result = ExecuteNonQuery(conn, trans, "PublishOrders", 24, 36);
	//	/// </remarks>
	//	/// <param name="transaction">A valid SqlTransaction</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
	//	/// <returns>An int? representing the number of rows affected by the command</returns>
	//	internal static int? ExecuteNonQuery(SqlTransaction transaction, string spName, params object[] parameterValues)
	//	{
	//		if (transaction == null) throw new ArgumentNullException("transaction");
	//		if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If we receive parameter values, we need to figure out where they go
	//		if ((parameterValues != null) && (parameterValues.Length > 0))
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(transaction.Connection, spName);

	//			// Assign the provided values to these parameters based on parameter order
	//			AssignParameterValues(commandParameters, parameterValues);

	//			// Call the overload that takes an array of SqlParameters
	//			return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			// Otherwise we can just call the SP without params
	//			return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	#endregion ExecuteNonQuery

	//	#region ExecuteDataset

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in
	//	/// the connection string.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders");
	//	/// </remarks>
	//	/// <param name="connectionString">A valid connection string for a SqlConnection</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <returns>A dataset containing the resultset generated by the command</returns>
	//	public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText)
	//	{
	//		// Pass through the call providing null for the set of SqlParameters
	//		return ExecuteDataset(connectionString, commandType, commandText, (SqlParameter[])null);
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in
	//	/// the connection string.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders");
	//	/// </remarks>
	//	/// <param name="connectionString">A valid connection string for a SqlConnection</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <returns>A dataset containing the resultset generated by the command</returns>
	//	public static DataSet ExecuteDataset_Custome(string connectionString, CommandType commandType, string commandText)
	//	{
	//		// Pass through the call providing null for the set of SqlParameters
	//		return ExecuteDataset_Custome(connectionString, commandType, commandText, (SqlParameter[])null);
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a resultset) against the database specified in the connection string
	//	/// using the provided parameters.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
	//	/// </remarks>
	//	/// <param name="connectionString">A valid connection string for a SqlConnection</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
	//	/// <returns>A dataset containing the resultset generated by the command</returns>
	//	internal static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
	//	{
	//		if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");

	//		// Create & open a SqlConnection, and dispose of it after we are done
	//		using (SqlConnection connection = new SqlConnection(connectionString))
	//		{
	//			connection.Open();
	//			// Call the overload that takes a connection in place of the connection string
	//			return ExecuteDataset(connection, commandType, commandText, commandParameters);
	//		}
	//	}

	//	internal static DataSet ExecuteDataset_Custome(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
	//	{
	//		if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");

	//		// Create & open a SqlConnection, and dispose of it after we are done
	//		using (SqlConnection connection = new SqlConnection(connectionString))
	//		{

	//			connection.Open();
	//			// Call the overload that takes a connection in place of the connection string
	//			return ExecuteDataset_Custome(connection, commandType, commandText, commandParameters);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in
	//	/// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <remarks>
	//	/// This method provides no access to output parameters or the stored procedure's return value parameter.
	//	///
	//	/// e.g.:
	//	///  DataSet ds = ExecuteDataset(connString, "GetOrders", 24, 36);
	//	/// </remarks>
	//	/// <param name="connectionString">A valid connection string for a SqlConnection</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
	//	/// <returns>A dataset containing the resultset generated by the command</returns>
	//	internal static DataSet ExecuteDataset(string connectionString, string spName, bool isRetrun, params object[] parameterValues)
	//	{
	//		if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If we receive parameter values, we need to figure out where they go
	//		if ((parameterValues != null) && (parameterValues.Length > 0))
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			//SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(connectionString, spName); -- Original code from sqlDAL
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(connectionString, spName, isRetrun); // Added Parameter true to support ReturnValues

	//			// Assign the provided values to these parameters based on parameter order
	//			AssignParameterValues(commandParameters, parameterValues);

	//			// Call the overload that takes an array of SqlParameters
	//			return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			// Otherwise we can just call the SP without params
	//			return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in
	//	/// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <remarks>
	//	/// This method provides no access to output parameters or the stored procedure's return value parameter.
	//	///
	//	/// e.g.:
	//	///  DataSet ds = ExecuteDataset(connString, "GetOrders", 24, 36);
	//	/// </remarks>
	//	/// <param name="connectionString">A valid connection string for a SqlConnection</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
	//	/// <returns>A dataset containing the resultset generated by the command</returns>
	//	internal static DataSet ExecuteDataset_Custome(string connectionString, string spName, bool isRetrun, params object[] parameterValues)
	//	{
	//		if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If we receive parameter values, we need to figure out where they go
	//		if ((parameterValues != null) && (parameterValues.Length > 0))
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			//SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(connectionString, spName); -- Original code from sqlDAL
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(connectionString, spName, isRetrun); // Added Parameter true to support ReturnValues

	//			// Assign the provided values to these parameters based on parameter order
	//			AssignParameterValues(commandParameters, parameterValues);

	//			// Call the overload that takes an array of SqlParameters
	//			return ExecuteDataset_Custome(connectionString, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			// Otherwise we can just call the SP without params
	//			return ExecuteDataset_Custome(connectionString, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders");
	//	/// </remarks>
	//	/// <param name="connection">A valid SqlConnection</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <returns>A dataset containing the resultset generated by the command</returns>
	//	internal static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText)
	//	{
	//		// Pass through the call providing null for the set of SqlParameters
	//		return ExecuteDataset(connection, commandType, commandText, (SqlParameter[])null);
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection
	//	/// using the provided parameters.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
	//	/// </remarks>
	//	/// <param name="connection">A valid SqlConnection</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
	//	/// <returns>A dataset containing the resultset generated by the command</returns>
	//	internal static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
	//	{
	//		if (connection == null) throw new ArgumentNullException("connection");

	//		// Create a command and prepare it for execution
	//		SqlCommand cmd = new SqlCommand();
	//		bool mustCloseConnection = false;
	//		PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

	//		// Create the DataAdapter & DataSet
	//		using (SqlDataAdapter da = new SqlDataAdapter(cmd))
	//		{
	//			DataSet ds = new DataSet();

	//			// Fill the DataSet using default values for DataTable names, etc

	//			da.Fill(ds);

	//			// Detach the SqlParameters from the command object, so they can be used again
	//			cmd.Parameters.Clear();

	//			if (mustCloseConnection)
	//				connection.Close();

	//			//Added by Suraj 03-March-2022 To Close Connection
	//			if (connection.State == ConnectionState.Open)
	//			{
	//				connection.Close();
	//			}
	//			// Return the dataset
	//			return ds;
	//		}
	//	}
	//	internal static DataSet ExecuteDataset_Custome(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
	//	{
	//		if (connection == null) throw new ArgumentNullException("connection");

	//		// Create a command and prepare it for execution
	//		SqlCommand cmd = new SqlCommand();
	//		bool mustCloseConnection = false;
	//		PrepareCommand_Custome(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

	//		// Create the DataAdapter & DataSet
	//		using (SqlDataAdapter da = new SqlDataAdapter(cmd))
	//		{
	//			DataSet ds = new DataSet();

	//			// Fill the DataSet using default values for DataTable names, etc

	//			da.Fill(ds);

	//			// Detach the SqlParameters from the command object, so they can be used again
	//			cmd.Parameters.Clear();

	//			if (mustCloseConnection)
	//				connection.Close();

	//			//Added by Suraj 03-March-2022 To Close Connection
	//			if (connection.State == ConnectionState.Open)
	//			{
	//				connection.Close();
	//			}

	//			// Return the dataset
	//			return ds;
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection
	//	/// using the provided parameter values.  This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <remarks>
	//	/// This method provides no access to output parameters or the stored procedure's return value parameter.
	//	///
	//	/// e.g.:
	//	///  DataSet ds = ExecuteDataset(conn, "GetOrders", 24, 36);
	//	/// </remarks>
	//	/// <param name="connection">A valid SqlConnection</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
	//	/// <returns>A dataset containing the resultset generated by the command</returns>
	//	internal static DataSet ExecuteDataset(SqlConnection connection, string spName, params object[] parameterValues)
	//	{
	//		if (connection == null) throw new ArgumentNullException("connection");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If we receive parameter values, we need to figure out where they go
	//		if ((parameterValues != null) && (parameterValues.Length > 0))
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(connection, spName);

	//			// Assign the provided values to these parameters based on parameter order
	//			AssignParameterValues(commandParameters, parameterValues);

	//			// Call the overload that takes an array of SqlParameters
	//			return ExecuteDataset(connection, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			// Otherwise we can just call the SP without params
	//			return ExecuteDataset(connection, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlTransaction.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders");
	//	/// </remarks>
	//	/// <param name="transaction">A valid SqlTransaction</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <returns>A dataset containing the resultset generated by the command</returns>
	//	internal static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText)
	//	{
	//		// Pass through the call providing null for the set of SqlParameters
	//		return ExecuteDataset(transaction, commandType, commandText, (SqlParameter[])null);
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a resultset) against the specified SqlTransaction
	//	/// using the provided parameters.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
	//	/// </remarks>
	//	/// <param name="transaction">A valid SqlTransaction</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
	//	/// <returns>A dataset containing the resultset generated by the command</returns>
	//	internal static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
	//	{
	//		if (transaction == null) throw new ArgumentNullException("transaction");
	//		if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

	//		// Create a command and prepare it for execution
	//		SqlCommand cmd = new SqlCommand();
	//		bool mustCloseConnection = false;
	//		PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

	//		// Create the DataAdapter & DataSet
	//		using (SqlDataAdapter da = new SqlDataAdapter(cmd))
	//		{
	//			DataSet ds = new DataSet();

	//			// Fill the DataSet using default values for DataTable names, etc
	//			da.Fill(ds);

	//			// Detach the SqlParameters from the command object, so they can be used again
	//			cmd.Parameters.Clear();

	//			// Return the dataset
	//			return ds;
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified
	//	/// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <remarks>
	//	/// This method provides no access to output parameters or the stored procedure's return value parameter.
	//	///
	//	/// e.g.:
	//	///  DataSet ds = ExecuteDataset(trans, "GetOrders", 24, 36);
	//	/// </remarks>
	//	/// <param name="transaction">A valid SqlTransaction</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
	//	/// <returns>A dataset containing the resultset generated by the command</returns>
	//	internal static DataSet ExecuteDataset(SqlTransaction transaction, string spName, params object[] parameterValues)
	//	{
	//		if (transaction == null) throw new ArgumentNullException("transaction");
	//		if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If we receive parameter values, we need to figure out where they go
	//		if ((parameterValues != null) && (parameterValues.Length > 0))
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(transaction.Connection, spName);

	//			// Assign the provided values to these parameters based on parameter order
	//			AssignParameterValues(commandParameters, parameterValues);

	//			// Call the overload that takes an array of SqlParameters
	//			return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			// Otherwise we can just call the SP without params
	//			return ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	#endregion ExecuteDataset

	//	#region ExecuteReader

	//	/// <summary>
	//	/// This enum is used to indicate whether the connection was provided by the caller, or created by sqlDAL, so that
	//	/// we can set the appropriate CommandBehavior when calling ExecuteReader()
	//	/// </summary>
	//	private enum SqlConnectionOwnership
	//	{
	//		/// <summary>Connection is owned and managed by sqlDAL</summary>
	//		Internal,

	//		/// <summary>Connection is owned and managed by the caller</summary>
	//		External
	//	}

	//	/// <summary>
	//	/// Create and prepare a SqlCommand, and call ExecuteReader with the appropriate CommandBehavior.
	//	/// </summary>
	//	/// <remarks>
	//	/// If we created and opened the connection, we want the connection to be closed when the DataReader is closed.
	//	///
	//	/// If the caller provided the connection, we want to leave it to them to manage.
	//	/// </remarks>
	//	/// <param name="connection">A valid SqlConnection, on which to execute this command</param>
	//	/// <param name="transaction">A valid SqlTransaction, or 'null'</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <param name="commandParameters">An array of SqlParameters to be associated with the command or 'null' if no parameters are required</param>
	//	/// <param name="connectionOwnership">Indicates whether the connection parameter was provided by the caller, or created by sqlDAL</param>
	//	/// <returns>SqlDataReader containing the results of the command</returns>
	//	private static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, SqlConnectionOwnership connectionOwnership)
	//	{
	//		if (connection == null) throw new ArgumentNullException("connection");

	//		bool mustCloseConnection = false;
	//		// Create a command and prepare it for execution
	//		SqlCommand cmd = new SqlCommand();
	//		try
	//		{
	//			PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

	//			// Create a reader
	//			SqlDataReader dataReader;

	//			// Call ExecuteReader with the appropriate CommandBehavior
	//			if (connectionOwnership == SqlConnectionOwnership.External)
	//			{
	//				dataReader = cmd.ExecuteReader();
	//			}
	//			else
	//			{
	//				dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
	//			}

	//			// Detach the SqlParameters from the command object, so they can be used again.
	//			// HACK: There is a problem here, the output parameter values are fletched
	//			// when the reader is closed, so if the parameters are detached from the command
	//			// then the SqlReader can�t set its values.
	//			// When this happen, the parameters can�t be used again in other command.
	//			bool canClear = true;
	//			foreach (SqlParameter commandParameter in cmd.Parameters)
	//			{
	//				if (commandParameter.Direction != ParameterDirection.Input)
	//					canClear = false;
	//			}

	//			if (canClear)
	//			{
	//				cmd.Parameters.Clear();
	//			}

	//			return dataReader;
	//		}
	//		catch
	//		{
	//			if (mustCloseConnection)
	//				connection.Close();
	//			throw;
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in
	//	/// the connection string.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  SqlDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders");
	//	/// </remarks>
	//	/// <param name="connectionString">A valid connection string for a SqlConnection</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <returns>A SqlDataReader containing the resultset generated by the command</returns>
	//	internal static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
	//	{
	//		// Pass through the call providing null for the set of SqlParameters
	//		return ExecuteReader(connectionString, commandType, commandText, (SqlParameter[])null);
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a resultset) against the database specified in the connection string
	//	/// using the provided parameters.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  SqlDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
	//	/// </remarks>
	//	/// <param name="connectionString">A valid connection string for a SqlConnection</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
	//	/// <returns>A SqlDataReader containing the resultset generated by the command</returns>
	//	internal static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
	//	{
	//		if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
	//		SqlConnection connection = null;
	//		try
	//		{
	//			connection = new SqlConnection(connectionString);
	//			connection.Open();

	//			// Call the private overload that takes an internally owned connection in place of the connection string
	//			return ExecuteReader(connection, null, commandType, commandText, commandParameters, SqlConnectionOwnership.Internal); //change From Internal to External
	//		}
	//		catch
	//		{
	//			// If we fail to return the SqlDatReader, we need to close the connection ourselves
	//			if (connection != null) connection.Close();
	//			throw;
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in
	//	/// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <remarks>
	//	/// This method provides no access to output parameters or the stored procedure's return value parameter.
	//	///
	//	/// e.g.:
	//	///  SqlDataReader dr = ExecuteReader(connString, "GetOrders", 24, 36);
	//	/// </remarks>
	//	/// <param name="connectionString">A valid connection string for a SqlConnection</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
	//	/// <returns>A SqlDataReader containing the resultset generated by the command</returns>
	//	internal static SqlDataReader ExecuteReader(string connectionString, string spName, params object[] parameterValues)
	//	{
	//		if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If we receive parameter values, we need to figure out where they go
	//		if ((parameterValues != null) && (parameterValues.Length > 0))
	//		{
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(connectionString, spName, true);

	//			AssignParameterValues(commandParameters, parameterValues);

	//			return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			// Otherwise we can just call the SP without params
	//			return ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  SqlDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders");
	//	/// </remarks>
	//	/// <param name="connection">A valid SqlConnection</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <returns>A SqlDataReader containing the resultset generated by the command</returns>
	//	internal static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText)
	//	{
	//		// Pass through the call providing null for the set of SqlParameters
	//		return ExecuteReader(connection, commandType, commandText, (SqlParameter[])null);
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection
	//	/// using the provided parameters.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  SqlDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
	//	/// </remarks>
	//	/// <param name="connection">A valid SqlConnection</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
	//	/// <returns>A SqlDataReader containing the resultset generated by the command</returns>
	//	internal static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
	//	{
	//		// Pass through the call to the private overload using a null transaction value and an externally owned connection
	//		return ExecuteReader(connection, (SqlTransaction)null, commandType, commandText, commandParameters, SqlConnectionOwnership.External);
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection
	//	/// using the provided parameter values.  This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <remarks>
	//	/// This method provides no access to output parameters or the stored procedure's return value parameter.
	//	///
	//	/// e.g.:
	//	///  SqlDataReader dr = ExecuteReader(conn, "GetOrders", 24, 36);
	//	/// </remarks>
	//	/// <param name="connection">A valid SqlConnection</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
	//	/// <returns>A SqlDataReader containing the resultset generated by the command</returns>
	//	internal static SqlDataReader ExecuteReader(SqlConnection connection, string spName, params object[] parameterValues)
	//	{
	//		if (connection == null) throw new ArgumentNullException("connection");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If we receive parameter values, we need to figure out where they go
	//		if ((parameterValues != null) && (parameterValues.Length > 0))
	//		{
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(connection, spName);

	//			AssignParameterValues(commandParameters, parameterValues);

	//			return ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			// Otherwise we can just call the SP without params
	//			return ExecuteReader(connection, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlTransaction.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  SqlDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders");
	//	/// </remarks>
	//	/// <param name="transaction">A valid SqlTransaction</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <returns>A SqlDataReader containing the resultset generated by the command</returns>
	//	internal static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText)
	//	{
	//		// Pass through the call providing null for the set of SqlParameters
	//		return ExecuteReader(transaction, commandType, commandText, (SqlParameter[])null);
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a resultset) against the specified SqlTransaction
	//	/// using the provided parameters.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///   SqlDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
	//	/// </remarks>
	//	/// <param name="transaction">A valid SqlTransaction</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
	//	/// <returns>A SqlDataReader containing the resultset generated by the command</returns>
	//	internal static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
	//	{
	//		if (transaction == null) throw new ArgumentNullException("transaction");
	//		if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

	//		// Pass through to private overload, indicating that the connection is owned by the caller
	//		return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, SqlConnectionOwnership.External);
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified
	//	/// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <remarks>
	//	/// This method provides no access to output parameters or the stored procedure's return value parameter.
	//	///
	//	/// e.g.:
	//	///  SqlDataReader dr = ExecuteReader(trans, "GetOrders", 24, 36);
	//	/// </remarks>
	//	/// <param name="transaction">A valid SqlTransaction</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
	//	/// <returns>A SqlDataReader containing the resultset generated by the command</returns>
	//	internal static SqlDataReader ExecuteReader(SqlTransaction transaction, string spName, params object[] parameterValues)
	//	{
	//		if (transaction == null) throw new ArgumentNullException("transaction");
	//		if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If we receive parameter values, we need to figure out where they go
	//		if ((parameterValues != null) && (parameterValues.Length > 0))
	//		{
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(transaction.Connection, spName);

	//			AssignParameterValues(commandParameters, parameterValues);

	//			return ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			// Otherwise we can just call the SP without params
	//			return ExecuteReader(transaction, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	#endregion ExecuteReader

	//	#region ExecuteScalar

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the database specified in
	//	/// the connection string.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  int? orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount");
	//	/// </remarks>
	//	/// <param name="connectionString">A valid connection string for a SqlConnection</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
	//	internal static object ExecuteScalar(string connectionString, CommandType commandType, string commandText)
	//	{
	//		// Pass through the call providing null for the set of SqlParameters
	//		return ExecuteScalar(connectionString, commandType, commandText, (SqlParameter[])null);
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string
	//	/// using the provided parameters.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  int? orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
	//	/// </remarks>
	//	/// <param name="connectionString">A valid connection string for a SqlConnection</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
	//	/// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
	//	internal static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
	//	{
	//		if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
	//		// Create & open a SqlConnection, and dispose of it after we are done
	//		using (SqlConnection connection = new SqlConnection(connectionString))
	//		{
	//			connection.Open();

	//			// Call the overload that takes a connection in place of the connection string
	//			return ExecuteScalar(connection, commandType, commandText, commandParameters);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the database specified in
	//	/// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <remarks>
	//	/// This method provides no access to output parameters or the stored procedure's return value parameter.
	//	///
	//	/// e.g.:
	//	///  int? orderCount = (int)ExecuteScalar(connString, "GetOrderCount", 24, 36);
	//	/// </remarks>
	//	/// <param name="connectionString">A valid connection string for a SqlConnection</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
	//	/// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
	//	internal static object ExecuteScalar(string connectionString, string spName, params object[] parameterValues)
	//	{
	//		if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If we receive parameter values, we need to figure out where they go
	//		if ((parameterValues != null) && (parameterValues.Length > 0))
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(connectionString, spName);

	//			// Assign the provided values to these parameters based on parameter order
	//			AssignParameterValues(commandParameters, parameterValues);

	//			// Call the overload that takes an array of SqlParameters
	//			return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			// Otherwise we can just call the SP without params
	//			return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided SqlConnection.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  int? orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount");
	//	/// </remarks>
	//	/// <param name="connection">A valid SqlConnection</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
	//	internal static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText)
	//	{
	//		// Pass through the call providing null for the set of SqlParameters
	//		return ExecuteScalar(connection, commandType, commandText, (SqlParameter[])null);
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection
	//	/// using the provided parameters.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  int? orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
	//	/// </remarks>
	//	/// <param name="connection">A valid SqlConnection</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
	//	/// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
	//	internal static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
	//	{
	//		if (connection == null) throw new ArgumentNullException("connection");

	//		// Create a command and prepare it for execution
	//		SqlCommand cmd = new SqlCommand();

	//		bool mustCloseConnection = false;
	//		PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

	//		// Execute the command & return the results
	//		object retval = cmd.ExecuteScalar();

	//		// Detach the SqlParameters from the command object, so they can be used again
	//		cmd.Parameters.Clear();

	//		if (mustCloseConnection)
	//			connection.Close();

	//		//Added by Suraj 03-March-2022 To Close Connection
	//		if (connection.State == ConnectionState.Open)
	//		{
	//			connection.Close();
	//		}
	//		return retval;
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection
	//	/// using the provided parameter values.  This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <remarks>
	//	/// This method provides no access to output parameters or the stored procedure's return value parameter.
	//	///
	//	/// e.g.:
	//	///  int? orderCount = (int)ExecuteScalar(conn, "GetOrderCount", 24, 36);
	//	/// </remarks>
	//	/// <param name="connection">A valid SqlConnection</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
	//	/// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
	//	internal static object ExecuteScalar(SqlConnection connection, string spName, params object[] parameterValues)
	//	{
	//		if (connection == null) throw new ArgumentNullException("connection");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If we receive parameter values, we need to figure out where they go
	//		if ((parameterValues != null) && (parameterValues.Length > 0))
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(connection, spName);

	//			// Assign the provided values to these parameters based on parameter order
	//			AssignParameterValues(commandParameters, parameterValues);

	//			// Call the overload that takes an array of SqlParameters
	//			return ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			// Otherwise we can just call the SP without params
	//			return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided SqlTransaction.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  int? orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount");
	//	/// </remarks>
	//	/// <param name="transaction">A valid SqlTransaction</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
	//	internal static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText)
	//	{
	//		// Pass through the call providing null for the set of SqlParameters
	//		return ExecuteScalar(transaction, commandType, commandText, (SqlParameter[])null);
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlTransaction
	//	/// using the provided parameters.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  int? orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
	//	/// </remarks>
	//	/// <param name="transaction">A valid SqlTransaction</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
	//	/// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
	//	internal static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
	//	{
	//		if (transaction == null) throw new ArgumentNullException("transaction");
	//		if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

	//		// Create a command and prepare it for execution
	//		SqlCommand cmd = new SqlCommand();
	//		bool mustCloseConnection = false;
	//		PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

	//		// Execute the command & return the results
	//		object retval = cmd.ExecuteScalar();

	//		// Detach the SqlParameters from the command object, so they can be used again
	//		cmd.Parameters.Clear();
	//		return retval;
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified
	//	/// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <remarks>
	//	/// This method provides no access to output parameters or the stored procedure's return value parameter.
	//	///
	//	/// e.g.:
	//	///  int? orderCount = (int)ExecuteScalar(trans, "GetOrderCount", 24, 36);
	//	/// </remarks>
	//	/// <param name="transaction">A valid SqlTransaction</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
	//	/// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
	//	internal static object ExecuteScalar(SqlTransaction transaction, string spName, params object[] parameterValues)
	//	{
	//		if (transaction == null) throw new ArgumentNullException("transaction");
	//		if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If we receive parameter values, we need to figure out where they go
	//		if ((parameterValues != null) && (parameterValues.Length > 0))
	//		{
	//			// PPull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(transaction.Connection, spName);

	//			// Assign the provided values to these parameters based on parameter order
	//			AssignParameterValues(commandParameters, parameterValues);

	//			// Call the overload that takes an array of SqlParameters
	//			return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			// Otherwise we can just call the SP without params
	//			return ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	#endregion ExecuteScalar

	//	#region ExecuteXmlReader

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  XmlReader r = ExecuteXmlReader(conn, CommandType.StoredProcedure, "GetOrders");
	//	/// </remarks>
	//	/// <param name="connection">A valid SqlConnection</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
	//	/// <returns>An XmlReader containing the resultset generated by the command</returns>
	//	internal static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText)
	//	{
	//		// Pass through the call providing null for the set of SqlParameters
	//		return ExecuteXmlReader(connection, commandType, commandText, (SqlParameter[])null);
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection
	//	/// using the provided parameters.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  XmlReader r = ExecuteXmlReader(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
	//	/// </remarks>
	//	/// <param name="connection">A valid SqlConnection</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
	//	/// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
	//	/// <returns>An XmlReader containing the resultset generated by the command</returns>
	//	internal static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
	//	{
	//		if (connection == null) throw new ArgumentNullException("connection");

	//		bool mustCloseConnection = false;
	//		// Create a command and prepare it for execution
	//		SqlCommand cmd = new SqlCommand();
	//		try
	//		{
	//			PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

	//			// Create the DataAdapter & DataSet
	//			XmlReader retval = cmd.ExecuteXmlReader();

	//			// Detach the SqlParameters from the command object, so they can be used again
	//			cmd.Parameters.Clear();

	//			return retval;
	//		}
	//		catch
	//		{
	//			if (mustCloseConnection)
	//				connection.Close();
	//			throw;
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection
	//	/// using the provided parameter values.  This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <remarks>
	//	/// This method provides no access to output parameters or the stored procedure's return value parameter.
	//	///
	//	/// e.g.:
	//	///  XmlReader r = ExecuteXmlReader(conn, "GetOrders", 24, 36);
	//	/// </remarks>
	//	/// <param name="connection">A valid SqlConnection</param>
	//	/// <param name="spName">The name of the stored procedure using "FOR XML AUTO"</param>
	//	/// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
	//	/// <returns>An XmlReader containing the resultset generated by the command</returns>
	//	internal static XmlReader ExecuteXmlReader(SqlConnection connection, string spName, params object[] parameterValues)
	//	{
	//		if (connection == null) throw new ArgumentNullException("connection");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If we receive parameter values, we need to figure out where they go
	//		if ((parameterValues != null) && (parameterValues.Length > 0))
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(connection, spName);

	//			// Assign the provided values to these parameters based on parameter order
	//			AssignParameterValues(commandParameters, parameterValues);

	//			// Call the overload that takes an array of SqlParameters
	//			return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			// Otherwise we can just call the SP without params
	//			return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlTransaction.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  XmlReader r = ExecuteXmlReader(trans, CommandType.StoredProcedure, "GetOrders");
	//	/// </remarks>
	//	/// <param name="transaction">A valid SqlTransaction</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
	//	/// <returns>An XmlReader containing the resultset generated by the command</returns>
	//	internal static XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType, string commandText)
	//	{
	//		// Pass through the call providing null for the set of SqlParameters
	//		return ExecuteXmlReader(transaction, commandType, commandText, (SqlParameter[])null);
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a resultset) against the specified SqlTransaction
	//	/// using the provided parameters.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  XmlReader r = ExecuteXmlReader(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
	//	/// </remarks>
	//	/// <param name="transaction">A valid SqlTransaction</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
	//	/// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
	//	/// <returns>An XmlReader containing the resultset generated by the command</returns>
	//	internal static XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
	//	{
	//		if (transaction == null) throw new ArgumentNullException("transaction");
	//		if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

	//		// Create a command and prepare it for execution
	//		SqlCommand cmd = new SqlCommand();
	//		bool mustCloseConnection = false;
	//		PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

	//		// Create the DataAdapter & DataSet
	//		XmlReader retval = cmd.ExecuteXmlReader();

	//		// Detach the SqlParameters from the command object, so they can be used again
	//		cmd.Parameters.Clear();
	//		return retval;
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified
	//	/// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <remarks>
	//	/// This method provides no access to output parameters or the stored procedure's return value parameter.
	//	///
	//	/// e.g.:
	//	///  XmlReader r = ExecuteXmlReader(trans, "GetOrders", 24, 36);
	//	/// </remarks>
	//	/// <param name="transaction">A valid SqlTransaction</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
	//	/// <returns>A dataset containing the resultset generated by the command</returns>
	//	internal static XmlReader ExecuteXmlReader(SqlTransaction transaction, string spName, params object[] parameterValues)
	//	{
	//		if (transaction == null) throw new ArgumentNullException("transaction");
	//		if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If we receive parameter values, we need to figure out where they go
	//		if ((parameterValues != null) && (parameterValues.Length > 0))
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(transaction.Connection, spName);

	//			// Assign the provided values to these parameters based on parameter order
	//			AssignParameterValues(commandParameters, parameterValues);

	//			// Call the overload that takes an array of SqlParameters
	//			return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			// Otherwise we can just call the SP without params
	//			return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	#endregion ExecuteXmlReader

	//	#region FillDataset

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in
	//	/// the connection string.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  FillDataset(connString, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"});
	//	/// </remarks>
	//	/// <param name="connectionString">A valid connection string for a SqlConnection</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
	//	/// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
	//	/// by a user defined name (probably the actual table name)</param>
	//	internal static void FillDataset(string connectionString, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
	//	{
	//		if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
	//		if (dataSet == null) throw new ArgumentNullException("dataSet");

	//		// Create & open a SqlConnection, and dispose of it after we are done
	//		using (SqlConnection connection = new SqlConnection(connectionString))
	//		{
	//			connection.Open();

	//			// Call the overload that takes a connection in place of the connection string
	//			FillDataset(connection, commandType, commandText, dataSet, tableNames);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a resultset) against the database specified in the connection string
	//	/// using the provided parameters.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  FillDataset(connString, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, new SqlParameter("@prodid", 24));
	//	/// </remarks>
	//	/// <param name="connectionString">A valid connection string for a SqlConnection</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
	//	/// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
	//	/// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
	//	/// by a user defined name (probably the actual table name)
	//	/// </param>
	//	internal static void FillDataset(string connectionString, CommandType commandType,
	//		string commandText, DataSet dataSet, string[] tableNames,
	//		params SqlParameter[] commandParameters)
	//	{
	//		if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
	//		if (dataSet == null) throw new ArgumentNullException("dataSet");
	//		// Create & open a SqlConnection, and dispose of it after we are done
	//		using (SqlConnection connection = new SqlConnection(connectionString))
	//		{
	//			connection.Open();
	//			// Call the overload that takes a connection in place of the connection string
	//			FillDataset(connection, commandType, commandText, dataSet, tableNames, commandParameters);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in
	//	/// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <remarks>
	//	/// This method provides no access to output parameters or the stored procedure's return value parameter.
	//	///
	//	/// e.g.:
	//	///  FillDataset(connString, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, 24);
	//	/// </remarks>
	//	/// <param name="connectionString">A valid connection string for a SqlConnection</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
	//	/// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
	//	/// by a user defined name (probably the actual table name)
	//	/// </param>
	//	/// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
	//	internal static void FillDataset(string connectionString, string spName,
	//		DataSet dataSet, string[] tableNames,
	//		params object[] parameterValues)
	//	{
	//		if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
	//		if (dataSet == null) throw new ArgumentNullException("dataSet");
	//		// Create & open a SqlConnection, and dispose of it after we are done
	//		using (SqlConnection connection = new SqlConnection(connectionString))
	//		{
	//			connection.Open();

	//			// Call the overload that takes a connection in place of the connection string
	//			FillDataset(connection, spName, dataSet, tableNames, parameterValues);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  FillDataset(conn, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"});
	//	/// </remarks>
	//	/// <param name="connection">A valid SqlConnection</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
	//	/// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
	//	/// by a user defined name (probably the actual table name)
	//	/// </param>
	//	internal static void FillDataset(SqlConnection connection, CommandType commandType,
	//		string commandText, DataSet dataSet, string[] tableNames)
	//	{
	//		FillDataset(connection, commandType, commandText, dataSet, tableNames, null);
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection
	//	/// using the provided parameters.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  FillDataset(conn, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, new SqlParameter("@prodid", 24));
	//	/// </remarks>
	//	/// <param name="connection">A valid SqlConnection</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
	//	/// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
	//	/// by a user defined name (probably the actual table name)
	//	/// </param>
	//	/// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
	//	internal static void FillDataset(SqlConnection connection, CommandType commandType,
	//		string commandText, DataSet dataSet, string[] tableNames,
	//		params SqlParameter[] commandParameters)
	//	{
	//		FillDataset(connection, null, commandType, commandText, dataSet, tableNames, commandParameters);
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection
	//	/// using the provided parameter values.  This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <remarks>
	//	/// This method provides no access to output parameters or the stored procedure's return value parameter.
	//	///
	//	/// e.g.:
	//	///  FillDataset(conn, "GetOrders", ds, new string[] {"orders"}, 24, 36);
	//	/// </remarks>
	//	/// <param name="connection">A valid SqlConnection</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
	//	/// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
	//	/// by a user defined name (probably the actual table name)
	//	/// </param>
	//	/// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
	//	internal static void FillDataset(SqlConnection connection, string spName,
	//		DataSet dataSet, string[] tableNames,
	//		params object[] parameterValues)
	//	{
	//		if (connection == null) throw new ArgumentNullException("connection");
	//		if (dataSet == null) throw new ArgumentNullException("dataSet");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If we receive parameter values, we need to figure out where they go
	//		if ((parameterValues != null) && (parameterValues.Length > 0))
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(connection, spName);

	//			// Assign the provided values to these parameters based on parameter order
	//			AssignParameterValues(commandParameters, parameterValues);

	//			// Call the overload that takes an array of SqlParameters
	//			FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames, commandParameters);
	//		}
	//		else
	//		{
	//			// Otherwise we can just call the SP without params
	//			FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlTransaction.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  FillDataset(trans, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"});
	//	/// </remarks>
	//	/// <param name="transaction">A valid SqlTransaction</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
	//	/// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
	//	/// by a user defined name (probably the actual table name)
	//	/// </param>
	//	internal static void FillDataset(SqlTransaction transaction, CommandType commandType,
	//		string commandText,
	//		DataSet dataSet, string[] tableNames)
	//	{
	//		FillDataset(transaction, commandType, commandText, dataSet, tableNames, null);
	//	}

	//	/// <summary>
	//	/// Execute a SqlCommand (that returns a resultset) against the specified SqlTransaction
	//	/// using the provided parameters.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  FillDataset(trans, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, new SqlParameter("@prodid", 24));
	//	/// </remarks>
	//	/// <param name="transaction">A valid SqlTransaction</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
	//	/// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
	//	/// by a user defined name (probably the actual table name)
	//	/// </param>
	//	/// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
	//	internal static void FillDataset(SqlTransaction transaction, CommandType commandType,
	//		string commandText, DataSet dataSet, string[] tableNames,
	//		params SqlParameter[] commandParameters)
	//	{
	//		FillDataset(transaction.Connection, transaction, commandType, commandText, dataSet, tableNames, commandParameters);
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified
	//	/// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <remarks>
	//	/// This method provides no access to output parameters or the stored procedure's return value parameter.
	//	///
	//	/// e.g.:
	//	///  FillDataset(trans, "GetOrders", ds, new string[]{"orders"}, 24, 36);
	//	/// </remarks>
	//	/// <param name="transaction">A valid SqlTransaction</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
	//	/// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
	//	/// by a user defined name (probably the actual table name)
	//	/// </param>
	//	/// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
	//	internal static void FillDataset(SqlTransaction transaction, string spName,
	//		DataSet dataSet, string[] tableNames,
	//		params object[] parameterValues)
	//	{
	//		if (transaction == null) throw new ArgumentNullException("transaction");
	//		if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
	//		if (dataSet == null) throw new ArgumentNullException("dataSet");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If we receive parameter values, we need to figure out where they go
	//		if ((parameterValues != null) && (parameterValues.Length > 0))
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(transaction.Connection, spName);

	//			// Assign the provided values to these parameters based on parameter order
	//			AssignParameterValues(commandParameters, parameterValues);

	//			// Call the overload that takes an array of SqlParameters
	//			FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames, commandParameters);
	//		}
	//		else
	//		{
	//			// Otherwise we can just call the SP without params
	//			FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames);
	//		}
	//	}

	//	/// <summary>
	//	/// Private helper method that execute a SqlCommand (that returns a resultset) against the specified SqlTransaction and SqlConnection
	//	/// using the provided parameters.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  FillDataset(conn, trans, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, new SqlParameter("@prodid", 24));
	//	/// </remarks>
	//	/// <param name="connection">A valid SqlConnection</param>
	//	/// <param name="transaction">A valid SqlTransaction</param>
	//	/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
	//	/// <param name="commandText">The stored procedure name or T-SQL command</param>
	//	/// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
	//	/// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
	//	/// by a user defined name (probably the actual table name)
	//	/// </param>
	//	/// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
	//	private static void FillDataset(SqlConnection connection, SqlTransaction transaction, CommandType commandType,
	//		string commandText, DataSet dataSet, string[] tableNames,
	//		params SqlParameter[] commandParameters)
	//	{
	//		if (connection == null) throw new ArgumentNullException("connection");
	//		if (dataSet == null) throw new ArgumentNullException("dataSet");

	//		// Create a command and prepare it for execution
	//		SqlCommand command = new SqlCommand();
	//		bool mustCloseConnection = false;
	//		PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

	//		// Create the DataAdapter & DataSet
	//		using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
	//		{
	//			// Add the table mappings specified by the user
	//			if (tableNames != null && tableNames.Length > 0)
	//			{
	//				//Changes added for string and string builder
	//				StringBuilder tableName = new StringBuilder("Table");
	//				for (int index = 0; index < tableNames.Length; index++)
	//				{
	//					if (tableNames[index] == null || tableNames[index].Length == 0) throw new ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames");
	//					dataAdapter.TableMappings.Add(tableName.ToString(), tableNames[index]);
	//					tableName.Append(index + 1);
	//				}
	//			}

	//			// Fill the DataSet using default values for DataTable names, etc
	//			dataAdapter.Fill(dataSet);

	//			// Detach the SqlParameters from the command object, so they can be used again
	//			command.Parameters.Clear();
	//		}

	//		if (mustCloseConnection)
	//			connection.Close();

	//		//Added by Suraj 03-March-2022 To Close Connection
	//		if (connection.State == ConnectionState.Open)
	//		{
	//			connection.Close();
	//		}
	//	}

	//	#endregion FillDataset

	//	#region UpdateDataset

	//	/// <summary>
	//	/// Executes the respective command for each inserted, updated, or deleted row in the DataSet.
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  UpdateDataset(conn, insertCommand, deleteCommand, updateCommand, dataSet, "Order");
	//	/// </remarks>
	//	/// <param name="insertCommand">A valid transact-SQL statement or stored procedure to insert new records into the data source</param>
	//	/// <param name="deleteCommand">A valid transact-SQL statement or stored procedure to delete records from the data source</param>
	//	/// <param name="updateCommand">A valid transact-SQL statement or stored procedure used to update records in the data source</param>
	//	/// <param name="dataSet">The DataSet used to update the data source</param>
	//	/// <param name="tableName">The DataTable used to update the data source.</param>
	//	internal static void UpdateDataset(SqlCommand insertCommand, SqlCommand deleteCommand, SqlCommand updateCommand, DataSet dataSet, string tableName)
	//	{
	//		if (insertCommand == null) throw new ArgumentNullException("insertCommand");
	//		if (deleteCommand == null) throw new ArgumentNullException("deleteCommand");
	//		if (updateCommand == null) throw new ArgumentNullException("updateCommand");
	//		if (tableName == null || tableName.Length == 0) throw new ArgumentNullException("tableName");

	//		// Create a SqlDataAdapter, and dispose of it after we are done
	//		using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
	//		{
	//			// Set the data adapter commands
	//			dataAdapter.UpdateCommand = updateCommand;
	//			dataAdapter.InsertCommand = insertCommand;
	//			dataAdapter.DeleteCommand = deleteCommand;

	//			// Update the dataset changes in the data source
	//			dataAdapter.Update(dataSet, tableName);

	//			// Commit all the changes made to the DataSet
	//			dataSet.AcceptChanges();
	//		}
	//	}

	//	#endregion UpdateDataset

	//	#region CreateCommand

	//	/// <summary>
	//	/// Simplify the creation of a Sql command object by allowing
	//	/// a stored procedure and optional parameters to be provided
	//	/// </summary>
	//	/// <remarks>
	//	/// e.g.:
	//	///  SqlCommand command = CreateCommand(conn, "AddCustomer", "CustomerID", "CustomerName");
	//	/// </remarks>
	//	/// <param name="connection">A valid SqlConnection object</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="sourceColumns">An array of string to be assigned as the source columns of the stored procedure parameters</param>
	//	/// <returns>A valid SqlCommand object</returns>
	//	internal static SqlCommand CreateCommand(SqlConnection connection, string spName, params string[] sourceColumns)
	//	{
	//		if (connection == null) throw new ArgumentNullException("connection");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// Create a SqlCommand
	//		SqlCommand cmd = new SqlCommand(spName, connection);
	//		cmd.CommandType = CommandType.StoredProcedure;

	//		// If we receive parameter values, we need to figure out where they go
	//		if ((sourceColumns != null) && (sourceColumns.Length > 0))
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(connection, spName);

	//			// Assign the provided source columns to these parameters based on parameter order
	//			for (int index = 0; index < sourceColumns.Length; index++)
	//				commandParameters[index].SourceColumn = sourceColumns[index];

	//			// Attach the discovered parameters to the SqlCommand object
	//			AttachParameters(cmd, commandParameters);
	//		}

	//		return cmd;
	//	}

	//	#endregion CreateCommand

	//	#region ExecuteNonQueryTypedParams

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns no resultset) against the database specified in
	//	/// the connection string using the dataRow column values as the stored procedure's parameters values.
	//	/// This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
	//	/// </summary>
	//	/// <param name="connectionString">A valid connection string for a SqlConnection</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
	//	/// <returns>An int? representing the number of rows affected by the command</returns>
	//	internal static int? ExecuteNonQueryTypedParams(String connectionString, String spName, DataRow dataRow)
	//	{
	//		if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If the row has values, the store procedure parameters must be initialized
	//		if (dataRow != null && dataRow.ItemArray.Length > 0)
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(connectionString, spName);

	//			// Set the parameters values
	//			AssignParameterValues(commandParameters, dataRow);

	//			return SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			return SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified SqlConnection
	//	/// using the dataRow column values as the stored procedure's parameters values.
	//	/// This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
	//	/// </summary>
	//	/// <param name="connection">A valid SqlConnection object</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
	//	/// <returns>An int? representing the number of rows affected by the command</returns>
	//	internal static int? ExecuteNonQueryTypedParams(SqlConnection connection, String spName, DataRow dataRow)
	//	{
	//		if (connection == null) throw new ArgumentNullException("connection");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If the row has values, the store procedure parameters must be initialized
	//		if (dataRow != null && dataRow.ItemArray.Length > 0)
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(connection, spName);

	//			// Set the parameters values
	//			AssignParameterValues(commandParameters, dataRow);

	//			return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified
	//	/// SqlTransaction using the dataRow column values as the stored procedure's parameters values.
	//	/// This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
	//	/// </summary>
	//	/// <param name="transaction">A valid SqlTransaction object</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
	//	/// <returns>An int? representing the number of rows affected by the command</returns>
	//	internal static int? ExecuteNonQueryTypedParams(SqlTransaction transaction, String spName, DataRow dataRow)
	//	{
	//		if (transaction == null) throw new ArgumentNullException("transaction");
	//		if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// Sf the row has values, the store procedure parameters must be initialized
	//		if (dataRow != null && dataRow.ItemArray.Length > 0)
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(transaction.Connection, spName);

	//			// Set the parameters values
	//			AssignParameterValues(commandParameters, dataRow);

	//			return SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			return SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	#endregion ExecuteNonQueryTypedParams

	//	#region ExecuteDatasetTypedParams

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in
	//	/// the connection string using the dataRow column values as the stored procedure's parameters values.
	//	/// This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
	//	/// </summary>
	//	/// <param name="connectionString">A valid connection string for a SqlConnection</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
	//	/// <returns>A dataset containing the resultset generated by the command</returns>
	//	internal static DataSet ExecuteDatasetTypedParams(string connectionString, String spName, DataRow dataRow)
	//	{
	//		if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		//If the row has values, the store procedure parameters must be initialized
	//		if (dataRow != null && dataRow.ItemArray.Length > 0)
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(connectionString, spName);

	//			// Set the parameters values
	//			AssignParameterValues(commandParameters, dataRow);

	//			return SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			return SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection
	//	/// using the dataRow column values as the store procedure's parameters values.
	//	/// This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
	//	/// </summary>
	//	/// <param name="connection">A valid SqlConnection object</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
	//	/// <returns>A dataset containing the resultset generated by the command</returns>
	//	internal static DataSet ExecuteDatasetTypedParams(SqlConnection connection, String spName, DataRow dataRow)
	//	{
	//		if (connection == null) throw new ArgumentNullException("connection");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If the row has values, the store procedure parameters must be initialized
	//		if (dataRow != null && dataRow.ItemArray.Length > 0)
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(connection, spName);

	//			// Set the parameters values
	//			AssignParameterValues(commandParameters, dataRow);

	//			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlTransaction
	//	/// using the dataRow column values as the stored procedure's parameters values.
	//	/// This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
	//	/// </summary>
	//	/// <param name="transaction">A valid SqlTransaction object</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
	//	/// <returns>A dataset containing the resultset generated by the command</returns>
	//	internal static DataSet ExecuteDatasetTypedParams(SqlTransaction transaction, String spName, DataRow dataRow)
	//	{
	//		if (transaction == null) throw new ArgumentNullException("transaction");
	//		if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If the row has values, the store procedure parameters must be initialized
	//		if (dataRow != null && dataRow.ItemArray.Length > 0)
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(transaction.Connection, spName);

	//			// Set the parameters values
	//			AssignParameterValues(commandParameters, dataRow);

	//			return SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			return SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	#endregion ExecuteDatasetTypedParams

	//	#region ExecuteReaderTypedParams

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in
	//	/// the connection string using the dataRow column values as the stored procedure's parameters values.
	//	/// This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <param name="connectionString">A valid connection string for a SqlConnection</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
	//	/// <returns>A SqlDataReader containing the resultset generated by the command</returns>
	//	internal static SqlDataReader ExecuteReaderTypedParams(String connectionString, String spName, DataRow dataRow)
	//	{
	//		if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If the row has values, the store procedure parameters must be initialized
	//		if (dataRow != null && dataRow.ItemArray.Length > 0)
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(connectionString, spName);

	//			// Set the parameters values
	//			AssignParameterValues(commandParameters, dataRow);

	//			return SqlHelper.ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			return SqlHelper.ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection
	//	/// using the dataRow column values as the stored procedure's parameters values.
	//	/// This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <param name="connection">A valid SqlConnection object</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
	//	/// <returns>A SqlDataReader containing the resultset generated by the command</returns>
	//	internal static SqlDataReader ExecuteReaderTypedParams(SqlConnection connection, String spName, DataRow dataRow)
	//	{
	//		if (connection == null) throw new ArgumentNullException("connection");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If the row has values, the store procedure parameters must be initialized
	//		if (dataRow != null && dataRow.ItemArray.Length > 0)
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(connection, spName);

	//			// Set the parameters values
	//			AssignParameterValues(commandParameters, dataRow);

	//			return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlTransaction
	//	/// using the dataRow column values as the stored procedure's parameters values.
	//	/// This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <param name="transaction">A valid SqlTransaction object</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
	//	/// <returns>A SqlDataReader containing the resultset generated by the command</returns>
	//	internal static SqlDataReader ExecuteReaderTypedParams(SqlTransaction transaction, String spName, DataRow dataRow)
	//	{
	//		if (transaction == null) throw new ArgumentNullException("transaction");
	//		if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If the row has values, the store procedure parameters must be initialized
	//		if (dataRow != null && dataRow.ItemArray.Length > 0)
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(transaction.Connection, spName);

	//			// Set the parameters values
	//			AssignParameterValues(commandParameters, dataRow);

	//			return SqlHelper.ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			return SqlHelper.ExecuteReader(transaction, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	#endregion ExecuteReaderTypedParams

	//	#region ExecuteScalarTypedParams

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the database specified in
	//	/// the connection string using the dataRow column values as the stored procedure's parameters values.
	//	/// This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <param name="connectionString">A valid connection string for a SqlConnection</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
	//	/// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
	//	internal static object ExecuteScalarTypedParams(String connectionString, String spName, DataRow dataRow)
	//	{
	//		if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If the row has values, the store procedure parameters must be initialized
	//		if (dataRow != null && dataRow.ItemArray.Length > 0)
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(connectionString, spName);

	//			// Set the parameters values
	//			AssignParameterValues(commandParameters, dataRow);

	//			return SqlHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			return SqlHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection
	//	/// using the dataRow column values as the stored procedure's parameters values.
	//	/// This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <param name="connection">A valid SqlConnection object</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
	//	/// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
	//	internal static object ExecuteScalarTypedParams(SqlConnection connection, String spName, DataRow dataRow)
	//	{
	//		if (connection == null) throw new ArgumentNullException("connection");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If the row has values, the store procedure parameters must be initialized
	//		if (dataRow != null && dataRow.ItemArray.Length > 0)
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(connection, spName);

	//			// Set the parameters values
	//			AssignParameterValues(commandParameters, dataRow);

	//			return SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			return SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified SqlTransaction
	//	/// using the dataRow column values as the stored procedure's parameters values.
	//	/// This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <param name="transaction">A valid SqlTransaction object</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
	//	/// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
	//	internal static object ExecuteScalarTypedParams(SqlTransaction transaction, String spName, DataRow dataRow)
	//	{
	//		if (transaction == null) throw new ArgumentNullException("transaction");
	//		if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If the row has values, the store procedure parameters must be initialized
	//		if (dataRow != null && dataRow.ItemArray.Length > 0)
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(transaction.Connection, spName);

	//			// Set the parameters values
	//			AssignParameterValues(commandParameters, dataRow);

	//			return SqlHelper.ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			return SqlHelper.ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	#endregion ExecuteScalarTypedParams

	//	#region ExecuteXmlReaderTypedParams

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection
	//	/// using the dataRow column values as the stored procedure's parameters values.
	//	/// This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <param name="connection">A valid SqlConnection object</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
	//	/// <returns>An XmlReader containing the resultset generated by the command</returns>
	//	internal static XmlReader ExecuteXmlReaderTypedParams(SqlConnection connection, String spName, DataRow dataRow)
	//	{
	//		if (connection == null) throw new ArgumentNullException("connection");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If the row has values, the store procedure parameters must be initialized
	//		if (dataRow != null && dataRow.ItemArray.Length > 0)
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(connection, spName);

	//			// Set the parameters values
	//			AssignParameterValues(commandParameters, dataRow);

	//			return SqlHelper.ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			return SqlHelper.ExecuteXmlReader(connection, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	/// <summary>
	//	/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlTransaction
	//	/// using the dataRow column values as the stored procedure's parameters values.
	//	/// This method will query the database to discover the parameters for the
	//	/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
	//	/// </summary>
	//	/// <param name="transaction">A valid SqlTransaction object</param>
	//	/// <param name="spName">The name of the stored procedure</param>
	//	/// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
	//	/// <returns>An XmlReader containing the resultset generated by the command</returns>
	//	internal static XmlReader ExecuteXmlReaderTypedParams(SqlTransaction transaction, String spName, DataRow dataRow)
	//	{
	//		if (transaction == null) throw new ArgumentNullException("transaction");
	//		if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
	//		if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

	//		// If the row has values, the store procedure parameters must be initialized
	//		if (dataRow != null && dataRow.ItemArray.Length > 0)
	//		{
	//			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
	//			SqlParameter[] commandParameters = SqlDalParameterCache.GetSpParameterSet(transaction.Connection, spName);

	//			// Set the parameters values
	//			AssignParameterValues(commandParameters, dataRow);

	//			return SqlHelper.ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
	//		}
	//		else
	//		{
	//			return SqlHelper.ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName);
	//		}
	//	}

	//	#endregion ExecuteXmlReaderTypedParams

	//	#region Public Methods

	//	public string ExecuteBulkInsert(string conn, string routineName, DataTable dt, string script)
	//	{
	//		SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(conn);
	//		builder.ConnectTimeout = 0;
	//		SqlConnection con = new SqlConnection(builder.ConnectionString);
	//		con.Open();
	//		using (SqlCommand cmd = con.CreateCommand())
	//		{
	//			using (SqlBulkCopy s = new SqlBulkCopy(con))
	//			{
	//				s.DestinationTableName = routineName;
	//				foreach (var column in dt.Columns)
	//					s.ColumnMappings.Add(column.ToString(), column.ToString());
	//				s.WriteToServer(dt);
	//			}

	//			return "";
	//		}
	//	}

	//	public static DataTable ExecuteQuery(string connectionString, string routineName, params object[] parameters)
	//	{
	//		SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
	//		//builder.ConnectTimeout = 2500;
	//		SqlConnection con = new SqlConnection(builder.ConnectionString);
	//		System.Data.Common.DbDataReader sqlReader;

	//		con.Open();

	//		using (SqlCommand cmd = con.CreateCommand())
	//		{
	//			cmd.CommandText = routineName;
	//			cmd.CommandType = System.Data.CommandType.StoredProcedure;
	//			cmd.Parameters.AddRange(parameters);
	//			//cmd.CommandTimeout = 6000;
	//			sqlReader = (System.Data.Common.DbDataReader)cmd.ExecuteReader();
	//			DataTable dt = new DataTable();
	//			while (!sqlReader.IsClosed)
	//			{
	//				dt = new DataTable();
	//				dt.Load(sqlReader);
	//			}
	//			con.Close();
	//			return dt;
	//		}
	//	}

	//	public static DataTable ExecuteQuery_Custom(string connectionString, string routineName, params object[] parameters)
	//	{
	//		SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
	//		builder.ConnectTimeout = 180;
	//		SqlConnection con = new SqlConnection(builder.ConnectionString);
	//		System.Data.Common.DbDataReader sqlReader;

	//		con.Open();
	//		try
	//		{
	//			using (SqlCommand cmd = con.CreateCommand())
	//			{
	//				cmd.CommandText = routineName;
	//				cmd.CommandType = System.Data.CommandType.StoredProcedure;
	//				cmd.Parameters.AddRange(parameters);
	//				cmd.CommandTimeout = 180;
	//				sqlReader = (System.Data.Common.DbDataReader)cmd.ExecuteReader();
	//				DataTable dt = new DataTable();
	//				while (!sqlReader.IsClosed)
	//				{
	//					dt = new DataTable();
	//					dt.Load(sqlReader);
	//				}
	//				con.Close();
	//				return dt;
	//			}
	//		}
	//		finally
	//		{
	//			if (con != null && con.State == ConnectionState.Open)
	//			{
	//				con.Close();
	//				con = null;
	//			}
	//		}
	//	}


	//	public static DataSet ExecuteQuery(string connectionString, string routineName, string DatasetName, params object[] parameters)
	//	{
	//		DataSet ds = new DataSet(DatasetName);
	//		SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
	//		//builder.ConnectTimeout = 2500;
	//		SqlConnection con = new SqlConnection(builder.ConnectionString);
	//		SqlDataAdapter da = new SqlDataAdapter();
	//		con.Open();
	//		using (SqlCommand cmd = con.CreateCommand())
	//		{
	//			cmd.CommandText = routineName;
	//			cmd.CommandType = System.Data.CommandType.StoredProcedure;
	//			if (parameters != null)
	//			{
	//				cmd.Parameters.AddRange(parameters);
	//			}
	//			//cmd.CommandTimeout = 6000;
	//			da.SelectCommand = cmd;

	//			da.Fill(ds);
	//			return ds;
	//		}
	//	}

	//	//public DataSet ExecutesqlQuery(String sql, String parameter, String paramValue)
	//	//{
	//	//    if (sql != null && sql.Length > 7)
	//	//    {
	//	//        DbCommand cmd = Database.Connection.CreateCommand();
	//	//        cmd.CommandType = CommandType.Text;
	//	//        cmd.CommandText = sql;
	//	//        DbParameter param = cmd.CreateParameter();
	//	//        param.ParameterName = parameter;
	//	//        param.DbType = DbType.String;
	//	//        param.Size = 8;
	//	//        param.Direction = ParameterDirection.Input;
	//	//        param.Value = paramValue;
	//	//        cmd.Parameters.Add(param);
	//	//        IDbDataAdapter adp = new SqlDataAdapter();
	//	//        adp.SelectCommand = cmd;
	//	//        DataSet dataset = new DataSet();
	//	//        adp.Fill(dataset);

	//	//        return dataset;
	//	//    }
	//	//    else
	//	//        return null;
	//	//}

	//	public IEnumerable<T> ExecuteRoutine<T>(string routineName, IDictionary<string, object> parameters)
	//	{
	//		var tList = default(IEnumerable<T>);

	//		if (!string.IsNullOrEmpty(routineName))
	//		{
	//			var sqlParameters = new List<SqlParameter>();

	//			if (parameters != default(IDictionary<string, object>) &&
	//				parameters.Count >= _minParametersLength)
	//			{
	//				foreach (var key in parameters.Keys)
	//				{
	//					if (String.IsNullOrEmpty(Convert.ToString(parameters[key])))
	//					{
	//						sqlParameters.Add(new SqlParameter(key, DBNull.Value));
	//					}
	//					else
	//						sqlParameters.Add(new SqlParameter(key, parameters[key]));
	//				}
	//			}

	//			// tList = Database.SqlQuery<T>(routineName, sqlParameters.ToArray()).ToList();
	//		}

	//		return tList;
	//	}

	//	public async Task<IEnumerable<T>> AsyncExecuteRoutine<T>(string routineName, IDictionary<string, object> parameters)
	//	{
	//		var tList = default(IEnumerable<T>);

	//		if (!string.IsNullOrEmpty(routineName))
	//		{
	//			var sqlParameters = new List<SqlParameter>();

	//			if (parameters != default(IDictionary<string, object>) &&
	//				parameters.Count >= _minParametersLength)
	//			{
	//				foreach (var key in parameters.Keys)
	//					sqlParameters.Add(new SqlParameter(key, parameters[key]));
	//			}

	//			tList = null;// Database.SqlQuery<T>(routineName, sqlParameters.ToArray()).ToList();
	//		}

	//		return await Task.FromResult(tList);
	//	}

	//	public IEnumerable<T> ExecuteRoutinebydt<T>(string routineName, ref SqlParameter[] parameters)
	//	{
	//		var tList = default(IEnumerable<T>);

	//		if (!string.IsNullOrEmpty(routineName))
	//		{
	//			var sqlParameters = new List<SqlParameter>();

	//			if (parameters != default(SqlParameter[]) &&
	//				parameters.Length >= _minParametersLength)
	//			{
	//				foreach (SqlParameter p in parameters)
	//				{
	//					//check for derived output value with no value assigned
	//					if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
	//					{
	//						p.Value = DBNull.Value;
	//					}
	//					sqlParameters.Add(p);
	//				}
	//			}
	//			//Database.CommandTimeout = 0;
	//			tList = null; //Database.SqlQuery<T>(routineName, sqlParameters.ToArray()).ToList();
	//		}

	//		return tList;
	//	}

	//	#endregion Public Methods

	//	#region Protected Methods

	//	protected virtual T ExecuteReader<T>(string connection, Func<DbDataReader, T> mapEntities, string exec, params object[] parameters)
	//	{
	//		using (var conn = new SqlConnection(connection))
	//		{
	//			using (var command = new SqlCommand(exec, conn))
	//			{
	//				conn.Open();
	//				command.Parameters.AddRange(parameters);
	//				command.CommandType = CommandType.StoredProcedure;
	//				try
	//				{
	//					using (var reader = command.ExecuteReader())
	//					{
	//						T data = mapEntities(reader);
	//						return data;
	//					}
	//				}
	//				finally
	//				{
	//					conn.Close();
	//				}
	//			}
	//		}
	//	}

	//	public int ExecuteScriptNonQuery(string conn, string routineName, string script)
	//	{
	//		int result = 0;
	//		SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(conn);
	//		//builder.ConnectTimeout = 2500;
	//		SqlConnection con = new SqlConnection(builder.ConnectionString);
	//		con.Open();
	//		using (SqlCommand cmd = con.CreateCommand())
	//		{
	//			cmd.CommandText = script;
	//			result = cmd.ExecuteNonQuery();
	//			return result;
	//		}
	//	}

	//	public object ExecuteScriptScaler(string conn, string script)
	//	{
	//		object result = 0;
	//		SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(conn);
	//		//builder.ConnectTimeout = 2500;
	//		SqlConnection con = new SqlConnection(builder.ConnectionString);
	//		con.Open();
	//		using (SqlCommand cmd = con.CreateCommand())
	//		{
	//			cmd.CommandText = script;
	//			result = cmd.ExecuteScalar();
	//			return result;
	//		}
	//	}

	//	public void BulkUpload(string conn, string procedureName, DataTable dtTable, string tableVariable)
	//	{
	//		using (SqlConnection connection = new SqlConnection(conn))
	//		{
	//			using (SqlCommand cmd = new SqlCommand(procedureName))
	//			{
	//				try
	//				{
	//					cmd.CommandType = CommandType.StoredProcedure;
	//					cmd.Connection = connection;
	//					cmd.Parameters.AddWithValue(tableVariable, dtTable);
	//					connection.Open();
	//					cmd.ExecuteNonQuery();
	//					connection.Close();
	//				}
	//				catch (Exception ex)
	//				{
	//					throw ex;
	//				}
	//			}
	//		}
	//	}

	//	#endregion Protected Methods
	//}
}
