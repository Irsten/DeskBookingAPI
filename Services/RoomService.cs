using DeskBookingAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DeskBookingAPI.Services
{
    public interface IRoomService
    {
        bool CreateRoom();
        bool DeleteRoom(Room room);
        List<Room> GetAll();
    }

    public class RoomService : IRoomService
    {
        // TODO
        private readonly ApplicationDbContext _dbContext;

        public RoomService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CreateRoom()
        {
            _dbContext.Rooms.Add(new Room());
            _dbContext.SaveChanges();

            return true;
        }

        public bool DeleteRoom(Room room)
        {
            _dbContext.Rooms.Remove(room);
            _dbContext.SaveChanges();

            return true;
        }


        public List<Room> GetAll()
        {
            var rooms = _dbContext.Rooms.ToList();
            return rooms;
        }
    }
}
