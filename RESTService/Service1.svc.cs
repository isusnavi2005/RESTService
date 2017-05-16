using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;
using RESTService.model;

namespace RESTService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        //JOBS

        private string ConnectionStr = "Server=tcp:myserverreeb.database.windows.net,1433;Initial Catalog=MyDB;Persist Security Info=False;User ID=isusnavi2005;Password=Alegria1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


        public Job SetFinishedJob(DateTime curentTime)
        {
            var lastjob = GetLastJob();
            lastjob.EndTime = curentTime;
            Job updatedJob = UpdateJob(lastjob);
            return updatedJob;
        }

        public Notification GetLastNotification()
        {

            Notification not = null;

            const string selectLastNotification = "select * from Notification order by NotId desc";

            using (SqlConnection databaseConnection = new SqlConnection(ConnectionStr))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectLastNotification, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        //if (reader.HasRows)
                        //{
                        bool any = reader.NextResult();
                        if (any)
                        {
                            not = ReadNotification(reader);
                            return not;
                        }
                        return new Notification()
                        {
                            NotId = -7 ,
                            NotDate = DateTime.Now
                        };
                    }
                }

            }
        }


        public int DeleteAllNotifications()
        {
            const string deleteAllNotif = "delete from Notification";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionStr))
            {
                databaseConnection.Open();
                using (SqlCommand deletenotif = new SqlCommand(deleteAllNotif, databaseConnection))
                {
                    int rowsAffected = deletenotif.ExecuteNonQuery();

                    return rowsAffected;
                }
            }
        }



        private Job GetLastJob()
        {
            Job job = null;
            const string selectLastJob = "select top 1 * from Job order by JobId desc";

            using (SqlConnection databaseConnection = new SqlConnection(ConnectionStr))
            {
                databaseConnection.Open();

                using (SqlCommand selectCommand = new SqlCommand(selectLastJob, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            job = ReadJob(reader);

                        }
                        return job;
                    }
                }

            }
        }


        private Job UpdateJob(Job j)
        {
            var jobId = j.JobId;
            var jobEndTime = j.EndTime;
            const string updateJob = "UPDATE JOB SET ENDTIME=@jobEndTime where JobId=@jobId";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionStr))
            {
                databaseConnection.Open();
                using (SqlCommand updateCommand = new SqlCommand(updateJob, databaseConnection))
                {
                    updateCommand.Parameters.AddWithValue("@jobEndTime", jobEndTime);
                    updateCommand.Parameters.AddWithValue("@jobId", jobId);
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    if (rowsAffected == 1)
                    {
                        return j;
                    }
                    return null;

                }
            }
        }


        private static Job ReadJob(SqlDataReader reader)
        {
            int jobId = reader.GetInt32(0);
            DateTime jobDate = reader.GetDateTime(1);
            DateTime startTime = reader.GetDateTime(2);
            DateTime endTime = reader.GetDateTime(3);
            int userId = reader.GetInt32(4);

            Job job = new Job()
            {
                JobId = jobId,
                StarTime = startTime,
                EndTime = endTime
            };
            return job;
        }


        private Notification ReadNotification(SqlDataReader reader)
        {
            int notId = reader.GetInt32(0);
            DateTime notDate = reader.GetDateTime(1);
            DateTime notTime = reader.GetDateTime(2);

            Notification not = new Notification()
            {
                NotId = notId,
                NotDate = notDate,
                NotTime = notTime
            };
            return not;
        }









    }
}
