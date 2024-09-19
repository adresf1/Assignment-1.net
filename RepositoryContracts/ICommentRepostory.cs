﻿using Entities;

namespace RepositoryContracts;

public interface ICommentRepostory
{
    Task<Comment> AddAsync(Comment comment);
    Task UpdateAsync(Comment comment);
    Task DeleteAsync(int id);
   Task<Comment> GetSingleAsync(int id);
    IQueryable<Comment> GetMany();
}