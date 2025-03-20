namespace shirtsApi.Model.Repository
{
    public static class ShirtRepository
    {
        private static List<Shirt> shirts = new List<Shirt>()
        {
            new Shirt { ShirtId = 1, Brand = "My Brand", Color = "Red", Gender = "Men", Size=11, Price = 25},
            new Shirt { ShirtId = 2, Brand = "My Brand", Color = "Blue", Gender = "Men", Size=10, Price = 21},
            new Shirt { ShirtId = 3, Brand = "My Brand", Color = "Yellow", Gender = "Women", Size=8, Price = 35},
            new Shirt { ShirtId = 4, Brand = "My Brand", Color = "White", Gender = "Women", Size=9, Price = 30},
        };

        public static List<Shirt> getAllShirts()
        {
            return shirts;
        }

        public static bool shirtExist(int id)
        {
            return shirts.Any(x => x.ShirtId == id);
        }

        public static Shirt? getShirtById(int id)
        {
            return shirts.FirstOrDefault(x => x.ShirtId == id);
        }

        public static Shirt? getShirtProperties(string? brand, string? gender, string? color, int? size)
        {
            return shirts.FirstOrDefault(
                x => 
                    !string.IsNullOrWhiteSpace(brand) &&
                    !string.IsNullOrWhiteSpace(x.Brand) &&
                    x.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase) &&

                    !string.IsNullOrWhiteSpace(gender) &&
                    !string.IsNullOrWhiteSpace(x.Gender) &&
                    x.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase) &&

                    !string.IsNullOrWhiteSpace(color) &&
                    !string.IsNullOrWhiteSpace(x.Color) &&
                    x.Color.Equals(color, StringComparison.OrdinalIgnoreCase) &&

                    size.HasValue && 
                    x.Size.HasValue &&
                    size.Value == x.Size.Value
            );
        }

        public static void AddShirt(Shirt shirt) 
        {
            int maxId = shirts.Max(x => x.ShirtId);
            shirt.ShirtId = maxId + 1;

            shirts.Add(shirt);
        }

        public static void updateShirt(Shirt shirt) 
        {
            var shirtToUpdate = shirts.First(x => x.ShirtId == shirt.ShirtId);
            shirtToUpdate.Brand = shirt.Brand;
            shirtToUpdate.Price = shirt.Price;
            shirtToUpdate.Size = shirt.Size;
            shirtToUpdate.Color = shirt.Color;
            shirtToUpdate.Gender = shirt.Gender;
        }

        public static void deleteShirt(int shirtId)
        {
            var shirt = getShirtById(shirtId);
            if(shirt != null) 
            {
                shirts.Remove(shirt);
            }
        }
    }
}
