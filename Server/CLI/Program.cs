// See https://aka.ms/new-console-template for more information

using CLI.UI;
using FileRepositories;
using InMemoryRepositories;
using RepositoryContracts;

Console.WriteLine("Starting...");
ICommentRepostory commentRepostory = new CommentFileRepository();
IUserRepository userRepository = new UserFileReposity();
IPostRepository postRepository = new PostFIleReposity();

CliApp cliapp = new CliApp( userRepository, commentRepostory,postRepository );

await cliapp.StartAsync();