// See https://aka.ms/new-console-template for more information

using CLI.UI;
using InMemoryRepositories;
using RepositoryContracts;

Console.WriteLine("Starting...");
ICommentRepostory commentRepostory = new CommentInMemoryRepository();
IUserRepository userRepository = new UserInMemoryRepository();
IPostRepository postRepository = new PostInMemoryRepository();

Cliapp cliapp = new Cliapp(commentRepostory, userRepository, postRepository);

await cliapp.StartAsync();