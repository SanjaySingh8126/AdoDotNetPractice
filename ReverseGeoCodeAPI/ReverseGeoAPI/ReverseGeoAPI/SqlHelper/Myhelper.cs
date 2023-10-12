using System.Data;
using System.Data.SqlClient;

namespace ReverseGeoAPI.SqlHelper
{
	public class Myhelper
	{
		private static string _conn;

		
        public static DataTable ExecuteQueryGetDataTable(string conn,string query) 
		{
			SqlConnection connection = new SqlConnection(conn);
			connection.Open();

			SqlCommand cmd = new SqlCommand(query, connection);
			cmd.CommandType = CommandType.Text;

			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);	
			DataTable dataTable = new DataTable();
			
			sqlDataAdapter.Fill(dataTable);
			//cmd.CommandType = CommandType.Text;
			connection.Close();	
			return dataTable;
		}	
		public void   UpdateRowSource(string con,string query)
		{
			SqlConnection connection = new SqlConnection(con);
			connection.Open();
			SqlCommand	command = new SqlCommand(query, connection);
			command.CommandType = CommandType.Text;
			command.ExecuteNonQuery();
			connection.Close();

		}

	}
	 public class SetConnection
	{
        public SetConnection(string conn)
        {
			connection= conn;	
            
        }
        public string connection { get; set; }
    }

}
