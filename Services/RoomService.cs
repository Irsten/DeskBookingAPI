using AutoMapper;
using DeskBookingAPI.Entities;
using DeskBookingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeskBookingAPI.Services
{
    public interface IRoomService
    {
        bool CreateRoom();
        bool DeleteRoom(Room room);
        List<RoomDto> GetAll();
    }

    public class RoomService : IRoomService
    {
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

        public List<RoomDto> GetAll()
        {
            var listOfRooms = new List<RoomDto>();
            return listOfRooms;
        }
    }
}
