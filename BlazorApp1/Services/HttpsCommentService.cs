﻿using ApiContracts;

namespace BlazorApp1.Services;

public class HttpsCommentService : ICommentService
{
    public Task<CommentDTO> AddAsync(CommentDTO comment)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(CommentDTO comment)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<CommentDTO> GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<CommentDTO> GetMany()
    {
        throw new NotImplementedException();
    }
}