using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private readonly IUserRepository userRepository;
    private readonly ICommentRepostory commentRepository;
    private readonly IPostRepository postRepository;

    // Views are directly defined as fields
    private readonly CreateUserView createUserView;
    private readonly ListUsersView listUsersView;
    private readonly ManageUserView manageUsersView;

    private readonly CreatPostView createPostView;
    private readonly ListPostView listPostsView;
    private readonly ManagePostView managePostsView;
    private readonly SinglePostView singlePostView;
    
    private readonly CreatCommentView createCommentView;

    public CliApp(IUserRepository userRepository, ICommentRepostory commentRepository, IPostRepository postRepository)
    {
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;

        // Initialize views directly
        createUserView = new CreateUserView(this.userRepository);
        listUsersView = new ListUsersView(this.userRepository);
        manageUsersView = new ManageUserView(this.userRepository);

        createPostView = new CreatPostView(this.postRepository);
        listPostsView = new ListPostView(this.postRepository);
        managePostsView = new ManagePostView(this.postRepository);
        singlePostView = new SinglePostView(this.postRepository);

        createCommentView = new CreatCommentView(this.commentRepository);
    }

    public async Task StartAsync()
    {
        bool running = true;

        while (running)
        {
            ShowMenu();

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await AddUserAsync();
                    break;
                
                case "2":
                    await AddPostAsync();
                    break;

                case "3":
                    await AddCommentAsync();
                    break;

                case "4":
                    ListUsers();
                    break;

                case "5":
                    ListPosts();
                    break;

                case "6":
                    // Assuming you have a method to list comments
                    ListComments();
                    break;

                case "7":
                    running = false;
                    Console.WriteLine("Exiting the application.");
                    break;

                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }

    // Method to display menu options
    private void ShowMenu()
    {
        Console.WriteLine("Choose an option:");
        Console.WriteLine("1. Add User");
        Console.WriteLine("2. Add Post");
        Console.WriteLine("3. Add Comment");
        Console.WriteLine("4. View Users");
        Console.WriteLine("5. View Posts");
        Console.WriteLine("6. View Comments");
        Console.WriteLine("7. Exit");
    }

    // Centralized logic for adding users
    private async Task AddUserAsync()
    {
        Console.WriteLine("Enter User Name: ");
        string userName = Console.ReadLine();

        Console.WriteLine("Enter Password: ");
        string password = Console.ReadLine();

        await createUserView.AddUserAsync(userName, password);
        Console.WriteLine("User added successfully!");
    }

    // Centralized logic for adding posts
    private async Task AddPostAsync()
    {
        Console.WriteLine("Enter Post Title: ");
        string postTitle = Console.ReadLine();

        Console.WriteLine("Enter Post Content: ");
        string postContent = Console.ReadLine();

        Console.WriteLine("Enter User Id: ");
        int userId = int.Parse(Console.ReadLine());

        await createPostView.addpostAsync(postTitle, postContent, userId);
        Console.WriteLine("Post added successfully!");
    }

    // Centralized logic for adding comments
    private async Task AddCommentAsync()
    {
        Console.WriteLine("Enter Comment Content: ");
        string commentContent = Console.ReadLine();

        Console.WriteLine("Enter Post Id: ");
        int postId = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter User Id: ");
        int commentUserId = int.Parse(Console.ReadLine());

        await createCommentView.AddAsync(commentContent);
        Console.WriteLine("Comment added successfully!");
    }

    // Centralized logic for listing users
    private void ListUsers()
    {
        listUsersView.GetMany();
    }

    // Centralized logic for listing posts
    private void ListPosts()
    {
        listPostsView.ListPosts();
    }

    // Stub method for listing comments (you would need to implement it)
    private void ListComments()
    {
        Console.WriteLine("Listing comments is not implemented yet.");
    }
}
