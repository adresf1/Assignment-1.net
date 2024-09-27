using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private readonly IUserRepository userRepository;
    private readonly ICommentRepostory commentRepository;
    private readonly IPostRepository postRepository;

    private readonly CreatPostView createPostView;
    private readonly ListPostView listPostsView;
    private readonly ManagePostView managePostsView;
    private readonly SinglePostView singlePostView;
    
    private readonly CreateUserView createUserView;
    private readonly ListUsersView listUsersView;
    private readonly ManageUserView manageUsersView;
    
    private readonly CreatCommentView createCommentView;
    
    public CliApp(IUserRepository userRepository, ICommentRepostory commentRepository, IPostRepository postRepository)
    {
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;

        createPostView = new CreatPostView(this.postRepository);
        listPostsView = new ListPostView(this.postRepository);
        managePostsView = new ManagePostView(this.postRepository);
        singlePostView = new SinglePostView(this.postRepository);
        
        createUserView = new CreateUserView(this.userRepository);
        listUsersView = new ListUsersView(this.userRepository);
        manageUsersView = new ManageUserView(this.userRepository);

        createCommentView = new CreatCommentView(this.commentRepository);
    }

    public async Task StartAsync()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Add User");
            Console.WriteLine("2. Add Post");
            Console.WriteLine("3. Add Comment");
            
            Console.WriteLine("4. View Users");
            Console.WriteLine("5. View Posts");
            Console.WriteLine("6. View Comments");
            Console.WriteLine("7. Exit");
            
            string option = Console.ReadLine();

            switch (option)
            {
             
                case "1":
                    Console.WriteLine("Enter User Name: ");
                    string userName = Console.ReadLine();
                    
                    Console.WriteLine("Enter Password: ");
                    string password = Console.ReadLine();
    
                    // Await the add operation
                    await createUserView.AddUserAsync(userName, password);
                    Console.WriteLine("User added successfully!");
                    break;
                
                case "2":
                    Console.WriteLine("Enter Post Title: ");
                    string postTitle = Console.ReadLine();
                    
                    Console.WriteLine("Enter Post Content: ");
                    string postContent = Console.ReadLine();
                    
                    Console.WriteLine("Enter User Id: ");
                    int userId = int.Parse(Console.ReadLine());
                    
                    // Add post and save it to file
                    await createPostView.addpostAsync(postTitle, postContent, userId);
                    Console.WriteLine("Post added successfully!");
                    break;
                
                case "3":
                    Console.WriteLine("Enter Comment Content: ");
                    string commentContent = Console.ReadLine();
                    
                    Console.WriteLine("Enter Post Id: ");
                    int postId = int.Parse(Console.ReadLine());
                    
                    Console.WriteLine("Enter User Id: ");
                    int commentUserId = int.Parse(Console.ReadLine());

                    // Add comment and save it to file
                    await createCommentView.AddAsync(commentContent);
                    Console.WriteLine("Comment added successfully!");
                    break;

                case "4":
                    // List all users
                    listUsersView.GetMany();
                    break;

                case "5":
                    // List all posts
                    listPostsView.ListPosts();
                    break;

                case "6":
                    commentRepository.GetMany();
                    break;

                case "7":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }
}