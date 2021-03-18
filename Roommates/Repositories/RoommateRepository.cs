using Roommates.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System;

namespace Roommates.Repositories
{
    class RoommateRepository : BaseRepository
    {
        public RoommateRepository(string connectionString) : base(connectionString) { }

        public List<Roommate> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select * from Roommate
                                        left join Room on Roommate.RoomId = Room.Id";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Roommate> roommates = new List<Roommate>();

                    while( reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int firstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string fNameValue = reader.GetString(firstNameColumnPosition);

                        int lastNameColumnPosition = reader.GetOrdinal("LastName");
                        string lNameValue = reader.GetString(lastNameColumnPosition);

                        int moveInColumnPosition = reader.GetOrdinal("MoveInDate");
                        DateTime moveInDateValue = reader.GetDateTime(moveInColumnPosition);

                        int rentPortionColumnPosition = reader.GetOrdinal("RentPortion");
                        int rentPortionValue = reader.GetInt32(rentPortionColumnPosition);

                        int roomColumnPosition = reader.GetOrdinal("Name");
                        string roomValue = reader.GetString(roomColumnPosition);

                        Room room = new Room
                        {
                            Name = roomValue,
                        };

                        Roommate roommate = new Roommate
                        {
                            Id = idValue,
                            FirstName = fNameValue,
                            LastName = lNameValue,
                            RentPortion = rentPortionValue,
                            MovedInDate = moveInDateValue,
                            Room = room,

                        };
                        roommates.Add(roommate);
                    }
                    reader.Close();
                    return roommates;

                }
            }
        }


    }
}
