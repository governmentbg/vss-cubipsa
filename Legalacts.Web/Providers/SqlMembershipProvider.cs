using System;
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
	public class SqlMembershipProvider : MembershipProvider
	{
		private const string DefaultProviderName = "SqlMembershipProvider";
		private const string DefaultProviderDescription = "Sql Server Membership Provider";

		private string _connectionString = null;
		private FormsAuthPasswordFormat _passwordFormat = FormsAuthPasswordFormat.Clear;

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

			if (!String.IsNullOrEmpty(config["passwordFormat"]))
			{
				try
				{
					_passwordFormat = (FormsAuthPasswordFormat)Enum.Parse(typeof(FormsAuthPasswordFormat), config["passwordFormat"], true);
				}
				catch (ArgumentException)
				{
					throw new ProviderException("Value of attribute passwordFormat is not equal to Clear, MD5 or SHA1");
				}
			}
			config.Remove("passwordFormat");

			if (config.Count > 0)
			{
				string attribute = config.GetKey(0);

				if (!String.IsNullOrEmpty(attribute))
					throw new ProviderException("Unrecognized attribute: " + attribute);
			}
		}

        /// <summary>
        /// Валидиране на потребителски достъп
        /// </summary>
        /// <param name="username">потребителско име</param>
        /// <param name="password">парола</param>
        /// <returns></returns>
		public override bool ValidateUser(string username, string password)
		{
			SqlConnection conn = new SqlConnection(_connectionString);
			SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"SELECT Id FROM Users WHERE UPPER(Username) = UPPER(@Username) AND 
                                Password = @Password";
			cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 200).Value = username;
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 200).Value = HashPassword(password);

			conn.Open();
			try
			{
				object UserID = cmd.ExecuteScalar();
				return (UserID != null);
			}
			finally
			{
				conn.Close();
			}
		}

        /// <summary>
        /// Смяна на парола
        /// </summary>
        /// <param name="username">потребителско име</param>
        /// <param name="oldPassword">стара парола</param>
        /// <param name="newPassword">нова парола</param>
        /// <returns></returns>
		public override bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			if (!ValidateUser(username, oldPassword))
				return false;

			return ChangePassword(username, newPassword);
		}

        /// <summary>
        /// Криптиране на парола
        /// </summary>
        /// <param name="password">парола</param>
        /// <returns></returns>
		public string HashPassword(string password)
		{
			string passwordHash = password;

			//if (_passwordFormat != FormsAuthPasswordFormat.Clear) 
			//	passwordHash = FormsAuthentication.HashPasswordForStoringInConfigFile(password, _passwordFormat.ToString());

			return passwordHash;
		}

        /// <summary>
        /// Метод за смяна на парола
        /// </summary>
        /// <param name="username">потребителско име</param>
        /// <param name="newPassword">нова парола</param>
        /// <returns></returns>
		bool ChangePassword(string username, string newPassword)
		{
			SqlConnection conn = new SqlConnection(_connectionString);
			SqlCommand cmd = conn.CreateCommand();

			cmd.CommandText = @"UPDATE Users SET Password = @Password WHERE Username = @Username";
			cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 200).Value = username;
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 200).Value = HashPassword(newPassword);

			conn.Open();
			try
			{
				cmd.ExecuteNonQuery();
				return true;
			}
			finally
			{
				conn.Close();
			}
		}

		#region Not Supported

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			throw new Exception("The method or operation is not implemented. 0");
		}

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userIsOnline"></param>
        /// <returns></returns>
		public override MembershipUser GetUser(string username, bool userIsOnline)
		{
			throw new Exception("The method or operation is not implemented. 1");
		}

        /// <summary>
        /// Неимплементирано свойство
        /// </summary>
		public override string ApplicationName
		{
			get
			{
				throw new Exception("The method or operation is not implemented. 2");
			}
			set
			{
				throw new Exception("The method or operation is not implemented. 3");
			}
		}

        /// <summary>
        /// Неимплементирано свойство
        /// </summary>
		public override bool EnablePasswordReset
		{
			get { throw new Exception("The method or operation is not implemented. 4"); }
		}

        /// <summary>
        /// Неимплементирано свойство
        /// </summary>
		public override bool EnablePasswordRetrieval
		{
			get { throw new Exception("The method or operation is not implemented. 5"); }
		}

        /// <summary>
        /// Неимплементирано свойство
        /// </summary>
		public override int MaxInvalidPasswordAttempts
		{
			get { throw new Exception("The method or operation is not implemented. 6"); }
		}

        /// <summary>
        /// Неимплементирано свойство
        /// </summary>
		public override int MinRequiredNonAlphanumericCharacters
		{
			get { throw new Exception("The method or operation is not implemented. 7"); }
		}

        /// <summary>
        /// Неимплементирано свойство
        /// </summary>
		public override int MinRequiredPasswordLength
		{
			get { return 3; }
		}

        /// <summary>
        /// Неимплементирано свойство
        /// </summary>
		public override int PasswordAttemptWindow
		{
			get { throw new Exception("The method or operation is not implemented. 8"); }
		}

        /// <summary>
        /// Неимплементирано свойство
        /// </summary>
		public override string PasswordStrengthRegularExpression
		{
			get { throw new Exception("The method or operation is not implemented. 9"); }
		}

        /// <summary>
        /// Неимплементирано свойство
        /// </summary>
		public override bool RequiresQuestionAndAnswer
		{
			get { throw new Exception("The method or operation is not implemented. 10"); }
		}

        /// <summary>
        /// Неимплементирано свойство
        /// </summary>
		public override MembershipPasswordFormat PasswordFormat
		{
			get { throw new Exception("The method or operation is not implemented. 11"); }
		}

        /// <summary>
        /// Неимплементирано свойство
        /// </summary>
		public override bool RequiresUniqueEmail
		{
			get { throw new Exception("The method or operation is not implemented. 12"); }
		}

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
		public override bool UnlockUser(string userName)
		{
			throw new Exception("The method or operation is not implemented. 13");
		}

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
		public override string GetUserNameByEmail(string email)
		{
			throw new Exception("The method or operation is not implemented. 14");
		}

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
        /// <param name="emailToMatch"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
		public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new Exception("The method or operation is not implemented 15.");
		}

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="newPasswordQuestion"></param>
        /// <param name="newPasswordAnswer"></param>
        /// <returns></returns>
		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			throw new Exception("The method or operation is not implemented. 16");
		}

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="passwordQuestion"></param>
        /// <param name="passwordAnswer"></param>
        /// <param name="isApproved"></param>
        /// <param name="providerUserKey"></param>
        /// <param name="status"></param>
        /// <returns></returns>
		public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{
			throw new Exception("The method or operation is not implemented. 17");
		}

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
        /// <param name="username"></param>
        /// <param name="deleteAllRelatedData"></param>
        /// <returns></returns>
		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			throw new Exception("The method or operation is not implemented. 18");
		}

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
        /// <param name="usernameToMatch"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
		public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new Exception("The method or operation is not implemented. 19");
		}

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
        /// <returns></returns>
		public override int GetNumberOfUsersOnline()
		{
			throw new Exception("The method or operation is not implemented. 20");
		}

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
        /// <param name="username"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
		public override string GetPassword(string username, string answer)
		{
			throw new Exception("The method or operation is not implemented. 21");
		}

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
        /// <param name="providerUserKey"></param>
        /// <param name="userIsOnline"></param>
        /// <returns></returns>
		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			throw new Exception("The method or operation is not implemented. 22");
		}

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
        /// <param name="username"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
		public override string ResetPassword(string username, string answer)
		{
			throw new Exception("The method or operation is not implemented. 23");
		}

        /// <summary>
        /// Неимплементиран метод
        /// </summary>
        /// <param name="user"></param>
		public override void UpdateUser(MembershipUser user)
		{
			throw new Exception("The method or operation is not implemented. 24");
		}

		#endregion
	}
}