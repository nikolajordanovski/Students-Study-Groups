using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace Students_Study_Groups.Services
{
    [ServiceContract(Namespace = "Students_Study_Groups")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class MainService
    {

        [OperationContract]
        public string LoginUser(string userData)
        {
            Dictionary<string, string> jsonData = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(userData);
            Dictionary<string, string> result = new Dictionary<string, string>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["SSG"].ConnectionString;
                try
                {
                    conn.Open();

                    string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@Username", jsonData["username"]);
                    command.Parameters.AddWithValue("@Password", jsonData["password"]);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        
                        if (HttpContext.Current.Session["User"] == null)
                        {
                            reader.Read();
                            HttpContext.Current.Session["User"] = reader["Username"];
                            HttpContext.Current.Session["UID"] = reader["UID"];
                            result["status"] = "success";
                            return new JavaScriptSerializer().Serialize(result);
                        }
                        else
                        {
                            HttpContext.Current.Session["User"] = null;
                            HttpContext.Current.Session["UID"] = null;
                            result["status"] = "error";
                            result["message"] = "Looks like you are already logged in. Please try again.";
                            return new JavaScriptSerializer().Serialize(result);
                        }
                    }
                    else
                    {
                        result["status"] = "error";
                        result["message"] = "The username or password is incorrect. Please try again.";
                        return new JavaScriptSerializer().Serialize(result);
                    }
                }
                catch (Exception e)
                {
                    result["status"] = "error";
                    result["message"] = e.Message;
                    return new JavaScriptSerializer().Serialize(result);
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        [OperationContract]
        public string LogoutUser()
        {
            HttpContext.Current.Session["User"] = null;
            HttpContext.Current.Session["UID"] = null;

            return "You have been logged out.";
        }

        [OperationContract]
        public string AskQuestion(string questionData)
        {
            Dictionary<string, object> jsonData = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(questionData);
            Dictionary<string, string> result = new Dictionary<string, string>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["SSG"].ConnectionString;
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand();
                    SqlDataReader reader;
                    command.Connection = conn;

                    //Insert the question in Questions-------------------------------------------------
                    command.CommandText = "INSERT INTO Questions (UID, Title, Body, Votes, Views, DateAsked) " +
                                          "VALUES (@UID, @Title, @Body, 0, 0, GETDATE()); SELECT SCOPE_IDENTITY();";
                    
                    command.Parameters.AddWithValue("@UID", jsonData["UID"].ToString());
                    command.Parameters.AddWithValue("@Title", jsonData["title"].ToString());
                    command.Parameters.AddWithValue("@Body", jsonData["body"].ToString());
                    string QID = command.ExecuteScalar().ToString();
                    //---------------------------------------------------------------------------------

                    //Insert the tags in Tags and QHasT------------------------------------------------
                    string TID = "";
                    
                    ArrayList tags = (ArrayList)jsonData["tags"];
                    foreach(string tag in tags) 
                    {
                        command.CommandText = "SELECT * FROM Tags WHERE Name = @Name";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@Name", tag);
                        reader = command.ExecuteReader();

                        if (!reader.HasRows)
                        {
                            reader.Close();
                            command.CommandText = "INSERT INTO Tags (Name, NumberOfQuestions) " +
                                                  "VALUES (@Name, 0); SELECT SCOPE_IDENTITY();";
                            TID = command.ExecuteScalar().ToString();
                        }
                        else
                        {
                            reader.Read();
                            TID = reader["TID"].ToString();
                            reader.Close();
                        }

                        command.CommandText = "INSERT INTO QHasT (QID, TID) VALUES (@QID, @TID)";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@QID", QID);
                        command.Parameters.AddWithValue("@TID", TID);
                        command.ExecuteNonQuery();
                    }
                    //---------------------------------------------------------------------------------

                    //Insert the question id in SHasQ--------------------------------------------------
                    command.CommandText = "INSERT INTO SHasQ (SID, QID) VALUES (@SID, @QID)";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@SID", jsonData["subjectId"]);
                    command.Parameters.AddWithValue("@QID", QID);
                    command.ExecuteNonQuery();    
                    //---------------------------------------------------------------------------------

                    result["status"] = "success";
                    result["QID"] = QID;
                    return new JavaScriptSerializer().Serialize(result);
                }
                catch (Exception e)
                {
                    result["status"] = "error";
                    result["message"] = e.Message;
                    return new JavaScriptSerializer().Serialize(result);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        [OperationContract]
        public string PostQuestionComment(string postData)
        {
            Dictionary<string, object> jsonData = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(postData);
            Dictionary<string, string> result = new Dictionary<string, string>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["SSG"].ConnectionString;
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand();
                    SqlDataReader reader;
                    command.Connection = conn;

                    command.CommandText = "SELECT * FROM QuestionComments WHERE QID = @QID AND UID = @UID " +
                                          "AND DateCommented = GETDATE()";

                    command.Parameters.AddWithValue("@QID", jsonData["questionId"]);
                    command.Parameters.AddWithValue("@UID", jsonData["userId"]);
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Close();
                        result["status"] = "error";
                        result["message"] = "Please don't spam";
                        return new JavaScriptSerializer().Serialize(result);
                    }
                    else
                    {
                        reader.Close();
                        command.CommandText = "INSERT INTO QuestionComments (QID, UID, Text, DateCommented) " +
                                              "VALUES (@QID, @UID, @Text, GETDATE());";

                        command.Parameters.AddWithValue("@Text", jsonData["text"]);
                        command.ExecuteNonQuery();

                        result["status"] = "success";
                        result["message"] = "";
                        return new JavaScriptSerializer().Serialize(result);
                    }
                }
                catch (Exception e)
                {
                    result["status"] = "error";
                    result["message"] = e.Message;
                    return new JavaScriptSerializer().Serialize(result);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        [OperationContract]
        public string PostAnswer(string answerData)
        {
            Dictionary<string, object> jsonData = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(answerData);
            Dictionary<string, string> result = new Dictionary<string, string>();

            using (SqlConnection conn = new SqlConnection()) 
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["SSG"].ConnectionString;
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;

                    command.CommandText = "INSERT INTO Answers (QID, UID, Body, Votes, Correct, DateAnswered)" +
                                          "VALUES (@QID, @UID, @Body, 0, 0, GETDATE()); SELECT SCOPE_IDENTITY()";
                    
                    command.Parameters.AddWithValue("@QID", jsonData["questionId"]);
                    command.Parameters.AddWithValue("@UID", jsonData["userId"]);
                    command.Parameters.AddWithValue("@Body", jsonData["text"]);
                    
                    result["status"] = "success";
                    result["AID"]    = command.ExecuteScalar().ToString();

                    return new JavaScriptSerializer().Serialize(result);
                }
                catch (Exception e)
                {
                    result["status"] = "error";
                    result["message"] = e.Message;
                    return new JavaScriptSerializer().Serialize(result);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        [OperationContract]
        public string PostAnswerComment(string postData)
        {
            Dictionary<string, object> jsonData = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(postData);
            Dictionary<string, string> result = new Dictionary<string, string>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["SSG"].ConnectionString;
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand();
                    SqlDataReader reader;
                    command.Connection = conn;

                    command.CommandText = "SELECT * FROM AnswerComments WHERE AID = @AID AND UID = @UID " +
                                          "AND DateCommented = GETDATE()";

                    command.Parameters.AddWithValue("@AID", jsonData["answerId"]);
                    command.Parameters.AddWithValue("@UID", jsonData["userId"]);
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Close();
                        result["status"] = "error";
                        result["message"] = "Please don't spam";
                        return new JavaScriptSerializer().Serialize(result);
                    }
                    else
                    {
                        reader.Close();
                        command.CommandText = "INSERT INTO AnswerComments (AID, UID, Text, DateCommented) " +
                                              "VALUES (@AID, @UID, @Text, GETDATE());";

                        command.Parameters.AddWithValue("@Text", jsonData["text"]);
                        command.ExecuteNonQuery();

                        result["status"] = "success";
                        result["message"] = "";
                        return new JavaScriptSerializer().Serialize(result);
                    }
                }
                catch (Exception e)
                {
                    result["status"] = "error";
                    result["message"] = e.Message;
                    return new JavaScriptSerializer().Serialize(result);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
