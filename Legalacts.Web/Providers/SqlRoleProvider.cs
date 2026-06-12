using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Security;

namespace Legalacts.Web.Providers
{
    /// <summary>
    /// Провайдър за потребителска аутентикация
    /// </summary>
	public class SqlRoleProvider : RoleProvider
	{
		private const int RoleMaxLength = 50;
		private const string DefaultProviderName = "SqlRoleProvider";
		private const string DefaultProviderDescription = "Sql Server Role Provider";

		private string _connectionString = null;

        /// <summary>
        /// Инициализация на провайдъра
        /// </summary>
        /// <param name="name">име</param>
        /// <param name="config">списък от конфигурации</param>
		public override void Initialize(string name, NameValueCollection config)
		{
			//TODO: Add SqlClientPermission check

			if (config == null)
				throw new ArgumentNullException("config");

			if (String.IsNullOrEmpty(name))
				name = DefaultProviderName;

			if (String.IsNullOrEmpty(config["description"]))
			{
				config.Remove("description");
				config.Add("description", DefaultProviderDescription);
			}

			base.Initialize(name, config);

			string connectionStringName = config["connectionStringName"];

			if (String.IsNullOrEmpty(connectionStringName))
				throw new ProviderException("Empty or missing connectionStringName");

			config.Remove("connectionStringName");

			if (WebConfigurationManager.ConnectionStrings[connectionStringName] == null)
				throw new ProviderException("Missing connection string");

			_connectionString = WebConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

			if (String.IsNullOrEmpty(_connectionString))
				throw new ProviderException("Empty connection string");

			if (config.Count > 0)
			{
				string attribute = config.GetKey(0);

				if (!String.IsNullOrEmpty(attribute))
					throw new ProviderException("Unrecognized attribute: " + attribute);
			}
		}

        /// <summary>
        /// Връща ролите на даден потребител
        /// </summary>
        /// <param name="username">потребителско име</param>
        /// <returns></returns>
		public override string[] GetRolesForUser(string username)
		{
            SqlConnection conn = new SqlConnection(_connectionString);
			SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText =
                @"SELECT r.Name
                  FROM Roles r INNER JOIN UsersRoles ur ON ur.RoleId = r.Id
                       INNER JOIN Users u ON ur.UserId = u.Id
                  WHERE UPPER(u.Username) = UPPER(@Username)";

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@Username", SqlDbType.VarChar, 200).Value = username;


			conn.Open();
			try
			{
				List<string> result = new List<string>();

				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					result.Add((string)reader["Name"]);
				}

				return result.ToArray();
			}
			finally
			{
				conn.Close();
			}
		}

        /// <summary>
        /// Връща всички налични роли
        /// </summary>
        /// <returns></returns>
		public override string[] GetAllRoles()
		{
            SqlConnection conn = new SqlConnection(_connectionString);
			SqlCommand cmd = conn.CreateCommand();

			cmd.CommandText = @"SELECT Name FROM Roles";
			cmd.CommandType = CommandType.Text;

			List<string> result = new List<string>();

			conn.Open();
			try
			{
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
                    result.Add(reader.GetString(0));
				}
			}
			finally
			{
				conn.Close();
			}

			return result.ToArray();
		}

        /// <summary>
        /// Връща дали потребител има дадена роля
        /// </summary>
        /// <param name="username">потребителско име</param>
        /// <param name="roleName">име на роля</param>
        /// <returns></returns>
		public override bool IsUserInRole(string username, string roleName)
		{
			string[] roles = GetRolesForUser(username);
			return (Array.IndexOf<string>(roles, roleName) >= 0);
		}

		#region Not Supported

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
		public override bool RoleExists(string roleName)
		{
			throw new Exception("The method or operation is not implemented. 25");
		}

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
		public override string[] GetUsersInRole(string roleName)
		{
			throw new Exception("The method or operation is not implemented. 27");
		}

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
		public override string ApplicationName
		{
			get
			{
				throw new Exception("The method or operation is not implemented. 28");
			}
			set
			{
				throw new Exception("The method or operation is not implemented. 29");
			}
		}

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
        /// <param name="roleName"></param>
		public override void CreateRole(string roleName)
		{
			throw new Exception("The method or operation is not implemented. 30");
		}

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
        /// <param name="usernames"></param>
        /// <param name="roleNames"></param>
		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			throw new Exception("The method or operation is not implemented. 31");
		}

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
        /// <param name="usernames"></param>
        /// <param name="roleNames"></param>
		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new Exception("The method or operation is not implemented. 32");
		}

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="throwOnPopulatedRole"></param>
        /// <returns></returns>
		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			throw new Exception("The method or operation is not implemented. 33");
		}

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
        /// <param name="username"></param>
        /// <param name="roleName"></param>
		private void AddUserToRole(string username, string roleName)
		{
			throw new Exception("The method or operation is not implemented. 34");
		}

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
        /// <param name="username"></param>
        /// <param name="roleName"></param>
		private void RemoveUserFromRole(string username, string roleName)
		{
			throw new Exception("The method or operation is not implemented. 35");
		}

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="usernameToMatch"></param>
        /// <returns></returns>
		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new Exception("The method or operation is not implemented. 36");
		}

		#endregion
	}
}