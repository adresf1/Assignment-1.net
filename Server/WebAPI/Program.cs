using FileRepositories;
using RepositoryContracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPostRepository, PostFIleReposity>();
builder.Services.AddScoped<IUserRepository, UserFileReposity>();
builder.Services.AddScoped<ICommentRepostory, CommentFileRepository>();
var app = builder.Build();

