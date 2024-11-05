﻿using ApiContracts;

namespace BlazorApp1.Services;

public interface ICommentService 
{
    Task<CommentDTO> AddAsync(CommentDTO comment);
    Task UpdateAsync(CommentDTO comment);
    Task DeleteAsync(int id);
    Task<CommentDTO> GetSingleAsync(int id);
    IQueryable<CommentDTO> GetMany();
  
}