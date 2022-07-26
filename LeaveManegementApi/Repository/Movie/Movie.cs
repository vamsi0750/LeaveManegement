﻿using LeaveManegementApi.Data;
using Microsoft.EntityFrameworkCore;

namespace LeaveManegementApi.Repository.Movie
{
    public class Movie : IMovie
    {
        private readonly LeaveManagementDBContext _leaveManagementDBContext;

        public Movie(LeaveManagementDBContext leaveManagementDBContext)
        {
            _leaveManagementDBContext = leaveManagementDBContext;
        }

        public async Task<List<Models.Movies.Movie>> GetAllMovies()
        {
            return await _leaveManagementDBContext.Movies
                .Include(x => x.Producer).Include(x => x.ActorMovies).ThenInclude(x => x.Actor)
                .Select(x =>
                new Models.Movies.Movie
                {
                    Id = x.Id,
                    Producer = x.Producer,
                    ActorMovies = x.ActorMovies.Select(am=>new Models.Movies.ActorMovie
                    {
                        Id=am.Id,
                        Actor = am.Actor
                    }).ToList()
                })
                .ToListAsync();
        }

        public Task<List<Models.Movies.Movie>> GetAllMoviesByActor(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Movies.Movie> GetAllMoviesById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Models.Movies.Movie>> GetAllMoviesByProducor(int id)
        {
            throw new NotImplementedException();
        }
    }
}
