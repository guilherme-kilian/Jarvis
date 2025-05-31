using Jarvis.Models.User;

namespace Jarvis.Mock
{
    public class UsersMock
    {
        public static List<UserModel> Users = 
        [
            new()
            { 
                Email = "guidkilian@gmail.com",
                Name = "Guilherme Kilian",
                Password = "123",
                Id = 2,
                ProfilePicture = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcSwbHpuLNwVr15XAIZowlY7YzZ6IkyXPc3XGa-cBZNzB1iigo8n9VOdke_glIAQR9uQEvP3qF34T4Ampo9hsk_-9UrHUMdSm7A5iMzfKQ",
            },
             new()
            {
                Email = "poly.fucilini.s@gmail.com",
                Name = "Polyana Fucilini",
                Password = "poly3011",
                Id = 1,
                ProfilePicture = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcSwbHpuLNwVr15XAIZowlY7YzZ6IkyXPc3XGa-cBZNzB1iigo8n9VOdke_glIAQR9uQEvP3qF34T4Ampo9hsk_-9UrHUMdSm7A5iMzfKQ",
            },
        ];
    }
}
