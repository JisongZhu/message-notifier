using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Xml;

namespace TaskbarNotifier
{
    public class MysqlHelper
    {
        private static MySqlConnection conn = null;

        private static MySqlConnection getConnection()
        {
            if (conn == null)
            {
                conn = new MySqlConnection();

                conn.ConnectionString = ApplicationContext.ConnectionString();
            }
            return conn;
        }

        private static string EncodingTitle(string str)
        {
            Encoding en = Encoding.GetEncoding("iso-8859-1");
            byte[] bs = en.GetBytes(str);
            string result = Encoding.UTF8.GetString(bs);
            return result;
        }

        private static string DateConvertor(long timeLong)
        {
            DateTime dt_1970 = new DateTime(1970, 1, 1);
            long tricks_1970 = dt_1970.Ticks;//1970年1月1日刻度
            long time_tricks = tricks_1970 + timeLong * 10000000;//日志日期刻度
            DateTime dt = new DateTime(time_tricks);//转化为DateTime
            return dt.ToString("yyyy-MM-dd hh:mm:ss");
        }

        public static List<MessageModel> GetNewMessage(string userId)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = getConnection();
            conn.Open();
            MySqlTransaction transaction = conn.BeginTransaction();
            command.Transaction = transaction;
            List<MessageModel> resultList = new List<MessageModel>();

            try
            {
                command.CommandText = "SET NAMES latin1; SELECT `id`, `title`, `link`, `createtime` FROM eip_message WHERE `read`=0 and `notice`=0 and `state`=1 and `to_emp`=" + userId + " ORDER BY `createtime` ASC";
                MySqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    MessageModel message = new MessageModel()
                    {
                        Id = Convert.IsDBNull(reader["id"]) ? 0 : Convert.ToInt32(reader["id"]),
                        Title = Convert.IsDBNull(reader["title"]) ? "" : EncodingTitle(reader["title"].ToString()),
                        Link = Convert.IsDBNull(reader["link"]) ? "" : reader["link"].ToString(),
                        CreateTime = Convert.IsDBNull(reader["createtime"]) ? "" : DateConvertor(Convert.ToInt32(reader["createtime"])),
                        HasRead = 0
                    };

                    resultList.Add(message);
                }
                reader.Close();
                transaction.Commit();
            }
            catch (MySqlException e)
            {
                transaction.Rollback();
            }
            finally
            {
                transaction.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return resultList;
        }

        public static void UpdateMessageState(int id)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = getConnection();
            conn.Open();
            command.CommandText = "UPDATE eip_message SET notice=1 WHERE id=" + id;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public static void InitData()
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = getConnection();
            conn.Open();
            command.CommandText = "UPDATE eip_message SET notice=0 WHERE id in (1,662,663)";
            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
